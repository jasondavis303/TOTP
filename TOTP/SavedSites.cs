using BasicOTP;
using Krypto.WonderDog;
using Krypto.WonderDog.Symmetric;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TOTP
{
    static class SavedSites
    {
        public static List<OtpKey> Keys { get; } = new List<OtpKey>();

        public static void Load()
        {
            Keys.Clear();

            if (!File.Exists(Path2.DataFile))
                return;

            var lst = File.ReadAllLines(Path2.DataFile);

            foreach (string line in lst)
                Keys.Add(OtpKey.FromString(Crypto.Unprotect(line)));
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

            var lst = new List<string>();

            foreach (var key in Keys)
                lst.Add(Crypto.Protect(key.ToString()));

            File.WriteAllLines(Path2.DataFile, lst);
        }

        public static void Export(string filename, string password)
        {
            var lst = new List<string>();
            var kryptKey = new Key(password, new byte[8]);

            foreach (var key in Keys)
            {
                var alg = SymmetricFactory.CreateAES();
                lst.Add(alg.Encrypt(kryptKey, key.ToString()));
            }

            File.WriteAllLines(filename, lst);
        }

        public static bool Import(string filename, string password)
        {
            bool ret = false;

            if (!File.Exists(filename))
                return ret;

            var kryptKey = new Key(password, new byte[8]);
            var alg = SymmetricFactory.CreateAES();

            var lst = File.ReadAllLines(filename);

            foreach(string line in lst)
            {
                string url = alg.Decrypt(kryptKey, line);
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
