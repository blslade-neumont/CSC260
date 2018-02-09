using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ICrudService<Course> courseService,
            SchoolDbContext ctx,
            SchoolDbInitializer dbInitializer
        ) {
            this.teacherService = teacherService;
            this.studentService = studentService;
            this.courseService = courseService;
            this.dbInitializer = dbInitializer;
            this.ctx = ctx;
        }

        private ICrudService<Teacher> teacherService;
        private ICrudService<Student> studentService;
        private ICrudService<Course> courseService;
        private SchoolDbInitializer dbInitializer;
        private SchoolDbContext ctx;

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
        public async Task<IActionResult> Student(int id)
        {
            var student = await this.studentService.GetAsync(id);
            if (student == null) return RedirectToAction("Students");
            return View(student);
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
        public async Task<IActionResult> Teacher(int id)
        {
            var teacher = await this.teacherService.GetAsync(id);
            if (teacher == null) return RedirectToAction("Teachers");
            return View(teacher);
        }

        public IActionResult Courses()
        {
            var courses = ctx.Courses.Include(course => course.Teacher).AsEnumerable().ToArray();
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science",
                Courses = courses
            };
            return View(school);
        }
        public IActionResult Course(int id)
        {
            var course = ctx.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .Where(c => c.Id == id)
                .FirstOrDefault();
            if (course == null) return RedirectToAction("Courses");
            return View(course);
        }

        public IActionResult Statistics()
        {
            var school = new SchoolViewModel()
            {
                Name = "Neumont College of Computer Science"
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
                student = await studentService.CreateAsync(student);
                return RedirectToAction("Student", new { student.Id });
            }
            else
            {
                return View("EditStudent", student);
            }
        }

        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await this.studentService.GetAsync(id);
            if (student == null) return RedirectToAction("Students");
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> EditStudent(int id, Student student)
        {
            if (ModelState.IsValid)
            {
                await studentService.UpdateAsync(student);
                return RedirectToAction("Student", new { Id = id });
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
                teacher = await teacherService.CreateAsync(teacher);
                if (teacher == null) return RedirectToAction("Teachers");
                return RedirectToAction("Teacher", new { teacher.Id });
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
                return RedirectToAction("Teacher", new { teacher.Id });
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
                course = await courseService.CreateAsync(course);
                return RedirectToAction("Course", new { course.Id });
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
                return RedirectToAction("Course", new { course.Id });
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

        [HttpPost]
        public async Task<IActionResult> AddStudentToCourse(int id)
        {
            if (!int.TryParse(Request.Form["StudentId"], out var studentId)) return RedirectToAction("Course", new { Id = id });
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
                        CourseId = id
                    });
                    await ctx.SaveChangesAsync();
                }
            }
            return RedirectToAction("Course", new { Id = id });
        }

        public async Task<IActionResult> RemoveStudentFromCourse(int id, int secondaryId)
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
            return RedirectToAction("Course", new { Id = id });
        }
        #endregion

        #region Admin
        public async Task<IActionResult> SeedData()
        {
            await this.dbInitializer.SeedData();
            return RedirectToAction("Home");
        }
        #endregion
    }
}
