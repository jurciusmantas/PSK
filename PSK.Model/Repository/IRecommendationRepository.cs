using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IRecommendationRepository : IRepository<Recommendation>
    {
        public IEnumerable<Recommendation> FindRecommended(int receiverId);
        public IEnumerable<Recommendation> FindCreated(int creatorId);
    }
}
