#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

public class InterventionsContext : DbContext
{
    public InterventionsContext(DbContextOptions<InterventionsContext> options)
        : base(options)
    {
    }

    public DbSet<Intervention> interventions { get; set; }
}