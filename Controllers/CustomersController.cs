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
    public class CustomersController : ControllerBase
    {
        private readonly CustomersContext _context;
        private readonly UsersContext _userContext;

        public CustomersController(CustomersContext context, UsersContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }


        // GET: api/Customers/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            User user = _userContext.users.Where(u => u.email == email).First();
            Customer customer = _context.customers.Where(c => c.user_id == user.Id).First();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Columns/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Column>> GetColumns(long id)
        // {
        //     var column = await _context.columns.FindAsync(id);

        //     if (column == null)
        //     {
        //         return NotFound();
        //     }

        //     return column;
        // }

        // PUT: api/Columns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutColumns(long id, Column column)
        // {
        //     if (id != column.Id)
        //     {
        //         return BadRequest();
        //     }
        //     var col = await _context.columns.Where(x => x.Id == id).ToListAsync();
        //     col[0].Status = column.Status;
        //     _context.Entry(col[0]).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ColumnsExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // private bool ColumnsExists(long id)
        // {
        //     return _context.columns.Any(e => e.Id == id);
        // }
    }
}