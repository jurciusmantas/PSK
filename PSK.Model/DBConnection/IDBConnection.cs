using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;

namespace PSK.Model.DBConnection
{
    public interface IDBConnection
    {
        public Employee GetEmployeeById(int id);
        public Employee Login(String email, String password);
        public void CreateEmployee(String name, String email, String password, int leaderId);
        public void CreateTopic(String name, String description);
        public void CreateSubTopic(String name, String description, int parentTopicId);
        public Topic GetTopicById(int id);
        public List<Topic> GetAllTopics();
        public List<Topic> GetSubTopics(int parentTopicId);
        public Employee GetEmployeeByName(string name);
        public void CreateRecommendation(int topicId, int recommendedToId, int createdById);
        public List<Recommendation> GetAllRecommendations();
        public Recommendation GetRecommendationById(int id);
        public void UpdateRecommendation(int id, int topicId, int recommendedToId);
        public void DeleteRecommendation(int id);
    }
}
