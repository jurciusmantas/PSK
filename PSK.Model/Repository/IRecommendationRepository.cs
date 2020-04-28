using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IRecommendationRepository : IRepository<Recommendation>
    {
        public List<Recommendation> FindRecommended(int receiverId);
        public List<Recommendation> FindCreated(int creatorId);
    }
}
