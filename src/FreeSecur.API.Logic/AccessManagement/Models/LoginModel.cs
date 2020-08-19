using FreeSecur.API.Core.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccessManagement.Models
{
    public class LoginModel
    {
        [FsRequired]
        public string Username { get; set; }
        [FsRequired]
        public string Password { get; set; }
    }
}
