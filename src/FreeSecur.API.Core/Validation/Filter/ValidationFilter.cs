using FreeSecur.API.Core;
using FreeSecur.API.Core.Validation.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FreeSecur.API.Core.Validation.Filter
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelValidator = context.HttpContext.RequestServices.GetRequiredService<ModelValidator>();
            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var serializer = context.HttpContext.RequestServices.GetRequiredService<ISerializer>();

            var argumentValidationResults = new List<ValidationResult>();
            
            foreach(var argument in context.ActionArguments)
            {
                var argumentMetadata = context.ActionDescriptor.Parameters.Single(x => x.Name == argument.Key);
                if (argumentMetadata.BindingInfo.BindingSource.Id == "Query") continue;

                var modelValidationResult = modelValidator.Validate(argument.Value);

                if (modelValidationResult.Any())
                {
                    var validationResult = new ValidationResult(argument.Key, modelValidationResult);
                    argumentValidationResults.Add(validationResult);
                }
            }

            if (argumentValidationResults.Any())
            {
                var errorResponse = new ModelErrorResponse(argumentValidationResults);
                var errorResult = new JsonResult(errorResponse);
                errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
                var logger = loggerFactory.CreateLogger<ValidationFilter>();
                logger.LogInformation($"Failed model validation: {serializer.Serialize(errorResponse)}");
                context.Result = errorResult;
            }
        }
    }
}
