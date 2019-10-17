using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidationDemo.Models;

namespace FluentValidationDemo.ValidationRules
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Surname).NotEmpty();
            RuleFor(customer => customer.Address).NotNull();
        }
    }
}
