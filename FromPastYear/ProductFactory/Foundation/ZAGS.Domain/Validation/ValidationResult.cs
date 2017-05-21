using System.Collections.Generic;
using System.Linq;
using Zags.Utility.Errors;

namespace ZAGS.Domain.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {

        }
        public bool IsValid { get; set; }

        public List<Error> Errors { get; internal set; } = new List<Error>();


        public static ValidationResult operator &(ValidationResult spec1, ValidationResult spec2)
        {
            return new ValidationResult
            {
                IsValid = spec1.IsValid && spec2.IsValid,
                Errors = spec1.Errors.Concat(spec2.Errors).ToList()
            };
        }
    }
}
