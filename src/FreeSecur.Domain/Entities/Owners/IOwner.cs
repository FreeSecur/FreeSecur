namespace FreeSecur.Domain.Entities.Owners
{
    public interface IOwner
    {
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
