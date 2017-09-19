using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebScheduler.Models.StaffViewModels;
using System.Collections;
using WebScheduler.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebScheduler.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebScheduler.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private ApplicationDbContext context;

        public StaffController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {

            context = dbContext;
            this.userManager = userManager;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]        
        public IActionResult Availability()
        {
            IList<Availability> availabilities = context.AvailabilitySet.Include(x => x.ApplicationUser).Where(x => x.ApplicationUserId == userManager.GetUserId(User)).ToList();
            ViewBag.availabilities = availabilities;

            AddEditAvailabilityViewModel model = new AddEditAvailabilityViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Availability(AddEditAvailabilityViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Availability newAvailability = new Availability
            {
                Day = model.Day,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                ApplicationUser = user
                
            };

            context.AvailabilitySet.Add(newAvailability);
            context.SaveChanges();          

            
            return Redirect("/staff/availability");
     
        }

        [HttpGet]
        public IActionResult RequestOff()
        {
            AddEditRequestOffViewModel model = new AddEditRequestOffViewModel();

            IList<RequestOff> requestsOff = context.RequestsOff.Include(x => x.ApplicationUser).Where(x => x.ApplicationUserId == userManager.GetUserId(User)).ToList();
            ViewBag.requestsOff = requestsOff;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RequestOff(AddEditRequestOffViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            RequestOff requestOff = new RequestOff
            {
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Notes = model.Notes,
                ApplicationUser = user

            };

            context.RequestsOff.Add(requestOff);
            context.SaveChanges();

            return Redirect("/staff/requestoff");
        }

        public IActionResult Schedules()
        {
            IList<Schedule> schedules = context.Schedules.OrderByDescending(x => x.StartDate).ToList();
            return View(schedules);
        }

        [Route("staff/viewschedule/{id}")]
        public IActionResult ViewSchedule(int id)
        {
            Schedule schedule = context.Schedules.Include(x => x.Shifts).Single(x => x.ID == id);
            ViewBag.users = context.Users.ToList();
            return View(schedule);
        }

        public async Task<IActionResult> ValidateRequestOff(DateTime date)
        {
            return Json(await CheckRequestsOff(date) ? "true" : $"A request off for {date.ToString("D")} already exists");
        }

        private async Task<bool> CheckRequestsOff(DateTime date)
        {
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);
            foreach (RequestOff ro in context.RequestsOff.Where(x => x.ApplicationUser == user))
            {
                if (ro.Date.Equals(date))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
