using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Domain.Entities.Owners
{
    public interface IOwner
    {
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
