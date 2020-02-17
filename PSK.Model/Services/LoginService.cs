using PSK.Model.Entities;

namespace PSK.Model.Services
{
    public class LoginService : ILoginService
    {
        public ServerResult<User> Login(LoginArgs args)
        {
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
    }
}
