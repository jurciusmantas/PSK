using PSK.Model.DTO;
using PSK.Model.Helpers;
using PSK.Model.Repository;
using System.Collections.Generic;
using System.Linq;

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
            return _employeeRep.Get().Select(e => e.EntityToDTO()).ToList();
        }

        public Employee Get(int id)
        {
            Entities.Employee employee = _employeeRep.Get(id);
            return employee != null ? employee.EntityToDTO() : null;
        }

        public List<Employee> GetSubordinates(int employeeId)
        {
            return _employeeRep.GetSubordinates(employeeId)
                .Select(e => e.EntityToDTO()).ToList();
        }

        public EmployeeProfile GetProfile(int id)
        {
            var profile = new EmployeeProfile();

            var employee = _employeeRep.Get(id);
            if (employee == null)
                return null;

            profile.LeaderName = employee.Leader?.Name;

            var activeTopics = _employeeRep.GetEmployeesActiveTopics(id);
            profile.Topics = activeTopics.Select(t => t.EntityToDTO()).ToList();

            profile.Subordinates = GetSubordinates(id);

            return profile;
        }
    }
}
