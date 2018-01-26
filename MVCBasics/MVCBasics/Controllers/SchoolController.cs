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
            ViewBag.studentStats  = studentService.GetStatistics();
            ViewBag.teacherStats = teacherService.GetStatistics();
            return View(school);
        }

        public IActionResult CreateStudent()
        {
            return View("EditStudent", new Student());
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
                return View("EditStudent", student);
            }
        }

        public IActionResult EditStudent(int id)
        {
            var student = this.studentService.Get(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult EditStudent(int id, Student student)
        {
            student.Id = id;
            if (ModelState.IsValid)
            {
                studentService.Update(student);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View(student);
            }
        }
        
        public IActionResult DestroyStudent(int id)
        {
            this.studentService.Destroy(id);
            return RedirectToAction("ShowAll");
        }

        public IActionResult CreateTeacher()
        {
            return View("EditTeacher", new Teacher());
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
                return View("EditTeacher", teacher);
            }
        }

        public IActionResult EditTeacher(int id)
        {
            var teacher = this.teacherService.Get(id);
            return View(teacher);
        }
        [HttpPost]
        public IActionResult EditTeacher(int id, Teacher teacher)
        {
            teacher.Id = id;
            if (ModelState.IsValid)
            {
                teacherService.Update(teacher);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View(teacher);
            }
        }
        
        public IActionResult DestroyTeacher(int id)
        {
            this.teacherService.Destroy(id);
            return RedirectToAction("ShowAll");
        }
    }
}
