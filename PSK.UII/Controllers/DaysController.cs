using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.IServices;
using System.Collections.Generic;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class DaysController : ControllerBase
    {
        private readonly ILearningDayService _learningDayService;

        public DaysController(ILearningDayService learningDayService)
        {
            _learningDayService = learningDayService;
        }

        [HttpGet]
        public ServerResult<List<Day>> GetDays([FromQuery(Name ="employeeId")] int? employeeId)
        {
            if (employeeId != null)
                return _learningDayService.GetEmployeeDays((int)employeeId);
            return _learningDayService.GetDays();
        }

        [HttpPost]
        public ServerResult CreateDay([FromBody] Day args)
        {
            return _learningDayService.AddNewLearningDay(args);
        }
    }
}