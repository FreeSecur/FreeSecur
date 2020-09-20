using FreeSecur.API.Core.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccountManagement.Models
{
    public class UserRequestPasswordResetModel
    {
        [FsRequired]
        public string Username { get; set; }
        [FsRequired]
        [FsUrl]
        public string PasswordResetUrl { get; set; }
    }
}
