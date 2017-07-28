using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;


namespace WebScheduler.Models.StaffViewModels
{
    public class AddEditAvailabilityViewModel
    {
        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        [Display(Name ="Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }


        
    }
}
