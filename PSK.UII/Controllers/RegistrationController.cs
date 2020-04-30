using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSK.Model.Entities;
using PSK.Model.Services;


namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]

    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet, Route("{token}")]
        public ServerResult<string> GetEmailFromToken(string token)
        {
            return _registrationService.GetEmailFromToken(token);
        }

        [HttpPost]
        public ServerResult AddNewUser([FromBody]RegistrationArgs args)
        {
            return _registrationService.AddNewUser(args);
        } 
    }
}