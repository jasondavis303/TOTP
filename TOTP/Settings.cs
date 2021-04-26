using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TOTP
{
    public class Settings
    {
        private const string REG_KEY_PATH = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public bool SavePassword { get; set; }
        public string Password { get; set; }
        public bool StartMinimized { get; set; }
        public bool MinimizeToTray { get; set; }

        [XmlIgnore]
        public bool RunOnLogin { get; set; }
        
        public void Save()
        {
            if (!RunOnLogin)
                StartMinimized = false;

            using var fs = File.Create(Path2.SettingsFile);
            new XmlSerializer(typeof(Settings)).Serialize(fs, this);

            using var key = Registry.CurrentUser.CreateSubKey(REG_KEY_PATH);
            if (RunOnLogin)
                key.SetValue("TOTP", Application.ExecutablePath);
            else
                key.DeleteValue("TOTP", false);
        }

        public static Settings Load()
        {
            var ret = new Settings();

            try
            {
                using var fs = File.OpenRead(Path2.SettingsFile);
                ret = (Settings)new XmlSerializer(typeof(Settings)).Deserialize(fs);
            }
            catch { }

            using var key = Registry.CurrentUser.CreateSubKey(REG_KEY_PATH);
            string s = (string)key.GetValue("TOTP", string.Empty, RegistryValueOptions.None);
            ret.RunOnLogin = Application.ExecutablePath.Equals(s + string.Empty, StringComparison.CurrentCultureIgnoreCase);
            if (!ret.RunOnLogin)
                ret.StartMinimized = false;

            return ret;
        }

    }
}
