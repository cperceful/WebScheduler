using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebScheduler.Models.AdminViewModels
{
    public class AddEditShiftViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public int ScheduleId { get; set; }

        public List<SelectListItem> Users { get; set; }

        public AddEditShiftViewModel(IEnumerable<ApplicationUser> users)
        {
            Users = new List<SelectListItem>();

            foreach (ApplicationUser user in users)
            {
                if (user.IsActive) { 
                    Users.Add(new SelectListItem
                    {
                        Value = user.Id,
                        Text = (user.Name).ToString()
                    });
                }
            }

        }

        public AddEditShiftViewModel() { }
    }
}
