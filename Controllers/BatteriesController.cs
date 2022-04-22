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
        private readonly BuildingsContext _bcontext;

        public BatteriesController(BatteriesContext context, BuildingsContext bcontext)
        {
            _context = context;
            _bcontext = bcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            return await _context.batteries.ToListAsync();
        }

        // GET: api/Batteries/{id}
        [HttpGet("portal/{id}")]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteryForPortal(long id)
        {
            var building = _bcontext.buildings.Where(buil => buil.Id == id).First(); 
            var battery = await _context.batteries.Where(b => b.building_id == building.Id).ToListAsync(); 
            if (battery == null)
            {
                return NotFound();
            }
            return battery;
        }


        // GET: api/Batteries/{id}
        // [HttpGet("portal/{id}")]
        // public async Task<ActionResult<IEnumerable<Battery>>> GetBatteryForPortal2(long id)
        // {

        //     var battery = await _context.batteries.Where(bat => bat.Id == id).ToListAsync(); 

           

        //     List<long> batteryBuildingList = new List<long>();
        //     foreach(Battery b in battery)
        //     {
        //        long batteryBuildingId = b.building_id;
        //        batteryBuildingList.Add(batteryBuildingId);
        //     }
            
        //     var building = await _bcontext.buildings.Where(b => batteryBuildingList.Contains(b.Id)).ToListAsync();
        //     if (battery == null)
        //     {
        //         return NotFound();
        //     }
        //     return battery;
        // }


        

        // GET: api/Battery/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> GetBatteries(long id)
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
        public async Task<IActionResult> PutBatteries(long id, Battery battery)
        {
            if (id != battery.Id)
            {
                return BadRequest();
            }
            var bat = await _context.batteries.Where(x => x.Id == id).ToListAsync();
            bat[0].Status = battery.Status;
            _context.Entry(bat[0]).State = EntityState.Modified;

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

        private bool BatteriesExists(long id)
        {
            return _context.batteries.Any(e => e.Id == id);
        }
    }
}