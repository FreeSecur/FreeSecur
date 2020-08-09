using BCryptHasher = BCrypt.Net.BCrypt;

namespace FreeSecur.Core.Cryptography
{
    public class BCryptHashModule : IHashModule
    {
        public string GetHash(string plainText)
            => BCryptHasher.EnhancedHashPassword(plainText);

        public bool Verify(string plainText, string hash)
            => BCryptHasher.EnhancedVerify(plainText, hash);
    }
}
