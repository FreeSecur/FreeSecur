using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Domain.Entities.VaultOwners;
using FreeSecur.API.Domain.VaultItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.API.Domain.Entities.Vaults
{
    public class Vault : IFsTrackedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [FsRequired]
        public string Name { get; set; }
        [FsRequired]
        [FsMinLength(10)]
        public string MasterKey { get; set; }
        [FsRequired]
        public string MasterKeySalt { get; set; }

        [FsRequired]
        public int CreatedById { get; set; }
        [FsRequired]
        public int ModifiedById { get; set; }
        [FsRequired]
        public DateTime CreatedOn { get; set; }
        [FsRequired]
        public DateTime ModifiedOn { get; set; }
        [ForeignKey(nameof(CreatedById))]
        public User CreatedByUser { get; set; }
        [ForeignKey(nameof(ModifiedById))]
        public User ModifiedByUser { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [InverseProperty(nameof(VaultOwner.Vault))]
        public ICollection<VaultOwner> Owners { get; set; }
        [InverseProperty(nameof(VaultItem.Vault))]
        public ICollection<VaultItem> VaultItems { get; set; }
    }
}
