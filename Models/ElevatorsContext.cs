using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RocketElevatorREST.Models
{
    public class ElevatorsContext : DbContext
    {
        public ElevatorsContext(DbContextOptions<ElevatorsContext> options)
            : base(options)
        {
        }

        public DbSet<Elevator> TodoItems { get; set; } = null!;
    }
}