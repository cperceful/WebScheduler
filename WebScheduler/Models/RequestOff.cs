using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebScheduler.Models
{
    public class RequestOff
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
