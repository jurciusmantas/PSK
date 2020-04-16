﻿using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSK.Model.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private List<Topic> _topicTree;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
            var bTopicList = _topicRepository.GetTopics();
            _topicTree = ConvertToTree(bTopicList);
        }

        public ServerResult<List<Topic>> GetTopics()
        {

            return new ServerResult<List<Topic>> { Data = _topicTree, Message = "Success", Success = true };
        }

        public ServerResult<Topic> GetTopic(int id)
        {

            var topic = _topicTree.Where(top => top.Id == id).FirstOrDefault();

            return new ServerResult<Topic> { Data = topic, Message = "Success", Success = true };
        }

        public ServerResult<List<BusinessEntities.Topic>> GetSubtopics(int topicId)
        {
            var topicList = _topicRepository.GetSubtopics(topicId);

            return new ServerResult<List<BusinessEntities.Topic>> { Data = topicList, Message = "Success", Success = true };
        }

        private List<Topic> ConvertToTree(List<BusinessEntities.Topic> topicList)
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

    }
}
