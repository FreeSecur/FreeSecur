using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.Url
{
    public class UrlMiddleware
    {
        private readonly RequestDelegate _next;

        public UrlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var urlService = context.RequestServices.GetRequiredService<IUrlService>();
            urlService.Update(context);

            await this._next(context);
        }
    }
}
