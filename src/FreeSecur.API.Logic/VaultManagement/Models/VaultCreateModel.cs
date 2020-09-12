using FreeSecur.API.Domain.Entities.Vaults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.VaultManagement.Models
{
    [MetadataType(typeof(Vault))]
    public class VaultCreateModel
    {
        public string Name { get; set; }
        public string MasterKey { get; set; }
    }
}
