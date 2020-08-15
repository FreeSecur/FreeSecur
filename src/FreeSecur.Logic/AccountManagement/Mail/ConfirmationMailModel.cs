using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Logic.AccountManagement.MailModels
{
    public class ConfirmationMailModel
    {
        public ConfirmationMailModel(string confirmationUrl, string firstName, string lastName)
        {
            ConfirmationUrl = confirmationUrl;
            FirstName = firstName;
            LastName = lastName;
        }

        public string ConfirmationUrl { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
