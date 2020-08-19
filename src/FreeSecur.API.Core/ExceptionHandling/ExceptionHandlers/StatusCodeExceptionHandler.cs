using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FreeSecur.API.Core.ExceptionHandling.ExceptionHandlers
{
    public class StatusCodeExceptionHandler : IExceptionHandler<StatusCodeException>
    {
        public IActionResult HandleException(ActionExecutedContext context, StatusCodeException exception, ILogger logger)
        {
            return context.Result = new ObjectResult(exception.Message)
            {
                StatusCode = (int)exception.StatusCode,
            };
        }
    }
}