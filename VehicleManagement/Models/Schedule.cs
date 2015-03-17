using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleManagement.Models
{
    public class Schedule
    {
        public int ScheduleId { set; get; }
        public int VehicleId { set; get; }
        public virtual Vehicle Vehicles { set; get; }
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime Dates { set; get; }
        public int ShiftId { set; get; }
        public virtual Shift Shifts { set; get; }
        [DisplayName("Booked By")]
        public string BookedBy { set; get; }
        [DataType(DataType.MultilineText)]
        public string Address { set; get; }
    }
}