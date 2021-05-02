using System;
using System.Security.Cryptography;
using System.Text;

namespace TOTP
{
    static class Crypto
    {
        /// <summary>
        /// Just some random entropy to help protect the saved password
        /// </summary>
        private static byte[] Entropy => new byte[] { 0xc1, 0x3c, 0x09, 0xaf, 0xec, 0x44, 0x48, 0x91, 0x90, 0xf8, 0xb9, 0xc0, 0x84, 0x95, 0x9d, 0xba };

        public static string Protect(string data) => Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(data), Entropy, DataProtectionScope.CurrentUser), Base64FormattingOptions.None);

        public static string Unprotect(string data) => Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(data), Entropy, DataProtectionScope.CurrentUser));
    }
}
