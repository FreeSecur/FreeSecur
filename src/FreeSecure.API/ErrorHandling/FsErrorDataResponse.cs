using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeSecure.API.ErrorHandling
{
    public class FsErrorDataResponse<T> : FsErrorResponse
        where T : class
    {
        public FsErrorDataResponse(
            Enum errorCode) : base(errorCode)
        {
            Content = null;
        }

        public FsErrorDataResponse(
            Enum errorCode,
            T content) : base(errorCode)
        {
            Content = content;
        }

        public T Content { get; }
    }
}
