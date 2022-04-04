#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

public class LeadsContext : DbContext
{
    public LeadsContext(DbContextOptions<LeadsContext> options)
        : base(options)
    {
    }

    public DbSet<Lead> leads { get; set; }
}