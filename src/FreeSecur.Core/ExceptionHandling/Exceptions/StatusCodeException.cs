using System;
using System.Net;

namespace FreeSecur.Core.ExceptionHandling.Exceptions
{
    public class StatusCodeException : Exception
    {
        public StatusCodeException(HttpStatusCode statusCode) : this(null, statusCode)
        {
        }
        public StatusCodeException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
