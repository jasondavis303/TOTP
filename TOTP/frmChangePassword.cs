using System;
using System.Windows.Forms;

namespace TOTP
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void TB_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled =
                !string.IsNullOrWhiteSpace(tbOld.Text) &&
                !string.IsNullOrWhiteSpace(tbNew.Text) &&
                tbConfirm.Text == tbNew.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SavedSites.Password = tbOld.Text;
            try
            {
                SavedSites.Load();
            }
            catch
            {
                MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SavedSites.Password = tbNew.Text;
            SavedSites.Save();

            var settings = Settings.Load();
            if(settings.SavePassword)
            {
                settings.Password = Crypto.Protect(tbNew.Text);
                settings.Save();
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
