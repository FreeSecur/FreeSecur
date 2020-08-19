using FreeSecur.Core.Validation.ErrorCodes;
using System.Collections.Generic;
using System.Linq;

namespace FreeSecur.Core.Validation.Validator
{
    public class FieldValidationResult
    {
		/// <summary>
		/// Creates a validationResult for a simple field
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="errorCodes"></param>
        public FieldValidationResult(
			string fieldName, 
			List<FieldValidationErrorCode> errorCodes,
			int? index)
        {
            FieldName = fieldName;
            ErrorCodes = errorCodes;
			SubFieldValidationResults = new List<FieldValidationResult>();
            Index = index;
		}

		/// <summary>
		/// Creates a field validation result for a field which is a list or object
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="errorCodes"></param>
		/// <param name="subFieldValidationResults"></param>
		public FieldValidationResult(
			string fieldName,
			List<FieldValidationErrorCode> errorCodes,
			List<FieldValidationResult> subFieldValidationResults,
			int? index)
		{
			FieldName = fieldName;
			ErrorCodes = errorCodes;
            SubFieldValidationResults = subFieldValidationResults;
            Index = index;
		}

		public string FieldName { get; }
        public List<FieldValidationErrorCode> ErrorCodes { get; }
        public List<FieldValidationResult> SubFieldValidationResults { get; }
        public int? Index { get; }
        public bool IsValid => !ErrorCodes.Any() && SubFieldValidationResults.All(x => x.IsValid);
    }
}
