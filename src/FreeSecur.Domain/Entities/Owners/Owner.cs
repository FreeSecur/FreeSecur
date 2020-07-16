using FreeSecur.Domain.Entities.VaultOwners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain.Entities.Owners
{
    public class Owner : IFsEntity
    {
        [Key]
        public int Id { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [InverseProperty(nameof(VaultOwner.Owner))]
        public ICollection<VaultOwner> Vaults { get; set; }
    }
}
