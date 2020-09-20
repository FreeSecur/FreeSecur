using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccountManagement.Mail
{

    public class ConfirmationMailModel : BaseMailModel
    {
        public ConfirmationMailModel(string confirmationUrl, string firstName, string lastName) : base(firstName, lastName)
        {
            ConfirmationUrl = confirmationUrl;
        }

        public string ConfirmationUrl { get; }
    }
}
