using Doc5_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doc5_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, POC")]
    public class EmployeesController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Alice", Department = "HR" },
            new Employee { Id = 2, Name = "Bob", Department = "Finance" },
            new Employee { Id = 3, Name = "Charlie", Department = "IT" }
        };

        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            return Ok(employees);
        }
    }
}
