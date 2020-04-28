using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Services
{
    class RecommendationsService : IRecommendationsService
    {
        private readonly IRecommendationsRepository _recRep;

        public RecommendationsService(IRecommendationsRepository rep)
        {
            _recRep = rep;
        }

        public ServerResult CreateRecommendation(Recommendation recommendation)
        {
            _recRep.Add(recommendation);
            return new ServerResult() { Success = true };
        }

        public ServerResult<Recommendation> GetRecommendation(int id)
        {
            return new ServerResult<Recommendation>() { Success = true, Data = _recRep.Get(id) };
        }

        public ServerResult<List<Recommendation>> GetRecommendations()
        {
            return new ServerResult<List<Recommendation>>() { Success = true, Data = _recRep.Get() };
        }

        public ServerResult<List<Recommendation>> GetReceivedRecommendations(int receiverId)
        {
            return new ServerResult<List<Recommendation>>() { Success = true, Data = _recRep.GetReceivedRecommendations(receiverId) };
        }
    }
}
