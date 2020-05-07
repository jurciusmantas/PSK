using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IEmployeesTokenRepository : IRepository<EmployeesToken>
    {
        public EmployeesToken FindByToken(string token);
    }
}
