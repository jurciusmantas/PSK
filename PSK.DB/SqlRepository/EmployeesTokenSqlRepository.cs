using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.SqlRepository
{
    public class EmployeesTokenSqlRepository : IEmployeesTokenRepository
    {
        private readonly PSKDbContext context;

        public EmployeesTokenSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }
        public EmployeesToken Add(EmployeesToken employeesToken)
        {
            context.EmployeesTokens.Add(employeesToken);
            context.SaveChanges();
            return employeesToken;
        }

        public EmployeesToken Delete(int id)
        {
            EmployeesToken employeesToken = context.EmployeesTokens.Find(id);
            if(employeesToken != null)
            {
                context.EmployeesTokens.Remove(employeesToken);
                context.SaveChanges();
            }
            return employeesToken;
        }

        public EmployeesToken Get(int id)
        {
            return context.EmployeesTokens.Find(id);
        }

        public EmployeesToken Update(EmployeesToken updatedEmployeesToken)
        {
            var employeesToken = context.EmployeesTokens.Attach(updatedEmployeesToken);
            employeesToken.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedEmployeesToken;
        }
    }
}
