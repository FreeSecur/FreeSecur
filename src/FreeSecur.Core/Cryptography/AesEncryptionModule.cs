using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core.Cryptography
{
    public class AesEncryptionModule : IEncryptionModule
    {

        public string Encrypt(string plainText, string key)
        {
            var keys = GetHashKeys(key);
            var encryptor = CreateAesEncryptor(keys[0], keys[1]);
            var encryptedData = Encrypt(plainText, encryptor);
            return encryptedData;
        }

        public string Encrypt(string plainText, ICryptoTransform encryptor)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                csEncrypt.FlushFinalBlock();
                var encryptedBytes = msEncrypt.ToArray();

                return Convert.ToBase64String(encryptedBytes);
            }
        }


        public string Decrypt(string encryptedText, string key)
        {
            var keys = GetHashKeys(key);
            var decryptor = CreateAesDecryptor(keys[0], keys[1]);
            var decryptedData = Decrypt(encryptedText, decryptor);
            return decryptedData;
        }

        public string Decrypt(string encryptedText, ICryptoTransform decryptor)
        {
            if (string.IsNullOrEmpty(encryptedText))
                throw new ArgumentNullException(nameof(encryptedText));

            var encryptedBytes = Convert.FromBase64String(encryptedText);

            if (encryptedBytes == null || encryptedBytes.Length <= 0)
                throw new ArgumentNullException("cipherText");


            using (MemoryStream decryptMemoryStream = new MemoryStream(encryptedBytes))
            using (CryptoStream decryptoStream = new CryptoStream(decryptMemoryStream, decryptor, CryptoStreamMode.Read))
            using (StreamReader decryptedStreamReader = new StreamReader(decryptoStream))
            {
                try
                {
                    var plainText = decryptedStreamReader.ReadToEnd();
                    return plainText;
                }
                catch (CryptographicException cryptoException)
                {
                    throw new CryptographicException("Failed to decrypt value", cryptoException);
                }
            }
        }

        private ICryptoTransform CreateAesEncryptor(byte[] Key, byte[] IV)
        {
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                return encryptor;
            }
        }

        private ICryptoTransform CreateAesDecryptor(byte[] Key, byte[] IV)
        {
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                return decryptor;
            }
        }

        private byte[][] GetHashKeys(string key)
        {
            var encoding = Encoding.UTF8;

            var sha2 = new SHA256CryptoServiceProvider();

            var rawKey = encoding.GetBytes(key);
            var rawIV = encoding.GetBytes(key);

            var hashKey = sha2.ComputeHash(rawKey);
            var hashIV = sha2.ComputeHash(rawIV);

            Array.Resize(ref hashIV, 16);

            var result = new byte[2][]
            {
                hashKey,
                hashIV
            };

            return result;
        }
    }
}
