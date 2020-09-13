using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.Teams;
using FreeSecur.API.Domain.Entities.TeamUserRights;
using FreeSecur.API.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.API.Domain.Entities.TeamUsers
{
    public class TeamUser : IFsTrackedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [FsRequired]
        public long UserId { get; set; }
        [FsRequired]
        public long TeamId { get; set; }

        [FsRequired]
        public long CreatedById { get; set; }
        [FsRequired]
        public long ModifiedById { get; set; }
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
