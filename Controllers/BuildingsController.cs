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
        [HttpGet("{intervention}")]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            var gogo = await _bdcontext.building_details.Where(x => x.email == "intervention").ToListAsync(); 
            var test = await _context.buildings.ToListAsync(); 
            var x = test.id[1];
            var elevator = await _econtext.elevators.Where(x => x.Status == "intervention").ToListAsync();        
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

            List<long> batteryBuildingIdList = new List<long>();
            foreach(Battery b in battery)
            {
               long batteryBuildingId = b.building_id;
               batteryBuildingIdList.Add(batteryBuildingId);
            }

            var building_detail = await _bdcontext.building_details.Where(x => x.information_Key == "status" &&  x.value == "intervention").ToListAsync();

            List<long> buildingDetailBuildingIdList = new List<long>();
            foreach(Building_detail bd in building_detail)
            {
               long buildingDetailBuildingId = bd.building_id;
               buildingDetailBuildingIdList.Add(buildingDetailBuildingId);
            }

             var building = await _context.buildings.Where(x => batteryBuildingIdList.Contains(x.Id) || buildingDetailBuildingIdList.Contains(x.Id) ).ToListAsync();   
        
            return building;
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildings(long id, Building buildings)
        {
            var x = buildings.Id;
            
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
        
        private bool BuildingsExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
