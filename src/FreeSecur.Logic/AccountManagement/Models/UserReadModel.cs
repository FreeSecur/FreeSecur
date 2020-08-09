using FreeSecur.Domain.Entities.Users;

namespace FreeSecur.Logic.UserLogic.Models
{
    public class UserReadModel
    {
        public UserReadModel(User user)
        {
            Email = user.Email;
            Username = user.Username;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
