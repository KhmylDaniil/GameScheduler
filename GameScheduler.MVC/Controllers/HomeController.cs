﻿using Microsoft.AspNetCore.Mvc;

namespace GameScheduler.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}