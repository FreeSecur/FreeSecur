using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Domain.VaultItems;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.API.Domain.Entities.VaultSecrets
{
    public class VaultSecret : IFsTrackedEntity, IVaultItem
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Id))]
        public VaultItem VaultItem { get; set; }

        [FsRequired]
        public long CreatedById { get; set; }
        [FsRequired]
        public long ModifiedById { get; set; }
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
    }
}
