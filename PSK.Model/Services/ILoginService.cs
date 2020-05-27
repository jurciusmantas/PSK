using PSK.Model.DTO;

namespace PSK.Model.Services
{
    public interface ILoginService
    {
        ServerResult<User> Login(LoginArgs args);
        ServerResult<User> LoginToken(string token);
        void Logout();
    }
}