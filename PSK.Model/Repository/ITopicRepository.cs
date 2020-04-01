using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface ITopicRepository
    {
        public Topic Add(Topic topic);
        public Topic GetTopic(int id);
        public Topic UpdateTopic(Topic updatedTopic);
        public Topic Delete(int id);
    }
}
