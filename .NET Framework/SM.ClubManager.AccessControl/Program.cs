using Gibraltar.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM.ClubManager.AccessControl
{



    /// <summary>
    /// Syncfusion license key
    /// MTcwMTgwOUAzMjMxMmUzMTJlMzMzNVM5WFZ6NktOUFAvcTBUUUR3UVJLckVVSDduQkRLYm14TVpMbnpKYTUybWs9;MTcwMTgxMEAzMjMxMmUzMTJlMzMzNWtCQVd0clFsUldDdy8yZUN3bElGZUlDejVOUW55ZlI4SGlBQ1owN3ZycVk9;MTcwMTgxMUAzMjMxMmUzMTJlMzMzNVVJQ29PSWlxNC81MGJUZFI2bHo0VGsrbGpRTkt4amVFWi96NzdJTFhVcGs9
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.StartSession();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmSplash.ShowSplashScreen();

            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ApplicationExit += Application_ApplicationExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new frmMain());
            Log.EndSession();
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Application.DoEvents();
            Application.ExitThread();
            
           // throw new NotImplementedException();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
            
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());            
        }
    }
}
