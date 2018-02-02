using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Models
{
    public class SchoolViewModel
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }
        public Teacher[] Teachers { get; set; }
    }
}
