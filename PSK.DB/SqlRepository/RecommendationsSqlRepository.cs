using PSK.DB.Contexts;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PSK.DB.SqlRepository
{
    public class RecommendationsSqlRepository : IRecommendationsRepository
    {
        private readonly PSKDbContext context;

        public RecommendationsSqlRepository(PSKDbContext context)
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

        public List<Recommendation> Get()
        {
            return context.Recommendations.ToList();
        }

        public List<Recommendation> GetReceivedRecommendations(int userId)
        {
            return context.Recommendations.Where(rec => rec.ReceiverId == userId).ToList();
        }

        public Recommendation Update(Recommendation updatedRecommendation)
        {
            var recommendation = context.Recommendations.Attach(updatedRecommendation);
            recommendation.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedRecommendation;
        }

        public List<Recommendation> GetCreatedRecommendations(int creatorId)
        {
            return context.Recommendations.Where(recommendation => recommendation.CreatorId == creatorId).ToList();
        }
    }
}
