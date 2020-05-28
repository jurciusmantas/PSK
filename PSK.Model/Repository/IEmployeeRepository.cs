using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee Login(DTO.LoginArgs loginArgs);
        public Employee FindByName(string name);
        List<Employee> GetSubordinates(int employeeId);
        List<Topic> GetEmployeesActiveTopics(int employeeId);
    }
}
