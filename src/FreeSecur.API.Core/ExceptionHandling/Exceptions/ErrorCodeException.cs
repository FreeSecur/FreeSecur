using System;
using System.Net;

namespace FreeSecur.API.Core.ExceptionHandling.Exceptions
{

    public class ErrorCodeException : Exception
    {
        public ErrorCodeException(Enum errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public Enum ErrorCode { get; }
    }
}
