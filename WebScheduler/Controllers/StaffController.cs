using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebScheduler.Models.StaffViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebScheduler.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Availablility add/edit screen. URL should be /staff/availability/{id}
        [HttpGet]
        
        public IActionResult Availability(AddEditAvailabilityViewModel model, string userId)
        {
            return View(model);
        }
    }
}
