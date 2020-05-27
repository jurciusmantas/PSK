using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IRecommendationsRepository : IRepository<Recommendation>
    {
        List<Recommendation> GetReceivedRecommendations(int receiverId);
        List<Recommendation> GetCreatedRecommendations(int creatorId);
    }
}
