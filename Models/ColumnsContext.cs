using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RocketElevatorREST.Models
{
    public class ColumnsContext : DbContext
    {
        public ColumnsContext(DbContextOptions<ColumnsContext> options)
            : base(options)
        {
        }

        public DbSet<Column> TodoItems { get; set; } = null!;
    }
}