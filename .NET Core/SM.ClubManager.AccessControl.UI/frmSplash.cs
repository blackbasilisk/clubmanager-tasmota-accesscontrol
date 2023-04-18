using System.Threading;
using System.Windows.Forms;
using SIS.Library.Base.Infrastructure.WindowsFormsExtensions;
using Syncfusion.Windows.Forms;

namespace SM.ClubManager.AccessControl
{
    public partial class frmSplash : MetroForm
    {
        public frmSplash(string status)
        {
            
            InitializeComponent();

            txtStatusMessage.Text = status;
        }
        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static frmSplash splashForm;

        public static void UpdateStatus(string message)
        {            
            if(splashForm != null)
            {
                splashForm.txtStatusMessage.InvokeIfRequired(l => l.Text = message);
            }
        }

        static public void ShowSplashScreen(string statusMessage = "One moment please...")
        {
            // Make sure it is only launched once.

            if (splashForm != null)
                return;

            Thread thread = new Thread(frmSplash.ShowForm);            
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(statusMessage);

        }

        static private void ShowForm(object statusMessage)
        {
            splashForm = new frmSplash(statusMessage.ToString());               
            Application.Run(splashForm);
        }

        static public void CloseForm(bool isOnlySplashForm = false)
        {            
            if (splashForm != null && !splashForm.IsDisposed)
            {
                if (splashForm.IsHandleCreated)
                    splashForm.Invoke(new CloseDelegate(frmSplash.CloseFormInternal));
            }
            else
            {
                if (!isOnlySplashForm)
                    Application.Exit();
            }
        }

        static private void CloseFormInternal()
        {
            
            splashForm.Close();
            splashForm.Dispose();
            splashForm = null;
        }
    }
}
