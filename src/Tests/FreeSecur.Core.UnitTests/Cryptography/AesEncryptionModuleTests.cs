using FreeSecur.Core.Cryptography;
using FreeSecur.Core.Serialization;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text.Json;

namespace FreeSecur.Core.UnitTests.Cryptography
{
    

    [TestClass]
    public class AesEncryptionModuleTests
    {
        private const string _key = "12345";
        private const string _key2 = "54321";
        private OptionsMock<FsEncryption> _options => new OptionsMock<FsEncryption>(new FsEncryption { DefaultEncryptionKey = "1234" });
        private FsJsonSerializer _serializer = new FsJsonSerializer(FsSerialization.GetDefaultJsonSerializerOptions());

        [TestMethod]
        public void CanDecryptWithSameKey()
        {
            var expected = "beautiful";

            var target = new AesEncryptionService(_options, _serializer);

            var encryptedValue = target.Encrypt(expected, _key);
            var actual = target.Decrypt(encryptedValue, _key);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CannotDecryptWithDifferentKey()
        {
            var expected = "beautiful";

            var target = new AesEncryptionService(_options, _serializer);

            var encryptedValue = target.Encrypt(expected, _key);

            Assert.ThrowsException<CryptographicException>(() => target.Decrypt(encryptedValue, _key2));
        }
    }
}
