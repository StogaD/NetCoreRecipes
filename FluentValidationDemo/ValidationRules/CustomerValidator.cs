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


            RuleSet("Names", () =>
            {
                RuleFor(x => x.Surname).NotNull().MinimumLength(3);
                RuleFor(x => x.Forename).NotNull().MinimumLength(3);
            });


            RuleFor(customer => customer.Surname).NotEmpty().NotEqual("nowak");
            RuleFor(customer => customer.Address).NotNull().SetValidator(new AddressValidator());
            RuleFor(customer => customer.Discount).LessThan(70);
        }
    }
}
