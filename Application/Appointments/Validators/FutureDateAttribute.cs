using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Appointments.Validators
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute() : base("Appointment date cannot be in the past.")
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null) return ValidationResult.Success;
            if (value is DateTime date && date < DateTime.Now)
            {
                return new ValidationResult("Appointment date cannot be in the past.");
            }

            return ValidationResult.Success;
        }
    }
}
