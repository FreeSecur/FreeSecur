using FreeSecur.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain
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
