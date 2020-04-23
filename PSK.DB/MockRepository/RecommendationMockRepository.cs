using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.MockRepository
{
    public class RecommendationMockRepository : IRecommendationRepository
    {
        public Recommendation Add(Recommendation recommendation)
        {
            throw new NotImplementedException();
        }

        public Recommendation Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Recommendation Get(int id)
        {
            throw new NotImplementedException();
        }

        public Recommendation Update(Recommendation updatedRecommendation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recommendation> FindRecommended(int receiverId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recommendation> FindCreated(int creatorId)
        {
            throw new NotImplementedException();
        }
    }
}
