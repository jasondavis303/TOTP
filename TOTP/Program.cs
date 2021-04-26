using System;
using System.Windows.Forms;

namespace TOTP
{
    static class Program
    {
        public const string APP_ID = "6E27CD89-E0A6-4578-873E-4B028BBF35B3";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG
            try
            {
                if(SelfUpdatingApp.Installer.IsUpdateAvailableAsync(APP_ID).Result)
                {
                    SelfUpdatingApp.Manager.InstallNewest();
                    SelfUpdatingApp.Installer.Launch(APP_ID);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif

            Application.Run(new frmMain());
        }
    }
}
