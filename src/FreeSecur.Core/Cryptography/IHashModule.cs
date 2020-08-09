namespace FreeSecur.Core.Cryptography
{
    public interface IHashModule
    {
        string GetHash(string plainText);
        bool Verify(string plainText, string hash);
    }
}