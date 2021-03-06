using System;
using System.IO;

namespace TOTP
{
    static class Path2
    {
        static readonly string AppData = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TOTP")).FullName;

        static readonly string IconCache = Directory.CreateDirectory(Path.Combine(AppData, "icon_cache")).FullName;


        public static string DataFile = Path.Combine(AppData, "data");

        public static string IconFile(string issuer)
        {
            string ret = Path.Combine(IconCache, issuer);
            
            if (File.Exists(ret + ".png"))
                return ret + ".png";

            if (File.Exists(ret + ".jpg"))
                return ret + ".jpg";

            if (File.Exists(ret + ".bmp"))
                return ret + ".bmp";

            //Default
            return ret + ".ico";        
        }

        public static string SettingsFile => Path.Combine(AppData, "settings.xml");
    }
}
