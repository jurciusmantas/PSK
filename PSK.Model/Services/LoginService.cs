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
    }
}
