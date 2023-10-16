using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Module_02.Task_01.CartingService.WebApi.Models;
using Module_02.Task_01.CartingService.WebApi.Static;

namespace Module_02.Task_01.CartingService.WebApi.ActionFilters;

internal class ModelValidationActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = new Dictionary<string, string[]>();
        foreach (var item in context.ModelState)
        {
            if (item.Value.Errors.Count == 0)
            {
                continue;
            }
            errors.Add(item.Key, item.Value.Errors.Select(x => x.ErrorMessage).ToArray());
        }
        context.Result = new BadRequestObjectResult(new ErrorModelResponse(ErrorType.INPUT_MODEL_VALIDATION, errors));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}