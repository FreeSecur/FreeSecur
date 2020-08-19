using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.API.Logic.UserLogic.Models
{
    [MetadataType(typeof(User))]
    public class UserRegistrationModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [FsRequired]
        [FsUrl]
        public string ConfirmationUrl { get; set; }
    }
}
