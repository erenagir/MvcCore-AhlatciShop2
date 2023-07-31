using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Exceptions
{
    public class ValidateException:Exception
    {
        public List<string> ErrorMessage { get; set; }
        public ValidateException(ValidationResult validationResult):base()
        {
            ErrorMessage=validationResult.Errors.Select(x=> x.ErrorMessage).ToList();
        }
    }
}
