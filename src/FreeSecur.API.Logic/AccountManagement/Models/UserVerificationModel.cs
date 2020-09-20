using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccountManagement.Models
{
    public class UserVerificationModel
    {
        public UserVerificationModel()
        {

        }

        public UserVerificationModel(long userId, DateTime expirationDate, UserVerificationType userVerificationType)
        {
            UserId = userId;
            ExpirationDate = expirationDate;
            UserVerificationType = userVerificationType;
        }

        public long UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UserVerificationType UserVerificationType { get; set; }
    }

    public enum UserVerificationType
    {
        PasswordReset,
        ConfirmEmail
    }
}
