#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

public class ElevatorsContext : DbContext
{
    public ElevatorsContext(DbContextOptions<ElevatorsContext> options)
        : base(options)
    {
    }

    public DbSet<RocketElevatorREST.Models.Elevator> elevators { get; set; }
}