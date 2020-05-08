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

        [HttpGet, Route("recommendations/{recommendedTo}")]
        public ServerResult<List<Recommendation>> GetRecommendations(int recommendedTo)
        {
            return _recommendationService.GetRecommendations(recommendedTo);
        }

        [HttpGet, Route("recommended/{createdBy}")]
        public ServerResult<List<Recommendation>> GetCreatedRecommendations(int createdBy)
        {
            return _recommendationService.GetCreatedRecommendations(createdBy);
        }

        [HttpGet, Route("recommendation/{id}")]
        public ServerResult<Recommendation> GetRecommendation(int id)
        {
            return _recommendationService.GetRecommendation(id);
        }

        [HttpPost]
        public ServerResult AddRecommendation([FromBody]RecommendationArgs args)
        {
            return _recommendationService.AddRecommendation(args);
        }

        [HttpPut, Route("update-recommendation/{id}")]
        public ServerResult ChangeRecommendation(int id, [FromBody]RecommendationArgs args)
        {
            return _recommendationService.ChangeRecommendation(id, args);
        }

        [HttpDelete, Route("delete/{id}")]
        public ServerResult DeleteRecommendation(int id)
        {
            return _recommendationService.DeleteRecommendation(id);
        }
    }
}