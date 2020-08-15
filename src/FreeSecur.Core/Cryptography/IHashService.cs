namespace FreeSecur.Core.Cryptography
{
    public interface IHashService
    {
        string GetHash(string plainText);
        bool Verify(string plainText, string hash);
    }
}