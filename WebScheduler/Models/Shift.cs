using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebScheduler.Models
{
    [DefaultValue("--")]
    public class Shift
    {
        public int ID { get; set; }

        public ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public Schedule Schedule { get; set; }
        public int ScheduleID { get; set; }

        public override string ToString()
        {
            return $"{StartTime} - {EndTime}";
        }





    }
}
