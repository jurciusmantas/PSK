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
        public ServerResult<List<Topic>> Topics([FromQuery(Name = "tree")] bool tree = false)
        {
            return _topicService.GetTopics(tree);
        }

        [HttpGet("{id}")]
        public ServerResult<Topic> GetDetailedTopic([FromRoute]int id)
        {
            return _topicService.GetTopic(id);
        }

        [HttpGet("{id}/subtopics")]
        public ServerResult<List<Topic>> GetSubtopics([FromRoute]int id)
        {
            return _topicService.GetSubtopics(id);
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

        [HttpPost("completed")]
        public ServerResult MarkAsCompleted([FromBody] TopicCompletion args)
        {
            return _topicService.MarkAsCompleted(args);
        }

        [HttpGet("{id}/learnedsubordinates")]
        public ServerResult<List<LearnedSubordinatesListItem>> LoadLearnedSubordinates([FromRoute(Name = "id")] int id/*[FromQuery(Name = "employeeId")] int employeeId */)
        {
            int? employeeId = null;
            var currEmployeeIdObject = Request.HttpContext.Items["currentEmployeeId"];
            if (currEmployeeIdObject != null)
                employeeId = int.Parse(currEmployeeIdObject.ToString());

            return _topicService.LoadLearnedSubordinates(employeeId, id);
        }
    }
}