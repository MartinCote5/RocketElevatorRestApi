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
        private readonly BatteriesContext _btcontext;
        private readonly ColumnsContext _colcontext;
        private readonly ElevatorsContext _elcontext;

        public BuildingsController(BuildingsContext context, BatteriesContext btcontext, ColumnsContext colcontext, ElevatorsContext elcontext)
        {
            _context = context;
            _btcontext = btcontext;
            _colcontext = colcontext;
            _elcontext = elcontext;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> GetBuildings()
        {
            return await _context.buildings.ToListAsync();
        }

        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buildings>> GetBuildings(long id)
        {
            var buildings = await _context.buildings.FindAsync(id);

            if (buildings == null)
            {
                return NotFound();
            }

            return buildings;
        }

        [HttpGet("intervention")]
       public async Task<ActionResult<IEnumerable<Buildings>>> GetIntervention()
        {
            var buildings = await _context.buildings.ToListAsync();

            return buildings;
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
            _context.buildings.Add(buildings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildings", new { id = buildings.Id }, buildings);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuildings(long id)
        {
            var buildings = await _context.buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(buildings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingsExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
