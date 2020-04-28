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

        public ServerResult<Topic> CreateTopic(Topic args)
        {
            return new ServerResult<Topic> { Data = null, Message = "success", Success = true };
        }
    }
}
