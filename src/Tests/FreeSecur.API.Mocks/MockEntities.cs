using FreeSecur.API.Domain;
using FreeSecur.API.Domain.Entities.Owners;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Domain.Entities.VaultOwnerRights;
using FreeSecur.API.Domain.Entities.VaultOwners;
using FreeSecur.API.Domain.Entities.Vaults;
using FreeSecur.API.Domain.VaultItems;
using System;
using System.Collections.Generic;

namespace FreeSecur.API.Mocks
{
    public static class MockEntities
    {
        public static User TestUser()
        {
            return new User
            {
                Email = "test@test.nl",
                FirstName = "first",
                Id = 1,
                IsEmailConfirmed = true,
                LastName = "last",
                OwnerId = 1,
                Password = "123",
                PasswordSalt = "123",
                Username = "test",
                Timestamp = new byte[10]
            };
        }

        public static Owner TestOwner()
        {
            return new Owner
            {
                Id = 1,
                Timestamp = new byte[10]
            };
        }

        public static Vault TestVault()
        {
            var value = new Vault
            {
                Id = 1,
                MasterKey = "123",
                MasterKeySalt = "123",
                Name = "test",
            };
            FillTrackedEntity(value);

            return value;
        }

        public static VaultOwner TestVaultOwner()
        {
            var value = new VaultOwner
            {
                Id = 1,

            };

            FillTrackedEntity(value);

            value.OwnerId = TestOwner().Id;
            value.VaultId = TestVault().Id;

            return value;
        }

        public static List<VaultOwnerRight> TestVaultOwnerRights()
        {
            var values = new List<VaultOwnerRight>
            {
                new VaultOwnerRight
                {
                    AccessRight = VaultOwnerRightType.CreateSecrets,
                    Id = 1
                },
                new VaultOwnerRight
                {
                    AccessRight = VaultOwnerRightType.DeleteSecrets,
                    Id = 2
                },
                new VaultOwnerRight
                {
                    AccessRight = VaultOwnerRightType.ReadSecrets,
                    Id = 3
                },
                new VaultOwnerRight
                {
                    AccessRight = VaultOwnerRightType.UpdateSecrets,
                    Id = 4
                }
            };

            foreach (var value in values)
            {

                FillTrackedEntity(value);
                value.VaultOwnerId = TestVaultOwner().Id;
            }

            return values;
        }

        public static VaultItem TestVaultItem()
        {
            var value = new VaultItem
            {
                Id = 1,
                Key = "123"
            };

            FillTrackedEntity(value);
            value.VaultId = TestVault().Id;

            return value;
        }

        public static void FillTrackedEntity(IFsTrackedEntity trackedEntity)
        {
            trackedEntity.CreatedById = TestUser().Id;
            trackedEntity.CreatedOn = default(DateTime);
            trackedEntity.ModifiedById = TestUser().Id;
            trackedEntity.ModifiedOn = default(DateTime);
            trackedEntity.Timestamp = new byte[10];
        }
    }
}
