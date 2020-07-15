using FreeSecur.Domain.Entities.Owners;
using FreeSecur.Domain.Entities.Users;
using FreeSecur.Domain.Entities.VaultOwnerRights;
using FreeSecur.Domain.Entities.Vaults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain.Entities.VaultOwners
{
    public class VaultOwner : IFsTrackedEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Vault))]
        public int VaultId { get; set; }
        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }

        [ForeignKey(nameof(CreatedByUser))]
        public int CreatedById { get; set; }
        [ForeignKey(nameof(ModifiedByUser))]
        public int ModifiedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public User CreatedByUser { get; set; }
        public User ModifiedByUser { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public Vault Vault { get; set; }
        public Owner Owner { get; set; }

        public ICollection<VaultOwnerRight> VaultOwnerRights { get; set; } 
    }
}
