using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class SchoolDbInitializer
    {
        public SchoolDbInitializer(ICrudService<Teacher> teacherService, ICrudService<Student> studentService)
        {
            this.teacherService = teacherService;
            this.studentService = studentService;
        }

        private ICrudService<Teacher> teacherService;
        private ICrudService<Student> studentService;

        public async Task SeedData()
        {
            await createTeachers();
            await createStudents();
        }

        private async Task createTeachers()
        {
            Teacher Fahim = new Teacher()
            {
                Name = "Fahim",
                Gender = Gender.Male,
                YearsTaught = 2,
                Status = TeacherStatus.Adjunct
            };
            await teacherService.CreateAsync(Fahim);

            Teacher Beatty = new Teacher()
            {
                Name = "Beatty",
                Gender = Gender.Male,
                YearsTaught = 6,
                Status = TeacherStatus.FullTime
            };
            await teacherService.CreateAsync(Beatty);

            Teacher Lamb = new Teacher()
            {
                Name = "Lamb",
                Gender = Gender.Male,
                YearsTaught = 4,
                Status = TeacherStatus.FullTime
            };
            await teacherService.CreateAsync(Lamb);

            Teacher Ray = new Teacher()
            {
                Name = "Ray",
                Gender = Gender.Male,
                YearsTaught = 1,
                Status = TeacherStatus.FullTime
            };
            await teacherService.CreateAsync(Ray);
        }

        private async Task createStudents()
        {
            Student JohnDoe = new Student()
            {
                Name = "John Doe",
                Gender = Gender.Male,
                Cohort = 31,
                Degree = Degree.BSCS,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(JohnDoe);

            Student BrandonSlade = new Student()
            {
                Name = "Brandon Slade",
                Gender = Gender.Male,
                Cohort = 31,
                Degree = Degree.BSGD,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(BrandonSlade);

            Student Justin = new Student()
            {
                Name = "Justin",
                Gender = Gender.Male,
                Cohort = 31,
                Degree = Degree.BSGD,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(Justin);

            Student Mary = new Student()
            {
                Name = "Mary",
                Gender = Gender.Female,
                Cohort = 33,
                Degree = Degree.BSWD,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(Mary);

            Student LazySue = new Student()
            {
                Name = "Lazy Sue",
                Gender = Gender.Female,
                Cohort = 32,
                Degree = Degree.BSTM,
                GraduationDate = new DateTime(2019, 9, 15)
            };
            await studentService.CreateAsync(LazySue);

            Student Damian = new Student()
            {
                Name = "Damian",
                Gender = Gender.Male,
                Cohort = 32,
                Degree = Degree.BSWD,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(Damian);

            Student Robert = new Student()
            {
                Name = "Robert",
                Gender = Gender.Male,
                Cohort = 33,
                Degree = Degree.BSCS,
                GraduationDate = new DateTime(2020, 9, 15)
            };
            await studentService.CreateAsync(Robert);
        }
    }
}
