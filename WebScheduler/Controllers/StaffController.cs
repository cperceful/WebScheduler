﻿using System;
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

        //Availablility add/edit screen. URL should be /staff/availability/{userName}
        [HttpGet]        
        public IActionResult Availability(string userName)
        {
            IList<Availability> availabilities = context.AvailabilitySet.Include(x => x.ApplicationUser).Where(x => x.ApplicationUserId == userManager.GetUserId(User)).ToList();
            ViewBag.availabilities = availabilities;

            AddEditAvailabilityViewModel model = new AddEditAvailabilityViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Availability(AddEditAvailabilityViewModel model, string userName)
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

            

            
            return Redirect($"/staff/availability/{userName}");
        }
    }
}
