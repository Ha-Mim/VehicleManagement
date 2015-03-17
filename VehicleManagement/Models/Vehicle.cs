using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VehicleManagement.Models
{
    public class Vehicle
    {
        public int VehicleId { set; get; }
        [DisplayName("Reg No")]
        [Required]
        [Remote("RegNoExits", "Vehicle", ErrorMessage = "Reg No already exists")]
        public string RegNo { set; get; }
        [DisplayName("Engine No")]
        [Required]
        [Remote("EngineNoExits", "Vehicle", ErrorMessage = "Engine No already exists")]
        public string EngineNo { set; get; }
        public virtual ICollection<Schedule> Schedules { set; get; } 
    }
}