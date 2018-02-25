using Microsoft.AspNetCore.Mvc;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Controllers
{
    public class TeacherController : Controller
    {
        public TeacherController(
            ICrudService<Teacher> teacherService,
            ICrudService<Student> studentService
        ) {
            this.teacherService = teacherService;
        }

        private ICrudService<Teacher> teacherService;

        #region Create
        public IActionResult Create()
        {
            return View("Edit", new Teacher());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher = await teacherService.CreateAsync(teacher);
                if (teacher == null) return RedirectToAction("List");
                return RedirectToAction("View", new { teacher.Id });
            }
            else
            {
                return View("Edit", teacher);
            }
        }
        #endregion

        #region Read
        public async Task<IActionResult> List()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science",
                Teachers = (await teacherService.FindAllAsync()).ToArray()
            };
            return View(school);
        }

        public async Task<IActionResult> View(int id)
        {
            var teacher = await this.teacherService.GetAsync(id);
            if (teacher == null) return RedirectToAction("List");
            return View(teacher);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await this.teacherService.GetAsync(id);
            return View(teacher);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                await teacherService.UpdateAsync(teacher);
                return RedirectToAction("View", new { teacher.Id });
            }
            else
            {
                return View(teacher);
            }
        }
        #endregion

        #region Destroy
        public async Task<IActionResult> Destroy(int id)
        {
            await this.teacherService.DestroyAsync(id);
            return RedirectToAction("List");
        }
        #endregion
    }
}
