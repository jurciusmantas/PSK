using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Services
{
    public interface IRecommendationService
    {
        ServerResult AddRecommendation(RecommendationArgs args);
        ServerResult<List<Recommendation>> GetRecommendations(int recommendedToId);
        ServerResult<List<Recommendation>> GetCreatedRecommendations(int createdById);
        ServerResult<Recommendation> GetRecommendation(int id);
        ServerResult ChangeRecommendation(int id, RecommendationArgs args);
        ServerResult DeleteRecommendation(int id);
    }
}
