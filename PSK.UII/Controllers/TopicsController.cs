using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.IServices;
using System.Collections.Generic;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
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

        [HttpPut("{id}")]
        public ServerResult<Topic> UpdateTopic([FromRoute(Name = "id")] int id, [FromBody] Topic topic)
        {
            return _topicService.UpdateTopic(topic);
        }

        [HttpPost]
        [Route("completed")]
        public ServerResult MarkAsCompleted([FromBody] TopicCompletion args)
        {
            return _topicService.MarkAsCompleted(args);
        }
    }
}