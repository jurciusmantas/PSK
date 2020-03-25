using PSK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Services
{
    public interface IRegistrationService
    {
        public ServerResult AddNewUser(RegistrationArgs args);
        public ServerResult<string> GetEmailFromToken(string token);
    }
}
