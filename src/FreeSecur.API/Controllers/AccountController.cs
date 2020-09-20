using FreeSecur.API.Utils;
using FreeSecur.API.Logic.AccountManagement.Models;
using FreeSecur.API.Logic.UserLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FreeSecur.API.Logic.VaultManagement.Models;
using System.Collections.Generic;
using FreeSecur.API.Logic.VaultManagement;
using FreeSecur.API.Logic.AccountManagement;

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
        [SwaggerResponse((int)HttpStatusCode.OK, "Created user id and confirmation code", typeof(UserRegistrationResponseModel))]
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

        [HttpGet("Vault/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns all personal user vaults", typeof(VaultDetailsModel))]
        public async Task<VaultDetailsModel> GetVaultForAccount(long id)
        {
            return await _vaultInformationService.GetVaultForAuthorizedUser(id);
        }


        [HttpGet("Vault/List")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns all personal user vaults", typeof(List<VaultDetailsModel>))]
        public async Task<List<VaultDetailsModel>> GetVaultsForAccount()
        {
            return await _vaultInformationService.GetVaultsForAuthorizedUser();
        }

        [HttpPost("RequestPasswordReset")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, "Password reset mail send")]
        [SwaggerOperation(Description = "Will send a password reset email to the requested e-mail address")]
        public async Task RequestPasswordReset(UserRequestPasswordResetModel userRequestPasswordResetModel)
        {
            await _accountManagementService.RequestPasswordReset(userRequestPasswordResetModel);
        }

        [HttpPatch("ResetPassword")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, "Password is reset")]
        public async Task ResetPassword(UserPasswordResetModel userPasswordResetModel)
        {
            await _accountManagementService.ResetPassword(userPasswordResetModel);
        }
    }
}
