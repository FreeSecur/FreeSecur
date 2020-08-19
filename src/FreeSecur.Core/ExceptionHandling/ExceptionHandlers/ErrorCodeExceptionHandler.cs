using FreeSecur.Core.ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FreeSecur.Core.ExceptionHandling.ExceptionHandlers
{
    public class ErrorCodeExceptionHandler : IExceptionHandler<ErrorCodeException>
    {
        public IActionResult HandleException(
            ActionExecutedContext context, 
            ErrorCodeException exception,
            ILogger logger)
        {
            return new ObjectResult(exception.ErrorCode)
            {
                StatusCode = ExceptionStatusCode.ErrorCodeException
            };
        }
    }
}
