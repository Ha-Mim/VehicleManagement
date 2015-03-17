using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VehicleManagement.Models
{
    public class VehicleDbContext:DbContext
    {
        public VehicleDbContext():base("VehicleDbConnectionString")
        { }
        public DbSet<Vehicle> Vehicles { set; get; }

        public System.Data.Entity.DbSet<VehicleManagement.Models.Schedule> Schedules { get; set; }

        public System.Data.Entity.DbSet<VehicleManagement.Models.Shift> Shifts { get; set; } 
    }
}