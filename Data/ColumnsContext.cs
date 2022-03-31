#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

public class ColumnsContext : DbContext
{
    public ColumnsContext(DbContextOptions<BuildingsContext> options)
        : base(options)
    {
    }

    public DbSet<RocketElevatorREST.Models.Columns> TodoItems { get; set; }
}