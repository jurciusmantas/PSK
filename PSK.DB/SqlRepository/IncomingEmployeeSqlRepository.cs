using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
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

        public IncomingEmployee GetIncomingEmployee(int id)
        {
            return context.IncomingEmployees.Find(id);
        }

        public IncomingEmployee Update(IncomingEmployee updatedIncomingEmployee)
        {
            var incomingEmployee = context.Attach(updatedIncomingEmployee);
            incomingEmployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedIncomingEmployee;
        }
    }
}
