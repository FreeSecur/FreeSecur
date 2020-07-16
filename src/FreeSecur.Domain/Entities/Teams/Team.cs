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

        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }

        [InverseProperty(nameof(TeamUser.Team))]
        public ICollection<TeamUser> Users { get; set; }
    }
}
