using PSK.Model.DTO;
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
            return _employeeRep.Get().Select(e => EntityToDTO(e)).ToList();
        }

        public Employee Get(int id)
        {
            Entities.Employee employee = _employeeRep.Get(id);
            return employee != null ? EntityToDTO(employee) : null;
        }

        public List<Employee> GetSubordinates(int employeeId)
        {
            return _employeeRep.FindTeamMembers(employeeId)
                .Select(e => EntityToDTO(e)).ToList();
        }

        private Employee EntityToDTO(Entities.Employee entity)
        {
            return new Employee()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                LeaderId = entity.LeaderId
            };
        }
    }
}
