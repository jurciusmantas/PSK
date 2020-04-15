using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

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

        public Topic Update(Topic updatedTopic)
        {
            throw new NotImplementedException();
        }
    }
}
