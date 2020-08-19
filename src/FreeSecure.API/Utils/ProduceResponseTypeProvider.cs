using FreeSecure.Core.Validation.Filter;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FreeSecure.API.Utils
{

    public class ProduceResponseTypeProvider : IApplicationModelProvider
    {
        public int Order => 3;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (ControllerModel controller in context.Result.Controllers)
            {
                foreach (ActionModel action in controller.Actions)
                {
                    action.Filters.Add(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, _badRequestDescription, typeof(ModelErrorResponse)));
                    action.Filters.Add(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, _notFoundDescription, typeof(string)));
                }
            }
        }

        private const string _notFoundDescription = "Data not found based on given parameters. Returns describing the exact error";
        private const string _badRequestDescription = "Returns bad request with response body containing which fields of the given arguments are invalid";
    }
}
