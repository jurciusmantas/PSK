using PSK.Model.Entities;

namespace PSK.Model.Services
{
    public interface ILearningDayService
    {
        ServerResult AddNewLearningDay(DayArgs args);
        ServerResult DeleteLearningDay(int id);
    }
}
