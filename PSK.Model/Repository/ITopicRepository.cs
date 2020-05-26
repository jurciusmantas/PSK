using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface ITopicRepository : IRepository<Topic>
    {
        List<Topic> GetSubtopics(int id);
    }
}
