using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.ExceptionHandling
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<ExceptionFilter>();
                var exception = context.Exception;
                logger.LogError(exception, exception.Message);

                var exceptionType = exception.GetType();
                var exceptionHandlerType = typeof(IExceptionHandler<>);

                var constructedType = exceptionHandlerType.MakeGenericType(exceptionType);
                var handleExceptionMethod = constructedType.GetMethod("HandleException");

                var parameters = new object[] { context, exception, logger };
                var exceptionHandler = context.HttpContext.RequestServices.GetRequiredService(constructedType);
                if (exceptionHandler != null)
                {

                    var contentResult = (IActionResult)handleExceptionMethod.Invoke(exceptionHandler, parameters);

                    context.Result = contentResult;
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
