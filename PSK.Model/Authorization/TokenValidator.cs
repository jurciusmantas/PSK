using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;

namespace PSK.Model.Services
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
                int result = DateTime.Compare(empToken.ExpiredAt, DateTime.Now);

                if (result < 1 || result == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
