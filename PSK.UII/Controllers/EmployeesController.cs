﻿using Microsoft.AspNetCore.Mvc;
using PSK.Model.DTO;
using PSK.Model.Services;
using System.Collections.Generic;

namespace PSK.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet("{id}")]
        public ServerResult<Employee> GetEmployee([FromRoute] int id)
        {
            return new ServerResult<Employee>()
            {
                Success = true,
                Data = _employeesService.Get(id)
            };
        }

        [HttpGet("{id}/subordinates")]
        public ServerResult<List<Employee>> GetSubordinates([FromRoute(Name = "id")] int employeeId)
        {
            return new ServerResult<List<Employee>>()
            {
                Success = true,
                Data = _employeesService.GetSubordinates(employeeId)
            };
        }
    }
}