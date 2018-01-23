using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Models
{
    public class Student
    {
        public Student()
            : this("", Gender.Male, 33)
        {
        }
        public Student(string name, Gender gender, int cohort, School school = null)
            : this(name, gender, cohort, null, null, null)
        {
        }
        public Student(string name, Gender gender, int cohort, Degree? degree, DateTime? graduationDate, School school = null)
        {
            this.Name = name;
            this.Gender = gender;
            this.Cohort = cohort;
            this.Degree = degree;
            this.GraduationDate = graduationDate;
        }

        public int Id;

        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required, Range(1, 50)]
        public int Cohort { get; set; }
        public Degree? Degree { get; set; }
        [InFuture, RequiredWithDegree, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? GraduationDate { get; set; }
        public School School { get; set; }
    }
}
