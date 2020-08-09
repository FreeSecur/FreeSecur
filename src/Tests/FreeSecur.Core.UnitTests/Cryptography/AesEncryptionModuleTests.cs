using FreeSecur.Core.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace FreeSecur.Core.UnitTests.Cryptography
{
    [TestClass]
    public class AesEncryptionModuleTests
    {
        private const string _key = "12345";
        private const string _key2 = "54321";

        [TestMethod]
        public void CanDecryptWithSameKey()
        {
            var expected = "beautiful";

            var target = new AesEncryptionModule();

            var encryptedValue = target.Encrypt(expected, _key);
            var actual = target.Decrypt(encryptedValue, _key);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CannotDecryptWithDifferentKey()
        {
            var expected = "beautiful";

            var target = new AesEncryptionModule();

            var encryptedValue = target.Encrypt(expected, _key);

            Assert.ThrowsException<CryptographicException>(() => target.Decrypt(encryptedValue, _key2));
        }
    }
}
