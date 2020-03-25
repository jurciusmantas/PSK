using PSK.Model.Entities;
using System;
using System.Collections.Generic;

namespace PSK.Model.Services
{
    public class TopicService : ITopicService
    {
        public ServerResult<List<Topic>> GetTopics()
        {
            var list = new List<Topic>();

            list.Add(new Topic { Id = 1, Name = "TopicName1", Description = "TopicDescription1" });
            list.Add(new Topic { Id = 2, Name = "TopicName2", Description = "TopicDescription2" });
            list.Add(new Topic { Id = 3, Name = "TopicName3", Description = "TopicDescription3" });

            return new ServerResult<List<Topic>> { Data = list, Message = "Success", Success = true };
        }

        public ServerResult<Topic> GetTopic(int id)
        {
            var list = new List<Topic>();

            list.Add(new Topic { Id = 4, Name = "SubTopicName1", Description = "SubTopicDescription1" });
            list.Add(new Topic { Id = 5, Name = "SubTopicName2", Description = "SubTopicDescription2" });
            list.Add(new Topic { Id = 6, Name = "SubTopicName3", Description = "SubTopicDescription3" });

            var topic = new Topic();
            topic.Name = "TopicName" + id.ToString();
            topic.Description = "TopicDescription" + id.ToString();
            topic.SubTopicList = list;

            return new ServerResult<Topic> { Data = topic, Message = "Success", Success = true };
        }
    }
}
