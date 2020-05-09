using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSK.Model.Entities;
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
        [Route("login")]
        public ServerResult<User> Login([FromBody]LoginArgs args)
        {
            return _loginService.Login(args);
        }

        [HttpPost]
        [Route("login_token")]
        public ServerResult<User> LoginFromToken([FromBody]string token)
        {
            return _loginService.LoginToken(token);
        }

        [HttpPost]
        [Route("logout")]
        public void Logout()
        {
            _loginService.Logout();
        }
    }
}
