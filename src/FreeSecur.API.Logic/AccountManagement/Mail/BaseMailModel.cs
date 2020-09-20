namespace FreeSecur.API.Logic.AccountManagement.Mail
{
    public class BaseMailModel
    {
        public BaseMailModel(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }
}
