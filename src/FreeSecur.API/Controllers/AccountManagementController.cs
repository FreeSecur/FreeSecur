using FreeSecur.API.Utils;
using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using FreeSecur.API.Core.Url;
using FreeSecur.API.Logic.AccountManagement.Models;
using FreeSecur.API.Logic.UserLogic;
using FreeSecur.API.Logic.UserLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace FreeSecur.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountManagementController : FsController
    {
        private readonly AccountManagementService _accountManagementService;

        public AccountManagementController(AccountManagementService userService)
        {
            _accountManagementService = userService;
        }

        [HttpPost("Register")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Created user id", typeof(int))]
        public async Task<UserRegistrationResponseModel> Register([FromBody] UserRegistrationModel registrationModel)
        {
            var user = await _accountManagementService.Register(registrationModel);

            return user;
        }

        [HttpPatch("ConfirmEmail")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Confirmation succesful")]
        public async Task ConfirmEmail([FromQuery]string key)
        {
            await _accountManagementService.ConfirmEmail(key);
        }
    }
}
