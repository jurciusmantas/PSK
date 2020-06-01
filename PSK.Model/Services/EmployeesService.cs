using PSK.Model.DTO;
using PSK.Model.Helpers;
using PSK.Model.IServices;
using PSK.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PSK.Model.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeeRepository _employeeRep;

        public EmployeesService(IEmployeeRepository employeeRepository)
        {
            _employeeRep = employeeRepository;
        }

        public List<Employee> Get()
        {
            return _employeeRep.Get().Select(e => e.ToDTO()).ToList();
        }

        public Employee Get(int id)
        {
            Entities.Employee employee = _employeeRep.Get(id);
            return employee != null ? employee.ToDTO() : null;
        }

        public List<Employee> GetSubordinates(int employeeId)
        {
            return _employeeRep.GetSubordinates(employeeId)
                .Select(e => e.ToDTO()).ToList();
        }

        public EmployeeProfile GetProfile(int id, int currentId)
        {
            var profile = new EmployeeProfile();


            var employee = id != currentId ? _employeeRep.GetAllSubordinates(currentId)
                                    .Where(x => x.Id == id).FirstOrDefault() : _employeeRep.Get(id);

            
           // var employee = _employeeRep.Get(id);
            if (employee == null)
                return null;

            profile.LeaderName = employee.Leader?.Name;

            var activeTopics = _employeeRep.GetEmployeesActiveTopics(id);
            profile.Topics = activeTopics.Select(t => t.ToDTO()).ToList();

            profile.Subordinates = GetSubordinates(id);

            return profile;
        }

        public ServerResult<EmployeeArgs> UpdateEmployee(EmployeeArgs employee)
        {
            try
            {
                Entities.Employee dbEmployee = _employeeRep.Get(employee.Id);
                if (dbEmployee == null)
                    return new ServerResult<EmployeeArgs>()
                    {
                        Success = false,
                        Message = "Employee does not exist",
                    };

                dbEmployee.Name = employee.Name;
                dbEmployee.Password = employee.Password.Hash();

                _employeeRep.Update(dbEmployee);
                return new ServerResult<EmployeeArgs>() { Success = true };
            }
            catch (Exception e)
            {
                return new ServerResult<EmployeeArgs>()
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }
    }
}
