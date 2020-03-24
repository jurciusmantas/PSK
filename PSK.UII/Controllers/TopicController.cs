using Microsoft.AspNetCore.Mvc;
using PSK.Model.Entities;
using PSK.Model.Services;
using System.Collections.Generic;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        [Route("topic")]
        public ServerResult<List<Topic>> Topic()
        {
            return _topicService.GetTopics();
        }

        [HttpGet]
        [Route("detailedtopic")]
        public ServerResult<Topic> DetailedTopic(int id)
        {
            return _topicService.GetTopic(id);
        }
    }
}