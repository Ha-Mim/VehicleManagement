using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VehicleManagement.Models
{
    public class Shift
    {
        public int ShiftId { set; get; }
        [DisplayName("Shift")]
        public string ShiftName { set; get; }
        public virtual ICollection<Schedule> Schedules { set; get; } 
        
    }
}