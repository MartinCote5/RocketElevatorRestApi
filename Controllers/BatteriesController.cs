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
    public class BatteriesController : ControllerBase
    {
        private readonly BatteriesContext _context;

        public BatteriesController(BatteriesContext context)
        {
            _context = context;
        }

        // GET: api/Battery
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batteries>>> GetBatteries()
        {
            return await _context.batteries.ToListAsync();
        }

        // GET: api/Battery/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Batteries>> GetBatteries(long id)
        {
            var batteries = await _context.batteries.FindAsync(id);

            if (batteries == null)
            {
                return NotFound();
            }

            return batteries;
        }

        // PUT: api/Battery/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatteries(long id, Batteries batteries)
        {
            if (id != batteries.Id)
            {
                return BadRequest();
            }

            _context.Entry(batteries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteriesExists(id))
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

        // POST: api/Battery
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Batteries>> PostBatteries(Batteries batteries)
        {
            _context.batteries.Add(batteries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBatteries", new { id = batteries.Id }, batteries);
        }

        // DELETE: api/Battery/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatteries(long id)
        {
            var batteries = await _context.batteries.FindAsync(id);
            if (batteries == null)
            {
                return NotFound();
            }

            _context.batteries.Remove(batteries);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatteriesExists(long id)
        {
            return _context.batteries.Any(e => e.Id == id);
        }
    }
}
