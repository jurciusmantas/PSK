using PSK.Model.Entities;
using System;
using System.Collections.Generic;

namespace PSK.Model.Services
{
    public class TopicService : ITopicService
    {
        List<Topic> list = new List<Topic>();

        public TopicService()
        {
            list.Add(new Topic { Id = 0, Name = "TopicName1", Description = "TopicDescription1" });
            list.Add(new Topic { Id = 2, Name = "TopicName2", Description = "TopicDescription2" });
            list.Add(new Topic { Id = 42069, Name = "TopicName3", Description = "TopicDescription3" });
        }
        public ServerResult<Topic> GetTopic(int id)
        {
            Topic topic = list.Find(t => t.Id == id);
            return new ServerResult<Topic>() { Success = topic != null, Data = topic };
        }

        public ServerResult<List<Topic>> GetTopics()
        {
            return new ServerResult<List<Topic>> { Data = list, Success = true };
        }
    }
}
