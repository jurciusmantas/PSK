﻿using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.Services;
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
        public ServerResult<List<Restriction>> GetCreatedRestrictions([FromQuery(Name = "id")] int employeeId)
        {
            return _restrictionService.GetRestrictionsTo(employeeId);
        }
        [HttpGet("{id}")]
        public ServerResult<Restriction> GetRestriction([FromRoute(Name = "id")] int employeeId)
        {
            return _restrictionService.GetRestrictionFrom(employeeId);
        }

        [HttpDelete]
        public ServerResult DeleteRestriction([FromQuery(Name = "id")]int id)
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