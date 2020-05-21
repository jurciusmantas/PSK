﻿using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.Services;
using System.Collections.Generic;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class TopicsController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public ServerResult<List<Topic>> Topics()
        {
            return _topicService.GetTopics();
        }

        [HttpGet]
        [Route("{id}")]
        public ServerResult<Topic> GetDetailedTopic([FromRoute]int id)
        {
            return _topicService.GetTopic(id);
        }

        [HttpPost]
        public ServerResult CreateTopic([FromBody]Topic args)
        {
            return _topicService.CreateTopic(args);
        }
    }
}