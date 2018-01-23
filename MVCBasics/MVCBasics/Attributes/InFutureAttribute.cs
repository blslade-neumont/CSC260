using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class InFutureAttribute : ValidationAttribute
    {
        public InFutureAttribute(bool required = false)
        {
            this.IsRequired = required;
        }

        public bool IsRequired { get; private set; }

        protected override ValidationResult IsValid(dynamic value, ValidationContext validationContext)
        {
            var valid = true;
            if (value == null) valid = !IsRequired;
            else if (value < DateTime.Now) valid = false;
            if (!valid) return new ValidationResult($"{validationContext.DisplayName} is required and must be in the future!");
            return ValidationResult.Success;
        }
    }
}
