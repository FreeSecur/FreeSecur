using FreeSecur.API.Core.Validation.Attributes;
using FreeSecur.API.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.API.Logic.UserLogic.Models
{
    [MetadataType(typeof(User))]
    public class UserUpdateModel
    {
        [FsNotRequired]
        public string Email { get; set; }
        [FsNotRequired]
        public string FirstName { get; set; }
        [FsNotRequired]
        public string LastName { get; set; }
    }
}
