using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class RestrictionController : Controller
    {
        private readonly IRestrictionService _restrictionService;

        public RestrictionController(IRestrictionService restrictionService)
        {
            _restrictionService = restrictionService;
        }

        [HttpGet, Route("current_restriction")]
        public ServerResult<Restriction> GetRestriction([FromQuery(Name = "id")] int employeeId)
        {
            return _restrictionService.GetRestriction(employeeId);
        }
        [HttpGet, Route("restrictions")]
        public ServerResult<List<Restriction>> GetCreatedRestrictions([FromQuery(Name = "id")] int employeeId)
        {
            return _restrictionService.GetCreatedRestrictions(employeeId);
        }
        [HttpDelete, Route("delete")]
        public ServerResult DeleteRestriction([FromQuery(Name = "id")]int id)
        {
            return _restrictionService.DeleteRestriction(id);
        }
        [HttpPost, Route("create")]
        public ServerResult CreateRestriction([FromBody] RestrictionArgs restrictionArgs)
        {
            return _restrictionService.CreateRestriction(restrictionArgs);
        }

        [HttpPost, Route("users")]
        public ServerResult<List<User>> GetLowerUsers([FromQuery(Name = "id")] int userId)
        {
            return _restrictionService.GetLowerUsers(userId);
        }

    }
}
