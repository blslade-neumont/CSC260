using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class SchoolService
    {
        private static List<School> _schools = new List<School>() { new School("Neumont College of Computer Science") };
        
        public static School Create(School school)
        {
            _schools.Add(school);
            return school;
        }
        public static IEnumerable<School> FindAll()
        {
            foreach (var school in _schools)
            {
                var students = StudentService.FindAll().Where(student => student.School == school).ToArray();
                var teachers = TeacherService.FindAll().Where(teacher => teacher.School == school).ToArray();
                school.Students = students;
                school.Teachers = teachers;
                yield return school;
            }
        }
    }
}
