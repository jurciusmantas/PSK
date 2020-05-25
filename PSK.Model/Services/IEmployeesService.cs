using PSK.Model.DTO;
using System.Collections.Generic;

namespace PSK.Model.Services
{
    public interface IEmployeesService
    {
        List<Employee> Get();
        Employee Get(int id);
        List<Employee> GetSubordinates(int employeeId);
    }
}
