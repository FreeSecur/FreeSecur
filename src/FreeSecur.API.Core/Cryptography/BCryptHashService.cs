using Microsoft.VisualBasic.CompilerServices;
using BCryptHasher = BCrypt.Net.BCrypt;

namespace FreeSecur.API.Core.Cryptography
{
    public class BCryptHashService : IHashService
    {
        private readonly ISecureRandomGenerator _secureRandomGenerator;
        private readonly IEncryptionService _encryptionService;

        public BCryptHashService(
            ISecureRandomGenerator secureRandomGenerator,
            IEncryptionService encryptionService)
        {
            _secureRandomGenerator = secureRandomGenerator;
            _encryptionService = encryptionService;
        }

        public HashResult GetHash(string plainText)
        {

            var salt = _secureRandomGenerator.RandomString(32);
            var saltedText = salt + plainText;
            var encryptedText = _encryptionService.Encrypt(saltedText);
            var hash = BCryptHasher.EnhancedHashPassword(encryptedText);

            return new HashResult(hash, salt);
        }
            

        public bool Verify(string plainText, string hash, string salt)
        {
            var saltedText = salt + plainText;
            var encryptedText = _encryptionService.Encrypt(saltedText);

            return BCryptHasher.EnhancedVerify(encryptedText, hash);
        }
    }
}
