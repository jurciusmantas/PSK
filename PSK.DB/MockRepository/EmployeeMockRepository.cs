using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.MockRepository
{
    public class EmployeeMockRepository : IEmployeeRepository
    {
        private static List<Employee> _employees = new List<Employee>();
        public Employee Add(Employee employee)
        {
            employee.Id = _employees.Count;
            _employees.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employees.Find(assignedTopic => assignedTopic.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
            return employee;
        }

        public Employee Get(int id)
        {
            return _employees.Find(employee => employee.Id == id);
        }

        public Employee Login(LoginArgs loginArgs)
        {
            throw new NotImplementedException();
        }

        public Employee Update(Employee updatedEmployee)
        {
            throw new NotImplementedException();
        }

        public Employee FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Employee> FindTeamMembers(int leaderId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> FindAllLower(int leaderId)
        {
            throw new NotImplementedException();
        }
    }
}
