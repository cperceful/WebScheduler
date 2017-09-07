using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebScheduler.Models
{
    public class Schedule
    {
        public int ID { get; set; }

        public DateTime StartDate { get; set; }

        public IList<Shift> Shifts { get; set; }

    }
}
