using System;
using System.Security.Cryptography;
using PSK.Model.DTO;
using PSK.Model.Entities;
using PSK.Model.IServices;
using PSK.Model.Repository;

namespace PSK.Model.Services
{
    class RegistrationService : IRegistrationService
    {
        private readonly IIncomingEmployeeRepository _incomingEmployeeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RegistrationService(IIncomingEmployeeRepository incomingEmployeeRepository,
            IEmployeeRepository employeeRepository)
        {
            _incomingEmployeeRepository = incomingEmployeeRepository;
            _employeeRepository = employeeRepository;
        }

        public ServerResult AddNewUser(Registration args)
        {
            try
            {
                IncomingEmployee emp = _incomingEmployeeRepository.FindByToken(args.Token);
                _employeeRepository.Add(new Entities.Employee
                {
                    Name = args.FullName.Trim(),
                    Email = emp.Email,
                    Password = HashPassword(args.Password),
                    LeaderId = 1,
                });
                _incomingEmployeeRepository.Delete(emp.Id);

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
            IncomingEmployee emp = _incomingEmployeeRepository.FindByToken(token);

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
