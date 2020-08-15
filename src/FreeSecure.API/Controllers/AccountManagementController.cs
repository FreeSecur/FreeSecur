using FreeSecur.API.Utils;
using FreeSecur.Core.ExceptionHandling.Exceptions;
using FreeSecur.Core.Url;
using FreeSecur.Logic.UserLogic;
using FreeSecur.Logic.UserLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace FreeSecure.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountManagementController : FsController
    {
        private readonly AccountManagementService _accountManagementService;
        private readonly IUrlService _urlService;

        public AccountManagementController(AccountManagementService userService, 
            IUrlService urlService)
        {
            _accountManagementService = userService;
            _urlService = urlService;
        }

        [HttpPost("Register")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Created user id", typeof(int))]
        public async Task<int> Register([FromBody] UserRegistrationModel registrationModel)
        {
            var user = await _accountManagementService.Register(registrationModel);

            return user.Id;
        }

        [HttpPatch("ConfirmEmail")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Confirmation succesful")]
        public async Task ConfirmEmail([FromQuery]string key)
        {
            await _accountManagementService.ConfirmEmail(key);
        }
    }
}
