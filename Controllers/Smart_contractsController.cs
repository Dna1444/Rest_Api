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
    public class smart_contractsController : ControllerBase
    {
        private readonly MaximeAuger_mysqlContext _context;
        

        public smart_contractsController(MaximeAuger_mysqlContext context)
        {
            _context = context;
        }

        // GET: api/smart_contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Smart_contracts>>> GetSmartContracts()
        {
            return await _context.Smart_contracts.ToListAsync();
        }

        //Updating the status of a given elevator. Frist, identification of the elevator is needed.
        // PUT: api/elevators/id/updatestatus        
        [HttpPut("addQualitySecurityHomologation/{solutionManufacturing}/{qualitySecurityHomologation}")]
        public async Task<ActionResult<List<Smart_contracts>>> PutQuality(string solutionManufacturing, string qualitySecurityHomologation)
        {
            
            
            var smart_Contracts = await _context.Smart_contracts.Where(s => s.solutionManufacturingAddress == solutionManufacturing).ToListAsync();
            long Id = 0;
            foreach (Smart_contracts e in smart_Contracts)
            {
                e.qualitySecurityHomologationAddress = qualitySecurityHomologation;
                Id = e.id;
            }

            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!smartContractsExists(Id))
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
        [HttpPut("addSolutionManufacturing/{materialProvider}/{solutionManufacturing}")]
        public async Task<ActionResult<List<Smart_contracts>>> PutSolution(string materialProvider, string solutionManufacturing)
        {
            
            
            var smart_Contracts = await _context.Smart_contracts.Where(s => s.materialProviderAddress == materialProvider).ToListAsync();
            long Id = 0;
            foreach (Smart_contracts e in smart_Contracts)
            {
                e.solutionManufacturingAddress = solutionManufacturing;
                Id = e.id;
            }

            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!smartContractsExists(Id))
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
        [HttpPut("addMaterialProvider/{projectOffice}/{materialProvider}")]
        public async Task<ActionResult<List<Smart_contracts>>> PutMaterial(string projectOffice, string materialProvider)
        {
            
            
            var smart_Contracts = await _context.Smart_contracts.Where(s => s.projectOfficeAddress == projectOffice).ToListAsync();
            long Id = 0;
            foreach (Smart_contracts e in smart_Contracts)
            {
                e.materialProviderAddress = materialProvider;
                Id = e.id;
            }

            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!smartContractsExists(Id))
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
        public async Task<ActionResult<IEnumerable<Smart_contracts>>> PostSmart_contracts(Smart_contracts smart_contracts)
        {
            Smart_contracts smart_ContractsForm = smart_contracts;
            _context.Smart_contracts.Add(smart_ContractsForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSmartContracts),new {id = smart_contracts.id}, smart_contracts);
        }

        private bool smartContractsExists(long id)
        {
            return _context.Smart_contracts.Any(e => e.id == id);
        }
    }
}
