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
    public class BuildingsController : ControllerBase
    {
        private readonly BuildingsContext _context;

        public BuildingsController(BuildingsContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> GetBuildings()
        {
            return await _context.Buildings.ToListAsync();
        }

        // GET: api/Buildings/5
        [HttpGet("intervention")]
        public async Task<ActionResult<IEnumerable<Buildings>>> GetBuildings(long id)
        {
            return await _context.Buildings.ToListAsync();
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildings(long id, Buildings buildings)
        {
            if (id != buildings.Id)
            {
                return BadRequest();
            }

            _context.Entry(buildings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingsExists(id))
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

        // POST: api/Buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Buildings>> PostBuildings(Buildings buildings)
        {
            _context.Buildings.Add(buildings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildings", new { id = buildings.Id }, buildings);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuildings(long id)
        {
            var buildings = await _context.Buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }

            _context.Buildings.Remove(buildings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingsExists(long id)
        {
            return _context.Buildings.Any(e => e.Id == id);
        }
    }
}
