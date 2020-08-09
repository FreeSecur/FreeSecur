using System.Security.Cryptography;

namespace FreeSecur.Core.Cryptography
{
    public interface IEncryptionModule
    {
        string Decrypt(string encryptedText, ICryptoTransform decryptor);
        string Decrypt(string encryptedText, string key);
        string Encrypt(string plainText, ICryptoTransform encryptor);
        string Encrypt(string plainText, string key);
    }
}