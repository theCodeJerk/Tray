using System;
using System.Windows.Forms;

namespace theCodeJerk.Tray.Icon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Make sure there isn't already an instance of this application running.
            bool result;
            var mutex = new System.Threading.Mutex(true, "theCodeJerk.Tray.Icon", out result);

            // If there is already an instance of this application
            // running, then return, preventing the new instance
            // of the application from getting started.
            if (!result)
            {
                // Show the user what is going on.
                MessageBox.Show("Another instance of theCodeJerk.Tray.Icon is already running.", "theCodeJerk Tray Icon");
                return;
            }

            // Start the Application, loading the tray form.
            Application.Run(new TrayForm());

            // Keep the Mutex so if another instance of Krakabot.Tray is
            // launched, it can detect that this instance is already
            // running.
            GC.KeepAlive(mutex);
        }
    }
}
