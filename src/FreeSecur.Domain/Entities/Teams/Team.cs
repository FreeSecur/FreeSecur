using FreeSecur.Domain.Entities.TeamUsers;
using FreeSecur.Domain.Entities.Users;
using FreeSecur.Domain.Entities.Owners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.Domain.Entities.Teams
{
    public class Team : IFsTrackedEntity, IOwner
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


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

        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        public ICollection<TeamUser> Users { get; set; }
    }
}
