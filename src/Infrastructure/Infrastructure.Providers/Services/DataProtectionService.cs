using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Providers.Services
{
    public class DataProtectionService : IDataProtectionService
    {
        public DataProtectionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public string Unprotect(string text)
        {
            var cryptoKey = _configuration.GetSection("CryptoKey").Value;
            var keybytes = Encoding.UTF8.GetBytes(cryptoKey);
            var cipherText = Convert.FromBase64String(text);
            var IV = Encoding.UTF8.GetBytes(cryptoKey);

            string plaintext = null;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.ECB;
                aes.Key = keybytes;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                if (decryptor == null || !decryptor.CanReuseTransform)
                {
                    decryptor?.Dispose();
                    decryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                }
                using MemoryStream ms = new MemoryStream(cipherText);
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using StreamReader reader = new StreamReader(cs);
                plaintext = reader.ReadToEnd();
            }

            return plaintext;
        }
    }
}