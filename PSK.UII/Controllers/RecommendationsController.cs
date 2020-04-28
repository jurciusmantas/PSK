using Microsoft.AspNetCore.Mvc;
using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
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
        public ServerResult<List<Recommendation>> Recommendations([FromQuery] int? receiverId)
        {
            return receiverId == null
                ? _recService.GetRecommendations()
                : _recService.GetReceivedRecommendations((int)receiverId);
        }

        [HttpGet("{id}")]
        public ServerResult<Recommendation> Recommendation([FromRoute] int id)
        {
            return _recService.GetRecommendation(id);
        }

    }
}