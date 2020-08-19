using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace FreeSecur.Core.ExceptionHandling
{
    public interface IExceptionHandler<T>
        where T : Exception
    {
        IActionResult HandleException(ActionExecutedContext context, T exception, ILogger logger);
    }
}
