using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Models
{
    public class User : IdentityUser
    {
        public int? StudentId { get; set; } = null;
        [ForeignKey("StudentId")]
        public Student Student { get; set; } = null;

        public int? TeacherId { get; set; } = null;
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; } = null;
    }
}
