using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RocketElevatorREST.Models
{
    public class LeadsContext : DbContext
    {
        public LeadsContext(DbContextOptions<ElevatorsContext> options)
            : base(options)
        {
        }

        public DbSet<Leads> Leads { get; set; } = null!;
    }
}