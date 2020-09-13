using FreeSecur.API.Logic.UserLogic;
using FreeSecur.API.Logic.UserLogic.Models;
using FreeSecur.API.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.UnitTests.AccountManagement
{
    [TestClass]
    public class AccountManagementServiceTests
    {
        [TestMethod]
        public async Task RegistrationFlowShouldWork()
        {
            var dbContext = MockServices.DbContextMock();
            var target = CreateAccountManagementService(dbContext);

            var registrationModel = new UserRegistrationModel
            {
                ConfirmationUrl = "https://test.nl",
                Email = "serviceTest@test.nl",
                FirstName = "servicetest",
                LastName = "servicetest",
                Password = "sdfsdf",
                Username = "dafsdf"
            };

            var result = await target.Register(registrationModel);

            var userPart1 = dbContext.Users.Single(x => x.Username == registrationModel.Username);
            Assert.IsFalse(userPart1.IsEmailConfirmed);

            dbContext.Entry(userPart1).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            await target.ConfirmEmail(result.ConfirmationKey);

            var user = dbContext.Users.Single(x => x.Username == registrationModel.Username);

            Assert.IsNotNull(user);
            Assert.AreEqual(registrationModel.Password, user.Password);
            Assert.IsTrue(user.IsEmailConfirmed);
            Assert.IsNotNull(user.Owner);
        }

        [TestMethod]
        public async Task UpdatePersonalDataShouldUpdateUser()
        {
            var dbContext = MockServices.DbContextMock();
            var authService = MockServices.AuthenticationServiceMock().Object;
            var userId = authService.UserId;
            var unmodifiedUser = dbContext.Users.Single(x => x.Id == userId);
            var expectedFirstName = unmodifiedUser.FirstName;
            dbContext.Entry(unmodifiedUser).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            var target = CreateAccountManagementService(dbContext);


            var userUpdateModel = new UserUpdateModel
            {
                Email = "9999@email.com",
                LastName = "12343"
            };

            var actual = await target.UpdatePersonalData(userUpdateModel);

            var user = dbContext.Users.Single(x => x.Id == userId);

            Assert.AreEqual(userUpdateModel.Email, actual.Email);
            Assert.AreEqual(userUpdateModel.LastName, actual.LastName);
            Assert.AreEqual(expectedFirstName, user.FirstName);
        }

        private static AccountManagementService CreateAccountManagementService(Domain.FsDbContext dbContext)
        {
            var entityRepostitory = MockServices.EntityRepositoryMock(dbContext);
            var hashServiceMock = MockServices.HashServiceMock().Object;
            var mailService = MockServices.MailServiceMock().Object;
            var encryptionService = MockServices.EncryptionServiceMock();
            var authenticationService = MockServices.AuthenticationServiceMock().Object;
            var target = new AccountManagementService(entityRepostitory, hashServiceMock, mailService, encryptionService, dbContext, authenticationService);
            return target;
        }
    }
}
