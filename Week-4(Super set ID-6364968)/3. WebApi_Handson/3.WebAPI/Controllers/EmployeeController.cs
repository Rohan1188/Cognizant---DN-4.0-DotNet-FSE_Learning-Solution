using Doc3_WebAPI.Models;
using Doc3_WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doc3_WebAPI.Controllers
{
    [ApiController]
    [Route("Emp")]
    [CustomAuthFilter]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee>? employees;

        public EmployeeController()
        {
            if (employees == null)
                employees = GetStandardEmployeeList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Employee>> Get()
        {
            throw new Exception("Testing exception filter");
            //return Ok(employees);
        }

        [HttpPost]
        public ActionResult AddEmployee([FromBody] Employee emp)
        {
            // Ensure 'employees' is not null before accessing it
            if (employees == null)
            {
                employees = new List<Employee>();
            }

            employees.Add(emp);
            return Ok(emp);
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Anindya Dolui",
                    Salary = 35000,
                    Permanent = true,
                    Department = new Department { Id = 1, Name = "Tester" },
                    Skills = new List<Skill> {
                        new Skill { Id = 1, Name = "C#" },
                        new Skill { Id = 2, Name = "SQL" }
                    },
                    DateOfBirth = new DateTime(2004, 8, 1)
                }
            };
        }

        [HttpGet("standard")]
        [AllowAnonymous]
        public ActionResult<Employee> GetStandard()
        {
            return Ok(employees?.FirstOrDefault());
        }
    }
}
