using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidationDemo.Models;

namespace FluentValidationDemo.ValidationRules
{
    public class CustomerEmailValidator : AbstractValidator<Customer>
    {
        public CustomerEmailValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
        }
    }
}
