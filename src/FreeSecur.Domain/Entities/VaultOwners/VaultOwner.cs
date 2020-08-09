using FreeSecur.Core.Validation.Attributes;
using FreeSecur.Domain.Entities.Owners;
using FreeSecur.Domain.Entities.Users;
using FreeSecur.Domain.Entities.VaultOwnerRights;
using FreeSecur.Domain.Entities.Vaults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.Domain.Entities.VaultOwners
{
    public class VaultOwner : IFsTrackedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [FsRequired]
        public int VaultId { get; set; }
        [FsRequired]
        public int OwnerId { get; set; }

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

        [ForeignKey(nameof(VaultId))]
        public Vault Vault { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }

        [InverseProperty(nameof(VaultOwnerRight.VaultOwner))]
        public ICollection<VaultOwnerRight> VaultOwnerRights { get; set; } 
    }
}
