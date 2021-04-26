using BasicOTP;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TOTP
{
    public partial class KeyDisplay : UserControl
    {
        public event EventHandler OnFocused;

        public KeyDisplay(OtpKey key)
        {
            InitializeComponent();
            Key = key ?? throw new ArgumentNullException(nameof(key));
            lblIssuer.Text = Key.Issuer;
            lblAcct.Text = Key.Account;
            try
            {
                pbIcon.Image = Image.FromFile(Path2.IconFile(key.Issuer));
            }
            catch
            {
                using var ms = new MemoryStream();
                Properties.Resources.knot.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                pbIcon.Image = Image.FromStream(ms);
            }
            tmrUpdate.Enabled = true;
        }

        public string IconFile { get; set; }

        public OtpKey Key { get; private set; }

        public string CurrentCode { get; private set; }

        private void Control_Click(object sender, EventArgs e)
        {
            OnFocused?.Invoke(this, EventArgs.Empty);
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            CurrentCode = Authenticator.GetCode(Key);
            string code = CurrentCode;
            switch (code.Length)
            {
                case 6:
                    code = code.Substring(0, 3) + " " + code.Substring(3);
                    break;

                case 8:
                    code = code.Substring(0, 4) + " " + code.Substring(4);
                    break;
            }
            if (lblCode.Text != code)
                lblCode.Text = code;

            string time = Authenticator.GetRemainingTime(Key).Seconds.ToString();
            if (lblTime.Text != time)
                lblTime.Text = time;
        }
    }
}
