using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;

namespace PSK.Model.DBConnection
{
    public interface IDBConnection
    {
        public Employee GetEmployeeById(int id);
        public Employee Login(String email, String password);
        public void CreateEmployee(String name, String email, String password, int leaderId, Employee leader);
        public void CreateTopic(String name, String description);
        public void CreateSubTopic(String name, String description, int parentTopicId);
        public Topic GetTopicById(int id);
        public List<Topic> GetAllTopics();
        public List<Topic> GetSubTopics(int parentTopicId);
        public void CreateIncomingEmployee(string email, string token, int leaderId, Employee leader);
        public IncomingEmployee GetIncomingEmployeeByToken(string token);
        public void DeleteIncomingEmployee(IncomingEmployee emp);
    }
}
