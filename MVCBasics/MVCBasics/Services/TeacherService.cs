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

        public dynamic GetStatistics()
        {
            var allTeachers = this.FindAll().ToArray();
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
