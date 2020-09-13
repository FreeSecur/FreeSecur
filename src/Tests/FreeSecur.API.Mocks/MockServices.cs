using FreeSecur.API.Core.Cryptography;
using FreeSecur.API.Core.GeneralHelpers;
using FreeSecur.API.Domain;
using FreeSecur.API.Logic.AccessManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;

namespace FreeSecur.API.Mocks
{
    public static class MockServices
    {
        private static FsDbContext _fsDbContext;

        public static FsDbContext DbContextMock()
        {
            if (_fsDbContext != null)
            {
                return _fsDbContext;
            }

            var options = new DbContextOptionsBuilder<FsDbContext>()
                .UseInMemoryDatabase(databaseName: "FreeSecurDB")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var dbContext = new FsDbContext(options);

            dbContext.Owners.Add(MockEntities.TestOwner());
            dbContext.Users.Add(MockEntities.TestUser());

            dbContext.Vaults.Add(MockEntities.TestVault());
            dbContext.VaultOwners.Add(MockEntities.TestVaultOwner());
            dbContext.VaultOwnerRights.AddRange(MockEntities.TestVaultOwnerRights());
            dbContext.VaultItems.Add(MockEntities.TestVaultItem());

            dbContext.SaveChanges();

            _fsDbContext = dbContext;

            return dbContext;
        }

        public static Mock<IAuthenticationService> AuthenticationServiceMock()
        {
            var mock = new Mock<IAuthenticationService>();
            mock.Setup(x => x.IsAuthenticated).Returns(true);
            mock.Setup(x => x.UserId).Returns(MockEntities.TestUser().Id);

            return mock;
        }

        public static IFsEntityRepository EntityRepositoryMock(FsDbContext dbContext)
        {
            return new FsEntityRepository(dbContext, DateTimeProviderMock().Object);
        }

        public static Mock<IDateTimeProvider> DateTimeProviderMock()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(x => x.Now).Returns(default(DateTime));

            return mock;
        }

        public static Mock<IHashService> HashServiceMock()
        {
            var mock = new Mock<IHashService>();

            Func<string, HashResult> getHashFunc = (text) => new HashResult(text, "1");
            Func<string, string, string, bool> verifyHashFunc = (text, hash, salt) => hash == text;

            mock.Setup(x => x.GetHash(It.IsAny<string>())).Returns(getHashFunc);
            mock.Setup(x => x.Verify(It.IsNotIn<string>(), It.IsNotIn<string>(), It.IsNotIn<string>())).Returns(verifyHashFunc);

            return mock;
        }
    }
}
