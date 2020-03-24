using PSK.Model.DBConnection;
using PSK.Model.Entities;
using System;
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
                _db.CreateEmployee("", args.Email, "", 0);

                SendInviteMail(args.Email);

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

        public void SendInviteMail(string receiverEmail)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("noreply.megstamkumpi@gmail.com");
            mail.To.Add(receiverEmail);
            mail.Subject = "Test";
            mail.Body = GenerateToken();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("noreply.megstamkumpi@gmail.com", "labai5l4pt45");
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
