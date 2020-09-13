using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.Organisations;
using FreeSecur.API.Domain.Entities.OrganisationUserRights;
using FreeSecur.API.Domain.Entities.Owners;
using FreeSecur.API.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.API.Domain.Entities.OrganisationUsers
{
    public class OrganisationUser : IFsTrackedEntity, IOwner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [FsRequired]
        public long UserId { get; set; }
        [FsRequired]
        public long OrganistationId { get; set; }

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
        [ForeignKey(nameof(OrganistationId))]
        public Organisation Organisation{ get; set; }

        [InverseProperty(nameof(OrganisationUserRight.OrganisationUser))]
        public ICollection<OrganisationUserRight> OrganisationUserRights { get; set; }
        public long OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }
    }
}
