using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSK.DB.SqlRepository
{
    public class IncomingEmployeeSqlRepository : IIncomingEmployeeRepository
    {
        private readonly PSKDbContext context;

        public IncomingEmployeeSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }
        public IncomingEmployee Add(IncomingEmployee incomingEmployee)
        {
            context.IncomingEmployees.Add(incomingEmployee);
            context.SaveChanges();
            return incomingEmployee;
        }

        public IncomingEmployee Delete(int id)
        {
            IncomingEmployee incomingEmployee = context.IncomingEmployees.Find(id);
            if(incomingEmployee != null)
            {
                context.IncomingEmployees.Remove(incomingEmployee);
                context.SaveChanges();
            }
            return incomingEmployee;
        }

        public IncomingEmployee Get(int id)
        {
            return context.IncomingEmployees.Find(id);
        }

        public IncomingEmployee Update(IncomingEmployee updatedIncomingEmployee)
        {
            var incomingEmployee = context.IncomingEmployees.Attach(updatedIncomingEmployee);
            incomingEmployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedIncomingEmployee;
        }

        public IncomingEmployee FindByToken(string token)
        {
            return context.IncomingEmployees.FirstOrDefault(incomingEmployee => incomingEmployee.Token.Equals(token));
        }
    }
}
