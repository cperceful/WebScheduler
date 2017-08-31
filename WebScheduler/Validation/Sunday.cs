using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.Models.AdminViewModels;

namespace WebScheduler.Validation
{
    public class Sunday : ValidationAttribute
    {

        private DayOfWeek dayOfWeek;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CreateScheduleViewModel model = (CreateScheduleViewModel)validationContext.ObjectInstance;

            if (model.StartDate.DayOfWeek != DayOfWeek.Sunday)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Start Date must be a Sunday";
        }
    }
}
