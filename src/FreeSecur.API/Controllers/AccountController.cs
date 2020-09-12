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
using Microsoft.AspNetCore.Authorization;
using FreeSecur.API.Logic.VaultManagement.Models;
using System.Collections.Generic;
using FreeSecur.API.Logic.VaultManagement;

namespace FreeSecur.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : FsController
    {
        private readonly AccountManagementService _accountManagementService;
        private readonly VaultInformationService _vaultInformationService;

        public AccountController(AccountManagementService userService, VaultInformationService vaultInformationService)
        {
            _accountManagementService = userService;
            _vaultInformationService = vaultInformationService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, "Created user id", typeof(int))]
        public async Task<UserRegistrationResponseModel> Register([FromBody] UserRegistrationModel registrationModel)
        {
            var user = await _accountManagementService.Register(registrationModel);

            return user;
        }

        [HttpPatch("ConfirmEmail")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, "Confirmation succesful")]
        public async Task ConfirmEmail([FromQuery]string key)
        {
            await _accountManagementService.ConfirmEmail(key);
        }

        [HttpGet("vaults")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns all personal user vaults", typeof(List<VaultDetailsModel>))]
        public async Task<List<VaultDetailsModel>> GetVaultsForAccount()
        {
            return await _vaultInformationService.GetVaultsForAuthorizedUser();
        }
    }
}
