using AspNetApiPractice.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetApiPractice.API.Filters;

class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if(context.ModelState.IsValid == false)
        {
            var dict = new Dictionary<string, IEnumerable<string>>();
            foreach(var i in context.ModelState)
            {
                dict.Add(i.Key, i.Value.Errors.Select(e => e.ErrorMessage));
            }
            var formValidationErrors = FormValidationError.FromDictionary(dict);
            
            var response = new ResponseViewModel
                <IEnumerable<FormValidationError>>(formValidationErrors, "Something went wrong", success: false);
            context.Result = new BadRequestObjectResult(response);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) {}
}