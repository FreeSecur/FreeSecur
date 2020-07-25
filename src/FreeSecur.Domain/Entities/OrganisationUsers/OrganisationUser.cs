using FreeSecur.Domain.Entities.Organisations;
using FreeSecur.Domain.Entities.OrganisationUserRights;
using FreeSecur.Domain.Entities.Owners;
using FreeSecur.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain.Entities.OrganisationUsers
{
    public class OrganisationUser : IFsTrackedEntity, IOwner
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrganistationId { get; set; }

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
