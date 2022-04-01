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
        private readonly Building_detailsContext _bdcontext;
        private readonly BatteriesContext _bcontext;
        private readonly ColumnsContext _ccontext;
        private readonly ElevatorsContext _econtext;

        public BuildingsController(BuildingsContext context,Building_detailsContext bdcontext, BatteriesContext bcontext, ElevatorsContext econtext, ColumnsContext ccontext)
        {
            _context = context;
            _bdcontext = bdcontext;
            _bcontext = bcontext;
            _ccontext = ccontext;
            _econtext = econtext;
            
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building_detail>>> GetBuildings()
        {
            
            var elevator = await _econtext.elevators.Where(x => x.Status == "intervention").ToListAsync();

            
            
            // var elevatorIdArray =elevator[0].Id;
            
            
            List<long> elevatorColumnIdList = new List<long>();
            foreach(Elevator e in elevator)
            {
               long elevatorColumnId = e.column_id;
               elevatorColumnIdList.Add(elevatorColumnId);
            }

            var column = await _ccontext.columns.Where(x => x.Status == "intervention" || elevatorColumnIdList.Contains(x.Id)).ToListAsync();

            List<long> columnBatteryIdList = new List<long>();
            foreach(Column c in column)
            {
               long columnBatteryId = c.battery_id;
               columnBatteryIdList.Add(columnBatteryId);
            }

            var battery = await _bcontext.batteries.Where(x => x.Status == "intervention" || columnBatteryIdList.Contains(x.Id)).ToListAsync();

            // var ngg = elevator.AddRange(column);
           
            // var test = String.Concat(elevator, column);

            var building_details = await _bdcontext.building_details.Where(x => x.information_Key == "status" &&  x.value == "intervention").ToListAsync();

            // foreach (RocketElevatorREST.Models.Elevator item in elevator)
            // {
            //     Console.Write("testing");
            // }
            
        
            return building_details;
            // return await _context.buildings.ToListAsync(); 
            

        }

        // GET: api/Buildings/5
        // [HttpGet("intervention")]
        // public async Task<ActionResult<IEnumerable<Building>>> GetIntervention()
        // {
        //     // return await _context.buildings.ToListAsync();
        //     var building = await _context.buildings.Where(x => x.Id == 1).ToListAsync();

        //     return building;
        // }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildings(long id, Building buildings)
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
        public async Task<ActionResult<Building>> PostBuildings(Building buildings)
        {
            _context.buildings.Add(buildings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildings", new { id = buildings.Id }, buildings);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuildings(long id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingsExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
