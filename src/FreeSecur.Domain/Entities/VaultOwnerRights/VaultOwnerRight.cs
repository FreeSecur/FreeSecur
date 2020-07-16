using FreeSecur.Domain.Entities.Users;
using FreeSecur.Domain.Entities.VaultOwners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain.Entities.VaultOwnerRights
{
    public class VaultOwnerRight
    {
        [Key]
        public int Id { get; set; }
        public int VaultOwnerId { get; set; }
        public VaultOwnerRightType AccessRight { get; set; }

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


        [ForeignKey(nameof(VaultOwnerId))]
        public VaultOwner VaultOwner { get; set; }
    }
}
