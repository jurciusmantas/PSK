
using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PSK.Model.DTO;
using PSK.Model.IServices;
using PSK.Model.Helpers;

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

        public ServerResult CreateRestriction(RestrictionArgs restrictionArgs)
        {
            var employee = _employeeRepository.Get(restrictionArgs.CreatorId);
            String msg = ValidateRestrictionArgs(restrictionArgs, employee);
            if (msg != null)
            {
                return new ServerResult { Message = msg, Success = false };
            }
            Entities.Restriction restriction = restrictionArgs.DTOToEntity();
            int applyTo = restrictionArgs.ApplyTo;
            List<Entities.Employee> employees;
            string foundNames;
            switch (applyTo)
            {
                case 1:
                    employees = _employeeRepository.GetAllSubordinates(restrictionArgs.CreatorId);
                    restriction.Global = employee.LeaderId == null;
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

        private string ValidateRestrictionArgs(RestrictionArgs restrictionArgs, Entities.Employee employee)
        {
            StringBuilder errorStringBuilder = new StringBuilder();
            bool errorFound = false;
            errorStringBuilder.Append("There were several errors:");
            if (employee == null)
            {
                errorFound = true;
                errorStringBuilder.Append("\nCreator was not found");
            }
            if (restrictionArgs.ConsecutiveDays <= 0 || restrictionArgs.ConsecutiveDays > 366)
            {
                errorFound = true;
                errorStringBuilder.Append("\nNumber of consecutive learning days must be from 1 to 366");
            }
            if (restrictionArgs.MaxDaysPerMonth <= 0 || restrictionArgs.MaxDaysPerMonth > 31)
            {
                errorFound = true;
                errorStringBuilder.Append("\nNumber of maximum learning days per month must be from 1 to 31");
            }
            if (restrictionArgs.MaxDaysPerQuarter <=0 || restrictionArgs.MaxDaysPerQuarter > 93)
            {
                errorFound = true;
                errorStringBuilder.Append("\nNumber of maximum learning days per quarter must be from 1 to 93");
            }
            if (restrictionArgs.MaxDaysPerYear <= 0 || restrictionArgs.MaxDaysPerYear > 366)
            {
                errorFound = true;
                errorStringBuilder.Append("\nNumber of maximum learning days per year must be from 1 to 366");
            }
            if (restrictionArgs.MaxDaysPerQuarter < restrictionArgs.MaxDaysPerMonth)
            {
                errorFound = true;
                errorStringBuilder.Append("\nNumber of maximum learning days per quarter should not be lower than days per month");
            }
            if (restrictionArgs.MaxDaysPerYear < restrictionArgs.MaxDaysPerQuarter)
            {
                errorFound = true;
                errorStringBuilder.Append("\nNumber of maximum learning days per year should not be lower than days per quarter"); ;
            }
            if (restrictionArgs.ApplyTo > 2 && (restrictionArgs.UserIds == null || restrictionArgs.UserIds.Length == 0))
            {
                errorFound = true;
                errorStringBuilder.Append("\nThere was not any specified restriction receiver");
            }
            return errorFound ? errorStringBuilder.ToString() : null;
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

        public ServerResult DeleteRestriction(int id, int employeeId)
        {
            string msg = ValidateRestrictionDeletion(id, employeeId);
            if(msg != null)
            {
                return new ServerResult { Message = msg, Success = false };
            }
            _restrictionRepository.Delete(id);
            return new ServerResult { Message = "Success", Success = true };
        }

        public ServerResult<List<DTO.Restriction>> GetRestrictionsTo(int employeeId)
        { 
            (Entities.Employee employee, string msg) = ValidateEmployee(employeeId);
            if (employee == null)
            {
                return new ServerResult<List<DTO.Restriction>> { Message = msg, Success = false };
            }
            var restrictionsTo = GetRestrictions(employeeId);
            return new ServerResult<List<DTO.Restriction>> { Data = restrictionsTo, Success = true, Message = "Success" };

        }

        private List<DTO.Restriction> GetRestrictions(int employeeId)
        {
            (List<Entities.Restriction> restrictions, List<List<string>> useCountNames) = _restrictionRepository.GetRestrictionsTo(employeeId);
            List<DTO.Restriction> restrictionsResult = new List<DTO.Restriction>();
            for(int i = 0; i < restrictions.Count; i++)
            {
                restrictionsResult.Add(restrictions[i].EntityToDTO(useCountNames[i]));
            }
            return restrictionsResult;
        }

        private (Entities.Employee, string msg) ValidateEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(employeeId);

            if (employee == null)
            {
                return (employee, "Employee was not found");
            }
            else
            {
                return (employee, null);
            }
        }

        public ServerResult<DTO.Restriction> GetRestrictionFrom(int employeeId)
        {
            (Entities.Employee employee, String msg) = ValidateEmployee(employeeId);
            if (employee == null)
            {
                return new ServerResult<DTO.Restriction> { Message = msg, Success = false };
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
                restrictionResult = employeeRestriction.Restriction.EntityToDTO();
            }
            return restrictionResult;
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

        private string ValidateRestrictionDeletion(int restrictionId, int employeeId)
        {
            (Entities.Employee employee, string msg) = ValidateEmployee(employeeId);
            if(msg != null)
            {
                return msg;
            }
            Entities.Restriction restriction = _restrictionRepository.Get(restrictionId);
            if (restriction == null)
            {
                return "Restriction was not found";
            }
            if (employee.Id != restriction.CreatorId)
            {
                return "You do not have permission to delete this restriction";
            }
            return null;
        }

    }
}
