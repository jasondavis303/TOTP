using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TOTP
{
    public partial class frmMain : Form
    {
        private readonly List<KeyDisplay> _keyDisplays = new List<KeyDisplay>();

        public frmMain()
        {
            InitializeComponent();
            Icon = Properties.Resources.knot;
            niTray.Icon = Properties.Resources.knot;
        }

        public KeyDisplay SelectedKey { get; set; }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Text += $" v{SelfUpdatingApp.Installer.GetInstalledVersion(Program.APP_ID)}";

            LoadKeys();
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                var settings = Settings.Load();
                if(settings.MinimizeToTray)
                {
                    e.Cancel = true;
                    WindowState = FormWindowState.Minimized;
                    Hide();
                }
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            var settings = Settings.Load();
            niTray.Visible = settings.MinimizeToTray;
            if (settings.StartMinimized)
            {
                WindowState = FormWindowState.Minimized;
                if (settings.MinimizeToTray)
                    Hide();
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                var settings = Settings.Load();
                if (settings.MinimizeToTray)
                    Hide();
            }
        }

        private void KeyDisplay_OnFocused(object sender, EventArgs e)
        {
            SelectedKey = (KeyDisplay)sender;
            Clipboard.SetText(SelectedKey.CurrentCode);
            foreach (var kd in _keyDisplays)
                kd.BackColor = kd == SelectedKey ? SystemColors.ControlDark : SystemColors.Control;

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var f = new frmKey();
            if (f.ShowDialog() != DialogResult.OK)
                return;
            SavedSites.Keys.Add(f.Key);
            SavedSites.Save();
            LoadKeys();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selKey = SelectedKey.Key;

            using var f = new frmKey(selKey);
            if (f.ShowDialog() != DialogResult.OK)
                return;

            f.Key.CopyTo(selKey);
            SavedSites.Save();
            LoadKeys();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selKey = SelectedKey.Key;

            string msg = $"Are you sure you want to delete the selected key ({selKey.Issuer})?";
            var ans = MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ans != DialogResult.Yes)
                return;

            SavedSites.Keys.Remove(selKey);
            SavedSites.Save();
            LoadKeys();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (ofdImport.ShowDialog() != DialogResult.OK)
                return;

            using var f = new frmImportExportPassword(false);
            while (true)
            {
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    SavedSites.Import(ofdImport.FileName, f.Password);
                    LoadKeys();
                    return;
                }
                catch
                {
                    MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (sfdExport.ShowDialog() != DialogResult.OK)
                return;

            using var f = new frmImportExportPassword(true);
            if (f.ShowDialog() != DialogResult.OK)
                return;

            SavedSites.Export(sfdExport.FileName, f.Password);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using var f = new frmSettings();
            f.ShowDialog();

            var settings = Settings.Load();
            niTray.Visible = settings.MinimizeToTray;
        }


        private void mnuShow_Click(object sender, EventArgs e)
        {
            Show();
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        private void LoadKeys()
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            _keyDisplays.Clear();
            tlpMain.Controls.Clear();
            while (tlpMain.RowCount > 1)
            {
                tlpMain.RowStyles.RemoveAt(tlpMain.RowCount - 1);
                tlpMain.RowCount--;
            }

            SavedSites.Load();
            SavedSites.Sort();

            int cnt = 0;
            foreach (var key in SavedSites.Keys)
            {
                if (tlpMain.RowCount == cnt)
                {
                    tlpMain.RowCount++;
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }

                var kd = new KeyDisplay(key);
                _keyDisplays.Add(kd);
                kd.OnFocused += KeyDisplay_OnFocused;

                tlpMain.Controls.Add(kd, 0, tlpMain.RowCount - 1);
                kd.Dock = DockStyle.Top;
                cnt++;
            }

            btnExport.Enabled = cnt > 0;
        }
    }
}
