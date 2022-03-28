#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

    public class BatteryContext : DbContext
    {
        public BatteryContext (DbContextOptions<BatteryContext> options)
            : base(options)
        {
        }

        public DbSet<RocketElevatorREST.Models.Battery> Battery { get; set; }
    }
