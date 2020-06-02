using PSK.Model.DTO;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using PSK.Model.Helpers;
using PSK.Model.IServices;
using PSK.Model.Entities;

namespace PSK.Model.Services
{
    public class LearningDayService : ILearningDayService
    {
        private readonly IDayRepository _dayRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicCompletionRepository _topicCompletionRepository;

        public LearningDayService(IDayRepository dayRepository
            , IEmployeeRepository employeeRepository
            , ITopicRepository topicRepository
            , ITopicCompletionRepository topicCompletionRepository)
        {
            _dayRepository = dayRepository;
            _employeeRepository = employeeRepository;
            _topicRepository = topicRepository;
            _topicCompletionRepository = topicCompletionRepository;
        }

        public ServerResult AddNewLearningDay(DTO.Day args)
        {
            try
            {
                if (args == null)
                    return new ServerResult
                    {
                        Success = false,
                        Message = "No arguments"
                    };

                var dayToAdd = args.ToEntity();
                DeletePreviousCompletions(dayToAdd);
                var error = Validate(dayToAdd);
                if (!string.IsNullOrEmpty(error))
                    return new ServerResult
                    {
                        Success = false,
                        Message = error,
                    };

                Entities.Day learningDay = _dayRepository.Add(dayToAdd);
                return new ServerResult() { Success = true };
            }
            catch (Exception e)
            {
                return new ServerResult 
                { 
                    Success = false, 
                    Message = e.Message 
                };
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
                Data = _dayRepository.Get().Select(d => d.ToDTO()).ToList()
            };
        }

        public ServerResult<List<DTO.Day>> GetEmployeeDays(int employeeId)
        {
            try
            {
                var employee = _employeeRepository.Get(employeeId);
                if (employee == null)
                    return new ServerResult<List<DTO.Day>>
                    {
                        Success = false,
                        Message = "Employee not found"
                    };

                var days = _dayRepository.GetEmployeeDays(employeeId);
                var topicCompletions = _topicCompletionRepository.GetEmployeesCompletions(employeeId);
                
                var result = new List<DTO.Day>();

                foreach(var day in days)
                {
                    var dayDto = day.ToDTO();

                    /* If a day in the future with the same topic is completed - mark this day as completed aswell */
                    if (topicCompletions != null && topicCompletions.Count > 0)
                    {
                        var completionsByTopic = topicCompletions.Where(c => c.TopicId == day.TopicId).ToList();
                        if (completionsByTopic != null && completionsByTopic.Count > 0)
                        {
                            var latestTopicCompletion = completionsByTopic.OrderByDescending(c => c.CompletedOn).First();

                            if (latestTopicCompletion != null && latestTopicCompletion.CompletedOn >= day.Date)
                                dayDto.Completed = true;
                        }
                    }

                    /* Only let complete the furthest day in the future with the same topic */
                    else
                    {
                        var furthestDay = days.Where(d => d.TopicId == day.TopicId).OrderByDescending(d => d.Date).First();
                        if (furthestDay.Id != day.Id)
                            dayDto.Completed = true;
                    }

                    result.Add(dayDto);
                }

                return new ServerResult<List<DTO.Day>>()
                {
                    Success = true,
                    Data = result
                };
            }
            catch(Exception e)
            {
                return new ServerResult<List<DTO.Day>> 
                { 
                    Success = false, 
                    Message = e.Message 
                };
                throw;
            }
        }

        private string Validate(Entities.Day newDay)
        {
            if (newDay == null)
                return "No arguments";

            var employee = _employeeRepository.Get(newDay.EmployeeId);
            if (employee == null)
                return "Employee not found";

            var topic = _topicRepository.Get(newDay.TopicId);
            if (topic == null)
                return "Topic not found";

            var days = _dayRepository.GetEmployeeDays(employee.Id);
            if (days != null && days.Count > 0)
            {
                var duplicateDay = days.FirstOrDefault(d => d.Date == newDay.Date);
                if (duplicateDay != null)
                    return $"You are already learning topic {duplicateDay.Topic.Name} on {newDay.Date}";
            }

            var employeeRestrictions = employee.EmployeeRestrictions;
            if (employeeRestrictions != null && employeeRestrictions.Count > 0)
            {
                EmployeeRestriction employeeRestriction = employeeRestrictions.OrderByDescending(er => er.Restriction.CreationDate).First();

                var monthCounter = 1;
                var yearCounter = 1;
                var quarterCounter = 1;

                var quarter = newDay.Date.GetQuarter();

                foreach (var d in days)
                {
                    if (d.Date.Month == newDay.Date.Month)
                        monthCounter++;

                    if (d.Date.Year == newDay.Date.Year)
                        yearCounter++;

                    if (d.Date.GetQuarter() == quarter)
                        quarterCounter++;

                    if (monthCounter > employeeRestriction.Restriction.MaxDaysPerMonth)
                        return "Could not add new learning day. Maximum days in this month reached";

                    if (yearCounter > employeeRestriction.Restriction.MaxDaysPerYear)
                        return "Could not add new learning day. Maximum days in this year reached";

                    if (quarterCounter > employeeRestriction.Restriction.MaxDaysPerQuarter)
                        return "Could not add new learning day. Maximum days in this quarter reached";

                }
            }

            return string.Empty;
        }

        private void DeletePreviousCompletions(Entities.Day day)
        {
            var topicsCompletions = _topicCompletionRepository.GetEmployeesCompletions(day.EmployeeId);
            if (topicsCompletions != null)
            {
                var foundTopicCompletions = topicsCompletions.Where(tc => tc.TopicId == day.TopicId);
                if (foundTopicCompletions != null)
                {
                    foreach (var foundTopicCompletion in foundTopicCompletions)
                    {
                        _topicCompletionRepository.Delete(foundTopicCompletion.Id);
                    }
                }
            }
        }
    }
}
