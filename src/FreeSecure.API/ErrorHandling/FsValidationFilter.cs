using FreeSecur.Core;
using FreeSecur.Core.Validation;
using FreeSecur.Core.Validation.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FreeSecure.API.ErrorHandling
{
    public class FsValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelValidator = context.HttpContext.RequestServices.GetRequiredService<FsModelValidator>();
            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var serializer = context.HttpContext.RequestServices.GetRequiredService<IFsSerializer>();


            var argumentValidationResults = new List<FsValidationResult>();

            foreach(var argument in context.ActionArguments)
            {
                var modelValidationResult = modelValidator.Validate(argument.Value);

                if (modelValidationResult.Any())
                {
                    var validationResult = new FsValidationResult(argument.Key, modelValidationResult);
                    argumentValidationResults.Add(validationResult);
                }
            }

            if (argumentValidationResults.Any())
            {
                var errorResponse = new FsModelErrorResponse(argumentValidationResults);
                var errorResult = new JsonResult(errorResponse);
                errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
                var logger = loggerFactory.CreateLogger<FsValidationFilter>();
                logger.LogInformation($"Failed model validation: {serializer.Serialize(errorResponse)}");
                context.Result = errorResult;
            }
        }
    }
}
