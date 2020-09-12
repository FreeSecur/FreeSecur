using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.VaultManagement.Models
{
    public class VaultDetailsModel
    {
        public VaultDetailsModel(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
