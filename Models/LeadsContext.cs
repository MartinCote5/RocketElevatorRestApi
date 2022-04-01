using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RocketElevatorREST.Models
{
    public class LeadsContext : DbContext
    {
        public LeadsContext(DbContextOptions<LeadsContext> options)
            : base(options)
        {
        }

        public DbSet<Lead> Leads { get; set; } = null!;
    }
}