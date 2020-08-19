using FreeSecur.API.Domain.Entities.Users;
using System;

namespace FreeSecur.API.Domain
{
    public interface IFsTrackedEntity : IFsEntity
    {
        int CreatedById { get; set; }
        int ModifiedById { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
        public User CreatedByUser { get; set; }
        public User ModifiedByUser { get; set; }
    }
}
