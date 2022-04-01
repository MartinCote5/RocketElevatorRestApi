using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RocketElevatorREST.Models
{
    public class Building_detailsContext : DbContext
    {
        public Building_detailsContext(DbContextOptions<Building_detailsContext> options)
            : base(options)
        {
        }

        public DbSet<Building_detail> TodoItems { get; set; } = null!;
    }
}