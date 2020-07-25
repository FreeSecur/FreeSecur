using FreeSecur.Domain.Entities.Users;
using FreeSecur.Domain.Entities.VaultOwners;
using FreeSecur.Domain.VaultItems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain.Entities.Vaults
{
    public class Vault : IFsTrackedEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
        public DateTime CreatedOn { get; set; }
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
