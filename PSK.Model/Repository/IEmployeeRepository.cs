using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee Login(DTO.LoginArgs loginArgs);
        public Employee FindByName(string name);
        public List<Employee> GetSubordinates(int employeeId);
        public List<Employee> GetAllSubordinates(int leaderId);
        List<Topic> GetEmployeesActiveTopics(int employeeId);
        bool CheckIfEmailExists(string email);
    }
}
