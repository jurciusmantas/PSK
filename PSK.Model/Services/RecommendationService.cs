using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSK.Model.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITopicRepository _topicRepository;

        public RecommendationService(IRecommendationRepository recommendationRepository, 
            IEmployeeRepository employeeRepository,
            ITopicRepository topicRepository)
        {
            _recommendationRepository = recommendationRepository;
            _employeeRepository = employeeRepository;
            _topicRepository = topicRepository;
        }

        public ServerResult<List<Recommendation>> GetRecommendations(int recommendedToId)
        {
            try
            {
                List<Recommendation> recommendations = new List<Recommendation>();
                BusinessEntities.Topic topic;
                List<BusinessEntities.Recommendation> recFromDb = _recommendationRepository.FindRecommended(recommendedToId).ToList();
                foreach (var r in recFromDb)
                {
                    topic = _topicRepository.Get(r.TopicId);
                    recommendations.Add(new Recommendation
                    {
                        Id = r.Id,
                        TopicName = topic.Name,
                        TopicId = topic.Id,
                        CreatorName = _employeeRepository.Get(r.CreatorId).Name
                    });

                }

                return new ServerResult<List<Recommendation>>
                {
                    Data = recommendations,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServerResult<List<Recommendation>>
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult<List<Recommendation>> GetCreatedRecommendations(int createdById)
        {
            try
            {
                List<Recommendation> recommendations = new List<Recommendation>();
                List<BusinessEntities.Recommendation> recFromDb = _recommendationRepository.FindCreated(createdById).ToList();
                foreach (var r in recFromDb)
                {
                    recommendations.Add(new Recommendation
                    {
                        Id = r.Id,
                        TopicName = _topicRepository.Get(r.TopicId).Name,
                        ReceiverName = _employeeRepository.Get(r.ReceiverId).Name,
                        CreatorName = _employeeRepository.Get(r.CreatorId).Name
                    });
                }

                return new ServerResult<List<Recommendation>>
                {
                    Data = recommendations,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServerResult<List<Recommendation>>
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult<Recommendation> GetRecommendation(int id)
        {
            try
            {
                BusinessEntities.Recommendation recommendation = _recommendationRepository.Get(id);

                return new ServerResult<Recommendation>
                {
                    Data = new Recommendation
                    {
                        Id = recommendation.Id,
                        TopicId = recommendation.TopicId,
                        TopicName = _topicRepository.Get(recommendation.TopicId).Name,
                        ReceiverName = _employeeRepository.Get(recommendation.ReceiverId).Name
                    },
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServerResult<Recommendation>
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult AddRecommendation(RecommendationArgs args)
        {
            int recommendedToId = FindEmployee(args.RecommendedTo);

            if (recommendedToId == -1)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = "Employee does not exist"
                };
            }

            if (CheckIfAbleToAssin(args.CreatedById, recommendedToId) == false)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = "You are not allowed to assign recommendation to this employee.\n" +
                    "You can assign recommendations only to yourself and your team."
                };
            }

            BusinessEntities.Recommendation newRecommendation = new BusinessEntities.Recommendation();

            try
            {
                _recommendationRepository.Add(new BusinessEntities.Recommendation { TopicId = args.TopicId, ReceiverId = recommendedToId, CreatorId = args.CreatedById });

                return new ServerResult
                {
                    Success = true,
                    Message = "Recommendation added successfully"
                };
            }
            catch (Exception e)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult ChangeRecommendation(int id, RecommendationArgs args)
        {
            int recommendedToId = FindEmployee(args.RecommendedTo);
            if (recommendedToId == -1)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = "Employee does not exist"
                };
            }

            if (CheckIfAbleToAssin(args.CreatedById, recommendedToId) == false)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = "You are not allowed to assign recommendation to this employee.\n" +
                    "You can assign recommendations only to yourself and your team."
                };
            }

            try
            {
                BusinessEntities.Recommendation recommendationToUpdate = _recommendationRepository.Get(id);
                recommendationToUpdate.TopicId = args.TopicId;
                recommendationToUpdate.ReceiverId = recommendedToId;
                _recommendationRepository.Update(recommendationToUpdate);

                return new ServerResult
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServerResult
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
                _recommendationRepository.Delete(id);
                return new ServerResult
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        private int FindEmployee(string name)
        {
            try
            {
                return _employeeRepository.FindByName(name).Id;
            }
            catch
            {
                return -1;
            }
        }

        private bool CheckIfAbleToAssin(int createdById, int recommendedToId)
        {
            try
            {
                if ((_employeeRepository.Get(recommendedToId).LeaderId == createdById) || createdById == recommendedToId)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

    }
}