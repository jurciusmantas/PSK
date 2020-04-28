using PSK.Model.Entities;
using PSK.Model.DTO;
using PSK.Model.Repository;
using Serilog;
using System;
using System.Security.Cryptography;

namespace PSK.Model.Services
{
    public class LoginService : ILoginService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public LoginService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ServerResult<User> Login(LoginArgs args)
        {
            try
            {
                if (args == null)
                    throw new ArgumentNullException("Arguments are empty");

                if (args.Login == "admin" && args.Password == "admin")
                    return new ServerResult<User>
                    {
                        Success = true,
                        Data = new User
                        {
                            Login = "admin",
                            Name = "admin",
                            Token = GetToken(),
                        },
                    };

                Employee employee = _employeeRepository.Login(args);
                VerifyPassword(args.Password, employee.Password);              
                return new ServerResult<User>
                {
                    Success = true,
                    Data = new User
                    {
                        Login = args.Login,
                        Name = employee.Name,
                        Token = GetToken()
                    }
                };
            }
            catch (UnauthorizedAccessException e)
            {
                return new ServerResult<User>
                {
                    Success = false,
                    Message = e.Message,
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

                if (token == "Pacman")
                {
                    return new ServerResult<User>
                    {
                        Success = true,
                        Data = new User
                        {
                            Login = "admin",
                            Name = "admin",
                            Token = token,
                        },
                    };
                }

                else
                    return new ServerResult<User>
                    {
                        Success = false,
                        Message = "token expired",
                        Data = null,
                    };
            }
            catch(Exception e)
            {
                return new ServerResult<User>
                {
                    Success = false,
                    Message = e.Message,
                };
            }
        }

        public void Logout()
        {
            //TODO:
            //Once authorization will be implemented (with _sessionData as service
            //with lifestyle session to have the currently logged in user) -
            //remove its token (call to DB too!).
        }

        private string GetToken()
        {
            //return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return "Pacman";
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
    }
}
