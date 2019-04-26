using Lime.Business;
using Microsoft.AspNetCore.Mvc;

namespace Lime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeLogic _employeeLogic;

        public EmployeesController(IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        // GET api/employees?q=...
        [HttpGet]
        public IActionResult Get(string q)
        {
            if (q == null || q.Length < 2)
                return BadRequest("Querystring must be at least 2 characters long.");

            var employees = _employeeLogic.GetEmployees(q);
            return Ok(employees);
        }
    }
}
