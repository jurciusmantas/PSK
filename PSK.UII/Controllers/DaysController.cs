using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.Services;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class DaysController : Controller
    {
        private readonly ILearningDayService _learningDayService;

        public DaysController(ILearningDayService learningDayService)
        {
            _learningDayService = learningDayService;
        }

        [HttpPost]
        public ServerResult CreateDay([FromBody] Day args)
        {
            return _learningDayService.AddNewLearningDay(args);
        }
    }
}