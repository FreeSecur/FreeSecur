﻿namespace FreeSecur.API.Core.Cryptography
{
    public interface IHashService
    {
        HashResult GetHash(string plainText);
        bool Verify(string plainText, string hash, string salt);
    }
}