using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace TOTP
{
    public partial class frmSettings : Form
    {
        private bool _manualChange = false;

        public frmSettings()
        {
            InitializeComponent();
            Icon = Properties.Resources.knot;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            _manualChange = true;

            var settings = Settings.Load();
            chkStartOnLogin.Checked = settings.RunOnLogin;
            chkStartMinimized.Enabled = chkStartOnLogin.Checked;
            chkStartMinimized.Checked = settings.RunOnLogin && settings.StartMinimized;
            chkMinimizeToTray.Checked = settings.MinimizeToTray;
            chkSavePassword.Checked = settings.SavePassword;

            _manualChange = false;
        }

        private void chkStartOnLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (_manualChange)
                return;

            chkStartMinimized.Enabled = chkStartOnLogin.Checked;
            if (!chkStartMinimized.Enabled)
            {
                _manualChange = true;
                chkStartMinimized.Checked = false;
                _manualChange = false;
            }

            var settings = Settings.Load();
            settings.RunOnLogin = chkStartOnLogin.Checked;
            settings.Save();
        }

        private void chkStartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            if (_manualChange)
                return;

            var settings = Settings.Load();
            settings.StartMinimized = chkStartMinimized.Checked;
            settings.Save();
        }

        private void chkMinimizeToTray_CheckedChanged(object sender, EventArgs e)
        {
            if (_manualChange)
                return;

            var settings = Settings.Load();
            settings.MinimizeToTray = chkMinimizeToTray.Checked;
            settings.Save();
        }

        private void chkSavePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (_manualChange)
                return;

            var settings = Settings.Load();
            settings.SavePassword = chkSavePassword.Checked;
            settings.Password = settings.SavePassword ? Crypto.Protect(SavedSites.Password) : null;
            settings.Save();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            using var f = new frmChangePassword();
            f.ShowDialog();
        }

        
    }
}
