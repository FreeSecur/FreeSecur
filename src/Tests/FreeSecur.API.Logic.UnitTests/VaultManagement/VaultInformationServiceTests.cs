using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using FreeSecur.API.Logic.VaultManagement;
using FreeSecur.API.Logic.VaultManagement.Models;
using FreeSecur.API.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.UnitTests.VaultManagement
{
    [TestClass]
    public class VaultInformationServiceTests
    {

        [TestMethod]
        public async Task ValidVaultForUserShouldReturn()
        {
            var dbContext = MockServices.DbContextMock();

            var authenticationService = MockServices.AuthenticationServiceMock().Object;

            var target = new VaultInformationService(authenticationService, dbContext);

            var validVaultId = MockEntities.TestVault().Id;
            var validUserId = MockEntities.TestUser().Id;

            var actual = await target.GetVaultForAuthorizedUser(validVaultId);

            Assert.AreEqual(validVaultId, actual.Id);
        }


        [TestMethod]
        public async Task ValidVaultForDifferentUserShouldNotFound()
        {
            var dbContext = MockServices.DbContextMock();

            var authenticationService = MockServices.AuthenticationServiceMock();
            authenticationService.Setup(x => x.UserId).Returns(777);

            var target = new VaultInformationService(authenticationService.Object, dbContext);

            var validVaultId = MockEntities.TestVault().Id;
            var validUserId = MockEntities.TestUser().Id;

            await Assert.ThrowsExceptionAsync<StatusCodeException>(async () => await target.GetVaultForAuthorizedUser(validVaultId));
        }

        [TestMethod]
        public async Task ShouldReturnAllVaultForUser()
        {
            var dbContext = MockServices.DbContextMock();

            var authenticationService = MockServices.AuthenticationServiceMock().Object;


            var target = new VaultInformationService(authenticationService, dbContext);

            var validVaultId = MockEntities.TestVault().Id;
            var validUserId = MockEntities.TestUser().Id;

            var vaults = await target.GetVaultsForAuthorizedUser();

            var expectedids = await dbContext.Vaults.Where(x => x.Owners.Any(y => y.OwnerId == authenticationService.UserId)).Select(x => x.Id).ToListAsync();
            var actualIds = vaults.Select(x => x.Id).ToList();

            CollectionAssert.AreEqual(expectedids, actualIds);
        }

        [TestMethod]
        public async Task ValidVaultForUnAuthenticatedUserShouldThrowUnAuthorized()
        {
            var dbContext = MockServices.DbContextMock();

            var authenticationService = MockServices.AuthenticationServiceMock();
            authenticationService.Setup(x => x.IsAuthenticated).Returns(false);

            var target = new VaultInformationService(authenticationService.Object, dbContext);

            var validVaultId = MockEntities.TestVault().Id;
            var validUserId = MockEntities.TestUser().Id;

            await Assert.ThrowsExceptionAsync<StatusCodeException>(async () => await target.GetVaultForAuthorizedUser(validVaultId));
            await Assert.ThrowsExceptionAsync<StatusCodeException>(async () => await target.GetVaultsForAuthorizedUser());
        }

    }
}
