using FreeSecur.Core.Validation.Attributes;
using FreeSecur.Domain.Entities.Teams;
using FreeSecur.Domain.Entities.TeamUserRights;
using FreeSecur.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.Domain.Entities.TeamUsers
{
    public class TeamUser : IFsTrackedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [FsRequired]
        public int UserId { get; set; }
        [FsRequired]
        public int TeamId { get; set; }

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

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; }

        [InverseProperty(nameof(TeamUserRight.TeamUser))]
        public ICollection<TeamUserRight> TeamUserRights { get; set; }
    }
}
