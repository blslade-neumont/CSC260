using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        
        public IActionResult Index()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science"
            };
            return View(school);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
