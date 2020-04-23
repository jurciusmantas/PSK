using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee Login(LoginArgs loginArgs);
        public Employee FindByName(string name);
    }
}
