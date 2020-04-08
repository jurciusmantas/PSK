using System;
using System.Security.Cryptography;
using PSK.Model.DBConnection;
using PSK.Model.Entities;
using PSK.Model.BusinessEntities;

namespace PSK.Model.Services
{
    class RegistrationService : IRegistrationService
    {
        private readonly IDBConnection _db;

        public RegistrationService(IDBConnection db)
        {
            _db = db;
        }

        public ServerResult AddNewUser(RegistrationArgs args)
        {
            try
            {
                IncomingEmployee emp = _db.GetIncomingEmployeeByToken(args.Token);
                _db.CreateEmployee(args.FullName, emp.Email, HashPassword(args.Password), 0, null);
                _db.DeleteIncomingEmployee(emp);

                return new ServerResult
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult<string> GetEmailFromToken(string token)
        {
            IncomingEmployee emp = _db.GetIncomingEmployeeByToken(token);

            if (emp != null)
                return new ServerResult<string>
                {
                    Success = true,
                    Data = emp.Email
                };

            else
                return new ServerResult<string>
                {
                    Success = false,
                    Message = "Token doesn't exist in the database"
                };
        }

        private string HashPassword(string password)
        {
            var crypto = new RNGCryptoServiceProvider();
            var salt = new byte[16];
            crypto.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
