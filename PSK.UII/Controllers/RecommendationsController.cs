using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.Services;
using System.Collections.Generic;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class RecommendationsController : Controller
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

    }
}