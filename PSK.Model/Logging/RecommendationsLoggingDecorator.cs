using PSK.Model.DTO;
using PSK.Model.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Logging
{
    class RecommendationsLoggingDecorator : IRecommendationsService
    {
        private readonly IRecommendationsService _decoratee;
        private readonly ILogger _logger;
        private readonly string _decorateeClassName;

        public RecommendationsLoggingDecorator(IRecommendationsService decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
            _decorateeClassName = decoratee.ToString();
        }

        public ServerResult CreateRecommendation(Recommendation recommendation)
        {
            try
            {
                ServerResult result = _decoratee.CreateRecommendation(recommendation);

                if (!result.Success)
                {
                    _logger.Information("{User}: creating recommendation {Name} unsuccessful", recommendation.CreatorName, recommendation.TopicName);
                    return result;
                }
                _logger.Information("{User}: {Recommendation} recommendation added successfully", recommendation.CreatorName, recommendation.TopicName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{User}: {_decorateeClassName}.CreateRecommendation {Name} failed {NewLine} {Exception}", recommendation.CreatorName, _decorateeClassName, recommendation.TopicName);
                throw;
            }
        }

        public ServerResult DeleteRecommendation(int id)
        {
            try
            {
                ServerResult result = _decoratee.DeleteRecommendation(id);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.DeleteRecommendation unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.DeleteRecommendation sucessful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{_decorateeClassName}.DeleteRecommendation failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }

        public ServerResult<List<Recommendation>> GetCreatedRecommendations(int creatorId)
        {
            try
            {
                ServerResult<List<Recommendation>> result = _decoratee.GetCreatedRecommendations(creatorId);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetCreatedRecommendations unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetCreatedRecommendations successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{_decorateeClassName}.GetCreatedRecommendations failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }

        public ServerResult<List<Recommendation>> GetReceivedRecommendations(int userId)
        {
            try
            {
                ServerResult<List<Recommendation>> result = _decoratee.GetReceivedRecommendations(userId);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetReceivedRecommendations unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetReceivedRecommendations successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{_decorateeClassName}.GetReceivedRecommendations failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }

        public ServerResult<Recommendation> GetRecommendation(int id)
        {
            try
            {
                ServerResult<Recommendation> result = _decoratee.GetRecommendation(id);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetRecommendation unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetRecommendation successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{_decorateeClassName}.GetRecommendation failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }

        public ServerResult<List<Recommendation>> GetRecommendations()
        {
            try
            {
                ServerResult<List<Recommendation>> result = _decoratee.GetRecommendations();

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetRecommendations unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetRecommendations successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{_decorateeClassName}.GetRecommendations failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }

        public ServerResult UpdateRecommendation(Recommendation rec)
        {
            try
            {
                ServerResult result = _decoratee.UpdateRecommendation(rec);

                if (!result.Success)
                {
                    _logger.Information("{User}: Updating recommendation {Name} unsuccessful", rec.CreatorName, rec.TopicName);
                    return result;
                }
                _logger.Information("{User}: updated recommendation {Name} successfully", rec.CreatorName, _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{User}: {_decorateeClassName}.UpdateRecommendation failed {NewLine} {Exception}", rec.CreatorName, _decorateeClassName);
                throw;
            }
        }
    }
}
