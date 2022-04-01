#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocketElevatorREST.Models;


    public class Building_detailsContext : DbContext
    {
        public Building_detailsContext(DbContextOptions<Building_detailsContext> options)
            : base(options)
        {
        }

        public DbSet<Building_detail> building_details { get; set; } = null!;
    }
