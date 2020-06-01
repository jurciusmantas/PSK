using PSK.Model.DTO;
using PSK.Model.IServices;
using Serilog;
using System;
using System.Collections.Generic;

namespace PSK.Model.Logging
{
    class TopicLoggingDecorator : ITopicService
    {
        private readonly ITopicService _decoratee;
        private readonly ILogger _logger;
        private readonly string _decorateeClassName;

        public TopicLoggingDecorator (ITopicService decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
            _decorateeClassName = decoratee.ToString();
        }

        public ServerResult CreateTopic(Topic args)
        {
            try
            {
                ServerResult result = _decoratee.CreateTopic(args);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.CreateTopic creating {Topic} unsuccessful", _decorateeClassName, args.Name);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.CreateTopic {Topic} created successfully", _decorateeClassName, args.Name);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.CreateTopic creating {Topic} failed {NewLine} {Exception}", _decorateeClassName, args.Name);
                return new ServerResult
                {
                    Success = false,
                    Message = "An error occured. Please try again later"
                };
            }
        }

        public ServerResult<List<Topic>> GetSubtopics(int id)
        {
            try
            {
                ServerResult<List<Topic>> result = _decoratee.GetSubtopics(id);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetSubtopics unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetSubtopics successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.GetSubtopics {TopicId} failed {NewLine} {Exception}", _decorateeClassName, id);
                return new ServerResult<List<Topic>>
                {
                    Success = false,
                    Message = "An error occured. Please try again later"
                };
            }

        }

        public ServerResult<Topic> GetTopic(int id)
        {
            try
            {
                ServerResult<Topic> result = _decoratee.GetTopic(id);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetTopic unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetTopic successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.GetTopic {NewLine} {Exception}", _decorateeClassName);
                return new ServerResult<Topic>
                {
                    Success = false,
                    Message = "An error occured. Please try again later"
                };
            }
        }

        public ServerResult<List<Topic>> GetTopics(bool tree)
        {
            try
            {
                ServerResult<List<Topic>> result = _decoratee.GetTopics(tree);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetTopics unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetTopics successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.GetTopics failed {NewLine} {Exception}", _decorateeClassName);
                return new ServerResult<List<Topic>>
                {
                    Success = false,
                    Message = "An error occured. Please try again later"
                };
            }
        }

        public ServerResult MarkAsCompleted(TopicCompletion args)
        {
            try
            {
                ServerResult result = _decoratee.MarkAsCompleted(args);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.MarkAsCompleted unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.MarkAsCompleted successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.MarkAsCompleted failed {NewLine} {Exception}", _decorateeClassName);
                return new ServerResult
                {
                    Success = false,
                    Message = "An error occured. Please try again later"
                };
            }
        }
        
        public ServerResult<Topic> UpdateTopic(Topic topic)
        {
            try
            {
                ServerResult<Topic> result = _decoratee.UpdateTopic(topic);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.UpdateTopic unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("Topic {Topic} updated successfully", topic.Name);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.UpdateTopic failed {NewLine} {Exception}", _decorateeClassName);
                return new ServerResult<Topic>
                {
                    Success = false,
                    Message = "An error occured. Please try again later"
                };
            }
        }

        public ServerResult<List<LearnedSubordinatesListItem>> LoadLearnedSubordinates(int? employeeId, int topicId)
        {
            try
            {
                ServerResult<List<LearnedSubordinatesListItem>> result = _decoratee.LoadLearnedSubordinates(employeeId, topicId);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.LoadLearnedSubordinates unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("Load Learned Subordinates for employee {0} topic - {1} successful", employeeId, topicId);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.LoadLearnedSubordinates failed {NewLine} {Exception}", _decorateeClassName);
                return new ServerResult<List<LearnedSubordinatesListItem>>
                {
                    Success = false,
                    Message = "An error occured. Please try again later"
                };
            }
        }
    }
}
