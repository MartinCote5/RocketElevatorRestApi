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
        private readonly BatteriesContext _bcontext;

        public ColumnsController(ColumnsContext context, BatteriesContext bcontext)
        {
            _context = context;
            _bcontext = bcontext;
        }

        // GET: api/Columns/{id}
        [HttpGet("portal/{id}")]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumnsForPortal(long id)
        {
            Battery battery = _bcontext.batteries.Where(b => b.Id == id).First(); 
            var column = await _context.columns.Where(c => c.battery_id == battery.Id).ToListAsync(); 
            if (column == null)
            {
                return NotFound();
            }
            return column;
        }



        // GET: api/Batteries/{id}
        // [HttpGet("portal/{id}")]
        // public async Task<ActionResult<IEnumerable<Column>>> GetColumnForPortal2(long id)
        // {

        //     var column = await _context.columns.Where(bat => bat.Id == id).ToListAsync(); 

           

        //     List<long> columnBatteryList = new List<long>();
        //     foreach(Column c in column)
        //     {
        //        long columnBatteryId = c.battery_id;
        //        columnBatteryList.Add(columnBatteryId);
        //     }
            
        //     var battery = await _bcontext.batteries.Where(b => columnBatteryList.Contains(b.Id)).ToListAsync();
        //     if (battery == null)
        //     {
        //         return NotFound();
        //     }
        //     return column;
        // }

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

        private bool ColumnsExists(long id)
        {
            return _context.columns.Any(e => e.Id == id);
        }
    }
}