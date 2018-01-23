using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Models
{
    public class Teacher
    {
        public Teacher()
            : this("", Gender.Male, 0, TeacherStatus.Adjunct)
        {
        }
        public Teacher(string name, Gender gender, int yearsTaught, TeacherStatus status, School school = null)
        {
            this.Name = name;
            this.Gender = Gender;
            this.YearsTaught = yearsTaught;
            this.Status = status;
            this.School = school ?? SchoolService.FindAll().FirstOrDefault();
        }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required, Range(0, 99999)]
        public int YearsTaught { get; set; }
        [Required]
        public TeacherStatus Status { get; set; }
        public School School { get; set; }
    }
}
