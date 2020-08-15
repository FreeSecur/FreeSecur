using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Logic.AccountManagement.Models
{
    public class UserRegistrationResponseModel
    {
        public UserRegistrationResponseModel(
            long id,
            string confirmationKey)
        {
            Id = id;
            ConfirmationKey = confirmationKey;
        }

        public long Id { get; }
        public string ConfirmationKey { get; }
    }
}
