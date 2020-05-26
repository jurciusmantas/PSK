using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.Services;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public ServerResult<User> Login([FromBody] LoginArgs args, [FromQuery] bool token = false)
        {
            if(token)
            {
                return _loginService.LoginToken(args.Token);
            }
            return _loginService.Login(args);
        }

        [HttpPost]
        [Route("logout")]
        public void Logout()
        {
            _loginService.Logout();
        }
    }
}
