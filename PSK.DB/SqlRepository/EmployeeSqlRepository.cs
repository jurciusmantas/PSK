using PSK.DB.Contexts;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public Employee Login(Model.DTO.LoginArgs loginArgs)
        {
            return context.Employees.FirstOrDefault(employee => employee.Email == loginArgs.Login);
        }

        public Employee Update(Employee updatedEmployee)
        {
            var employee = context.Employees.Attach(updatedEmployee);
            employee.State = EntityState.Modified;
            context.SaveChanges();
            return updatedEmployee;
        }

        public Employee FindByName(string name)
        {
            return context.Employees.FirstOrDefault(employee => employee.Name.Equals(name));
        }

        public List<Employee> GetSubordinates(int employeeId)
        {
            return context.Employees.Where(e => e.LeaderId == employeeId).ToList();
        }

        public List<Employee> GetAllSubordinates(int leaderId)
        {
            List<Employee> employees = new List<Employee>();
            var teamMembers = GetSubordinates(leaderId);
            employees.AddRange(teamMembers);
            foreach (Employee employee in teamMembers)
            {
                List<Employee> allLower = GetAllSubordinates(employee.Id) ?? new List<Employee>();
                if (allLower.Count > 0) { employees.AddRange(allLower); }
            } 
            return employees;
        }

        public List<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public List<Topic> GetEmployeesActiveTopics(int employeeId)
        {
            return context.Topics.FromSqlRaw(@"
                SELECT t.Id, t.Name, t.Description, t.ParentTopicId, t.RowVersion
                FROM days AS d
                INNER JOIN employees AS e ON d.EmployeeId = e.Id
                INNER JOIN topics AS t ON d.TopicId = t.Id
                LEFT JOIN topiccompletions AS tc ON t.id = tc.`TopicId`
                WHERE 
                    e.Id = {0} AND tc.id IS NULL
                GROUP BY t.Id;
            ", employeeId).ToList();
        }

        public bool CheckIfEmailExists(string email)
        {
            if (context.Employees.FirstOrDefault(employee => employee.Email.Equals(email)) != null)
                return true;
            return false;
        }
    }
}
