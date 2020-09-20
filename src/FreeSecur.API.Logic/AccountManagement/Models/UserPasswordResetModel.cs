using FreeSecur.API.Core.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccountManagement.Models
{
    public class UserPasswordResetModel
    {
        [FsRequired]
        public string Username { get; set; }

        [FsMinLength(10)]
        [FsRequired]
        public string Password { get; set; }

        [FsEquals(nameof(Password))]
        [FsRequired]
        public string PasswordVerification { get; set; }

        [FsRequired]
        public string Key { get; set; }
    }
}
