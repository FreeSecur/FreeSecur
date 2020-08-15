using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core.Url
{
    public class UrlService : IUrlService
    {
        public string BaseUrl { get; private set; }
        public string Path { get; private set; }
        public string QueryString { get; private set; }
        public string FullUrl { get; private set; }


        public void Update(HttpContext context)
        {
            var request = context.Request;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            var path = $"{request.Path}";
            var queryString = $"{request.QueryString}";
            var fullUrl = $"{baseUrl}{path}{queryString}";

            BaseUrl = baseUrl;
            Path = path;
            QueryString = queryString;
            FullUrl = fullUrl;
        }
    }
}
