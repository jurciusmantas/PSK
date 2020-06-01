using System;
using System.Security.Cryptography;
using PSK.Model.DTO;
using PSK.Model.Entities;
using PSK.Model.IServices;
using PSK.Model.Repository;
using PSK.Model.Helpers;

namespace PSK.Model.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IIncomingEmployeeRepository _incomingEmployeeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RegistrationService(IIncomingEmployeeRepository incomingEmployeeRepository,
            IEmployeeRepository employeeRepository)
        {
            _incomingEmployeeRepository = incomingEmployeeRepository;
            _employeeRepository = employeeRepository;
        }

        public ServerResult AddNewUser(Registration args)
        {
            try
            {
                IncomingEmployee emp = _incomingEmployeeRepository.FindByToken(args.Token);
                _employeeRepository.Add(new Entities.Employee
                {
                    Name = args.FullName.Trim(),
                    Email = emp.Email,
                    Password = args.Password.Hash(),
                    LeaderId = emp.LeaderId,
                });
                _incomingEmployeeRepository.Delete(emp.Id);

                return new ServerResult
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult<string> GetEmailFromToken(string token)
        {
            IncomingEmployee emp = _incomingEmployeeRepository.FindByToken(token);

            if (emp != null)
                return new ServerResult<string>
                {
                    Success = true,
                    Data = emp.Email
                };

            else
                return new ServerResult<string>
                {
                    Success = false,
                    Message = "Token doesn't exist in the database"
                };
        }
    }
}
