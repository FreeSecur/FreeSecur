using FreeSecur.API.Domain.Entities.Users;

namespace FreeSecur.API.Logic.UserLogic.Models
{
    public class UserReadModel
    {
        public UserReadModel()
        {

        }

        public UserReadModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public long? Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
