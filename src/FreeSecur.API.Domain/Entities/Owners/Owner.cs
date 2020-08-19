using FreeSecur.API.Domain.Entities.VaultOwners;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.API.Domain.Entities.Owners
{
    public class Owner : IFsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [InverseProperty(nameof(VaultOwner.Owner))]
        public ICollection<VaultOwner> Vaults { get; set; }
    }
}
