using Microsoft.AspNetCore.Mvc;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Controllers
{
    public class SchoolController : Controller
    {
        public SchoolController()
        {
        }

        public IActionResult ShowAll()
        {
            var school = SchoolService.FindAll().First();
            return View(school);
        }

        public IActionResult CreateStudent()
        {
            return View(new Student());
        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentService.Create(student);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View(student);
            }
        }

        public IActionResult CreateTeacher()
        {
            return View(new Teacher());
        }
        [HttpPost]
        public IActionResult CreateTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                TeacherService.Create(teacher);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View(teacher);
            }
        }
    }
}
