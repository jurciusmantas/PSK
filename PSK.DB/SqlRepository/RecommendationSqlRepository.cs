using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.SqlRepository
{
    public class RecommendationSqlRepository : IRecommendationRepository
    {
        private readonly PSKDbContext context;

        public RecommendationSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }
        public Recommendation Add(Recommendation recommendation)
        {
            context.Recommendations.Add(recommendation);
            context.SaveChanges();
            return recommendation;
        }

        public Recommendation Delete(int id)
        {
            Recommendation recommendation = context.Recommendations.Find(id);
            if(recommendation != null)
            {
                context.Recommendations.Remove(recommendation);
                context.SaveChanges();
            }
            return recommendation;
        }

        public Recommendation GetRecommendation(int id)
        {
            return context.Recommendations.Find(id);
        }

        public Recommendation Update(Recommendation updatedRecommendation)
        {
            var recommendation = context.Recommendations.Attach(updatedRecommendation);
            recommendation.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedRecommendation;
        }
    }
}
