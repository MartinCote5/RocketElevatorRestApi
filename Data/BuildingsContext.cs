#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;


    public class BuildingsContext : DbContext
    {
        public BuildingsContext(DbContextOptions<BuildingsContext> options)
            : base(options)
        {
        }

<<<<<<< HEAD
        public DbSet<Buildings> buildings { get; set; } = null!;
    }
=======
        public DbSet<Building> buildings { get; set; } = null!;
    }
}
>>>>>>> development
