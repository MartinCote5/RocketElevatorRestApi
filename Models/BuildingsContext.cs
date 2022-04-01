using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RocketElevatorREST.Models
{
    public class BuildingsContext : DbContext
    {
        public BuildingsContext(DbContextOptions<BuildingsContext> options)
            : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; } = null!;
    }
}