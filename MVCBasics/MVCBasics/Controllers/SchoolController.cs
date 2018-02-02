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
        public SchoolController(ICrudService<Teacher> teacherService, ICrudService<Student> studentService)
        {
            this.teacherService = teacherService;
            this.studentService = studentService;
        }

        private ICrudService<Teacher> teacherService;
        private ICrudService<Student> studentService;

        public async Task<IActionResult> ShowAll()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science",
                Students = (await studentService.FindAllAsync()).ToArray(),
                Teachers = (await teacherService.FindAllAsync()).ToArray()
            };
            return View(school);
        }

        public IActionResult CreateStudent()
        {
            return View("EditStudent", new Student());
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                await studentService.CreateAsync(student);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View("EditStudent", student);
            }
        }

        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await this.studentService.GetAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> EditStudent(int id, Student student)
        {
            if (ModelState.IsValid)
            {
                await studentService.UpdateAsync(student);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View(student);
            }
        }
        
        public async Task<IActionResult> DestroyStudent(int id)
        {
            await this.studentService.DestroyAsync(id);
            return RedirectToAction("ShowAll");
        }

        public IActionResult CreateTeacher()
        {
            return View("EditTeacher", new Teacher());
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                await teacherService.CreateAsync(teacher);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View("EditTeacher", teacher);
            }
        }

        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacher = await this.teacherService.GetAsync(id);
            return View(teacher);
        }
        [HttpPost]
        public async Task<IActionResult> EditTeacher(int id, Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                await teacherService.UpdateAsync(teacher);
                return RedirectToAction("ShowAll");
            }
            else
            {
                return View(teacher);
            }
        }
        
        public async Task<IActionResult> DestroyTeacher(int id)
        {
            await this.teacherService.DestroyAsync(id);
            return RedirectToAction("ShowAll");
        }
    }
}
