using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSK.Model.Services
{
    public class TopicService : ITopicService
    {
        private ITopicRepository _topicRepository;

        private List<Topic> _topicList;

        public TopicService()
        {
            var list = new List<Topic>();   //just mock

            list.Add(new Topic { Id = 1, Name = "TopicName1", Description = "TopicDescription1" });
            list.Add(new Topic { Id = 2, Name = "TopicName2", Description = "TopicDescription2" });
            list.Add(new Topic { Id = 3, Name = "TopicName3", Description = "TopicDescription3" });

            int i = 3;

            foreach (var topic in list)
            {
                var subtopicList = new List<Topic>();

                subtopicList.Add(new Topic { Id = ++i, Name = $"SubTopicName{i}", Description = $"SubTopicDescription{i}" });
                subtopicList.Add(new Topic { Id = ++i, Name = $"SubTopicName{i}", Description = $"SubTopicDescription{i}" });
                subtopicList.Add(new Topic { Id = ++i, Name = $"SubTopicName{i}", Description = $"SubTopicDescription{i}" });

                topic.SubTopicList = subtopicList;
            }

            _topicList = list;
        }

        public ServerResult<List<Topic>> GetTopics()
        {
            return new ServerResult<List<Topic>> { Data = _topicList, Message = "Success", Success = true };
        }

        public ServerResult<Topic> GetTopic(int id)
        {
            var topic = _topicList.Where(top => top.Id == id).FirstOrDefault();

            return new ServerResult<Topic> { Data = topic, Message = "Success", Success = true };
        }
    }
}
