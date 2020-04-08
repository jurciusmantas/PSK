using PSK.Model.BusinessEntities;
using PSK.Model.DBConnection;
using PSK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSK.Model.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IDBConnection _db;
        public RecommendationService(IDBConnection db)
        {
            _db = db;
        }

        //-------------------temporary-------------------------------
        public ServerResult<List<BusinessEntities.Topic>> GetTopics()
        {
            //-----------------TEMPORARY---------------------------
            _db.CreateEmployee("Vardas", "pastas", "slapt", 1);
            _db.CreateEmployee("Name", "mail", "slapt", 1);
            _db.CreateTopic("TopicName1", "Description1");
            _db.CreateSubTopic("SubTopic1_1", "SubTopicDesc1_1", 0);
            _db.CreateTopic("TopicName2", "Description2");
            _db.CreateSubTopic("SubTopic1_2", "SubTopicDesc1_2", 1);
            _db.CreateSubTopic("SubTopic2_2", "SubTopicDesc2_2", 1);
            _db.CreateTopic("TopicName3", "Description3");
            //-----------------------------------------------------

            try
            {
                return new ServerResult<List<BusinessEntities.Topic>>
                {
                    Data = _db.GetAllTopics(),
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServerResult<List<BusinessEntities.Topic>>
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }
        //------------------------------------------------------------

        public ServerResult<List<Recommendation>> GetRecommendations(int recommendedToId)
        {
            try
            {
                return new ServerResult<List<Recommendation>>
                {
                    Data = _db.GetAllRecommendations().Where(r => recommendedToId == r.RecommendedTo.Id).ToList(),
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
                return new ServerResult<List<Recommendation>>
                {
                    Data = _db.GetAllRecommendations().Where(r => createdById.Equals(r.CreatedBy.Id)).ToList(),
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
                return new ServerResult<Recommendation>
                {
                    Data = _db.GetRecommendationById(id),
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
            int recommendedToId = CheckIfEmployeeExists(args.RecommendedTo);

            if (recommendedToId == -1)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = "Employee does not exist"
                };
            }

            try
            {
                _db.CreateRecommendation(args.TopicId, recommendedToId, args.CreatedById);

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
            int recommendedToId = CheckIfEmployeeExists(args.RecommendedTo);
            Console.WriteLine("id is " + id);
            if (recommendedToId == -1)
            {
                return new ServerResult
                {
                    Success = false,
                    Message = "Employee does not exist"
                };
            }

            try
            {
                _db.UpdateRecommendation(id, args.TopicId, recommendedToId);
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
                _db.DeleteRecommendation(id);
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

        private int CheckIfEmployeeExists(string name)
        {
            try
            {
                return _db.GetEmployeeByName(name).Id;
            }
            catch
            {
                return -1;
            }
        }

    }
}