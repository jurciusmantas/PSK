using PSK.Model.BusinessEntities;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Services
{
    class LearningDayService : ILearningDayService
    {
        private readonly IAssignedTopicRepository _assignedTopicRepository;
        private readonly IPlanRepository _planRepository;

        public LearningDayService(IAssignedTopicRepository assignedTopicRepository, IPlanRepository planRepository)
        {
            _assignedTopicRepository = assignedTopicRepository;
            _planRepository = planRepository;
        }
        public ServerResult addNewLearningDay(DayArgs args)
        {
            try
            {
                // create assigned topic
                AssignedTopic newAT = _assignedTopicRepository.Add(new AssignedTopic() { IsCompleted = false, EmployeeId = args.EmployeeId, TopicId = args.TopicId });
                // create plan
                Plan newPlan = _planRepository.Add(new Plan() { AssignedTopicId = newAT.Id, WorkDate = DateTime.Parse(args.Date) });
                return new ServerResult() { Success = true };
            }
            catch (Exception e)
            {
                // assuming errors happen as exceptions. No idea what kind though
                return new ServerResult() { Success = false, Message = e.Message };
                throw;
            }
            
        }

        public ServerResult deleteLearningDay(int id)
        {
            throw new NotImplementedException();
        }
    }
}
