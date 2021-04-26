using System;
using System.Threading;
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

            bool mutexIsAvailable = false;
            Mutex mutex = null;
            try
            {
                mutex = new Mutex(true, APP_ID);
                mutexIsAvailable = mutex.WaitOne(1, false); // Wait only 1 ms
            }
            catch (AbandonedMutexException)
            {
                // don't worry about the abandonment; 
                // the mutex only guards app instantiation
                mutexIsAvailable = true;
            }
            if (!mutexIsAvailable)
                return;

            try
            {
#if !DEBUG
                try
                {
                    if (SelfUpdatingApp.Installer.IsUpdateAvailableAsync(APP_ID).Result)
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
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
