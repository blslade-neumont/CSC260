using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Controllers
{
    public class CourseController : Controller
    {
        public CourseController(
            ICrudService<Student> studentService,
            ICrudService<Course> courseService,
            SchoolDbContext ctx
        ) {
            this.studentService = studentService;
            this.courseService = courseService;
            this.ctx = ctx;
        }
        
        private ICrudService<Student> studentService;
        private ICrudService<Course> courseService;
        private SchoolDbContext ctx;

        #region Create
        public IActionResult Create()
        {
            return View("Edit", new Course());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                course = await courseService.CreateAsync(course);
                if (course == null) return RedirectToAction("List");
                return RedirectToAction("View", new { course.Id });
            }
            else
            {
                return View("Edit", course);
            }
        }
        #endregion

        #region Read
        public IActionResult List()
        {
            var courses = ctx.Courses.Include(course => course.Teacher).AsEnumerable().ToArray();
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science",
                Courses = courses
            };
            return View(school);
        }

        public IActionResult View(int id)
        {
            var course = ctx.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .Where(c => c.Id == id)
                .FirstOrDefault();
            if (course == null) return RedirectToAction("List");
            return View(course);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Edit(int id)
        {
            var course = await this.courseService.GetAsync(id);
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (ModelState.IsValid)
            {
                await courseService.UpdateAsync(course);
                return RedirectToAction("View", new { course.Id });
            }
            else
            {
                return View(course);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(int id)
        {
            if (int.TryParse(Request.Form["StudentId"], out var studentId) && Enum.TryParse<LetterGrade>(Request.Form["LetterGrade"], out var letterGrade))
            {
                var student = await studentService.GetAsync(studentId);
                var course = await courseService.GetAsync(id);
                if (course != null && student != null)
                {
                    var preexisting = ctx.Enrollments
                        .Where(e => e.CourseId == id && e.StudentId == studentId)
                        .FirstOrDefault();
                    if (preexisting == null)
                    {
                        await ctx.Enrollments.AddAsync(new Enrollment()
                        {
                            StudentId = studentId,
                            CourseId = id,
                            Grade = letterGrade
                        });
                    }
                    else if (preexisting.Grade != letterGrade)
                    {
                        preexisting.Grade = letterGrade;
                        ctx.Enrollments.Update(preexisting);
                    }
                    await ctx.SaveChangesAsync();
                }
            }
            return RedirectToAction("View", new { Id = id });
        }
        
        public async Task<IActionResult> RemoveStudent(int id, int secondaryId)
        {
            var courseId = id;
            var studentId = secondaryId;
            var course = await courseService.GetAsync(id);
            var student = await studentService.GetAsync(studentId);
            if (course != null && student != null)
            {
                var preexisting = ctx.Enrollments
                    .Where(e => e.CourseId == id && e.StudentId == studentId)
                    .FirstOrDefault();
                if (preexisting != null)
                {
                    ctx.Remove(preexisting);
                    await ctx.SaveChangesAsync();
                }
            }
            return RedirectToAction("View", new { Id = id });
        }
        #endregion

        #region Destroy
        public async Task<IActionResult> Destroy(int id)
        {
            await this.courseService.DestroyAsync(id);
            return RedirectToAction("List");
        }
        #endregion
    }
}
