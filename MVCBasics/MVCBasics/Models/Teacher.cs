using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Models
{
    public class Teacher
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required, Range(0, 99999)]
        public int YearsTaught { get; set; }
        [Required]
        public TeacherStatus Status { get; set; }
    }
}
