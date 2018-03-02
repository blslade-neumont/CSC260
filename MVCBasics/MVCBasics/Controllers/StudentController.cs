using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Controllers
{
    public class StudentController : Controller
    {
        public StudentController(
            ICrudService<Student> studentService,
            ICrudService<Course> courseService,
            SchoolDbContext ctx
        ) {
            this.studentService = studentService;
            this.ctx = ctx;
        }
        
        private ICrudService<Student> studentService;
        private SchoolDbContext ctx;

        #region Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View("Edit", new Student());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                student = await studentService.CreateAsync(student);
                if (student == null) return RedirectToAction("List");
                return RedirectToAction("View", new { student.Id });
            }
            else
            {
                return View("Edit", student);
            }
        }
        #endregion

        #region Read
        [Authorize(Roles = "Admin, Registrar")]
        public async Task<IActionResult> List()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science",
                Students = (await studentService.FindAllAsync()).ToArray()
            };
            return View(school);
        }

        [Authorize(Policy = "StudentIsSelf")]
        public IActionResult View(int id)
        {
            var student = ctx.Students
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Course)
                .Where(c => c.Id == id)
                .FirstOrDefault();
            if (student == null) return RedirectToAction("List");
            return View(student);
        }
        #endregion

        #region Update
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await this.studentService.GetAsync(id);
            if (student == null) return RedirectToAction("List");
            return View(student);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (ModelState.IsValid)
            {
                await studentService.UpdateAsync(student);
                return RedirectToAction("View", new { Id = id });
            }
            else
            {
                return View(student);
            }
        }
        #endregion

        #region Destroy
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Destroy(int id)
        {
            await this.studentService.DestroyAsync(id);
            return RedirectToAction("List");
        }
        #endregion
    }
}
