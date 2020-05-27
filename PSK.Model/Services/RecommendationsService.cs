using PSK.Model.DTO;
using PSK.Model.Repository;
using System.Collections.Generic;
using System.Linq;

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
            _recRep.Add(DTOToEntity(recommendation));
            return new ServerResult() { Success = true };
        }

        public ServerResult<Recommendation> GetRecommendation(int id)
        {
            Entities.Recommendation dbRec = _recRep.Get(id);
            if (dbRec != null)
                return new ServerResult<Recommendation>() { Success = true, Data = EntityToDTO(dbRec) };
            return new ServerResult<Recommendation>() { Success = false, Message = "Recommendation not found" };
        }

        public ServerResult<List<Recommendation>> GetRecommendations()
        {
            return new ServerResult<List<Recommendation>>() { Success = true, Data = _recRep.Get().Select(r => EntityToDTO(r)).ToList() };
        }

        public ServerResult<List<Recommendation>> GetReceivedRecommendations(int receiverId)
        {
            return new ServerResult<List<Recommendation>>()
            {
                Success = true,
                Data = _recRep.GetReceivedRecommendations(receiverId)
                              .Select(r => EntityToDTO(r)).ToList()
            };
        }

        public ServerResult<List<Recommendation>> GetCreatedRecommendations(int creatorId)
        {
            return new ServerResult<List<Recommendation>>()
            {
                Success = true,
                Data = _recRep.GetCreatedRecommendations(creatorId).Select(r => EntityToDTO(r)).ToList()
            };
        }

        private Recommendation EntityToDTO(Entities.Recommendation entity)
        {
            return new Recommendation()
            {
                Id = entity.Id,
                ReceiverId = entity.ReceiverId,
                CreatorId = entity.CreatorId,
                TopicId = entity.TopicId,
                CreatorName = entity.Creator?.Name,
                ReceiverName = entity.Receiver?.Name,
                TopicName = entity.Topic?.Name
            };
        }

        private Entities.Recommendation DTOToEntity(Recommendation dto)
        {
            return new Entities.Recommendation()
            {
                Id = dto.Id,
                CreatorId = dto.CreatorId,
                ReceiverId = dto.ReceiverId,
                TopicId = dto.TopicId
            };
        }
    }
}
