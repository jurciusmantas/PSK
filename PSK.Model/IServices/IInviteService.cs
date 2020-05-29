using PSK.Model.DTO;

namespace PSK.Model.IServices
{
    public interface IInviteService
    {
        ServerResult<Invite> Invite(Invite args);
    }
}
