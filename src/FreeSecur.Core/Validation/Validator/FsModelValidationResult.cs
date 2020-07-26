using System.Collections.Generic;
using System.Linq;

namespace FreeSecur.Core.Validation.Validator
{
    public class FsModelValidationResult
    {
        public FsModelValidationResult(List<FsFieldValidationResult> fieldValidationResults)
        {
            FieldValidationResults = fieldValidationResults;
        }

        public List<FsFieldValidationResult> FieldValidationResults { get; }
        public bool IsValid => FieldValidationResults.All(fieldValidationResult => fieldValidationResult.IsValid);
    }
}
