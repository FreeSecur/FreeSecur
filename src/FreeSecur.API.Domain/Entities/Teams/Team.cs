using FreeSecur.API.Domain.Entities.TeamUsers;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Domain.Entities.Owners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreeSecur.API.Domain.Entities.Organisations;
using FreeSecur.API.Core.Validation.Attributes;

namespace FreeSecur.API.Domain.Entities.Teams
{
    public class Team : IFsTrackedEntity, IOwner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [FsRequired]
        public string Name { get; set; }
        [FsRequired]
        public long OrganisationId { get; set; }
        [ForeignKey(nameof(OrganisationId))]
        public Organisation Organisation { get; set; }

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

        [FsRequired]
        public long OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }

        [InverseProperty(nameof(TeamUser.Team))]
        public ICollection<TeamUser> Users { get; set; }
    }
}
