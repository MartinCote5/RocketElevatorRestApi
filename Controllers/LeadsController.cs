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
    public class LeadsController : ControllerBase
    {
        private readonly LeadsContext _context;
        private readonly UsersContext _userscontext;

        public LeadsController(LeadsContext context, UsersContext userscontext)
        {
            _context = context;
            _userscontext = userscontext;
        }

<<<<<<< HEAD
        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetEmail(string email)
        {
            return await _userscontext.users.Where(x => x.Email == email).ToListAsync();
        }
        

=======
>>>>>>> development
        // GET: api/Leads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lead>> GetLeads(long id)
        {
            var leads = await _context.leads.FindAsync(id);

            if (leads == null)
            {
                return NotFound();
            }

            return leads;
        }

        [HttpGet("nonuser")]
        public async Task<ActionResult<IEnumerable<Leads>>> GetNonuser()
        {
            return await _context.leads.ToListAsync();
        }

        // PUT: api/Leads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeads(long id, Lead leads)
        {
            if (id != leads.Id)
            {
                return BadRequest();
            }

            _context.Entry(leads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadsExists(id))
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

<<<<<<< HEAD
        // POST: api/Leads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Leads>> PostLeads(Leads leads)
        {
            _context.leads.Add(leads);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeads", new { id = leads.Id }, leads);
        }

        // DELETE: api/Leads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeads(long id)
        {
            var leads = await _context.leads.FindAsync(id);
            if (leads == null)
            {
                return NotFound();
            }

            _context.leads.Remove(leads);
            await _context.SaveChangesAsync();

            return NoContent();
        }

=======
>>>>>>> development
        private bool LeadsExists(long id)
        {
            return _context.leads.Any(e => e.Id == id);
        }
    }
}
