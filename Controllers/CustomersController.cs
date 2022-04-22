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
        private readonly BuildingsContext _buildingContext;
        private readonly BatteriesContext _batteriesContext;
        private readonly ColumnsContext _columnsContext;
        private readonly ElevatorsContext _elevatorsContext;
        private readonly UsersContext _userContext;

        public CustomersController(CustomersContext context, UsersContext userContext, BuildingsContext buildingsContext, BatteriesContext batteriesContext, ColumnsContext columnsContext, ElevatorsContext elevatorsContext)
        {
            _context = context;
            _userContext = userContext;
            _buildingContext = buildingsContext;
            _batteriesContext = batteriesContext;
            _columnsContext = columnsContext;
            _elevatorsContext = elevatorsContext;
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

        // GET: api/Customers/Products/{id}
        [HttpGet("Produts/{id}")]
        public async Task<ActionResult<Product>> GetCustomerProducts(long id)
        {
            Customer customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            List<Building> buildings = await _buildingContext.buildings.Where(b => b.customer_id == customer.Id).ToListAsync();

            List<Battery> batteries = new List<Battery>();
            foreach(Building building in buildings) {
                List<Battery> buildingBatteries = await _batteriesContext.batteries.Where(b => b.building_id == building.Id).ToListAsync();
                foreach(Battery battery in buildingBatteries) {
                    batteries.Add(battery);
                }
            }

            List<Column> columns = new List<Column>();
            foreach(Battery battery in batteries) {
                List<Column> batteryColumns = await _columnsContext.columns.Where(b => b.battery_id == battery.Id).ToListAsync();
                foreach(Column column in batteryColumns) {
                    columns.Add(column);
                }
            }

            List<Elevator> elevators = new List<Elevator>();
            foreach(Column column in columns) {
                List<Elevator> columnElevators = await _elevatorsContext.elevators.Where(b => b.column_id == column.Id).ToListAsync();
                foreach(Elevator elevator in columnElevators) {
                    elevators.Add(elevator);
                }
            }



            Product customerProducts = new Product{
                buildings = buildings,
                batteries = batteries,
                columns = columns,
                elevators = elevators
            };

            return customerProducts;
        }

    }
}