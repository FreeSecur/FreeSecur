using FreeSecur.Domain.Entities.Users;
using FreeSecur.Domain.Entities.Vaults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain.VaultItems
{
    public class VaultItem : IFsTrackedEntity
    {
        [Key]
        public int Id { get; set; }
        public int VaultId { get; set; }
        public string Key { get; set; }

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

        [ForeignKey(nameof(VaultId))]
        public Vault Vault { get; set; }
    }
}
