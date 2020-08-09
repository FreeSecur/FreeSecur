using FreeSecur.Core.ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FreeSecure.API.ErrorHandling
{
    public class FsExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is FunctionalException exception)
            {
                var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<FsExceptionFilter>();

                logger.LogError(exception, exception.Message);

                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = (int)exception.StatusCode,
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
