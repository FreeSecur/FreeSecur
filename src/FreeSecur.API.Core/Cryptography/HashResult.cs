namespace FreeSecur.API.Core.Cryptography
{
    public class HashResult
    {
        public HashResult(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
        public string Hash { get; }
        public string Salt { get; }
    }
}
