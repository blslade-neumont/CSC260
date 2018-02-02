using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class StatisticsService
    {
        public StatisticsService(ICrudService<Student> studentService, ICrudService<Teacher> teacherService)
        {
            this.studentService = studentService;
            this.teacherService = teacherService;
        }

        private ICrudService<Student> studentService;
        private ICrudService<Teacher> teacherService;

        public dynamic GetStatistics()
        {
            return new Anon(new
            {
                Student = this.getStudentStatistics(),
                Teacher = this.getTeacherStatistics()
            });
        }
        private dynamic getStudentStatistics()
        {
            var allStudents = this.studentService.FindAll().ToArray();
            return new Anon(new
            {
                Count = allStudents.Length,
                Genders = new Anon(new
                {
                    Male = allStudents.Length == 0 ? 0 : Math.Floor(((double)allStudents.Count(s => s.Gender == Gender.Male) / allStudents.Length) * 100d),
                    Female = allStudents.Length == 0 ? 0 : Math.Floor(((double)allStudents.Count(s => s.Gender == Gender.Female) / allStudents.Length) * 100d)
                }),
                Degrees = new Anon(new
                {
                    Undecided = allStudents.Length == 0 ? 0 : Math.Floor(((double)allStudents.Count(s => s.Degree == null) / allStudents.Length) * 100d),
                    BSGD = allStudents.Length == 0 ? 0 : Math.Floor(((double)allStudents.Count(s => s.Degree == Degree.BSGD) / allStudents.Length) * 100d),
                    BSCS = allStudents.Length == 0 ? 0 : Math.Floor(((double)allStudents.Count(s => s.Degree == Degree.BSCS) / allStudents.Length) * 100d),
                    BSIS = allStudents.Length == 0 ? 0 : Math.Floor(((double)allStudents.Count(s => s.Degree == Degree.BSIS) / allStudents.Length) * 100d),
                    BSTM = allStudents.Length == 0 ? 0 : Math.Floor(((double)allStudents.Count(s => s.Degree == Degree.BSTM) / allStudents.Length) * 100d),
                    BSWD = allStudents.Length == 0 ? 0 : Math.Floor(((double)allStudents.Count(s => s.Degree == Degree.BSWD) / allStudents.Length) * 100d)
                })
            });
        }
        private dynamic getTeacherStatistics()
        {
            var allTeachers = this.teacherService.FindAll().ToArray();
            return new Anon(new
            {
                Count = allTeachers.Length,
                Genders = new Anon(new
                {
                    Male = allTeachers.Length == 0 ? 0 : Math.Floor(((double)allTeachers.Count(s => s.Gender == Gender.Male) / allTeachers.Length) * 100d),
                    Female = allTeachers.Length == 0 ? 0 : Math.Floor(((double)allTeachers.Count(s => s.Gender == Gender.Female) / allTeachers.Length) * 100d)
                }),
                Status = new Anon(new
                {
                    FullTime = allTeachers.Length == 0 ? 0 : Math.Floor(((double)allTeachers.Count(s => s.Status == TeacherStatus.FullTime) / allTeachers.Length) * 100d),
                    Adjunct = allTeachers.Length == 0 ? 0 : Math.Floor(((double)allTeachers.Count(s => s.Status == TeacherStatus.Adjunct) / allTeachers.Length) * 100d)
                }),
                AverageYearsTaught = allTeachers.Length == 0 ? 0 : (double)allTeachers.Aggregate(0, (old, t) => old + t.YearsTaught) / allTeachers.Length
            });
        }
    }
}
