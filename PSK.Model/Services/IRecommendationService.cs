using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Services
{
    public interface IRecommendationService
    {
        ServerResult AddRecommendation(RecommendationArgs args);
        ServerResult<List<Recommendation>> GetRecommendations();
        ServerResult<List<Recommendation>> GetRecommendationsForEmployee(int recommendedToId);
        ServerResult<List<Recommendation>> GetRecommendationsByEmployee(int createdById);
        ServerResult<Recommendation> GetRecommendation(int id);
        ServerResult ChangeRecommendation(int id, RecommendationArgs args);
        ServerResult DeleteRecommendation(int id);
    }
}
