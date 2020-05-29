using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.IServices;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class InviteController : ControllerBase
    {
        private readonly IInviteService _inviteService;

        public InviteController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        [HttpPost]
        public ServerResult<Invite> Invite([FromBody]Invite args)
        {
            return _inviteService.Invite(args);
        }
    }
}