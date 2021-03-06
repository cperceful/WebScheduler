﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebScheduler.Models.AdminViewModels
{
    public class EditShiftViewModel
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public int ShiftId { get; set; }

        public ApplicationUser User { get; set; }

        public DayOfWeek Day { get; set; }

        public int ScheduleId { get; set; }

    }
}
