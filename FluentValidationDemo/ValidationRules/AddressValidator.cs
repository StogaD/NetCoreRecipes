using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidationDemo.Models;

namespace FluentValidationDemo.ValidationRules
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {

            RuleFor(address => address.Postcode).NotNull();
            RuleFor(address => address.City)
                .NotEmpty()
                .WithMessage("City is required")
                .Must(y => y.Equals("Wrocław", StringComparison.InvariantCultureIgnoreCase))
                .When(x => x.Postcode != null && x.Postcode.StartsWith("50-"));

            RuleFor(address => address.StreetLines).NotNull().ForEach(x => x.MaximumLength(20));
            RuleFor(address => address.Number).GreaterThan(0);

        }
    }
}
