using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        public IActionResult Statistics()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science"
            };
            return View(school);
        }
    }
}
