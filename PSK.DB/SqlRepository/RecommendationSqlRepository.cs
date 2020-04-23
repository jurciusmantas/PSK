using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Recommendation Get(int id)
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

        public IEnumerable<Recommendation> FindRecommended(int receiverId)
        {
            return context.Recommendations.Where(recommendation => recommendation.ReceiverId == receiverId);
        }

        public IEnumerable<Recommendation> FindCreated(int creatorId)
        {
            return context.Recommendations.Where(recommendation => recommendation.CreatorId == creatorId);
        }
    }
}
