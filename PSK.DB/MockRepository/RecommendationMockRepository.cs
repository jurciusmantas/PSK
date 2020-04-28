using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSK.DB.MockRepository
{
    public class RecommendationsMockRepository : IRecommendationsRepository
    {
        private readonly List<Recommendation> _recs = new List<Recommendation>()
        {
            new Recommendation() { Id = 1, CreatorId = 2, ReceiverId = 3, TopicId = 1},
            new Recommendation() { Id = 2, CreatorId = 2, ReceiverId = 3, TopicId = 2},
            new Recommendation() { Id = 3, CreatorId = 1, ReceiverId = 3, TopicId = 42069},
            new Recommendation() { Id = 4, CreatorId = 1, ReceiverId = 2, TopicId = 1}
        };
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
            return _recs.FirstOrDefault(r => r.Id == id);
        }

        public List<Recommendation> Get()
        {
            return _recs;
        }

        public List<Recommendation> GetReceivedRecommendations(int receiverId)
        {
            return _recs.Where(r => r.ReceiverId == receiverId).ToList();
        }

        public Recommendation Update(Recommendation updatedRecommendation)
        {
            throw new NotImplementedException();
        }
    }
}
