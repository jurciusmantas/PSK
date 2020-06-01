using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;

namespace PSK.DB.MockRepository
{
    public class TopicCompletionMockRepository : ITopicCompletionRepository
    {
        public TopicCompletion Add(TopicCompletion entity)
        {
            throw new NotImplementedException();
        }

        public TopicCompletion Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TopicCompletion Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<TopicCompletion> Get()
        {
            throw new NotImplementedException();
        }

        public List<TopicCompletion> GetEmployeesCompletions(int employeeId)
        {
            throw new NotImplementedException();
        }

        public List<TopicCompletion> GetEmployeesTopicCompletions(List<int> employeesIds, int topicId)
        {
            throw new NotImplementedException();
        }

        public TopicCompletion Update(TopicCompletion entity)
        {
            throw new NotImplementedException();
        }
    }
}
