using System;
using System.Windows.Forms;

namespace TOTP
{
    public partial class frmImportExportPassword : Form
    {
        const string EXPORT_DESC = "Enter a password to encrypt the file.";
        const string IMPORT_DESC = "Enter the password to decrypt the file.";

        private readonly bool _exportMode;

        public frmImportExportPassword(bool exportMode)
        {
            InitializeComponent();
            Icon = Properties.Resources.knot;

            _exportMode = exportMode;
            Text = _exportMode ? "Export Password" : "Import Password";
            lblDesc.Text = _exportMode ? EXPORT_DESC : IMPORT_DESC;
            lblConfirm.Visible = exportMode;
            tbConfirm.Visible = exportMode;
            pnlSpacer.Visible = exportMode;
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }

        private void tbConfirm_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }

        public string Password => tbPassword.Text;

        private void EnableSave()
        {
            btnSave.Enabled = string.IsNullOrWhiteSpace(tbPassword.Text) ||
                !tbConfirm.Visible || 
                tbConfirm.Text == tbPassword.Text;
        }
    }
}
