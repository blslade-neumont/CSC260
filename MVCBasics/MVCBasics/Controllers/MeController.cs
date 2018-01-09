using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCBasics.Models;

namespace MVCBasics.Controllers
{
    public class MeController : Controller
    {
        MeViewModel model = new MeViewModel { Name = "Brandon Slade", Email = "brandonyoyoslade@gmail.com" };

        public IActionResult Index()
        {
            return View(model);
        }
    }
}
