using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Services
{
    public class RestrictionService : IRestrictionService
    {
        private readonly IRestrictionRepository _restrictionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public RestrictionService(IRestrictionRepository restrictionRepository, IEmployeeRepository employeeRepository)
        {
            _restrictionRepository = restrictionRepository;
            _employeeRepository = employeeRepository;
        }

        public ServerResult CreateGlobalRestriction()
        {
            throw new NotImplementedException();
        }

        public ServerResult CreateRestriction(RestrictionArgs restrictionArgs)
        {
            String msg = ValidateRestrictionArgs(restrictionArgs);
            if (msg != null)
            {
                return new ServerResult { Message = msg, Success = false };
            }
            BusinessEntities.Restriction restriction = ParseRestriction(restrictionArgs);
            int receiverId = restrictionArgs.ReceiverId;
            List<Employee> employees;
            switch (receiverId)
            {
                case -1:
                    employees = _employeeRepository.FindAllLower(restrictionArgs.CreatorId);
                    CreateRestrictionForEmployees(restriction, employees);
                    break;
                case -2:
                    employees = _employeeRepository.FindTeamMembers(restrictionArgs.CreatorId);
                    CreateRestrictionForEmployees(restriction, employees);
                    break;
                default:
                    CreateRestrictionForEmployee(restriction, receiverId);
                    break;
            }
            return new ServerResult { Message = "Success", Success = true };
        }

        private String ValidateRestrictionArgs(RestrictionArgs restrictionArgs)
        {
            int creatorId = restrictionArgs.CreatorId;
            int receiverId = restrictionArgs.ReceiverId;
            Employee employee = _employeeRepository.Get(creatorId);
            if(employee == null)
            {
                return "Creator was not found";
            }
            if (receiverId >= 0)
            {
                employee = _employeeRepository.Get(receiverId);
                if(employee == null)
                {
                    return "Receiver was not found";
                }
            }
            return null;
        }

        private void CreateRestrictionForEmployees(BusinessEntities.Restriction restriction, List<Employee> employees)
        {
            List<EmployeeRestriction> employeeRestrictions = new List<EmployeeRestriction>();
            foreach(Employee employee in employees)
            {
                employeeRestrictions.Add(CreateEmployeeRestriction(employee.Id, restriction));
            }
            restriction.RestrictionEmployees = employeeRestrictions;
            _restrictionRepository.Add(restriction);
        }

        private void CreateRestrictionForEmployee(BusinessEntities.Restriction restriction, int employeeId)
        {
            EmployeeRestriction employeeRestriction = CreateEmployeeRestriction(employeeId, restriction);
            List<EmployeeRestriction> restrictionEmployees = new List<EmployeeRestriction>
            {
                employeeRestriction
            };
            restriction.RestrictionEmployees = restrictionEmployees;
            _restrictionRepository.Add(restriction);
            
        }

        private EmployeeRestriction CreateEmployeeRestriction(int receiverId, BusinessEntities.Restriction restriction)
        {
            return new EmployeeRestriction
            {
                EmployeeId = receiverId, //patikrinti ar Egz toks id
                Restriction = restriction
            };
        }

        private BusinessEntities.Restriction ParseRestriction(RestrictionArgs restrictionArgs)
        {
            return new BusinessEntities.Restriction
            {
                ConsecutiveDays = restrictionArgs.ConsecutiveDays,
                MaxDaysPerMonth = restrictionArgs.MaxDaysPerMonth,
                MaxDaysPerQuarter = restrictionArgs.MaxDaysPerQuarter,
                MaxDaysPerYear = restrictionArgs.MaxDaysPerYear,
                Global = false,
                CreatorId = restrictionArgs.CreatorId,
                CreationDate = DateTime.Now,
            };
        }

        public ServerResult DeleteRestriction(int id)
        {
            BusinessEntities.Restriction deletedRestriction = _restrictionRepository.Delete(id);
            if (deletedRestriction == null)
            {
                return new ServerResult { Message = "Restriction was not found", Success = false };
            }
            return new ServerResult { Message = "Success", Success = true };
        }

        public ServerResult<Entities.Restriction> GetRestriction(int employeeId)
        {
            Employee employee = _employeeRepository.Get(employeeId);
            if (employee == null)
            {
                return new ServerResult<Entities.Restriction> { Message = "Employee was not found", Success = false };
            } 
            var employeeRestrictions = employee.EmployeeRestrictions;
            Entities.Restriction restrictionResult = null;
            if (employeeRestrictions != null && employeeRestrictions.Count > 0)
            {
                EmployeeRestriction employeeRestriction = employeeRestrictions.OrderByDescending(er => er.Restriction.CreationDate).First();
                restrictionResult = ParseRestriction(employeeRestriction.Restriction);
            }
            return new ServerResult<Entities.Restriction> { Data = restrictionResult, Message = "Success", Success = true };
        }

        private Entities.Restriction ParseRestriction(BusinessEntities.Restriction restriction)
        {
            return new Entities.Restriction
            {
                Id = restriction.Id,
                ConsecutiveDays = restriction.ConsecutiveDays,
                MaxDaysPerYear = restriction.MaxDaysPerYear,
                MaxDaysPerQuarter = restriction.MaxDaysPerQuarter,
                MaxDaysPerMonth = restriction.MaxDaysPerMonth,
                Global = restriction.Global,
                UseCount = restriction.RestrictionEmployees.Count
            };
        }

        public ServerResult<List<Entities.Restriction>> GetCreatedRestrictions(int employeeId)
        {
            List<BusinessEntities.Restriction> restrictions = _restrictionRepository.GetCreatedRestrictions(employeeId);
            List<Entities.Restriction> restrictionsResult = new List<Entities.Restriction>();
            foreach (BusinessEntities.Restriction restriction in restrictions)
            {
                var restrictionResultItem = ParseRestriction(restriction);
                restrictionsResult.Add(restrictionResultItem);
            }
            return new ServerResult<List<Entities.Restriction>> { Data = restrictionsResult, Message = "Success", Success = true };
        }
    }
}
