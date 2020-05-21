using PSK.Model.DTO;

namespace PSK.Model.Services
{
    public interface ILearningDayService
    {
        ServerResult AddNewLearningDay(Day args);
        ServerResult DeleteLearningDay(int id);
    }
}
