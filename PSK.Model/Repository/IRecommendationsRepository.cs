using PSK.Model.BusinessEntities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IRecommendationsRepository : IRepository<Recommendation>
    {
        // maybe move to IRepository?
        List<Recommendation> Get();

        List<Recommendation> GetReceivedRecommendations(int receiverId);
    }
}
