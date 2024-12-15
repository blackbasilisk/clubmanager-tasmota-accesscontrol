//using Gibraltar.Agent;

using SM.ClubManager.AccessControl.UI.Infrastructure;
//using Microsoft.VisualBasic.Logging;

namespace SM.ClubManager.AccessControl.UI
{
    //internal static class Program
    //{
    //    ///////// <summary>
    //    /////////  The main entry point for the application.
    //    ///////// </summary>
    //    //////[STAThread]
    //    //////static void Main()
    //    //////{
    //    //////    // To customize application configuration such as set high DPI settings or default font,
    //    //////    // see https://aka.ms/applicationconfiguration.

    //    //////    ApplicationConfiguration.Initialize();
    //    //////    Application.Run(new frmMain());
    //    //////}
    //    ///
    //}
    /// <summary>
    /// Syncfusion license key
    /// MTcwMTgwOUAzMjMxMmUzMTJlMzMzNVM5WFZ6NktOUFAvcTBUUUR3UVJLckVVSDduQkRLYm14TVpMbnpKYTUybWs9;MTcwMTgxMEAzMjMxMmUzMTJlMzMzNWtCQVd0clFsUldDdy8yZUN3bElGZUlDejVOUW55ZlI4SGlBQ1owN3ZycVk9;MTcwMTgxMUAzMjMxMmUzMTJlMzMzNVVJQ29PSWlxNC81MGJUZFI2bHo0VGsrbGpRTkt4amVFWi96NzdJTFhVcGs9
    /// </summary>
    public static class Program
    {
    //    public static IConfiguration Configuration { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Log.StartSession();
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo + DSMBaFt + QHFqVk9rXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQlliQXxSc0BmXX5ccXQ =; Mgo + DSMBPh8sVXJ1S0d + X1hPd11dXmJWd1p / THNYflR1fV9DaUwxOX1dQl9gSXpSfkdiXHpbdHVWRWE =; ORg4AjUWIQA / Gnt2VFhhQlJNfV5AQmBIYVp / TGpJfl96cVxMZVVBJAtUQF1hSn5Xd01hWHpbdXBUQmdc; MTc2OTIyNEAzMjMxMmUzMTJlMzMzOUxMQ3lsMmlDaE91WmtHeWk0WnFvek9rbnFrRlo2WTNGRzNyWGxzOFVNVkk9; MTc2OTIyNUAzMjMxMmUzMTJlMzMzOUtSN3BwdFltSnY0c3FaSG9reUNvNnlURFNFNVB1RWhhdUpLTDFCYVRhOFU9; NRAiBiAaIQQuGjN / V0d + XU9Hf1RDX3xKf0x / TGpQb19xflBPallYVBYiSV9jS31TckRrW35bdXFRRGFUUA ==; MTc2OTIyN0AzMjMxMmUzMTJlMzMzOUpJNnJnSnBiNmpQK2lVNmk0OFZvOWNwTzBaQWRwM2RmWDZ2cHFrL1NrZ2s9; MTc2OTIyOEAzMjMxMmUzMTJlMzMzOVFUMDZuSkZnQXJxdHNkS1dZR1lkQ0I3OEJqb28zZ2E0OEs2Rm5ya0hqQnc9; Mgo + DSMBMAY9C3t2VFhhQlJNfV5AQmBIYVp / TGpJfl96cVxMZVVBJAtUQF1hSn5Xd01hWHpbdXBXT2Nd; MTc2OTIzMEAzMjMxMmUzMTJlMzMzOUw0TTlmNFU2dElYREVlLzZ3ZTVnZzg5U0lVc2RUNFFlVkEzTjRWQVhPZG89; MTc2OTIzMUAzMjMxMmUzMTJlMzMzOWtMNDBFYTJ5MVVGT3lMWWYyWmlCbzQ4WXoyalUrZWZXRTB1NlFYOHZIM0E9; MTc2OTIzMkAzMjMxMmUzMTJlMzMzOUpJNnJnSnBiNmpQK2lVNmk0OFZvOWNwTzBaQWRwM2RmWDZ2cHFrL1NrZ2s9");
            string syncFusionlicenseKey = "MzYyNjU3MUAzMjM4MmUzMDJlMzBYTTV2eWxaUno0MDR0ZURLOUF2ZkdpNGtGdnc1RGhiV0hLYndUNzdOL0xBPQ==";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncFusionlicenseKey);

            AppSettingsManager.InitAppSettings();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            

            var isShowSplashScreen = AppSettingsManager.configuration["AppSettings:IsShowSplashScreen"];

            bool showSplash = Convert.ToBoolean(isShowSplashScreen);

            if (showSplash)
            {
                frmSplash.ShowSplashScreen();
            }

            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ApplicationExit += Application_ApplicationExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new frmMain());
            //Log.EndSession();
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