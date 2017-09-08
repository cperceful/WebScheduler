using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace WebScheduler.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public IList<Availability> Availabilities { get; set; }

        public IList<RequestOff> RequestsOff { get; set; }

        public string Name { get; set; }

        public IList<Shift> Shifts { get; set; }

        public string GetShift(DayOfWeek day, int id)
        {
            string shift;

            try
            {
                shift = Shifts.Single(x => x.Day == day && x.ScheduleID == id).ToString();
            } catch (Exception ex) when (ex is ArgumentNullException || ex is InvalidOperationException)
            {
                shift = "--";
            }

            return shift;
            
        }

    }
}
