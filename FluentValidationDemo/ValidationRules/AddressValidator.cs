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
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("City is required")
                .Must(y => y.Equals("Wrocław", StringComparison.InvariantCultureIgnoreCase))
                .When(x => x.Postcode != null && x.Postcode.StartsWith("50-"));

            //Other conditional rule
            When(addr => addr.Postcode != null && addr.Postcode.StartsWith("50-"), () =>
            {
                RuleFor(address => address.City).NotNull().Must(x => x.Equals("Wroclaw", StringComparison.InvariantCultureIgnoreCase));
            });

            RuleFor(address => address.StreetLines).NotNull().ForEach(x => x.MaximumLength(20));
            RuleFor(address => address.Number).GreaterThan(0);

        }
    }
}
