using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IRecommendationRepository
    {
        public Recommendation Add(Recommendation recommendation);
        public Recommendation GetRecommendation(int id);
        public Recommendation Update(Recommendation updatedRecommendation);
        public Recommendation Delete(int id);
    }
}
