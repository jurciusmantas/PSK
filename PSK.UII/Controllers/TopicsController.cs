using Microsoft.AspNetCore.Mvc;
using PSK.Model.Entities;
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

        [HttpGet("{id}")]
        public ServerResult<Topic> Topic(int id)
        {
            return _topicService.GetTopic(id);
        }
    }
}