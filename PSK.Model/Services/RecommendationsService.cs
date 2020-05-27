using PSK.Model.DTO;
using PSK.Model.Repository;
using System;
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
            try
            {
                _recRep.Add(DTOToEntity(recommendation));
                return new ServerResult() { Success = true };
            }
            catch (Exception e)
            {
                return new ServerResult() { Success = false, Message = e.Message };
            }

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
            try
            {
                return new ServerResult<List<Recommendation>>()
                {
                    Success = true,
                    Data = _recRep.GetCreatedRecommendations(creatorId).Select(r => EntityToDTO(r)).ToList()
                };
            }
            catch (Exception e)
            {
                return new ServerResult<List<Recommendation>>()
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult UpdateRecommendation(Recommendation rec)
        {
            try
            {
                Entities.Recommendation dbRec = _recRep.Get(rec.Id);
                if (dbRec == null)
                    return new ServerResult()
                    {
                        Success = false,
                        Message = "Recommendation does not exist"
                    };
                dbRec.TopicId = rec.TopicId;
                dbRec.ReceiverId = rec.ReceiverId;
                dbRec.CreatorId = rec.CreatorId;
                _recRep.Update(dbRec);
                return new ServerResult() { Success = true };
            }
            catch (Exception e)
            {
                return new ServerResult()
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult DeleteRecommendation(int id)
        {
            try
            {
                _recRep.Delete(id);
                return new ServerResult() { Success = true };
            }
            catch (Exception e)
            {
                return new ServerResult() { Success = false, Message = e.Message };
            }
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
