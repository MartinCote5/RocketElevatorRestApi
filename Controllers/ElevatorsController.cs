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
    public class ElevatorsController : ControllerBase
    {
        private readonly ElevatorsContext _context;

        public ElevatorsController(ElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        
        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevators>> GetElevators(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        [HttpGet("inactive")]
        public async Task<ActionResult<IEnumerable<Elevators>>> GetInactive()
        {
            var elevator = await _context.elevators.Where(x => x.Status == "inactive").ToListAsync();

            return elevator;
        }

        // PUT: api/Elevators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevators(long id, Elevators elevator)
        {
            if (id != elevator.Id)
            {
                return BadRequest();
            }

            _context.Entry(elevator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorsExists(id))
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

        // POST: api/Elevators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Elevators>> PostElevators(Elevators elevator)
        {
            _context.elevators.Add(elevator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElevators", new { id = elevator.Id }, elevator);
        }

        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevators(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            _context.elevators.Remove(elevator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElevatorsExists(long id)
        {
            return _context.elevators.Any(e => e.Id == id);
        }
    }
}
