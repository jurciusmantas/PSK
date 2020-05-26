using PSK.Model.DTO;

namespace PSK.Model.Services
{
    public interface IInviteService
    {
        ServerResult<Invite> Invite(Invite args);
    }
}
