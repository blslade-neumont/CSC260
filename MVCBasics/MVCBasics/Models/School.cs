using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Models
{
    public class School
    {
        public School(string name)
        {
            this.Name = name;
        }

        [Required]
        public string Name { get; set; }
        public Teacher[] Teachers { get; set; }
        public Student[] Students { get; set; }
    }
}
