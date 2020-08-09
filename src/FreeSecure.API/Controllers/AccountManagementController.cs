using FreeSecur.API.Utils;
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

        public AccountManagementController(AccountManagementService userService)
        {
            _accountManagementService = userService;
        }

        [HttpPost("Register")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Created user id", typeof(int))]
        public async Task<int> Register([FromBody] UserRegistrationModel registrationModel)
        {
            var user = await _accountManagementService.Register(registrationModel);

            return user.Id;
        }
    }
}
