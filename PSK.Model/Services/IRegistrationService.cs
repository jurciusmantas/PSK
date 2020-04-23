using PSK.Model.Entities;

namespace PSK.Model.Services
{
    public interface IRegistrationService
    {
        public ServerResult AddNewUser(RegistrationArgs args);
        public ServerResult<string> GetEmailFromToken(string token);
    }
}
