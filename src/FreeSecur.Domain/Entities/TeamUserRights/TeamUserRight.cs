using FreeSecur.Core.Validation.Attributes;
using FreeSecur.Domain.Entities.Teams;
using FreeSecur.Domain.Entities.TeamUsers;
using FreeSecur.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain.Entities.TeamUserRights
{
    public class TeamUserRight : IFsTrackedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [FsRequired]
        public int TeamUserId { get; set; }
        [FsRequired]
        public TeamUserRightType AccessRight { get; set; }

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

        [ForeignKey(nameof(TeamUserId))]
        public TeamUser TeamUser { get; set; }
    }
}
