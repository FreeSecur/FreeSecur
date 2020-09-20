using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccountManagement.Mail
{
    public class PasswordResetMailModel : BaseMailModel
    {
        public PasswordResetMailModel(string firstName, string lastName, string passwordResetUrl) : base(firstName, lastName)
        {
            PasswordResetUrl = passwordResetUrl;
        }

        public string PasswordResetUrl { get; }
    }
}
