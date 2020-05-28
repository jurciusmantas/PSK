
using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PSK.Model.DTO;

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
            Entities.Restriction restriction = ParseRestriction(restrictionArgs);
            int applyTo = restrictionArgs.ApplyTo;
            List<Entities.Employee> employees;
            string foundNames;
            switch (applyTo)
            {
                case 1:
                    employees = _employeeRepository.GetAllSubordinates(restrictionArgs.CreatorId);
                    CreateRestrictionForEmployees(restriction, employees);
                    break;
                case 2:
                    employees = _employeeRepository.GetSubordinates(restrictionArgs.CreatorId);
                    CreateRestrictionForEmployees(restriction, employees);
                    break;
                default:
                    (employees, foundNames) = FindByIds(restrictionArgs.UserIds);
                    CreateRestrictionForEmployees(restriction, employees);
                    if (foundNames.Length > 0)
                    {
                        return new ServerResult { Message = "Restriction was added only for those employees " + foundNames, Success = true };
                    }
                    break;
            }
            return new ServerResult { Message = "Success", Success = true };
        }

        private string ValidateRestrictionArgs(RestrictionArgs restrictionArgs)
        {
            int creatorId = restrictionArgs.CreatorId;
            Entities.Employee employee = _employeeRepository.Get(creatorId);
            if (employee == null)
            {
                return "Creator was not found";
            }
            if (restrictionArgs.ApplyTo > 2 && (restrictionArgs.UserIds == null || restrictionArgs.UserIds.Length == 0))
            {
                return "There was not any specified restriction receiver";
            }
            return null;
        }

        private void CreateRestrictionForEmployees(Entities.Restriction restriction, List<Entities.Employee> employees)
        {
            List<EmployeeRestriction> employeeRestrictions = new List<EmployeeRestriction>();
            foreach (Entities.Employee employee in employees)
            {
                employeeRestrictions.Add(CreateEmployeeRestriction(employee.Id, restriction));
            }
            restriction.RestrictionEmployees = employeeRestrictions;
            _restrictionRepository.Add(restriction);
        }

        private EmployeeRestriction CreateEmployeeRestriction(int receiverId, Entities.Restriction restriction)
        {
            return new EmployeeRestriction
            {
                EmployeeId = receiverId,
                Restriction = restriction
            };
        }

        private Entities.Restriction ParseRestriction(RestrictionArgs restrictionArgs)
        {
            return new Entities.Restriction
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
            Entities.Restriction deletedRestriction = _restrictionRepository.Delete(id);
            if (deletedRestriction == null)
            {
                return new ServerResult { Message = "Restriction was not found", Success = false };
            }
            return new ServerResult { Message = "Success", Success = true };
        }

        public ServerResult<List<DTO.Restriction>> GetRestrictionsTo(int employeeId)
        { 
            (Entities.Employee employee, ServerResult<List<DTO.Restriction>> result) = ValidateEmployee<List<DTO.Restriction>>(employeeId);
            if (employee == null)
            {
                return result;
            }
            var restrictionsTo = GetRestrictions(employeeId);
            return new ServerResult<List<DTO.Restriction>> { Data = restrictionsTo, Success = true, Message = "Success" };

        }

        private List<DTO.Restriction> GetRestrictions(int employeeId)
        {
            (List<Entities.Restriction> restrictions, List<int> useCounts) = _restrictionRepository.GetRestrictionsTo(employeeId);
            List<DTO.Restriction> restrictionsResult = new List<DTO.Restriction>();
            for(int i = 0; i < restrictions.Count; i++)
            {
                restrictionsResult.Add(ParseRestriction(restrictions[i], useCounts[i]));
            }
            return restrictionsResult;
        }

        private (Entities.Employee, ServerResult<T>) ValidateEmployee<T>(int employeeId)
        {
            var employee = _employeeRepository.Get(employeeId);

            if (employee == null)
            {
                return (employee, new ServerResult<T> { Message = "Employee was not found", Success = false });
            }
            else
            {
                return (employee, null);
            }
        }

        public ServerResult<DTO.Restriction> GetRestrictionFrom(int employeeId)
        {
            (Entities.Employee employee, ServerResult<DTO.Restriction> result) = ValidateEmployee<DTO.Restriction>(employeeId);
            if (employee == null)
            {
                return result;
            }
            else
            {
                var restrictionFrom = GetRestriction(employee);
                return new ServerResult<DTO.Restriction> { Data = restrictionFrom, Success = true, Message = "Success" };
            }
        }
        private DTO.Restriction GetRestriction(Entities.Employee employee)
        {
            var employeeRestrictions = employee.EmployeeRestrictions;
            DTO.Restriction restrictionResult = null;
            if (employeeRestrictions != null && employeeRestrictions.Count > 0)
            {
                EmployeeRestriction employeeRestriction = employeeRestrictions.OrderByDescending(er => er.Restriction.CreationDate).First();
                restrictionResult = ParseRestriction(employeeRestriction.Restriction);
            }
            return restrictionResult;
        }

        private DTO.Restriction ParseRestriction(Entities.Restriction restriction, int useCount = 0)
        {
            return new DTO.Restriction
            {
                Id = restriction.Id,
                ConsecutiveDays = restriction.ConsecutiveDays,
                MaxDaysPerYear = restriction.MaxDaysPerYear,
                MaxDaysPerQuarter = restriction.MaxDaysPerQuarter,
                MaxDaysPerMonth = restriction.MaxDaysPerMonth,
                Global = restriction.Global,
                UseCount = useCount
            };
        }

        private (List<Entities.Employee>, string) FindByIds(int[] ids)
        {
            Entities.Employee employee;
            List<Entities.Employee> foundEmployees = new List<Entities.Employee>();
            StringBuilder foundEmplStrBldr = new StringBuilder();
            bool employeeNotFound = false;
            foreach(int id in ids)
            {
                employee = _employeeRepository.Get(id);
                if (employee != null)
                {
                    foundEmployees.Add(employee);
                    if (foundEmplStrBldr.Length > 0)
                    {
                        foundEmplStrBldr.Append(", ");
                    }
                    foundEmplStrBldr.Append(employee.Name);
                }
                else
                {
                    employeeNotFound = true;
                }
            }
            string names = employeeNotFound ? foundEmplStrBldr.ToString() : "";
            return (foundEmployees, names);
        }
    }
}
