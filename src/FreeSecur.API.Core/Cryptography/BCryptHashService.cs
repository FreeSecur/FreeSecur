using BCryptHasher = BCrypt.Net.BCrypt;

namespace FreeSecur.API.Core.Cryptography
{
    public class BCryptHashService : IHashService
    {
        public string GetHash(string plainText)
            => BCryptHasher.EnhancedHashPassword(plainText);

        public bool Verify(string plainText, string hash)
            => BCryptHasher.EnhancedVerify(plainText, hash);
    }
}
