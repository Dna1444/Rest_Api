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
    public class CustomerController : ControllerBase
    {
        private readonly MaximeAuger_mysqlContext _context;

        public CustomerController(MaximeAuger_mysqlContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomer()
        {
            return await _context.Customers.ToListAsync();
        }

         // Action that recuperates a given elevators by Id 
        // GET: api/elevators/id
        [HttpGet("{Email}")]
        public async Task<ActionResult<List<Customers>>> GetCustomerbyEmail(string Email)
        {
            var customer = await _context.Customers.Where(c => c.CpyContactEmail == Email).ToListAsync();

            if (CustomerExists(Email))
            {
                return NotFound();
            }

            return customer;
        }



       

        private bool CustomerExists(string email)
        {
            return _context.Customers.Any(e => e.CpyContactEmail == email);
        }
    }
}
