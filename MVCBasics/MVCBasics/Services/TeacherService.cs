using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class TeacherService : LocalCrudService<Teacher>
    {
        public TeacherService()
        {
            CreateTeachers();
        }

        private void CreateTeachers()
        {
            Teacher Fahim = new Teacher("Fahim", Gender.Male, 2, TeacherStatus.Adjunct);
            Create(Fahim);

            Teacher Beatty = new Teacher("Beatty", Gender.Male, 6, TeacherStatus.FullTime);
            Create(Beatty);

            Teacher Lamb = new Teacher("Lamb", Gender.Male, 4, TeacherStatus.FullTime);
            Create(Lamb);

            Teacher Ray = new Teacher("Ray", Gender.Male, 1, TeacherStatus.FullTime);
            Create(Ray);
        }
    }
}
