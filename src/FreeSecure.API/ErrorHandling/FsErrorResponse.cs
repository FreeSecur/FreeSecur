using System;

namespace FreeSecure.API.ErrorHandling
{
    public class FsErrorResponse
    {
        public FsErrorResponse(
            Enum errorCode)
        {
            ErrorCode = errorCode;
        }
        public Enum ErrorCode { get; }
    }
}
