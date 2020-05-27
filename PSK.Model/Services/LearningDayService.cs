using PSK.Model.Entities;
using PSK.Model.DTO;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

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
                Entities.Day learningDay = _dayRepository.Add(DTOToEntity(args));
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

        public ServerResult<List<DTO.Day>> GetDays()
        {
            return new ServerResult<List<DTO.Day>>()
            {
                Success = true,
                Data = _dayRepository.Get().Select(d => EntityToDTO(d)).ToList()
            };
        }

        public ServerResult<List<DTO.Day>> GetEmployeeDays(int employeeId)
        {
            return new ServerResult<List<DTO.Day>>()
            {
                Success = true,
                Data = _dayRepository.GetEmployeeDays(employeeId)
                                     .Select(d => EntityToDTO(d)).ToList()
            };
        }

        private DTO.Day EntityToDTO(Entities.Day day)
        {
            return new DTO.Day()
            {
                Id = day.Id,
                EmployeeId = day.EmployeeId,
                EmployeeName = day.Employee.Name,
                TopicId = day.TopicId,
                TopicName = day.Topic.Name,
                Date = day.Date.ToShortDateString()
            };
        }

        private Entities.Day DTOToEntity(DTO.Day day)
        {
            DateTime date = DateTime.Parse(day.Date);
            return new Entities.Day()
            {
                Id = day.Id,
                Date = date,
                EmployeeId = day.EmployeeId,
                TopicId = day.TopicId,
            };
        }
    }
}
