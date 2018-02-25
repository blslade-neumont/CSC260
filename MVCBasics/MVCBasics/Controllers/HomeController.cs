using Microsoft.AspNetCore.Mvc;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(SchoolDbInitializer dbInitializer)
        {
            this.dbInitializer = dbInitializer;
        }

        private SchoolDbInitializer dbInitializer;

        public IActionResult Index()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science"
            };
            return View(school);
        }

        #region Admin
        public async Task<IActionResult> SeedData()
        {
            await this.dbInitializer.SeedData();
            return RedirectToAction("Home");
        }
        #endregion
    }
}
