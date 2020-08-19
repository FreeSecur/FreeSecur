namespace FreeSecur.API.Core.Validation.Filter
{
    public interface IErrorResponse<TErrorCode>
        where TErrorCode : struct
    {
        TErrorCode ErrorCode { get; }
    }
}
