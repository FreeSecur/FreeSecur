using FreeSecur.API.Logic.AccessManagement;
using FreeSecur.API.Logic.VaultManagement;
using FreeSecur.API.Logic.VaultManagement.Models;
using FreeSecur.API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FreeSecur.API.Controllers
{
    [Route("api/[controller]")]
    public class VaultController : FsController
    {
        private readonly VaultService _vaultService;

        public VaultController(
            VaultService vaultService)
        {
            _vaultService = vaultService;
        }


        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Created vault id", typeof(long))]
        public async Task<int> Register([FromBody] VaultCreateModel vaultCreateModel)
        {
            var vaultId = await _vaultService.CreateVaultForAuthenticatedUser(vaultCreateModel);

            return vaultId;
        }
    }
}
