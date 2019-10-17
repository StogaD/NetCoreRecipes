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
            RuleFor(address => address.City).NotNull();
            RuleFor(address => address.Street).NotNull();
            RuleFor(address => address.Number).GreaterThan(0);

        }
    }
}
