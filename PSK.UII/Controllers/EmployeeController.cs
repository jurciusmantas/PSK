using Microsoft.AspNetCore.Mvc;
using PSK.Model.Services;

namespace PSK.UI.Controllers
{
    [Route("/api/[controller]")]
    [Controller]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
    }
}
