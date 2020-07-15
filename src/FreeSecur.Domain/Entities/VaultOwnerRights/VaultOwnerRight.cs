﻿using FreeSecur.Domain.Entities.Users;
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
        [ForeignKey(nameof(VaultOwner))]
        public int VaultOwnerId { get; set; }
        public VaultOwnerRightType AccessRight { get; set; }

        [ForeignKey(nameof(CreatedByUser))]
        public int CreatedById { get; set; }
        [ForeignKey(nameof(ModifiedByUser))]
        public int ModifiedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public User CreatedByUser { get; set; }
        public User ModifiedByUser { get; set; }
        public VaultOwner VaultOwner { get; set; }
    }
}
