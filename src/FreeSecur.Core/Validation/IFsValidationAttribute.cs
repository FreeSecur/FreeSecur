using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core.Validation
{
    public interface IFsValidationAttribute
    {
        FieldValidationErrorCode GetErrorCode();
        bool IsValid(object value);
    }
}
