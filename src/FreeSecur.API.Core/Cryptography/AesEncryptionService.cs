﻿using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FreeSecur.API.Core.Cryptography
{
    public class AesEncryptionService : IEncryptionService
    {
        private readonly IOptions<FsEncryption> _options;
        private readonly ISerializer _fsSerializer;

        public AesEncryptionService(
            IOptions<FsEncryption> options,
            ISerializer fsSerializer)
        {
            _options = options;
            _fsSerializer = fsSerializer;
        }

        public string EncryptModel<T>(T model)
        {
            var serializedModel = _fsSerializer.Serialize(model);
            return Encrypt(serializedModel);
        }

        public string EncryptModel<T>(T model, string key)
        {
            var serializedModel = _fsSerializer.Serialize(model);
            return Encrypt(serializedModel, key);
        }

        public string Encrypt(string plainText)
        {
            var settings = _options.Value;
            var encryptionKey = settings.DefaultEncryptionKey;

            return Encrypt(plainText, encryptionKey);
        }

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

            using (var encryptMemoryStream = new MemoryStream())
            using (var encryptCryptoStream = new CryptoStream(encryptMemoryStream, encryptor, CryptoStreamMode.Write))
            {
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                encryptCryptoStream.Write(plainBytes, 0, plainBytes.Length);
                encryptCryptoStream.FlushFinalBlock();
                var encryptedBytes = encryptMemoryStream.ToArray();

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public T DecryptModel<T>(string encryptedText)
        {
            var plainText = Decrypt(encryptedText);
            var model = _fsSerializer.Deserialize<T>(plainText);
            return model;
        }

        public T DecryptModel<T>(string encryptedText, string key)
        {
            var plainText = Decrypt(encryptedText, key);
            var model = _fsSerializer.Deserialize<T>(plainText);
            return model;
        }

        public string Decrypt(string encryptedText)
        {
            var settings = _options.Value;
            var encryptionKey = settings.DefaultEncryptionKey;

            return Decrypt(encryptedText, encryptionKey);
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

            using (var decryptMemoryStream = new MemoryStream(encryptedBytes))
            using (var decryptoStream = new CryptoStream(decryptMemoryStream, decryptor, CryptoStreamMode.Read))
            using (var decryptedStreamReader = new StreamReader(decryptoStream))
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

            using (var aes = new AesManaged())
            {
                aes.Key = Key;
                aes.IV = IV;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                return encryptor;
            }
        }

        private ICryptoTransform CreateAesDecryptor(byte[] Key, byte[] IV)
        {
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (var aes = Aes.Create())
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
