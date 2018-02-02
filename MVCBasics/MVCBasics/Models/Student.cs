using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Models
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required, Range(1, 50)]
        public int Cohort { get; set; }
        public Degree? Degree { get; set; }
        [InFuture, RequiredWithDegree, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? GraduationDate { get; set; }
    }
}
