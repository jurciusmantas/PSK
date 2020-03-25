using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Entities
{
    public class RegistrationArgs
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
