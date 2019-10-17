using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace FluentValidationDemo.ValidationRules
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            return ruleBuilder.Must(list => list.Count < num).WithMessage("The list contains too many items");
        }
    }
}
