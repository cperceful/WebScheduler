﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebScheduler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebScheduler.Models.AdminViewModels;
using WebScheduler.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebScheduler.Controllers
{   
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //UserManager object for accessing user store
        private readonly UserManager<ApplicationUser> userManager;

        private ApplicationDbContext context;        

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext dBContext)
        {
            this.userManager = userManager;
            context = dBContext;            
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Availability()
        {
            IList<ApplicationUser> users = userManager.Users.Include(x => x.Availabilities).Where(x => x.UserName != "Admin").ToList();
            
            return View(users);
        }

        public IActionResult RequestsOff()
        {
            IList<ApplicationUser> users = userManager.Users.Include(x => x.RequestsOff).ToList();

            return View(users);
        }

        [HttpGet]
        public IActionResult CreateSchedule()
        {
            CreateScheduleViewModel model = new CreateScheduleViewModel();     
            return View();
        }

        [HttpPost]
        public IActionResult CreateSchedule(CreateScheduleViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Schedule newSchedule = new Schedule
            {
                StartDate = model.StartDate,
                IsPosted = false
            };

            context.Schedules.Add(newSchedule);
            context.SaveChanges();

            return Redirect($"/admin/editschedule/{newSchedule.ID}");
        }

        [HttpGet]
        [Route("admin/editschedule/{id}")]
        public IActionResult EditSchedule(int id)
        {
            Schedule schedule = context.Schedules.Include(x => x.Shifts).Single(x => x.ID == id);
            ViewBag.schedule = schedule;

            IEnumerable<ApplicationUser> users = context.Users.ToList();
            IEnumerable<Shift> shifts = context.Shifts.ToList();
            ViewBag.users = users;

            AddShiftViewModel model = new AddShiftViewModel(users, shifts, id);            

            
            return View(model);
        }

        [HttpPost]
        [Route("admin/editschedule/{id}")]
        public async Task<IActionResult> EditSchedule(AddShiftViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                Schedule schedule = context.Schedules.Include(x => x.Shifts).Single(x => x.ID == id);
                ViewBag.schedule = schedule;
                return View(model);
            }


            Shift newShift = new Shift
            {
                User = await userManager.FindByIdAsync(model.UserId),
                Day = model.Day,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Schedule = context.Schedules.Single(x => x.ID == id)
                
            };

            context.Shifts.Add(newShift);
            context.SaveChanges();
            return Redirect($"/admin/editschedule/{id}");
        }

        public IActionResult ViewSchedules()
        {
            IList<Schedule> schedules = context.Schedules.OrderByDescending(x => x.StartDate).ToList();
            return View(schedules);
        }

        [HttpGet]
        public IActionResult ManageStaff()
        {
            IEnumerable<ApplicationUser> users = context.Users.ToList();
            return View(users);
        }

        [HttpPost]
        [Route("admin/deactivate/{id}")]
        public IActionResult Deactivate(string id)
        {
            ApplicationUser user = context.Users.Single(x => x.Id == id);
            user.IsActive = false;
            context.SaveChanges();

            return RedirectToAction("ManageStaff");
        }

        [HttpPost]
        [Route("admin/activate/{id}")]
        public IActionResult Activate(string id)
        {
            ApplicationUser user = context.Users.Single(x => x.Id == id);
            user.IsActive = true;
            context.SaveChanges();
            return RedirectToAction("ManageStaff");
        }

        [HttpGet]
        [Route("admin/delete/{id}")]
        public IActionResult Delete(int id)
        {
            Schedule schedule = context.Schedules.Single(x => x.ID == id);
            return View(schedule);
        }

        [HttpPost]
        [Route("admin/deleteschedule/{id}")]
        public IActionResult DeleteSchedule(int id)
        {
            Schedule schedule = context.Schedules.Single(x => x.ID == id);
            context.Shifts.RemoveRange(context.Shifts.Where(x => x.ScheduleID == id));
            context.Schedules.Remove(schedule);
            context.SaveChanges();

            return RedirectToAction("viewschedules");
        }

        [HttpGet]
        [Route("admin/editshift/{id}")]
        public IActionResult EditShift(int id)
        {
            Shift shift = context.Shifts.Include(x => x.User).Include(x => x.Schedule).Single(x => x.ID == id);
            
            EditShiftViewModel model = new EditShiftViewModel
            {
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                ShiftId = shift.ID,
                User = shift.User,
                Day = shift.Day,
                ScheduleId = shift.ScheduleID
            };
            return View(model);
        }

        [HttpPost]
        [Route("admin/editshift/{id}")]
        public IActionResult EditShift(EditShiftViewModel model)
        {
            Shift shift = context.Shifts.Include(x => x.Schedule).Single(x => x.ID == model.ShiftId);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            shift.StartTime = model.StartTime;
            shift.EndTime = model.EndTime;
            context.SaveChanges();

            return Redirect($"/admin/editschedule/{shift.ScheduleID}");
        }

        [HttpPost]
        [Route("admin/postschedule/{id}")]
        public IActionResult PostSchedule(int id)
        {
            Schedule schedule = context.Schedules.Single(x => x.ID == id);
            schedule.IsPosted = true;
            context.SaveChanges();
            return RedirectToAction("viewschedules");
        }

        [HttpPost]
        [Route("admin/unpostschedule/{id}")]
        public IActionResult UnpostSchedule(int id)
        {
            Schedule schedule = context.Schedules.Single(x => x.ID == id);
            schedule.IsPosted = false;
            context.SaveChanges();
            return RedirectToAction("EditSchedule", routeValues: id);
        }

        [HttpPost]
        [Route("admin/deleteshift/{id}")]
        public IActionResult DeleteShift(int id, EditShiftViewModel model)
        {
            Shift shift = context.Shifts.Single(x => x.ID == id);
            context.Remove(shift);
            context.SaveChanges();

            return Redirect($"/admin/editschedule/{model.ScheduleId}");
        }

        //Remote validation methods
        public IActionResult ValidateStartDate(DateTime startDate)
        {
            return Json(CheckDate(startDate) ? "true" : $"A schedule with the start date {startDate.ToString("D")} already exists");
        }

        private bool CheckDate(DateTime date)
        {
            foreach (Schedule schedule in context.Schedules.ToList())
            {
                if (schedule.StartDate.Equals(date))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<IActionResult> ValidateShiftDay(DayOfWeek day, int ScheduleId, string UserId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(UserId);
            return Json(await CheckShiftDay(day, ScheduleId, UserId) ? "true" : $"{user.Name} already has a shift for {day} on this schedule. Edit it if you need to change times");
        }

        public async Task<bool> CheckShiftDay(DayOfWeek day, int scheduleId, string UserId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(UserId);

            foreach (Shift shift in context.Shifts.Where(x => x.User == user && x.ScheduleID == scheduleId).ToList())
            {
                if (shift.Day.Equals(day))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
