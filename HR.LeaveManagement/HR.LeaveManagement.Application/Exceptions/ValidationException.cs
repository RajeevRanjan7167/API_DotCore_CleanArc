using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Error { get; set; }

        public ValidationException(ValidationResult validationResult ) 
        {
            foreach (var error in validationResult.Errors)
            {
                Error.Add(error.ErrorMessage);
            }
        }
    }
}
