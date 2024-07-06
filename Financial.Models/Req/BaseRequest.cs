using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Models.Req
{
    public record BaseRequest
    {
        public string? Validate()
        {
            List<ValidationResult> validationResult = [];

            Validator.TryValidateObject(this, new ValidationContext(this), validationResult, true);

            if (validationResult.Count > 0) return validationResult.First().ErrorMessage;
            else return null;
        }
    }
}
