using PSK.Model.Entities;
using PSK.Model.DTO;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee Login(LoginArgs loginArgs);
        public Employee FindByName(string name);
        public List<Employee> FindTeamMembers(int leaderId);
        public List<Employee> FindAllLower(int leaderId);
    }
}
