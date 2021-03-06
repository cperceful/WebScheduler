﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebScheduler.Models;

namespace WebScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);
                if (await userManager.IsInRoleAsync(user, "Admin"))
                {
                    return Redirect("/admin");
                }
                else
                {
                    return Redirect("/staff");
                }
            }
            
        }

        public IActionResult About()
        {
            ViewData["Message"] = "WebScheduler is an ASP.Net Core webapp utilizing the ASP.Net Identity framework";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Developed by Cameron Perceful";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
