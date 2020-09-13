using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.OrganisationUsers;
using FreeSecur.API.Domain.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSecur.API.Domain.Entities.OrganisationUserRights
{
    public class OrganisationUserRight : IFsTrackedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [FsRequired]
        public long OrganisationUserId { get; set; }
        [FsRequired]
        public OrganisationUserRightType AccessRight { get; set; }

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

        [ForeignKey(nameof(OrganisationUserId))]
        public OrganisationUser OrganisationUser{ get; set; }
    }
}
