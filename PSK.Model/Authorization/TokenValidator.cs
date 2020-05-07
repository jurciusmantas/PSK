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
                if (empToken.ExpiredAt > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    _employeesTokenRepository.Delete(empToken.Id);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
