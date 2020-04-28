using PSK.Model.BusinessEntities;
using PSK.Model.Entities;

namespace PSK.Model.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee Login(LoginArgs loginArgs);
    }
}
