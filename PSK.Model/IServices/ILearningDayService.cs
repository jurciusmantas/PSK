using PSK.Model.DTO;
using System.Collections.Generic;

namespace PSK.Model.IServices
{
    public interface ILearningDayService
    {
        ServerResult AddNewLearningDay(Day args);
        ServerResult DeleteLearningDay(int id);
        ServerResult<List<Day>> GetEmployeeDays(int employeeId);
        ServerResult<List<Day>> GetDays();
    }
}
