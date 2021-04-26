using System;
using System.IO;
using System.Windows.Forms;

namespace TOTP
{
    public partial class frmPassword : Form
    {
        private Settings _settings;

        public frmPassword()
        {
            InitializeComponent();
        }

        private void frmPassword_Load(object sender, EventArgs e)
        {
            Show();

            _settings = Settings.Load();
            
            if (!File.Exists(Path2.DataFile))
            {
                lblConfirm.Visible = true;
                tbConfirm.Visible = true;
                pnlSpacer.Visible = true;
                MessageBox.Show("You must create a master password", "Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (_settings.SavePassword)
            {
                chkSave.Checked = true;
                try { tbPassword.Text = Crypto.Unprotect(_settings.Password); }
                catch { }
                this.ActiveControl = btnLogin;
                btnLogin_Click(btnLogin, EventArgs.Empty);
            }
            else
            {
               this.ActiveControl = tbPassword;
            }

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            bool b = !string.IsNullOrWhiteSpace(tbPassword.Text);
            if (b && tbConfirm.Visible)
                b = tbConfirm.Text == tbPassword.Text;
            btnLogin.Enabled = b;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SavedSites.Password = tbPassword.Text;
            if (File.Exists(Path2.DataFile))
                try
                {
                    SavedSites.Load();
                }
                catch
                {
                    MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            if (chkSave.Checked)
            {
                _settings.SavePassword = true;
                _settings.Password = Crypto.Protect(tbPassword.Text);
            }
            else
            {
                _settings.SavePassword = false;
                _settings.Password = null;
            }
            _settings.Save();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
