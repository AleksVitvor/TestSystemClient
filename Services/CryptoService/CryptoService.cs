using System;
using System.Security.Cryptography;
using System.Text;

namespace Services.CryptoService
{
    public class CryptoService : ICrypto
    {
        public string SHA256GetHash(string source)
        {
            using SHA256 sha256Hash = SHA256.Create();

            byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
            byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            return hash;
        }
    }
}
