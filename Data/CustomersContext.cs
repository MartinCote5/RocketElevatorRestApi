#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;

public class CustomersContext : DbContext
{
    public CustomersContext(DbContextOptions<CustomersContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> customers { get; set; }
}