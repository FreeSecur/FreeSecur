using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.OrganisationUsers;
using FreeSecur.API.Domain.Entities.Teams;
using FreeSecur.API.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.API.Domain.Entities.Organisations
{
    public class Organisation : IFsTrackedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [FsRequired]
        public string Name { get; set; }

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

        [InverseProperty(nameof(OrganisationUser.Organisation))]
        public ICollection<OrganisationUser> Users { get; set; }
        [InverseProperty(nameof(Team.Organisation))]
        public ICollection<Team> Teams { get; set; }
    }
}
