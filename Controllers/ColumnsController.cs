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
    public class ColumnsController : ControllerBase
    {
        private readonly ColumnsContext _context;

        public ColumnsController(ColumnsContext context)
        {
            _context = context;
        }

        // GET: api/Battery
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Columns>>> GetColumns()
        {
            return await _context.columns.ToListAsync();
        }

        // GET: api/Battery/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Columns>> GetColumns(long id)
        {
            var columns = await _context.columns.FindAsync(id);

            if (columns == null)
            {
                return NotFound();
            }

            return columns;
        }

        // PUT: api/Battery/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColumns(long id, Columns columns)
        {
            if (id != columns.Id)
            {
                return BadRequest();
            }

            _context.Entry(columns).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnsExists(id))
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
        public async Task<ActionResult<Columns>> PostColumns(Columns columns)
        {
            _context.columns.Add(columns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColumns", new { id = columns.Id }, columns);
        }

        // DELETE: api/Battery/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumns(long id)
        {
            var columns = await _context.columns.FindAsync(id);
            if (columns == null)
            {
                return NotFound();
            }

            _context.columns.Remove(columns);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColumnsExists(long id)
        {
            return _context.columns.Any(e => e.Id == id);
        }
    }
}