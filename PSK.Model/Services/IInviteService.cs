using PSK.Model.Entities;

namespace PSK.Model.Services
{
    public interface IInviteService
    {
        ServerResult<InviteArgs> Invite(InviteArgs args);
    }
}
