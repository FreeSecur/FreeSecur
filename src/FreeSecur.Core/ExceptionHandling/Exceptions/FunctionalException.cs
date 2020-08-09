using System;
using System.Net;

namespace FreeSecur.Core.ExceptionHandling.Exceptions
{
    public class FunctionalException : Exception
    {
        public FunctionalException(
            string message,
            HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
