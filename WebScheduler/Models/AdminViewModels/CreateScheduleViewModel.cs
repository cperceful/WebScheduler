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
        //[UniqueStartDate] THIS IS DUMB FIX IT
        public DateTime StartDate { get; set; }

        public List<Schedule> Schedules { get; set; }

        public CreateScheduleViewModel() { }

        public CreateScheduleViewModel(List<Schedule> schedules)
        {
            Schedules = new List<Schedule>(schedules);
        }
    }
}
