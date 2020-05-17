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

        [HttpGet]
        public ServerResult<Restriction> GetRestriction(int employeeId)
        {
            return _restrictionService.GetRestriction(employeeId);
        }
        [HttpGet]
        public ServerResult<List<Restriction>> GetCreatedRestrictions(int employeeId)
        {
            return _restrictionService.GetCreatedRestrictions(employeeId);
        }
        [HttpDelete]
        public ServerResult DeleteRestriction(int id)
        {
            return _restrictionService.DeleteRestriction(id);
        }
        [HttpPost]
        public ServerResult CreateRestriction([FromBody] RestrictionArgs restrictionArgs)
        {
            return _restrictionService.CreateRestriction(restrictionArgs);
        }

    }
}
