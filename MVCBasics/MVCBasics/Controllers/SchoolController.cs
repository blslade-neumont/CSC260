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
        public SchoolController(
            ICrudService<Teacher> teacherService,
            ICrudService<Student> studentService,
            ICrudService<Course> courseService
        ) {
            this.teacherService = teacherService;
            this.studentService = studentService;
            this.courseService = courseService;
        }

        private ICrudService<Teacher> teacherService;
        private ICrudService<Student> studentService;
        private ICrudService<Course> courseService;

        #region List Views
        public IActionResult Home()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science"
            };
            return View(school);
        }

        public async Task<IActionResult> Students()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science",
                Students = (await studentService.FindAllAsync()).ToArray()
            };
            return View(school);
        }

        public async Task<IActionResult> Teachers()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science",
                Teachers = (await teacherService.FindAllAsync()).ToArray()
            };
            return View(school);
        }

        public async Task<IActionResult> Courses()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science",
                Courses = (await courseService.FindAllAsync()).ToArray()
            };
            return View(school);
        }
        #endregion

        #region Students
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
                return RedirectToAction("Students");
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
                return RedirectToAction("Students");
            }
            else
            {
                return View(student);
            }
        }
        
        public async Task<IActionResult> DestroyStudent(int id)
        {
            await this.studentService.DestroyAsync(id);
            return RedirectToAction("Students");
        }
        #endregion

        #region Teachers
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
                return RedirectToAction("Teachers");
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
                return RedirectToAction("Teachers");
            }
            else
            {
                return View(teacher);
            }
        }
        
        public async Task<IActionResult> DestroyTeacher(int id)
        {
            await this.teacherService.DestroyAsync(id);
            return RedirectToAction("Teachers");
        }
        #endregion

        #region Courses
        public IActionResult CreateCourse()
        {
            return View("EditCourse", new Course());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                await courseService.CreateAsync(course);
                return RedirectToAction("Courses");
            }
            else
            {
                return View("EditCourse", course);
            }
        }

        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await this.courseService.GetAsync(id);
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> EditCourse(int id, Course course)
        {
            if (ModelState.IsValid)
            {
                await courseService.UpdateAsync(course);
                return RedirectToAction("Courses");
            }
            else
            {
                return View(course);
            }
        }

        public async Task<IActionResult> DestroyCourse(int id)
        {
            await this.courseService.DestroyAsync(id);
            return RedirectToAction("Courses");
        }
        #endregion
    }
}
