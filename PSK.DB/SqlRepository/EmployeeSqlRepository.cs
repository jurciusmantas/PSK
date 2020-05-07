using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.SqlRepository
{
    public class EmployeeSqlRepository : IEmployeeRepository
    {
        private readonly PSKDbContext context;

        public EmployeeSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if(employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public Employee Get(int id)
        {
            return context.Employees.Find(id);
        }

        public Employee Login(LoginArgs loginArgs)
        {
            return context.Employees.FirstOrDefault(employee => employee.Name == loginArgs.Login /*&& employee.Password == loginArgs.Password*/);
        }

        public Employee Update(Employee updatedEmployee)
        {
            var employee = context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedEmployee;
        }

        public Employee FindByName(string name)
        {
            return context.Employees.FirstOrDefault(employee => employee.Name.Equals(name));
        }
    }
}
