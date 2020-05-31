using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface ITopicCompletionRepository : IRepository<TopicCompletion>
    {
        List<TopicCompletion> GetEmployeesCompletions(int employeeId);
    }
}
