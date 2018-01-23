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
        public SchoolController(SchoolService schoolService, TeacherService teacherService, StudentService studentService)
        {
            this.schoolService = schoolService;
            this.teacherService = teacherService;
            this.studentService = studentService;
        }

        private SchoolService schoolService;
        private TeacherService teacherService;
        private StudentService studentService;

        public IActionResult ShowAll()
        {
            var school = schoolService.DefaultSchool;
            school.Students = studentService.FindAll().ToArray();
            school.Teachers = teacherService.FindAll().ToArray();
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
                studentService.Create(student);
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
                teacherService.Create(teacher);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View(teacher);
            }
        }
    }
}
