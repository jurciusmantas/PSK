using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IRecommendationRepository : IRepository<Recommendation>
    {
        public List<Recommendation> FindRecommended(int receiverId);
        public List<Recommendation> FindCreated(int creatorId);
    }
}
