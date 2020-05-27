using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.Services;
using System.Collections.Generic;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationsService _recService;

        public RecommendationsController(IRecommendationsService recService)
        {
            _recService = recService;
        }

        [HttpGet]
        public ServerResult<List<Recommendation>> Recommendations([FromQuery(Name = "to")] int? receiverId, [FromQuery(Name = "by")] int? creatorId)
        {
            if (receiverId != null)
                return _recService.GetReceivedRecommendations((int)receiverId);
            if (creatorId != null)
                return _recService.GetCreatedRecommendations((int)creatorId);
            return _recService.GetRecommendations();
        }

        [HttpGet("{id}")]
        public ServerResult<Recommendation> Recommendation([FromRoute] int id)
        {
            return _recService.GetRecommendation(id);
        }

        [HttpPost]
        public ServerResult CreateRecommendation([FromBody] Recommendation recommendation)
        {
            return _recService.CreateRecommendation(recommendation);
        }

        [HttpPut("{id}")]
        public ServerResult UpdateRecommendation([FromRoute(Name = "id")] int id, [FromBody] Recommendation rec)
        {
            rec.Id = id;
            return _recService.UpdateRecommendation(rec);
        }

        [HttpDelete("{id}")]
        public ServerResult DeleteRecommendation([FromRoute(Name = "id")] int id)
        {
            return _recService.DeleteRecommendation(id);
        }

    }
}