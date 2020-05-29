using PSK.Model.Entities;
using PSK.Model.DTO;
using PSK.Model.Repository;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using PSK.Model.IServices;

namespace PSK.Model.Services
{
    public class InviteService : IInviteService
    {
        private readonly IIncomingEmployeeRepository _db;

        public InviteService(IIncomingEmployeeRepository db)
        {
            _db = db;
        }

        public ServerResult<Invite> Invite(Invite args)
        {
            try
            {
                var token = GenerateToken();

                _db.Add(new IncomingEmployee {Email = args.Email, Token = token, LeaderId = args.LeaderId });

                SendInviteMail(args.Email, token);

                return new ServerResult<Invite>
                {
                    Success = true
                };

            }
            catch (Exception e)
            {
                return new ServerResult<Invite>
                {
                    Success = false,
                    Message = e.Message,
                };
            }
        }

        public void SendInviteMail(string receiverEmail, string token)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            string mailAdr = ConfigurationManager.AppSettings["NoreplyEmailAddress"];
            string password = ConfigurationManager.AppSettings["Password"];

            mail.From = new MailAddress(mailAdr);
            mail.To.Add(receiverEmail);
            mail.Subject = "Registration link";
            mail.Body = ConfigurationManager.AppSettings["Url"] + "registration/" + token;

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
