using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSK.UI.Controllers
{
    [Route("api/[controller]")]
    public class RestrictionsController : ControllerBase
    {
        private readonly IRestrictionService _restrictionService;

        public RestrictionsController(IRestrictionService restrictionService)
        {
            _restrictionService = restrictionService;
        }

        [HttpGet]
        public ServerResult<List<Restriction>> GetCreatedRestrictions([FromQuery(Name = "employeeId")] int employeeId)
        {
            var currEmployeeIdObject = Request.HttpContext.Items["currentEmployeeId"];
            if(currEmployeeIdObject != null)
            {
                employeeId = int.Parse(currEmployeeIdObject.ToString());
            }
            
            return _restrictionService.GetRestrictionsTo(employeeId);
        }
        [HttpGet("active")]
        public ServerResult<Restriction> GetRestriction([FromQuery(Name = "employeeId")] int employeeId)
        {
            var currEmployeeIdObject = Request.HttpContext.Items["currentEmployeeId"];
            if (currEmployeeIdObject != null)
            {
                employeeId = int.Parse(currEmployeeIdObject.ToString());
            }
            return _restrictionService.GetRestrictionFrom(employeeId);
        }

        [HttpDelete]
        public ServerResult DeleteRestriction([FromQuery(Name = "id")] int id, [FromQuery(Name = "employeeId")] int employeeId)
        {
            var currEmployeeIdObject = Request.HttpContext.Items["currentEmployeeId"];
            if (currEmployeeIdObject != null)
            {
                employeeId = int.Parse(currEmployeeIdObject.ToString());
            }
            return _restrictionService.DeleteRestriction(id, employeeId);
        }
        [HttpPost]
        public ServerResult CreateRestriction([FromBody] RestrictionArgs restrictionArgs)
        {
            var currEmployeeIdObject = Request.HttpContext.Items["currentEmployeeId"];
            if (currEmployeeIdObject != null)
            {
                restrictionArgs.CreatorId = int.Parse(currEmployeeIdObject.ToString());
            }
            return _restrictionService.CreateRestriction(restrictionArgs);
        }

    }
}
