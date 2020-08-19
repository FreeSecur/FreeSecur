using FreeSecur.API.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.API.Logic.UserLogic.Models
{
    [MetadataType(typeof(User))]
    public class UserUpdateModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
