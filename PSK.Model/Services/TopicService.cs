using PSK.Model.DTO;
using PSK.Model.Helpers;
using PSK.Model.IServices;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSK.Model.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicCompletionRepository _topicCompletionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDayRepository _dayRepository;

        public TopicService(ITopicRepository topicRepository
            , ITopicCompletionRepository topicCompletionRepository
            , IEmployeeRepository employeeRepository
            , IDayRepository dayRepository)
        {
            _topicRepository = topicRepository;
            _topicCompletionRepository = topicCompletionRepository;
            _employeeRepository = employeeRepository;
            _dayRepository = dayRepository;
        }

        public ServerResult<List<Topic>> GetTopics(bool tree)
        {
            var topics = _topicRepository.Get();
            if (tree)
            {
                var topicTree = ConvertToTree(topics);
                return new ServerResult<List<Topic>> { Data = topicTree, Success = true };
            }
            return new ServerResult<List<Topic>> { Data = topics.Select(t => t.ToDTO()).ToList(), Success = true };
        }

        public ServerResult<Topic> GetTopic(int id)
        {
            var topic = _topicRepository.Get(id);
            if (topic == null)
                return new ServerResult<Topic>
                {
                    Success = false,
                    Message = "Not found"
                };

            return new ServerResult<Topic>
            {
                Data = topic.ToDTO(),
                Success = true
            };
        }

        private List<Topic> ConvertToTree(List<Entities.Topic> topicList)
        {
            var topics = new List<Topic>();

            foreach (var item in topicList)
            {
                var topic = new Topic { Id = item.Id, Description = item.Description, Name = item.Name, ParentId = item.ParentTopicId };
                topics.Add(topic);
            }

            foreach (var topic in topics)
            {
                topic.SubTopicList = topics.Where(x => x.ParentId.HasValue && x.ParentId == topic.Id).ToList();
            }

            var tree = topics.Where(x => !x.ParentId.HasValue).ToList();

            return tree;
        }

        public ServerResult CreateTopic(Topic args)
        {
            if (args == null)
                return new ServerResult
                {
                    Success = false,
                    Message = "No arguments"
                };

            if (string.IsNullOrEmpty(args.Name))
                return new ServerResult
                {
                    Success = false,
                    Message = "No name"
                };

            var newTopic = new Entities.Topic { Name = args.Name, Description = args.Description };

            if (args.ParentId.HasValue)
            {
                var parentTopic = _topicRepository.Get(args.ParentId.Value);

                if (parentTopic == null)
                    return new ServerResult
                    {
                        Message = "Parent topic does not exist",
                        Success = false
                    };

                newTopic.ParentTopic = parentTopic;
                newTopic.ParentTopicId = args.ParentId.Value;
            }

            _topicRepository.Add(newTopic);

            return new ServerResult { Success = true };
        }

        public ServerResult MarkAsCompleted(TopicCompletion args)
        {
            if (args == null)
                return new ServerResult
                {
                    Success = false,
                    Message = "No arguments"
                };

            var topic = _topicRepository.Get(args.TopicId);
            if (topic == null)
                return new ServerResult
                {
                    Success = false,
                    Message = "Topic not found"
                };

            var employee = _employeeRepository.Get(args.EmployeeId);
            if (employee == null)
                return new ServerResult
                {
                    Success = false,
                    Message = "Employee not found"
                };

            _topicCompletionRepository.Add(new Entities.TopicCompletion
            {
                TopicId = topic.Id,
                Topic = topic,
                EmployeeId = employee.Id,
                Employee = employee,
                CompletedOn = DateTime.Now,
            });

            return new ServerResult { Success = true };
        }

        public ServerResult<Topic> UpdateTopic(Topic topic)
        {
            try
            {
                Entities.Topic dbTopic = _topicRepository.Get(topic.Id);
                if (dbTopic == null)
                    return new ServerResult<Topic>()
                    {
                        Success = false,
                        Message = "Topic does not exist",
                    };

                if (!topic.RowVersion.SequenceEqual(dbTopic.RowVersion))
                {
                    return new ServerResult<Topic>()
                    {
                        Success = false,
                        Message = "Already updated. Try again.",
                        Data = new Topic { Description = dbTopic.Description, Name = dbTopic.Name, RowVersion = dbTopic.RowVersion }
                    };
                }
                dbTopic.Name = topic.Name;
                dbTopic.Description = topic.Description;
                dbTopic.RowVersion = topic.RowVersion;
                _topicRepository.Update(dbTopic);
                return new ServerResult<Topic>() { Success = true };
            }

            catch (Exception e)
            {
                return new ServerResult<Topic>()
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult<List<Topic>> GetSubtopics(int id)
        {
            try
            {
            var subtopics = _topicRepository.GetSubtopics(id);
                return new ServerResult<List<Topic>>
                {
                    Success = true,
                    Data = subtopics.Select(t => t.ToDTO()).ToList()
                };
            }
            catch(Exception e)
            {
                return new ServerResult<List<Topic>>
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public ServerResult<List<LearnedSubordinatesListItem>> LoadLearnedSubordinates(int? employeeId, int topicId)
        {
            if (employeeId == null)
                return null;

            var res = new List<LearnedSubordinatesListItem>();

            var allSubordinates = _employeeRepository.GetAllSubordinates(employeeId.Value);
            if (allSubordinates != null && allSubordinates.Count > 0)
            {
                var completions = _topicCompletionRepository.GetEmployeesTopicCompletions(allSubordinates.Select(s => s.Id).ToList(), topicId);
                foreach(var subordinate in allSubordinates)
                {
                    /* Check if allready completed */
                    var completion = completions.FirstOrDefault(tc => tc.EmployeeId == subordinate.Id);
                    if (completion != null)
                        res.Add(new LearnedSubordinatesListItem
                        {
                            SubordinateName = subordinate.Name,
                            CompletedOn = completion.CompletedOn,
                        });

                    /* Check if is learning in future */
                    else
                    {
                        var learningDay = _dayRepository.GetEmployeeFeatureDayByTopic(topicId, subordinate.Id);
                        if (learningDay != null)
                            res.Add(new LearnedSubordinatesListItem
                            {
                                SubordinateName = subordinate.Name,
                                LearnsAt = learningDay.Date,
                            });
                    }
                }
            }

            return new ServerResult<List<LearnedSubordinatesListItem>>
            {
                Success = true,
                Data = res,
            };
        }
    }
}
