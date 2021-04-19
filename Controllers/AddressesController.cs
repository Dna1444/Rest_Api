using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class addressesController : ControllerBase
    {
        private readonly MaximeAuger_mysqlContext _context;

        public addressesController(MaximeAuger_mysqlContext context)
        {
            _context = context;
        }

        //Action that gives the list of all columns
        // GET: api/columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addresses>>> Getaddresse()
        {
            return await _context.Addresses.ToListAsync();
        }

    

    //Action that gives the list of all columns
        // GET: api/columns
        [HttpGet("Cities")]
        public int  GetuniqueCity()
        {
            IEnumerable<Addresses> buildingAddresses = from address in _context.Addresses where address.City !="" select address;

            int amountOfCities = buildingAddresses.Select(building => building.City).Distinct().Count();
            
            return amountOfCities;
        }

    }
}