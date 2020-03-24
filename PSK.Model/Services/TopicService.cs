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

            list.Add(new Topic { Name = "TopicName1", Description = "TopicDescription1" });
            list.Add(new Topic { Name = "TopicName2", Description = "TopicDescription2" });
            list.Add(new Topic { Name = "TopicName3", Description = "TopicDescription3" });

            return new ServerResult<List<Topic>> { Data = list, Message = "Success", Success = true };
        }

        public ServerResult<Topic> GetTopic(int id)
        {
            var list = new List<Topic>();

            list.Add(new Topic { Name = "SubTopicName1", Description = "SubTopicDescription1" });
            list.Add(new Topic { Name = "SubTopicName2", Description = "SubTopicDescription2" });
            list.Add(new Topic { Name = "SubTopicName3", Description = "SubTopicDescription3" });

            var topic = new Topic();
            topic.Name = "TopicName" + (id + 1).ToString();
            topic.Description = "TopicDescription" + (id + 1).ToString();
            topic.SubTopicList = list;

            return new ServerResult<Topic> { Data = topic, Message = "Success", Success = true };
        }
    }
}
