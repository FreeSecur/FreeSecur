namespace FreeSecur.Domain.VaultItems
{
    public interface IVaultItem
    {
        int VaultItemId { get; set; }
        VaultItem VaultItem { get; set; }
    }
}
