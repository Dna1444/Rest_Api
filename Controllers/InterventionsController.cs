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
        public async Task<IActionResult> PutmodifyStatusIntervention(long id)
        {
            
            var intervention = await _context.Interventions.FindAsync(id);

            intervention.Status = "InProgress";
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
        public async Task<IActionResult> PutEndingIntervention(long id)
        {
            

            var intervention = await _context.Interventions.FindAsync(id);

            intervention.Status = "Completed";
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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Interventions>>> PostInterventions(Interventions intervention)
        {
            Interventions interventionForm = intervention;
            interventionForm.author = 9;
            interventionForm.Result = "Incomplete";
            interventionForm.Status = "Pending";
            _context.Interventions.Add(interventionForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInterventions),new {id = intervention.id}, intervention);
        }

        [HttpPost("{product}/{id}/{report}")]
        public async Task<ActionResult<IEnumerable<Interventions>>> PostAlexaInterventions(string product, long? id, string report)
        {
            long? customerId;
            long? buildingId;
            long? batteryId;
            long? columnId = null;
            long? elevatorId = null;


            if(product == "battery" || product == "bateria" || product == "batterie"){
                batteryId = id;
                var battery = await _context.Batteries.FindAsync(id);
                var building = await _context.Buildings.FindAsync(battery.BuildingId);
                var customer = await _context.Customers.FindAsync(building.customer_id);
                buildingId = battery.BuildingId;
                customerId = building.customer_id;
                

            }else if(product == "column" || product == "columna" || product == "colonne"){
                columnId = id;
                var column = await _context.Columns.FindAsync(id);
                var battery = await _context.Batteries.FindAsync(column.BatteryId);
                var building = await _context.Buildings.FindAsync(battery.BuildingId);
                var customer = await _context.Customers.FindAsync(building.customer_id);
                batteryId = column.BatteryId;
                buildingId = battery.BuildingId;
                customerId = building.customer_id;

            }else if(product == "elevator" || product == "elevador" || product == "ascensor" || product == "ascenseur"){
                elevatorId = id;
                var elevator = await _context.Elevators.FindAsync(id);
                var column = await _context.Columns.FindAsync(elevator.ColumnId);
                var battery = await _context.Batteries.FindAsync(column.BatteryId);
                var building = await _context.Buildings.FindAsync(battery.BuildingId);
                var customer = await _context.Customers.FindAsync(building.customer_id);
                columnId = elevator.ColumnId;
                batteryId = column.BatteryId;
                buildingId = battery.BuildingId;
                customerId = building.customer_id;
                
            }else {
                return BadRequest();
            }
        

            Interventions interventionForm = new Interventions();
            interventionForm.author = 10;
            interventionForm.customers_id = (long)customerId;
            interventionForm.building_id = (long)buildingId;
            interventionForm.batteries_id = batteryId;
            interventionForm.columns_id = columnId;
            interventionForm.elevators_id = elevatorId;
            interventionForm.Report = report;
            interventionForm.Result = "Incomplete";
            interventionForm.Status = "Pending";
            _context.Interventions.Add(interventionForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInterventions),new {id = interventionForm.id}, interventionForm);
        }

        private bool interventionExists(long id)
        {
            return _context.Interventions.Any(e => e.id == id);
        }
    }
}
