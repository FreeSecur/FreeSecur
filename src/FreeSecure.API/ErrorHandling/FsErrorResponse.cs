using System;

namespace FreeSecure.API.ErrorHandling
{
    public interface IFsErrorResponse<TErrorCode>
        where TErrorCode : struct
    {
        TErrorCode ErrorCode { get; }
    }
}
