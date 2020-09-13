namespace FreeSecur.API.Core.Cryptography
{
    public interface ISecureRandomGenerator
    {
        string RandomString(int byteCount);
    }
}