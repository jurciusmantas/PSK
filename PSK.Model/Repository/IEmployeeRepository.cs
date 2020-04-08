using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IEmployeeRepository
    {
        public Employee Add(Employee employee);
        public Employee GetEmployee(int id);
        public Employee Update(Employee updatedEmployee);
        public Employee Delete(int id);
        public Employee Login(LoginArgs loginArgs);
    }
}
