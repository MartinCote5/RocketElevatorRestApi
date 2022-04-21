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


        // GET: api/Customers/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomerForPortal(string email)
        {
            User user = _userContext.users.Where(u => u.email == email).First();
            Customer customer = _context.customers.FirstOrDefault(c => c.user_id == user.Id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

    }
}