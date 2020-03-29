﻿using PSK.Model.BusinessEntities;
using System.Collections.Generic;
using System.Linq;

namespace PSK.Model.DBConnection
{
    public class MockDBConnection : IDBConnection
    {
        private static readonly List<Employee> _employees = new List<Employee>();
        private static readonly List<Topic> _topics = new List<Topic>();

        public void CreateEmployee(string name, string email, string password, int leaderId)
        {
            Employee leader = GetEmployeeById(leaderId);
            Employee employee = new Employee { Id = _employees.Count, Name = name, Email = email, Password = password, Leader = leader };
            _employees.Add(employee);
        }

        public void CreateSubTopic(string name, string description, int parentTopicId)
        {
            Topic parentTopic = GetTopicById(parentTopicId);
            Topic subTopic = new Topic { Id = _topics.Count, Name = name, Description = description, ParentTopic = parentTopic };
            _topics.Add(subTopic);
        }

        public void CreateTopic(string name, string description)
        {
            Topic topic = new Topic { Id = _topics.Count, Name = name, Description = description };
            _topics.Add(topic);
        }

        public List<Topic> GetAllTopics()
        {
            return _topics;
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(employee => employee.Id == id);
        }

        public List<Topic> GetSubTopics(int parentTopicId)
        {
            return _topics.FindAll(topic => topic.ParentTopic.Id == parentTopicId);
        }

        public Topic GetTopicById(int id)
        {
            return _topics.FirstOrDefault(topic => topic.Id == id);
        }

        public Employee Login(string email, string password)
        {
            return _employees.FirstOrDefault(employee => employee.Email == email && employee.Password == password);
        }
    }
}