using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FluentValidationDemo.Filters
{
    public class ValidationResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errorMessages = GetErrorMessages(context.ModelState);

                context.Result = new JsonResult(errorMessages)
                {
                    StatusCode = 406
                };
            }
        }

        private List<string> GetErrorMessages(ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();
        }
    }
}
