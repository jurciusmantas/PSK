using PSK.Model.DTO;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using PSK.Model.Helpers;
using PSK.Model.IServices;

namespace PSK.Model.Services
{
    public class LearningDayService : ILearningDayService
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
                Entities.Day learningDay = _dayRepository.Add(args.DTOToEntity());
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
                Data = _dayRepository.Get().Select(d => d.EntityToDTO()).ToList()
            };
        }

        public ServerResult<List<DTO.Day>> GetEmployeeDays(int employeeId)
        {
            return new ServerResult<List<DTO.Day>>()
            {
                Success = true,
                Data = _dayRepository.GetEmployeeDays(employeeId).Select(d => d.EntityToDTO()).ToList()
            };
        }
    }
}
