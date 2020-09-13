using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using FreeSecur.API.Logic.VaultManagement;
using FreeSecur.API.Logic.VaultManagement.Models;
using FreeSecur.API.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.UnitTests.VaultManagement
{
    [TestClass]
    public class VaultCreationServiceTests
    {

        [TestMethod]
        public async Task CreatingVaultForAuthenticatedUserShouldSucceed()
        {
            var dbContext = MockServices.DbContextMock();

            var authenticationService = MockServices.AuthenticationServiceMock().Object;

            var target = new VaultCreationService(authenticationService, MockServices.EntityRepositoryMock(dbContext), dbContext, MockServices.HashServiceMock().Object);

            var vaultCreateModel = new VaultCreateModel
            {
                MasterKey = "555",
                Name = "555"
            };

            var vaultId = await target.CreateVaultForAuthenticatedUser(vaultCreateModel);

            var vault = dbContext.Vaults.Single(x => x.Id == vaultId);
            var vaultOwner = dbContext.VaultOwners.Single(x => x.VaultId == vaultId);
            var user = dbContext.Users.Single(x => x.OwnerId == vaultOwner.OwnerId);

            var vaultOwnerRightsCount = dbContext.VaultOwnerRights.Where(x => x.VaultOwnerId == vaultOwner.Id).Count();

            Assert.AreEqual(authenticationService.UserId, user.Id);
            Assert.AreEqual(4, vaultOwnerRightsCount);
            Assert.AreEqual(vaultCreateModel.MasterKey, vault.MasterKey);
            Assert.AreEqual(vaultCreateModel.Name, vault.Name);
        }

        [TestMethod]
        public async Task CreatingVaultForUnAuthenticatedUserShouldFail()
        {
            var dbContext = MockServices.DbContextMock();

            var authenticationServiceMock = MockServices.AuthenticationServiceMock();

            authenticationServiceMock.Setup(x => x.IsAuthenticated).Returns(false);

            var target = new VaultCreationService(authenticationServiceMock.Object, MockServices.EntityRepositoryMock(dbContext), dbContext, MockServices.HashServiceMock().Object);

            var vaultCreateModel = new VaultCreateModel
            {
                MasterKey = "555",
                Name = "555"
            };

            await Assert.ThrowsExceptionAsync<StatusCodeException>(async () => await target.CreateVaultForAuthenticatedUser(vaultCreateModel));
        }
    }
}
