using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Module_02.Task_01.CartingService.WebApi.Models;
using Module_02.Task_01.CartingService.WebApi.Static;
using Сommon.DB.Exceptions;

namespace Module_02.Task_01.CartingService.WebApi.ActionFilters;

public class ExceptionHandlerActionFilter : IActionFilter
{
    private const string AN_ERROR_HAS_OCCURED_ERR_MSG = "An error has occurred.";

    private static readonly IDictionary<Type, (int StatusCode, bool CanOutputErrorMessage, string ErrorType)>
        ExceptionStatusCodeMap =
            new Dictionary<Type, (int, bool, string)>
            {
                { typeof(DbRecordNotFoundException), (StatusCodes.Status404NotFound, true, ErrorType.ITEM_NOT_FOUND) }
            };

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null)
        {
            return;
        }

        HandleError(context);
    }

    private static void HandleError(ActionExecutedContext context)
    {
        var error = context.Exception;
        var statusCode = StatusCodes.Status500InternalServerError;
        var errorMessage = AN_ERROR_HAS_OCCURED_ERR_MSG;
        var errorType = ErrorType.UNHANDLED;
        if (ExceptionStatusCodeMap.TryGetValue(error.GetType(), out var value))
        {
            statusCode = value.StatusCode;
            errorType = value.ErrorType;
            errorMessage = value.CanOutputErrorMessage
                ? error.Message
                : AN_ERROR_HAS_OCCURED_ERR_MSG;
        }

        context.ExceptionHandled = true;
        context.Result = new ObjectResult(new ErrorModelResponse(errorType, errorMessage))
        {
            StatusCode = statusCode
        };
    }
}