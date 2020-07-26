using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core.Logging
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = Guid.NewGuid().ToString();

            context.Response.Headers.Add("trace-id", traceId);

            var logger = context.RequestServices.GetRequiredService<ILogger<LoggingMiddleware>>();
            using (logger.BeginScope("{@TraceId}", traceId))
            {
                await this._next(context);
            }
        }
    }
}
