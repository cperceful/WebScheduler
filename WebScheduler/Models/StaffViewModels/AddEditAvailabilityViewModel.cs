using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebScheduler.Models.StaffViewModels
{
    public class AddEditAvailabilityViewModel
    {
        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        public TimeSpan StarTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public List<SelectListItem> Days { get; set; }


        public AddEditAvailabilityViewModel() { }

        
    }
}
