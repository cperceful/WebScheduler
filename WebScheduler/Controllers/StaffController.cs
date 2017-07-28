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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebScheduler.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;

        public StaffController(UserManager<ApplicationUser> userManager)
        {
            

            this.userManager = userManager;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Availablility add/edit screen. URL should be /staff/availability/{userName}
        [HttpGet]
        
        public async Task<IActionResult> Availability(AddEditAvailabilityViewModel model, string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            ViewBag.availabilities = user.AvailabilitySet;
            return View(model);
        }
    }
}
