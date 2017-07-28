using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebScheduler.Models
{
    public class AvailabilitySet
    {

        public int ID { get; set; }       

        public IList<Availability> Availabilities { get; set; }

    }
}
