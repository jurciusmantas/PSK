using PSK.Model.DTO;
using System.Collections.Generic;

namespace PSK.Model.IServices
{
    public interface ITopicService
    {
        ServerResult<List<Topic>> GetTopics();
        ServerResult<Topic> GetTopic(int id);
        ServerResult CreateTopic(Topic args);
        ServerResult<Topic> UpdateTopic(Topic topic);
        ServerResult MarkAsCompleted(TopicCompletion args);
    }
}
