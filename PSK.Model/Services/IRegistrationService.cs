using PSK.Model.DTO;

namespace PSK.Model.Services
{
    public interface IRegistrationService
    {
        public ServerResult AddNewUser(Registration args);
        public ServerResult<string> GetEmailFromToken(string token);
    }
}
