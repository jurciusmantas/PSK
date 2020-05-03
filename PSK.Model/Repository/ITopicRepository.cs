using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface ITopicRepository : IRepository<Topic>
    {
        List<Topic> GetTopics();

        List<Topic> GetSubtopics(int id);
    }
}
