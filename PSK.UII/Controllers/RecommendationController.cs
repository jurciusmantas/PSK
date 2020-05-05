using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PSK.Model.Entities;
using PSK.Model.Services;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class RecommendationsController : Controller
    {
        private readonly IRecommendationService _recommendationService;
        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public ServerResult<List<Recommendation>> GetRecommendations([FromQuery(Name = "to")] int? recommendedTo, [FromQuery(Name = "by")] int? createdBy)
        {
            if(recommendedTo != null)
                return _recommendationService.GetRecommendationsForEmployee((int)recommendedTo);
            if (createdBy != null)
                return _recommendationService.GetRecommendationsByEmployee((int)createdBy);
            return _recommendationService.GetRecommendations();
        }

        [HttpGet, Route("{id}")]
        public ServerResult<Recommendation> GetRecommendation([FromRoute] int id)
        {
            return _recommendationService.GetRecommendation(id);
        }

        [HttpPost]
        public ServerResult AddRecommendation([FromBody] RecommendationArgs args)
        {
            return _recommendationService.AddRecommendation(args);
        }

        [HttpPut, Route("{id}")]
        public ServerResult ChangeRecommendation([FromRoute] int id, [FromBody] RecommendationArgs args)
        {
            return _recommendationService.ChangeRecommendation(id, args);
        }

        [HttpDelete, Route("{id}")]
        public ServerResult DeleteRecommendation([FromRoute] int id)
        {
            return _recommendationService.DeleteRecommendation(id);
        }
    }
}