namespace FreeSecur.API.Domain.Entities.Owners
{
    public interface IOwner
    {
        public long OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
