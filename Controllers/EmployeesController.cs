using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildingApi.Models;

namespace BuildingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly MaximeAuger_mysqlContext _context;

        public EmployeesController(MaximeAuger_mysqlContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployee()
        {
            return await _context.Employees.ToListAsync();
        }

        // Action that recuperates a given elevators by Id 
        // GET: api/elevators/id
        [HttpGet("email/{Email}")]
        public async Task<ActionResult<List<Employees>>> GetCustomerbyEmail(string Email)
        {
            var employees = await _context.Employees.Where(c => c.Email == Email).ToListAsync();

            if (!EmployeesExists(Email))
            {
                return BadRequest();
            }

            return employees;
        }





        private bool EmployeesExists(string email)
        {
            return _context.Employees.Any(e => e.Email == email);
        }
    }
}
