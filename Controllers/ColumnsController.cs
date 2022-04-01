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

        // GET: api/Columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumns()
        {
            return await _context.columns.ToListAsync();
        }

        // GET: api/Columns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> GetColumns(long id)
        {
            var column = await _context.columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return column;
        }

        // PUT: api/Columns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColumns(long id, Column column)
        {
            if (id != column.Id)
            {
                return BadRequest();
            }
            var col = await _context.columns.Where(x => x.Id == id).ToListAsync();
            col[0].Status = column.Status;
            _context.Entry(col[0]).State = EntityState.Modified;

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

        // POST: api/Columns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Column>> PostColumns(Column columns)
        {
            _context.columns.Add(columns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColumns", new { id = columns.Id }, columns);
        }

        // DELETE: api/Columns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumns(long id)
        {
            var column = await _context.columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }

            _context.columns.Remove(column);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColumnsExists(long id)
        {
            return _context.columns.Any(e => e.Id == id);
        }
    }
}