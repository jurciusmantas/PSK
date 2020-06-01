using PSK.Model.DTO;
using System.Collections.Generic;

namespace PSK.Model.IServices
{
    public interface IEmployeesService
    {
        List<Employee> Get();
        Employee Get(int id);
        List<Employee> GetSubordinates(int employeeId);
        EmployeeProfile GetProfile(int id);
        ServerResult<EmployeeArgs> UpdateEmployee(EmployeeArgs emplyee);
    }
}
