using System.IO;
using System.Net;

namespace TOTP
{
    static class IconCache
    {
        public static void TryDownload(string issuer)
        {
            string url = issuer + ".com/favicon.ico";
            string filename = Path2.IconFile(issuer);

            if(!Download("https://" + url, filename))
                Download("http://" + url, filename);
        }

        private static bool Download(string url, string filename)
        {
            try
            {
                if (File.Exists(filename))
                    File.Delete(filename);
                using var wc = new WebClient();
                wc.DownloadFile(url, filename);
                return true;
            }
            catch { }

            return false;
        }
    }
}
