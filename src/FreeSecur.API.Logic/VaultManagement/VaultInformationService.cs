using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using FreeSecur.API.Domain;
using FreeSecur.API.Domain.Entities.Owners;
using FreeSecur.API.Domain.Entities.Vaults;
using FreeSecur.API.Logic.AccessManagement;
using FreeSecur.API.Logic.VaultManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.VaultManagement
{
    public class VaultInformationService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly FsDbContext _dbContext;

        public VaultInformationService(
            IAuthenticationService authenticationService,
            FsDbContext dbContext)
        {
            _authenticationService = authenticationService;
            _dbContext = dbContext;
        }

        public async Task<List<VaultDetailsModel>> GetVaultsForAuthorizedUser()
        {
            if (!_authenticationService.IsAuthenticated)
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.Unauthorized);
            }

            var ownerId = _authenticationService.UserId;
            var vaults = await _dbContext.Vaults
                .Where(x => x.Owners.Any(x => x.OwnerId == ownerId))
                .Select(vault => new VaultDetailsModel(vault.Id, vault.Name))
                .AsNoTracking()
                .ToListAsync();

            return vaults;
        }
    }
}
