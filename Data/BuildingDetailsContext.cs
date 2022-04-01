#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

namespace RocketElevatorREST.Models
{
    public class BuildingDetailsContext : DbContext
    {
        public BuildingDetailsContext(DbContextOptions<BuildingsContext> options)
            : base(options)
        {
        }

        public DbSet<Building> building_details { get; set; } = null!;
    }
}