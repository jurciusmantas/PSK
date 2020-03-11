using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.DBConnection
{
    public interface DBConnection
    {
        public Employee GetEmployeeById(int id);
        public Employee Login(String Email, String Password);
        public void CreateEmployee(String Name, String email, String password, int LeaderId);
        public void CreateTopic(String Name, String Description);
        public void CreateSubTopic(String Name, String Description, int ParentTopicId);
        public Topic GetTopicById(int id);
        public List<Topic> GetAllTopics();
        public List<Topic> GetSubTopics(int ParentTopicId);
    }
}
