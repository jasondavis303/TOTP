using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TOTP
{
    /// <summary>
    /// This is designed to encrypt/decrypt relatively small strings in memory
    /// </summary>
    static class Crypto
    {
        private static Aes CreateAes(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            var ret = Aes.Create();

            //AES Key = 256 bits, SHA256 = 256 bits, so...
            using var sha = SHA256.Create();
            ret.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Just a tiny bit of obfuscation. Won't really add much extra security, but I like it
            //It's strange that I can't update via: ret.IV[0] = 0
            byte[] iv = new byte[16];
            for (int i = 0; i < 16; i++)
                iv[i] = ret.Key[31 - (i * 2)];
            ret.IV = iv;

            return ret;
        }

        public static string Encrypt(string data, string password)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data));

            using var aes = CreateAes(password);            
            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(data);
            sw.Flush();
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray(), Base64FormattingOptions.None);
        }

        public static string Decrypt(string data, string password)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data));

            using var aes = CreateAes(password);
            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(data));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            
            return sr.ReadToEnd();
        }


        /// <summary>
        /// Just some random entropy to help protect the saved password
        /// </summary>
        private static byte[] Entropy => new byte[] { 0xc1, 0x3c, 0x09, 0xaf, 0xec, 0x44, 0x48, 0x91, 0x90, 0xf8, 0xb9, 0xc0, 0x84, 0x95, 0x9d, 0xba };

        public static string Protect(string data) => Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(data), Entropy, DataProtectionScope.CurrentUser), Base64FormattingOptions.None);

        public static string Unprotect(string data) => Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(data), Entropy, DataProtectionScope.CurrentUser));
    }
}
