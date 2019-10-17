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
            RuleFor(customer => customer.Surname).NotEmpty().NotEqual("nowak");
            RuleFor(customer => customer.Address).NotNull().SetValidator(new AddressValidator());
            RuleFor(customer => customer.Discount).LessThan(70);
        }
    }
}
