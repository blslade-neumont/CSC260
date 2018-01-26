using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class StudentService : LocalCrudService<Student>
    {
        public StudentService()
        {
            CreateStudents();
        }
        
        private void CreateStudents()
        {
            Student JohnDoe = new Student("John Doe", Gender.Male, 31, Degree.BSCS, new DateTime(2018, 9, 15));
            Create(JohnDoe);

            Student BrandonSlade = new Student("Brandon Slade", Gender.Male, 31, Degree.BSGD, new DateTime(2018, 9, 15));
            Create(BrandonSlade);

            Student Justin = new Student("Justin", Gender.Male, 31, Degree.BSGD, new DateTime(2018, 9, 15));
            Create(Justin);

            Student Mary = new Student("Mary", Gender.Female, 33, Degree.BSWD, new DateTime(2020, 9, 15));
            Create(Mary);

            Student LazySue = new Student("Lazy Sue", Gender.Female, 32, Degree.BSTM, new DateTime(2019, 9, 15));
            Create(LazySue);

            Student Damian = new Student("Damian", Gender.Male, 32, Degree.BSWD, new DateTime(2019, 9, 15));
            Create(Damian);

            Student Robert = new Student("Robert", Gender.Male, 33, Degree.BSCS, new DateTime(2020, 9, 15));
            Create(Robert);
        }
    }
}
