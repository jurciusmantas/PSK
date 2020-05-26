using PSK.Model.Entities;
using PSK.Model.DTO;

namespace PSK.Model.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee Login(LoginArgs loginArgs);
        public Employee FindByName(string name);
    }
}
