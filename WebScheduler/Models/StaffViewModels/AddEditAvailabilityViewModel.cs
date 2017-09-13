﻿using Microsoft.AspNetCore.Identity;
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
        [DataType(DataType.Time)]
        [Display(Name ="Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }


        
    }
}
