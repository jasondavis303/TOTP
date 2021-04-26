using System;
using System.IO;

namespace TOTP
{
    static class Path2
    {
        static readonly string AppData = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TOTP")).FullName;

        static readonly string IconCache = Directory.CreateDirectory(Path.Combine(AppData, "icon_cache")).FullName;


        public static string DataFile = Path.Combine(AppData, "data");

        public static string IconFile(string issuer) => Path.Combine(IconCache, issuer + ".ico");

        public static string SettingsFile => Path.Combine(AppData, "settings.xml");
    }
}
