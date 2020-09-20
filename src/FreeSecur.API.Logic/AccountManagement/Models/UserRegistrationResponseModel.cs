using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccountManagement.Models
{
    public class UserRegistrationResponseModel
    {
        public UserRegistrationResponseModel(
            long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
