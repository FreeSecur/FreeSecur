using System.Security.Cryptography;
using System.Text;

namespace FreeSecur.API.Core.Cryptography
{
    public class SecureRandomGenerator : ISecureRandomGenerator
    {
        private readonly RNGCryptoServiceProvider _cryptoServiceProvider;

        public SecureRandomGenerator()
        {
            _cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public string RandomString(int byteCount)
        {
            var byteArray = new byte[byteCount];

            _cryptoServiceProvider.GetNonZeroBytes(byteArray);

            return Encoding.Default.GetString(byteArray);
        }
    }
}
