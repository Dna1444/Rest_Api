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
    public class interventionsController : ControllerBase
    {
        private readonly MaximeAuger_mysqlContext _context;

        public interventionsController(MaximeAuger_mysqlContext context)
        {
            _context = context;
        }

        // GET: api/interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interventions>>> GetInterventions()
        {
            return await _context.Interventions.ToListAsync();
        }

         // Action that recuperates a given elevators by Id 
        // GET: api/elevators/id
        [HttpGet("{interventionPending}")]
        public async Task<ActionResult<List<Interventions>>> GetInterventionsByStatus()
        {
            var intervention = await _context.Interventions.Where(i => i.Status == "Pending").ToListAsync();
            var interventionNew = intervention.Where(i => i.Start_of_the_intervention == null).ToList();

            if (interventionNew == null)
            {
                return NotFound();
            }

            return interventionNew;
        }



       
        //Updating the status of a given elevator. Frist, identification of the elevator is needed.
        // PUT: api/elevators/id/updatestatus        
        [HttpPut("{id}/updatePending")]
        public async Task<IActionResult> PutmodifyStatusIntervention(long id, string Status)
        {
            if (Status != "InProgress")
            {
                return BadRequest();
            }

            var intervention = await _context.Interventions.FindAsync(id);

            intervention.Status = Status;
            intervention.Start_of_the_intervention = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!interventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Updating the status of a given elevator. Frist, identification of the elevator is needed.
        // PUT: api/elevators/id/updatestatus        
        [HttpPut("{id}/EndIntervention")]
        public async Task<IActionResult> PutEndingIntervention(long id, string Status)
        {
            if (Status != "Completed")
            {
                return BadRequest();
            }

            var intervention = await _context.Interventions.FindAsync(id);

            intervention.Status = Status;
            intervention.End_of_the_intervention = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!interventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool interventionExists(long id)
        {
            return _context.Interventions.Any(e => e.id == id);
        }
    }
}
