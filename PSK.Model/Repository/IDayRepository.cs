using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IDayRepository : IRepository<Day>
    {
        List<Day> GetEmployeeDays(int employeeId);
        Day GetEmployeeFutureDayByTopic(int topicId, int employeeId);
    }
}
