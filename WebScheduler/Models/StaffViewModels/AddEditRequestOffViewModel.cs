using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebScheduler.Models.StaffViewModels
{
    public class AddEditRequestOffViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        public string Notes { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
