using PSK.Model.DTO;
using PSK.Model.Services;
using Serilog;
using System;
using System.Collections.Generic;

namespace PSK.Model.Logging
{
    class LearningDayLoggingDecorator : ILearningDayService
    {
        private readonly ILearningDayService _decoratee;
        private readonly ILogger _logger;
        private readonly string _decorateeClassName;


        public LearningDayLoggingDecorator(ILearningDayService decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
            _decorateeClassName = decoratee.ToString();
        }

        public ServerResult AddNewLearningDay(Day args)
        {
            try
            {
                ServerResult result = _decoratee.AddNewLearningDay(args);

                if (!result.Success)
                {
                    _logger.Information("{Login}: {DecorateeClassName}.AddNewLearningDay unsuccessful", args.EmployeeName, _decorateeClassName);
                    return result;
                }
                _logger.Information("{User}: added {Topic} to a learning day successfully", args.EmployeeName, args.TopicName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{Login}: {DecorateeClassName}.AddNewLearningDay failed {NewLine} {Exception}", _decorateeClassName, args.EmployeeName);
                throw;
            }
        }

        public ServerResult DeleteLearningDay(int id)
        {
            try
            {
                ServerResult result = _decoratee.DeleteLearningDay(id);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.DeleteLearningDay unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.DeleteLearningDay unsuccessful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.DeleteLearningDay failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }

        public ServerResult<List<Day>> GetDays()
        {
            try
            {
                ServerResult<List<Day>> result = _decoratee.GetDays();

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetDays unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetDays successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.GetDays failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }

        public ServerResult<List<Day>> GetEmployeeDays(int employeeId)
        {
            try
            {
                ServerResult<List<Day>> result = _decoratee.GetEmployeeDays(employeeId);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetEmployeeDaysunsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetEmployeeDays successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.GetEmployeeDays failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }
    }
}
