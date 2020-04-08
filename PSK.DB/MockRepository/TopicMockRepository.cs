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

        public Topic GetTopic(int id)
        {
            throw new NotImplementedException();
        }

        public Topic UpdateTopic(Topic updatedTopic)
        {
            throw new NotImplementedException();
        }
    }
}
