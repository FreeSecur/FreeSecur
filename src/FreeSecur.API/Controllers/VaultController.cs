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
        private readonly VaultCreationService _vaultService;
        private readonly VaultItemService _vaultItemService;
        private readonly VaultInformationService _vaultInformationService;

        public VaultController(
            VaultCreationService vaultCreationService,
            VaultItemService vaultItemService,
            VaultInformationService vaultInformationService)
        {
            _vaultService = vaultCreationService;
            _vaultItemService = vaultItemService;
            _vaultInformationService = vaultInformationService;
        }


        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Created vault id", typeof(long))]
        public async Task<long> Create([FromBody] VaultCreateModel vaultCreateModel)
        {
            var vaultId = await _vaultService.CreateVaultForAuthenticatedUser(vaultCreateModel);

            return vaultId;
        }
    }
}
