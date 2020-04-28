using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;

namespace PSK.DB.MockRepository
{
    public class TopicMockRepository : ITopicRepository
    {
        public Topic Add(Topic topic)
        {
            throw new NotImplementedException();
        }

        public Topic Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Topic Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Topic> Get()
        {
            throw new NotImplementedException();
        }

        public List<Topic> GetSubtopics(int id)
        {
            throw new NotImplementedException();
        }

        public List<Topic> GetTopics()
        {
            throw new NotImplementedException();
        }

        public Topic Update(Topic updatedTopic)
        {
            throw new NotImplementedException();
        }
    }
}
