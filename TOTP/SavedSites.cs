using BasicOTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TOTP
{
    static class SavedSites
    {
        private const string MAGIC_TEXT = "If you can read this, I was decrypted successfully!";

        public static string Password { get; set; }

        public static List<OtpKey> Keys { get; } = new List<OtpKey>();

        public static void Load()
        {
            Keys.Clear();

            if (!File.Exists(Path2.DataFile))
                return;

            var lst = File.ReadAllLines(Path2.DataFile);

            string test = Crypto.Decrypt(lst[0], Password);
            if (test != MAGIC_TEXT)
                throw new Exception("Invalid password!");

            for(int i = 1; i < lst.Length; i++)
                Keys.Add(OtpKey.FromString(Crypto.Decrypt(lst[i], Password)));
        }

        public static void Sort()
        {
            Keys.Sort((x, y) =>
            {
                int ret = x.Issuer.CompareTo(y.Issuer);
                if (ret == 0)
                    ret = x.Account.CompareTo(y.Account);
                return ret;
            });
        }

        public static void Save()
        {
            Delete();
            if (Keys.Count == 0)
                return;

            var lst = new List<string>
            {
                Crypto.Encrypt(MAGIC_TEXT, Password)
            };

            foreach (var key in Keys)
                lst.Add(Crypto.Encrypt(key.ToString(), Password));
            File.WriteAllLines(Path2.DataFile, lst);
        }

        public static void Export(string filename, string password)
        {
            var lst = new List<string>
            {
                Crypto.Encrypt(MAGIC_TEXT, password)
            };

            foreach (var key in Keys)
                lst.Add(Crypto.Encrypt(key.ToString(), password));
            File.WriteAllLines(filename, lst);
        }

        public static bool Import(string filename, string password)
        {
            bool ret = false;

            if (!File.Exists(filename))
                return ret;

            var lst = File.ReadAllLines(filename);

            string test = Crypto.Decrypt(lst[0], password);
            if (test != MAGIC_TEXT)
                throw new Exception("Invalid password!");

            for (int i = 1; i < lst.Length; i++)
            {
                string url = Crypto.Decrypt(lst[i], password);
                var existing = Keys.FirstOrDefault(item => item.ToString() == url);
                if (existing == null)
                {
                    Keys.Add(OtpKey.FromString(url));
                    ret = true;
                }
            }

            if (ret)
                Save();

            return ret;
        }

        public static void Delete()
        {
            try { File.Delete(Path2.DataFile); }
            catch { }
        }
    }
}
