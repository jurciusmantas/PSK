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

        public Recommendation GetRecommendation(int id)
        {
            throw new NotImplementedException();
        }

        public Recommendation Update(Recommendation updatedRecommendation)
        {
            throw new NotImplementedException();
        }
    }
}
