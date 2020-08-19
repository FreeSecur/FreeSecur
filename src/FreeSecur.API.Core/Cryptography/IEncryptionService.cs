using System.Security.Cryptography;

namespace FreeSecur.API.Core.Cryptography
{
    public interface IEncryptionService
    {
        string Decrypt(string encryptedText, ICryptoTransform decryptor);
        string Decrypt(string encryptedText, string key);
        string Decrypt(string encryptedText);
        T DecryptModel<T>(string encryptedText, string key);
        T DecryptModel<T>(string encryptedText);
        string Encrypt(string plainText, ICryptoTransform encryptor);
        string Encrypt(string plainText, string key);
        string Encrypt(string plainText);
        string EncryptModel<T>(T model, string key);
        string EncryptModel<T>(T model);
    }
}