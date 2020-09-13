using FreeSecur.API.Core.Cryptography;
using FreeSecur.API.Core.ExceptionHandling;
using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using FreeSecur.API.Domain;
using FreeSecur.API.Domain.Entities.Owners;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Domain.Entities.VaultOwnerRights;
using FreeSecur.API.Domain.Entities.VaultOwners;
using FreeSecur.API.Domain.Entities.Vaults;
using FreeSecur.API.Logic.AccessManagement;
using FreeSecur.API.Logic.VaultManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.VaultManagement
{
    public class VaultCreationService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IFsEntityRepository _entityRepository;
        private readonly FsDbContext _dbContext;
        private readonly IHashService _hashService;

        public VaultCreationService(
            IAuthenticationService authenticationService, 
            IFsEntityRepository entityRepository,
            FsDbContext dbContext,
            IHashService hashService)
        {
            _authenticationService = authenticationService;
            _entityRepository = entityRepository;
            _dbContext = dbContext;
            _hashService = hashService;
        }

        public async Task<int> CreateVaultForAuthenticatedUser(VaultCreateModel vaultCreateModel)
        {
            if (!_authenticationService.IsAuthenticated)
            {
                throw new StatusCodeException(HttpStatusCode.Unauthorized);
            }

            var userId = _authenticationService.UserId;
            var user = await _entityRepository.GetEntity<User>(x => x.Id == userId);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var vaultId = await CreateVault(vaultCreateModel, user.Id);
                    var vaultOwnerId = await CreateVaultOwner(vaultId, user.OwnerId, user.Id);
                    await CreateVaultOwnerRights(vaultOwnerId, user.Id, VaultOwnerRightType.CreateSecrets, VaultOwnerRightType.DeleteSecrets, VaultOwnerRightType.ReadSecrets, VaultOwnerRightType.UpdateSecrets);

                    await transaction.CommitAsync();

                    return vaultId;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task<int> CreateVaultOwner(int vaultId, int ownerId, int createdByUserId)
        {
            var vaultOwner = new VaultOwner
            {
                OwnerId = ownerId,
                VaultId = vaultId
            };

            var createdVaultOwner = await _entityRepository.AddEntity(vaultOwner, createdByUserId);

            return createdVaultOwner.Id;
        }

        private async Task CreateVaultOwnerRights(
            int vaultOwnerId, 
            int createdByUserId,
            params VaultOwnerRightType[] accessRights)
        {
            var vaultOwnerRights = accessRights.Select(accessRight =>
                new VaultOwnerRight
                {
                    AccessRight = accessRight,
                    VaultOwnerId = vaultOwnerId
                }).ToList();

            await _entityRepository.AddEntities(vaultOwnerRights, createdByUserId);
        }

        private async Task<int> CreateVault(VaultCreateModel vaultCreateModel, int createdByUserId)
        {
            var hashedMasterKey = _hashService.GetHash(vaultCreateModel.MasterKey);
            var vault = new Vault
            {
                MasterKey = hashedMasterKey.Hash,
                MasterKeySalt = hashedMasterKey.Salt,
                Name = vaultCreateModel.Name
            };
            var createdVault = await _entityRepository.AddEntity(vault, createdByUserId);

            return createdVault.Id;
        }
    }
}
