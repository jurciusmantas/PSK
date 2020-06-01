using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IDayRepository : IRepository<Day>
    {
        List<Day> GetEmployeeDays(int employeeId);
        Day GetEmployeeFeatureDayByTopic(int topicId, int employeeId);
    }
}
