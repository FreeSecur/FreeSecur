namespace FreeSecur.API.Domain.VaultItems
{
    public interface IVaultItem
    {
        int VaultItemId { get; set; }
        VaultItem VaultItem { get; set; }
    }
}
