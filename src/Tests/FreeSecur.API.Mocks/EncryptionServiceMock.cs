using FreeSecur.API.Core.Cryptography;
using System.Security.Cryptography;
using System.Text.Json;

namespace FreeSecur.API.Mocks
{
    public class EncryptionServiceMock : IEncryptionService
    {


        public string Decrypt(string encryptedText, ICryptoTransform decryptor)
            => encryptedText;

        public string Decrypt(string encryptedText, string key)
                    => encryptedText;

        public string Decrypt(string encryptedText)
                    => encryptedText;

        public T DecryptModel<T>(string encryptedText, string key)
            => JsonSerializer.Deserialize<T>(encryptedText);

        public T DecryptModel<T>(string encryptedText)
            => JsonSerializer.Deserialize<T>(encryptedText);

        public string Encrypt(string plainText, ICryptoTransform encryptor)
        => plainText;

        public string Encrypt(string plainText, string key)
               => plainText;

        public string Encrypt(string plainText)
                       => plainText;

        public string EncryptModel<T>(T model, string key)
                  => JsonSerializer.Serialize(model);

        public string EncryptModel<T>(T model)
                  => JsonSerializer.Serialize(model);
    }
}
