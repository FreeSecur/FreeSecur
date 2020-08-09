using FreeSecur.Core.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FreeSecur.Core.UnitTests.Cryptography
{
    [TestClass]
    public class BCryptHashModuleTests
    {
        [TestMethod]
        public void CanVerifyStringWithSameText()
        {
            var plainText = "beautiful";
            var target = new BCryptHashModule();
            var hash = target.GetHash(plainText);

            var actual = target.Verify(plainText, hash);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CannotVerifyStringWithDifferentHash()
        {
            var plainText = "beautiful";
            var target = new BCryptHashModule();
            var hash = target.GetHash(plainText);

            var actual = target.Verify("Not so good", hash);

            Assert.IsFalse(actual);
        }
    }
}
