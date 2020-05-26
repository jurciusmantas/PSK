using Microsoft.AspNetCore.Mvc;
using PSK.Model.Entities;
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

        [HttpGet, Route("restriction/{employeeId}")]
        public ServerResult<Restriction> GetRestriction(int employeeId)
        {
            return _restrictionService.GetRestriction(employeeId);
        }
        [HttpGet, Route("restrictions/{employeeId}")]
        public ServerResult<List<Restriction>> GetCreatedRestrictions(int employeeId)
        {
            return _restrictionService.GetCreatedRestrictions(employeeId);
        }
        [HttpDelete, Route("delete/{id}")]
        public ServerResult DeleteRestriction(int id)
        {
            return _restrictionService.DeleteRestriction(id);
        }
        [HttpPost, Route("create")]
        public ServerResult CreateRestriction([FromBody] RestrictionArgs restrictionArgs)
        {
            return _restrictionService.CreateRestriction(restrictionArgs);
        }

        [HttpPost, Route("users/{userId}")]
        public ServerResult<List<User>> GetLowerUsers(int userId)
        {
            return _restrictionService.GetLowerUsers(userId);
        }

    }
}
