using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
