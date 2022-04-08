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

        // GET: api/Interventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetInterventions(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }

        [HttpGet("pendingRequest")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventionRequestRecords()
        {
            // var interventionStatusPending = await _context.interventions.Where(x => x.Status == "pending").ToListAsync();
            // List<string> interventionStatusPendingList = new List<string>();
            // foreach(Intervention i in interventionStatusPending)
            // {
            //    string eachInterventionStatusPending = i.Status;
            //    interventionStatusPendingList.Add(eachInterventionStatusPending);
            // }

            // var interventionEmptyStartDate = await _context.interventions.Where(x => x.start_date_and_time_of_the_intervention == null).ToListAsync();
            // List<DateTime?> interventionEmptyStartDateList = new List<DateTime?>();
            // foreach(Intervention i in interventionEmptyStartDate)
            // {
            //    var eachInterventionEmptyStartDate = i.start_date_and_time_of_the_intervention;
            //    interventionEmptyStartDateList.Add(eachInterventionEmptyStartDate);
            // }

            // var intervention = await _context.interventions.Where(x => interventionStatusPendingList.Contains(x.Status) && interventionEmptyStartDateList.Contains(x.start_date_and_time_of_the_intervention)).ToListAsync();   
            // return intervention;

            var intervention = await _context.interventions.Where(x => x.Status == "Pending" && x.start_date_and_time_of_the_intervention == null).ToListAsync();
            
            
            
            return intervention;
        }

        // PUT: api/Interventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

          // PUT: api/Interventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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