using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class SchoolService : LocalCrudService<School>
    {
        public SchoolService()
        {
            CreateSchools();
        }
        
        private void CreateSchools()
        {
            School neumont = new School("Neumont College of Computer Science");
            this.DefaultSchool = Create(neumont);
        }

        public School DefaultSchool { get; private set; }
    }
}
