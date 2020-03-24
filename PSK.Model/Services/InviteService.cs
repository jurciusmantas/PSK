using PSK.Model.DBConnection;
using PSK.Model.Entities;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace PSK.Model.Services
{
    public class InviteService : IInviteService
    {
        private readonly IDBConnection _db;

        public InviteService(IDBConnection db)
        {
            _db = db;
        }

        public ServerResult<InviteArgs> Invite(InviteArgs args)
        {
            try
            {
                var token = GenerateToken();

                _db.CreateEmployee("", args.Email, "", 0, token);

                SendInviteMail(args.Email, token);

                return new ServerResult<InviteArgs>
                {
                    Success = true
                };

            }
            catch (Exception e)
            {
                return new ServerResult<InviteArgs>
                {
                    Success = false,
                    Message = e.Message,
                };
            }
        }

        private void SendInviteMail(string receiverEmail, string token)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            string mailAdr = ConfigurationManager.AppSettings["NoreplyEmailAddress"];
            string password = ConfigurationManager.AppSettings["Password"];

            Console.WriteLine("mail: " + mailAdr);
            Console.WriteLine("pass: " + password);


            mail.From = new MailAddress(mailAdr);
            mail.To.Add(receiverEmail);
            mail.Subject = "Registration link";
            mail.Body = "https://localhost:44395/registration/" + token;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailAdr, password);
            SmtpServer.EnableSsl = true;

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string GenerateToken()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var crypto = new RNGCryptoServiceProvider();
            var data = new byte[36];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(46);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
