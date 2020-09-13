using FreeSecur.API.Core.Cryptography;
using FreeSecur.API.Core.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace FreeSecur.API.Core.UnitTests.Cryptography
{
    [TestClass]
    public class BCryptHashModuleTests
    {

        private ISecureRandomGenerator _secureRandomGenerator = new SecureRandomGenerator();
        private IEncryptionService _encryptionService = new AesEncryptionService(new OptionsMock<FsEncryption>(new FsEncryption { 
            DefaultEncryptionKey = "12334253421234123"
        }), new JsonSerializer(new JsonSerializerOptions()));

        [TestMethod]
        public void CanVerifyStringWithSameText()
        {
            var plainText = "beautiful";
            var target = new BCryptHashService(_secureRandomGenerator, _encryptionService);
            var hashResult = target.GetHash(plainText);

            var actual = target.Verify(plainText, hashResult.Hash, hashResult.Salt);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CannotVerifyStringWithDifferentHash()
        {
            var plainText = "beautiful";
            var target = new BCryptHashService(_secureRandomGenerator, _encryptionService);
            var hashResult = target.GetHash(plainText);

            var actual = target.Verify("Not so good", hashResult.Hash, hashResult.Salt);

            Assert.IsFalse(actual);
        }
    }
}
