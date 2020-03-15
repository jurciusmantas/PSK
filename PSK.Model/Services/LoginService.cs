using PSK.Model.Entities;
using System;

namespace PSK.Model.Services
{
    public class LoginService : ILoginService
    {
        public ServerResult<User> Login(LoginArgs args)
        {
            try
            {
                if (args == null)
                    throw new Exception("Arguments are empty");

                if (args.Login == "admin" && args.Password == "admin")
                    return new ServerResult<User>
                    {
                        Success = true,
                        Data = new User
                        {
                            Login = "admin",
                            FirstName = "admin",
                            LastName = "admin",
                            Token = GetToken(),
                        },
                    };

                else
                    return new ServerResult<User>
                    {
                        Success = false,
                        Message = "bad credentials",
                        Data = null,
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
                    throw new Exception("Token cannot be empty");

                if (token == "Pacman")
                    return new ServerResult<User>
                    {
                        Success = true,
                        Data = new User
                        {
                            Login = "admin",
                            FirstName = "admin",
                            LastName = "admin",
                            Token = token,
                        },
                    };

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

        private string GetToken()
        {
            //return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return "Pacman";
        }
    }
}
