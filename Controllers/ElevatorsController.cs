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
        private readonly CustomersContext _cuscontext;
        public ElevatorsController(ElevatorsContext context, CustomersContext cuscontext)
        {
            _context = context;
            _cuscontext = cuscontext;
        }
        
         // GET: api/Elevators/
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevatorsAll()
        {
            return await _context.elevators.ToListAsync();                 
        }

    
        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevators(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        [HttpGet("inactive")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetInactive()
        {
            var elevator = await _context.elevators.Where(x => x.Status == "inactive").ToListAsync();

            return elevator;
        }

        // [HttpGet("portalCustomerElevators")]
        // public async Task<ActionResult<IEnumerable<Elevator>>> GetPortalCustomerElevators()
        // {
            // var elevator = await _context.elevators.Where(x => x.Status == "inactive").ToListAsync();
            // var customer = await _cuscontext.customers.Where(x => x.e_mail == "inactive").ToListAsync();
            // var x = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
        //     return elevator;
        // }
        // PUT: api/Elevators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevators(long id, Elevator elevator)
        {
            if (id != elevator.Id)
            {
                return BadRequest();
            }
            var ev = await _context.elevators.Where(x => x.Id == id).ToListAsync();
            ev[0].Status = elevator.Status;
            _context.Entry(ev[0]).State = EntityState.Modified;

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

        private bool ElevatorsExists(long id)
        {
            return _context.elevators.Any(e => e.Id == id);
        }
    }
}
