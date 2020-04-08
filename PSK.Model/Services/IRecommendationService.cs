using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Services
{
    public interface IRecommendationService
    {
        //--temp
        public ServerResult<List<BusinessEntities.Topic>> GetTopics();
        //----

        public ServerResult AddRecommendation(RecommendationArgs args);
        public ServerResult<List<Recommendation>> GetRecommendations(int recommendedToId);
        public ServerResult<List<Recommendation>> GetCreatedRecommendations(int createdById);
        public ServerResult<Recommendation> GetRecommendation(int id);
        public ServerResult ChangeRecommendation(int id, RecommendationArgs args);
        public ServerResult DeleteRecommendation(int id);

    }
}
