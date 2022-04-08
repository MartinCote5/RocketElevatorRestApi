#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

namespace RocketElevatorREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly InterventionsContext _context;

        public InterventionsController(InterventionsContext context)
        {
            _context = context;
        }

        // GET: api/Interventions/pendingRequest
        [HttpGet("pendingRequest")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventionRequestRecords()
        {
            var intervention = await _context.interventions.Where(x => x.Status == "Pending" && x.start_date_and_time_of_the_intervention == null).ToListAsync();
            return intervention;
        }

        // PUT: api/Interventions/inProgress/2
        [HttpPut("inProgress/{id}")]
        public async Task<IActionResult> PutInterventionsInProgress(long id, Intervention intervention)
        {
            if (id != intervention.Id)
            {
                return BadRequest();
            }
            var interv = await _context.interventions.Where(x => x.Id == id).ToListAsync();

            DateTime datenow = DateTime.Now;
            interv[0].result = "Incomplete";
            interv[0].Status = "InProgress";
            interv[0].start_date_and_time_of_the_intervention = datenow;
            interv[0].end_date_and_time_of_the_intervention = null;
            

            _context.Entry(interv[0]).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionsExists(id))
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

        // PUT: api/Interventions/Completed/4
        [HttpPut("completed/{id}")]
        public async Task<IActionResult> PutInterventionsCompleted(long id, Intervention intervention)
        {
            if (id != intervention.Id)
            {
                return BadRequest();
            }
            var interv = await _context.interventions.Where(x => x.Id == id).ToListAsync();
            
            DateTime datenow = DateTime.Now;
            interv[0].result = "Completed";
            interv[0].Status = "Completed";
            interv[0].end_date_and_time_of_the_intervention = datenow;

            _context.Entry(interv[0]).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionsExists(id))
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

        private bool InterventionsExists(long id)
        {
            return _context.interventions.Any(e => e.Id == id);
        }
    }
}