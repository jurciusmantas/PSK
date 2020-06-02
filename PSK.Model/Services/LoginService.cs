using PSK.Model.DTO;
using PSK.Model.Entities;
using PSK.Model.Helpers;
using PSK.Model.IServices;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PSK.Model.Services
{
    public class LoginService : ILoginService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeesTokenRepository _employeesTokenRepository;

        public LoginService(IEmployeeRepository employeeRepository,
            IEmployeesTokenRepository employeesTokenRepository)
        {
            _employeeRepository = employeeRepository;
            _employeesTokenRepository = employeesTokenRepository;
        }

        public ServerResult<User> Login(LoginArgs args)
        {
            try
            {
                if (args == null)
                    throw new ArgumentNullException("Arguments are empty");

                Entities.Employee employee = _employeeRepository.Login(args);
                if (employee == null)
                    throw new ArgumentException("Employee not found");
                VerifyPassword(args.Password, employee.Password);
                DeleteExpiredTokens(employee.Id);
                string token = GetToken();
                DateTime expiredAt = DateTime.Now.AddMinutes(60);
                _employeesTokenRepository.Add(new EmployeesToken
                {
                    Employee = employee,
                    EmployeeId = employee.Id,
                    Token = token,
                    CreatedAt = DateTime.Now,
                    ExpiredAt = expiredAt
                });
                return new ServerResult<User>
                {
                    Success = true,
                    Data = new User
                    {
                        Employee = employee.ToDTO(),
                        Token = token,
                        ExpiredAt = expiredAt.ToString()
                    }
                };
            }
            catch (Exception e)
            {
                return new ServerResult<User>
                {
                    Success = false,
                    Message = e.Message,
                };
            }
        }

        public ServerResult<User> LoginToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    throw new ArgumentNullException("Token cannot be empty");

                EmployeesToken employeesToken = _employeesTokenRepository.FindByToken(token);

                if (employeesToken == null || employeesToken.ExpiredAt < DateTime.Now)
                {
                    return new ServerResult<User>
                    {
                        Success = false,
                        Message = "token expired",
                        Data = null,
                    };
                }

                return new ServerResult<User>
                {
                    Success = true,
                    Data = new User
                    {
                        Employee = _employeeRepository.Get(employeesToken.EmployeeId).ToDTO(),
                        Token = token,
                        ExpiredAt = employeesToken.ExpiredAt.ToString()
                    },
                };
            }
            catch (Exception e)
            {
                return new ServerResult<User>
                {
                    Success = false,
                    Message = e.Message,
                };
            }
        }

        public void Logout(string token)
        {
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Token "))
            {
                token = token.Substring(6);
                var tokenInst = _employeesTokenRepository.FindByToken(token);
                if (tokenInst != null && tokenInst.ExpiredAt > DateTime.Now)
                {
                    tokenInst.ExpiredAt = DateTime.Now;
                    _employeesTokenRepository.Update(tokenInst);
                }     
            }
        }

        private string GetToken()
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
            var token = result.ToString();
            if (_employeesTokenRepository.FindByToken(token) != null)
                return GetToken();
            else
                return result.ToString();
        }

        private void VerifyPassword(string password, string savedPasswordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
        }

        private void DeleteExpiredTokens(int employeeId)
        {
            List<EmployeesToken> tokens = _employeesTokenRepository.AllEmployeesTokens(employeeId);
            int result;
            foreach (EmployeesToken t in tokens)
            {
                result = DateTime.Compare(t.ExpiredAt, DateTime.Now);
                if (result < 1 || result == 0)
                {
                    _employeesTokenRepository.Delete(t.Id);
                }
            }
        }
    }
}
