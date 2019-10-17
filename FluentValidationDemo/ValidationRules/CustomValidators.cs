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

        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThanWithCustomMessage<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            return ruleBuilder.Must((rootObject, list, context) =>
            {
                context.MessageFormatter.AppendArgument("MaxElements", num) //add new arguments
                .AppendArgument("TotalElements", list.Count);

                return list.Count < num;
            }).WithMessage("{PropertyName} must contain fewer than {MaxElements} items. Now it contains {TotalElements} elements");
        }
    }
}
