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
            return View(model);
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
                StartDate = model.StartDate
            };

            context.Schedules.Add(newSchedule);
            context.SaveChanges();

            return Redirect($"/admin/editschedule/{newSchedule.ID}");
        }

        [HttpGet]
        [Route("admin/editschedule/{id}")]
        public IActionResult EditSchedule(int id)
        {
            Schedule schedule = context.Schedules.Single(x => x.ID == id);
            EditScheduleViewModel model = new EditScheduleViewModel
            {
                StartDate = schedule.StartDate,
                id = schedule.ID
            };

            //TODO: build view for editing schedule
            return View(model);
        }
    }
}
