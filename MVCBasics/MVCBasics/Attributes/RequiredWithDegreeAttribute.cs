using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class RequiredWithDegreeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valid = true;
            dynamic model = validationContext.ObjectInstance;
            if (model.Degree != null) valid = value != null;
            if (!valid) return new ValidationResult($"{validationContext.MemberName} is required when a degree has been selected.");
            return ValidationResult.Success;
        }
    }
}
