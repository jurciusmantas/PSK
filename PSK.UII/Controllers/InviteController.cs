using Microsoft.AspNetCore.Mvc;
using PSK.Model.Entities;
using PSK.Model.Services;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class InviteController : Controller
    {
        private readonly IInviteService _inviteService;

        public InviteController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        [HttpGet]
        public void GetInviteArgs()
        {
        }

        [HttpPost]
        public ServerResult<InviteArgs> 
            Invite([FromBody]InviteArgs args)
        {
            return _inviteService.Invite(args);
        }
    }
}