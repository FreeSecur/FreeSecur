using FreeSecur.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.Logic.UserLogic.Models
{
    [MetadataType(typeof(User))]
    public class UserCreateModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
