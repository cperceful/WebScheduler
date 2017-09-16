using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.Models;
using WebScheduler.Models.AdminViewModels;

namespace WebScheduler.Validation
{
    public class UniqueStartDate : ValidationAttribute
    {
        private string errorMessage = "Schedule for this start date already exists";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CreateScheduleViewModel model = (CreateScheduleViewModel)validationContext.ObjectInstance;

            foreach (Schedule schedule in model.Schedules)
            {
                if (schedule.StartDate == model.StartDate)
                {
                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;

        }
    }
}
