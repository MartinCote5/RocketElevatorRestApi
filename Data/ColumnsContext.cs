#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

public class ColumnsContext : DbContext
{
    public ColumnsContext(DbContextOptions<ColumnsContext> options)
        : base(options)
    {
    }

    public DbSet<Column> columns { get; set; }
}