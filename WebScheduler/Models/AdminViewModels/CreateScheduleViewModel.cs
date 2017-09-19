using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.Validation;

namespace WebScheduler.Models.AdminViewModels
{
    public class CreateScheduleViewModel
    {
        [Sunday]
        [Display(Name = "Select Starting Date", Description = "Starting date must be on a Sunday")]
        [Required]
        [Remote("ValidateStartDate", "Admin")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public CreateScheduleViewModel() { }

    }
}
