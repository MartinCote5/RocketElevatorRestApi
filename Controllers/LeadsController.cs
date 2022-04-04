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
        private readonly CustomersContext _cuscontext;
        

        public LeadsController(LeadsContext context, CustomersContext cuscontext)
        {
            _context = context;
            _cuscontext = cuscontext;
        }

        // GET: api/Leads
        [HttpGet("{noncustomer}")]
        public async Task<ActionResult<IEnumerable<Lead>>> GetEmail()
        {
            var customer = await _cuscontext.customers.ToListAsync();
            List<string> customerEmailList = new List<string>();
            foreach(Customer c in customer)
            {
               string customerEmail = c.email_of_the_company_contact;
               customerEmailList.Add(customerEmail);
            }
            
            DateTime dateOfThe30LastDays = DateTime.Now.AddDays(-30);
            
            var lead = await _context.leads.ToListAsync();
            List<DateTime> leadCreationDateList = new List<DateTime>();
            foreach(Lead l in lead)
            {
                var leadCreationDate = l.created_at;
                if(leadCreationDate >= dateOfThe30LastDays)
                {
                    leadCreationDateList.Add(leadCreationDate);
                }
                
            }
            
            var leadListResult = await _context.leads.Where(x => !(customerEmailList.Contains(x.e_mail)) && leadCreationDateList.Contains(x.created_at)).ToListAsync();
            return leadListResult;
        }
    }
}
