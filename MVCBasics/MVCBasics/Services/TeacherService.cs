using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class TeacherService
    {
        private static List<Teacher> _teachers = new List<Teacher>();

        static TeacherService()
        {
            CreateTeachers();
        }
        private static void CreateTeachers()
        {
            Teacher Fahim = new Teacher("Fahim", Gender.Male, 2, TeacherStatus.Adjunct);
            TeacherService.Create(Fahim);

            Teacher Beatty = new Teacher("Beatty", Gender.Male, 6, TeacherStatus.FullTime);
            TeacherService.Create(Beatty);

            Teacher Lamb = new Teacher("Lamb", Gender.Male, 4, TeacherStatus.FullTime);
            TeacherService.Create(Lamb);

            Teacher Ray = new Teacher("Ray", Gender.Male, 1, TeacherStatus.FullTime);
            TeacherService.Create(Ray);
        }

        public static Teacher Create(Teacher teacher)
        {
            _teachers.Add(teacher);
            return teacher;
        }
        public static IEnumerable<Teacher> FindAll()
        {
            return _teachers.AsEnumerable();
        }
    }
}
