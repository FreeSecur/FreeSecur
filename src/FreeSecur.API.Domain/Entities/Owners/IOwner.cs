namespace FreeSecur.API.Domain.Entities.Owners
{
    public interface IOwner
    {
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
