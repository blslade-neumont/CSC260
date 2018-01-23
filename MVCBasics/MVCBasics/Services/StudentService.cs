using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class StudentService
    {
        private static List<Student> _students = new List<Student>();

        static StudentService()
        {
            CreateStudents();
        }
        private static void CreateStudents()
        {
            Student JohnDoe = new Student("John Doe", Gender.Male, 31, Degree.BSCS, new DateTime(2018, 9, 15));
            StudentService.Create(JohnDoe);

            Student BrandonSlade = new Student("Brandon Slade", Gender.Male, 31, Degree.BSGS, new DateTime(2018, 9, 15));
            StudentService.Create(BrandonSlade);

            Student Justin = new Student("Justin", Gender.Male, 31, Degree.BSGS, new DateTime(2018, 9, 15));
            StudentService.Create(Justin);

            Student Mary = new Student("Mary", Gender.Female, 33, Degree.BSWD, new DateTime(2020, 9, 15));
            StudentService.Create(Mary);

            Student LazySue = new Student("Lazy Sue", Gender.Female, 32, Degree.BSTM, new DateTime(2019, 9, 15));
            StudentService.Create(LazySue);

            Student Damian = new Student("Damian", Gender.Male, 32, Degree.BSWD, new DateTime(2019, 9, 15));
            StudentService.Create(Damian);

            Student Robert = new Student("Robert", Gender.Male, 33, Degree.BSCS, new DateTime(2020, 9, 15));
            StudentService.Create(Robert);
        }

        public static Student Create(Student student)
        {
            _students.Add(student);
            return student;
        }
        public static IEnumerable<Student> FindAll()
        {
            return _students.AsEnumerable();
        }
    }
}
