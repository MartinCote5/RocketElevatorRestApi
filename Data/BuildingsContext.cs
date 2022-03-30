#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

namespace RocketElevatorREST.Models
{
    public class BuildingsContext : DbContext
    {
        public BuildingsContext(DbContextOptions<BuildingsContext> options)
            : base(options)
        {
        }

        public DbSet<Buildings> Buildings { get; set; } = null!;
    }
}