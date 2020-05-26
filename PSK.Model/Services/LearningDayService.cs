using PSK.Model.Entities;
using PSK.Model.DTO;
using PSK.Model.Repository;
using System;

namespace PSK.Model.Services
{
    class LearningDayService : ILearningDayService
    {
        private readonly IDayRepository _dayRepository;

        public LearningDayService(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }
        public ServerResult AddNewLearningDay(DTO.Day args)
        {
            try
            {
                Entities.Day learningDay = _dayRepository.Add(new Entities.Day() { Date = DateTime.Parse(args.Date), EmployeeId = args.EmployeeId, TopicId = args.TopicId });
                return new ServerResult() { Success = true };
            }
            catch (Exception e)
            {
                // assuming errors happen as exceptions. No idea what kind though
                return new ServerResult() { Success = false, Message = e.Message };
                throw;
            }
            
        }

        public ServerResult DeleteLearningDay(int id)
        {
            throw new NotImplementedException();
        }
    }
}
