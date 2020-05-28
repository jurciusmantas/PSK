using PSK.Model.Entities;
using PSK.Model.Repository;
using System;

namespace PSK.Model.Authorization
{
    public class TokenValidator : ITokenValidator
    {
        private readonly IEmployeesTokenRepository _employeesTokenRepository;
        public TokenValidator(IEmployeesTokenRepository employeesTokenRepository)
        {
            _employeesTokenRepository = employeesTokenRepository;
        }

        public bool Validate(string token)
        {
            try
            {
                EmployeesToken empToken = _employeesTokenRepository.FindByToken(token);
                if (empToken == null)
                    return false;
                int result = DateTime.Compare(empToken.ExpiredAt, DateTime.Now);

                return result >= 1;
            }
            catch
            {
                return false;
            }
        }
    }
}
