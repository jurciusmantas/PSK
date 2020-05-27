
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
            string notFoundNames;
            switch (applyTo)
            {
                case 1:
                    employees = _employeeRepository.FindAllLower(restrictionArgs.CreatorId);
                    CreateRestrictionForEmployees(restriction, employees);
                    break;
                case 2:
                    employees = _employeeRepository.FindTeamMembers(restrictionArgs.CreatorId);
                    CreateRestrictionForEmployees(restriction, employees);
                    break;
                default:
                    (employees, notFoundNames) = FindByNames(restrictionArgs.UserNames);
                    CreateRestrictionForEmployees(restriction, employees);
                    if (notFoundNames.Length > 0)
                    {
                        return new ServerResult { Message = "These names where not found " + notFoundNames, Success = true };
                    }
                    break;
            }
            return new ServerResult { Message = "Success", Success = true };
        }

        private String ValidateRestrictionArgs(RestrictionArgs restrictionArgs)
        {
            int creatorId = restrictionArgs.CreatorId;
            Entities.Employee employee = _employeeRepository.Get(creatorId);
            if (employee == null)
            {
                return "Creator was not found";
            }
            if (restrictionArgs.ApplyTo > 2 && (restrictionArgs.UserNames == null || restrictionArgs.UserNames.Length == 0))
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

        private void CreateRestrictionForEmployee(Entities.Restriction restriction, int employeeId)
        {
            EmployeeRestriction employeeRestriction = CreateEmployeeRestriction(employeeId, restriction);
            List<EmployeeRestriction> restrictionEmployees = new List<EmployeeRestriction>
            {
                employeeRestriction
            };
            restriction.RestrictionEmployees = restrictionEmployees;
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

        public ServerResult<Restrictions> GetRestrictions(int employeeId)
        {
            Entities.Employee employee = _employeeRepository.Get(employeeId);
            if (employee == null)
            {
                return new ServerResult<Restrictions> { Message = "Employee was not found", Success = false };
            }
            Restrictions restrictions = new Restrictions
            {
                Restriction = GetRestriction(employee),
                RestrictionsList = GetToRestrictions(employeeId),
                TeamMembers = GetTeamMembers(employeeId),
            };
            return new ServerResult<Restrictions> { Data = restrictions, Success = true, Message = "Success" };

        }
        public DTO.Restriction GetRestriction(Entities.Employee employee)
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

        private DTO.Restriction ParseRestriction(Entities.Restriction restriction)
        {
            return new DTO.Restriction
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

        private DTO.Employee ParseUser(Entities.Employee employee)
        {
            return new DTO.Employee { Name = employee.Name };
        }

        private (List<Entities.Employee>, string) FindByNames(string[] names)
        {
            Entities.Employee employee;
            List<Entities.Employee> foundEmployees = new List<Entities.Employee>();
            StringBuilder notFoundEmplStrBldr = new StringBuilder();
            foreach(string name in names)
            {
                employee = _employeeRepository.FindByName(name);
                if (employee != null)
                {
                    foundEmployees.Add(employee);
                }
                else
                {
                    if (notFoundEmplStrBldr.Length > 0)
                    {
                        notFoundEmplStrBldr.Append(", ");
                    }
                    notFoundEmplStrBldr.Append(name);
                }
            }
            return (foundEmployees, notFoundEmplStrBldr.ToString());
        }

        public List<DTO.Restriction> GetToRestrictions(int employeeId)
        {
            List<Entities.Restriction> restrictions = _restrictionRepository.GetToRestrictions(employeeId);
            List<DTO.Restriction> restrictionsResult = new List<DTO.Restriction>();
            foreach (Entities.Restriction restriction in restrictions)
            {
                var restrictionResultItem = ParseRestriction(restriction);
                restrictionsResult.Add(restrictionResultItem);
            }
            return restrictionsResult;
        }

        public List<DTO.Employee> GetTeamMembers(int leaderId)
        {
            List<Entities.Employee> employees = _employeeRepository.FindTeamMembers(leaderId);
            List<DTO.Employee> users = new List<DTO.Employee>();
            foreach(Entities.Employee employee in employees)
            {
                var user = ParseUser(employee);
                users.Add(user);
            }
            return users;
        }
    }
}
