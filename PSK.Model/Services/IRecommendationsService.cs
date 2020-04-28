using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Services
{
    public interface IRecommendationsService
    {
        ServerResult<List<Recommendation>> GetRecommendations();
        ServerResult<List<Recommendation>> GetReceivedRecommendations(int userId);
        ServerResult<Recommendation> GetRecommendation(int id);
        ServerResult CreateRecommendation(Recommendation recommendation);
    }
}
