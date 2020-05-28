using PSK.Model.DTO;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSK.Model.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public ServerResult<List<Topic>> GetTopics()
        {
            var topicTree = ConvertToTree(_topicRepository.Get());

            return new ServerResult<List<Topic>> { Data = topicTree, Message = "Success", Success = true };
        }

        public ServerResult<Topic> GetTopic(int id)
        {
            var bTopic = _topicRepository.Get(id);
            var bSubtopic = _topicRepository.GetSubtopics(id);

            var subtopics = new List<Topic>();

            foreach (var top in bSubtopic )
            {
                var subtop = new Topic { Id = top.Id, Description = top.Description, Name = top.Name };
                subtopics.Add(subtop);
            }

            var topic = new Topic { Id = bTopic.Id, Description = bTopic.Description, Name = bTopic.Name, SubTopicList = subtopics };

            return new ServerResult<Topic> { Data = topic, Message = "Success", Success = true };
        }

        private List<Topic> ConvertToTree(List<Entities.Topic> topicList)
        {
            var topics = new List<Topic>();

            foreach (var item in topicList)
            {
                var topic = new Topic { Id = item.Id, Description = item.Description, Name = item.Name, ParentId = item.ParentTopicId };
                topics.Add(topic);
            }

            foreach (var topic in topics)
            {
                topic.SubTopicList = topics.Where(x => x.ParentId.HasValue && x.ParentId == topic.Id).ToList();
            }

            var tree = topics.Where(x => !x.ParentId.HasValue).ToList();

            return tree;
        }

        public ServerResult CreateTopic(Topic args)
        {
            if (args == null)
                return new ServerResult
                {
                    Success = false,
                    Message = "No arguments"
                };

            if (string.IsNullOrEmpty(args.Name))
                return new ServerResult
                {
                    Success = false,
                    Message = "No name"
                };

            var newTopic = new Entities.Topic { Name = args.Name, Description = args.Description};

            if (args.ParentId.HasValue)
            {
                var parentTopic = _topicRepository.Get(args.ParentId.Value);

                if (parentTopic == null)
                    return new ServerResult 
                    { 
                        Message = "Parent topic does not exist", 
                        Success = false 
                    };

                newTopic.ParentTopic = parentTopic;
                newTopic.ParentTopicId = args.ParentId.Value;
            }

            _topicRepository.Add(newTopic);

            return new ServerResult { Success = true };
        }
    }
}
