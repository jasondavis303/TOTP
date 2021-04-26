using BasicOTP;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TOTP
{
    public partial class frmKey : Form
    {
        private readonly bool _editMode;

        public frmKey()
        {
            InitializeComponent();
            Icon = Properties.Resources.knot;
            Key = new OtpKey();
        }

        public frmKey(OtpKey key) : this()
        {
            _editMode = true;
            key.CopyTo(Key);
            tbURL.Text = Key.ToString();
            UpdateAll();
        }

        public OtpKey Key { get; set; } = new OtpKey();

        private void tbURL_TextChanged(object sender, EventArgs e)
        {
            if (tbURL.Focused)
            {
                try { Key = OtpKey.FromString(tbURL.Text); }
                catch { Key = new OtpKey(); }
                UpdateAll();
            }
        }

        private void tbIssuer_TextChanged(object sender, EventArgs e)
        {
            if(tbIssuer.Focused)
            {
                Key.Issuer = tbIssuer.Text;
                UpdateKey();
            }
        }

        private void tbAccount_TextChanged(object sender, EventArgs e)
        {
            if(tbAccount.Focused)
            {
                Key.Account = tbAccount.Text;
                UpdateKey();
            }
        }

        private void cbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbAlgorithm.Focused)
            {
                Key.Algorithm = (Algorithms)cbAlgorithm.SelectedIndex;
                UpdateKey();
            }
        }

        private void nudPeriod_ValueChanged(object sender, EventArgs e)
        {
            if(nudPeriod.Focused)
            {
                Key.Period = (uint)nudPeriod.Value;
                UpdateKey();
            }
        }

        private void nudDigits_ValueChanged(object sender, EventArgs e)
        {
            if(nudDigits.Focused)
            {
                Key.Digits = (uint)nudDigits.Value;
                UpdateKey();
            }
        }

        private void tbSecret_TextChanged(object sender, EventArgs e)
        {
            if(tbSecret.Focused)
            {
                Key.Secret = tbSecret.Text.Replace(" ", string.Empty);
                UpdateKey();
            }
        }
        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (ofdQR.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                using var img = Image.FromFile(ofdQR.FileName);
                Key = OtpKey.FromQR(img);
                tbURL.Text = Key.ToString();
                UpdateAll();
            }
            catch
            {
                MessageBox.Show("Unable to decode QR image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveQR_Click(object sender, EventArgs e)
        {
            if (sfdQR.ShowDialog() != DialogResult.OK)
                return;
            using var img = Key.ToQR();
            img.Save(sfdQR.FileName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach(var existingKey in SavedSites.Keys)
                if(existingKey.ToString().Equals(Key.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    if(!_editMode)
                        MessageBox.Show("There is already an identical key saved in your list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            DialogResult = DialogResult.OK;
            Close();
        }


        private void UpdateAll()
        {
            tbIssuer.Text = Key.Issuer;
            tbAccount.Text = Key.Account;
            cbAlgorithm.SelectedIndex = (int)Key.Algorithm;
            nudPeriod.Value = Key.Period;
            nudDigits.Value = Key.Digits;
            tbSecret.Text = Key.Secret;

            UpdateButtons();
        }

        private void UpdateKey()
        {
            string newURI = Key.ToString();
            if (tbURL.Text != newURI)
            {
                tbURL.Text = newURI;
                UpdateButtons();
            }
        }

        private void UpdateButtons()
        {
            if (string.IsNullOrWhiteSpace(Key.Secret))
            {
                pbQR.Image = null;
                btnSaveQR.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                pbQR.Image = Key.ToQR();
                btnSaveQR.Enabled = true;
                btnSave.Enabled = true;
            }
        }

    }
}
