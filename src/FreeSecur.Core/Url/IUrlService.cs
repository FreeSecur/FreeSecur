using Microsoft.AspNetCore.Http;

namespace FreeSecur.Core.Url
{
    public interface IUrlService
    {
        string BaseUrl { get; }
        string FullUrl { get; }
        string Path { get; }
        string QueryString { get; }

        void Update(HttpContext context);
    }
}