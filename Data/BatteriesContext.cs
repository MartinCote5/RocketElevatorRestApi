#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

    public class BatteriesContext : DbContext
    {
        public BatteriesContext (DbContextOptions<BatteriesContext> options)
            : base(options)
        {
        }

        public DbSet<Battery> batteries { get; set; }
    }


