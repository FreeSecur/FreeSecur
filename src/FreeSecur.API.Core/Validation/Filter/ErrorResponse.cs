namespace FreeSecur.API.Core.Validation.Filter
{
    public class ErrorResponse<TErrorCode> : IErrorResponse<TErrorCode>
        where TErrorCode : struct
    {
        public ErrorResponse(TErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public TErrorCode ErrorCode { get; }
    }
}
