using FreeSecur.Core.Validation.Attributes;
using FreeSecur.Domain.Entities.Organisations;
using FreeSecur.Domain.Entities.OrganisationUserRights;
using FreeSecur.Domain.Entities.Owners;
using FreeSecur.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.Domain.Entities.OrganisationUsers
{
    public class OrganisationUser : IFsTrackedEntity, IOwner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [FsRequired]
        public int UserId { get; set; }
        [FsRequired]
        public int OrganistationId { get; set; }

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
        [ForeignKey(nameof(OrganistationId))]
        public Organisation Organisation{ get; set; }

        [InverseProperty(nameof(OrganisationUserRight.OrganisationUser))]
        public ICollection<OrganisationUserRight> OrganisationUserRights { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }
    }
}
