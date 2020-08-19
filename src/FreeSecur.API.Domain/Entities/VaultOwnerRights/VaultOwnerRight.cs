using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Domain.Entities.VaultOwners;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.API.Domain.Entities.VaultOwnerRights
{
    public class VaultOwnerRight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [FsRequired]
        public int VaultOwnerId { get; set; }
        [FsRequired]
        public VaultOwnerRightType AccessRight { get; set; }

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


        [ForeignKey(nameof(VaultOwnerId))]
        public VaultOwner VaultOwner { get; set; }
    }
}
