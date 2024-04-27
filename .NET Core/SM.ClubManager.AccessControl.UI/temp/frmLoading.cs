
using SM.ClubManager.AccessControl.Infrastructure;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SM.ClubManager.AccessControl
{
    public partial class frmLoading : MetroForm
    {
        private static string defaultMessage = "One moment please";

        public frmLoading(string message = null)
        {
            InitializeComponent();

            if(message == null)
            {
                message = defaultMessage;
            }
            txtStatusMessage.InvokeIfRequired(t => t.Text = message);
        }
        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static frmLoading loadingForm;

        public static void UpdateStatus(string message)
        {
            if(loadingForm != null)
                loadingForm.txtStatusMessage.InvokeIfRequired(t => t.Text = message);
        }

        static public void ShowLoadingForm(string message = null)
        {
            if (loadingForm != null)
                return; 

            if(message !=  null)
            {
                defaultMessage = message;
            }

            Thread thread = new Thread(new ThreadStart(frmLoading.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            if(loadingForm == null)
            {
               // if (loadingForm.Handle == null || !loadingForm.IsHandleCreated)
                //{
                    loadingForm = new frmLoading(defaultMessage);
                    loadingForm.TopMost = true;                
                    Application.Run(loadingForm);
                    
                    loadingForm.Focus();
                //}               
            }       
        }

        static public void CloseForm()
        {
            try
            {
              
                if (loadingForm != null && !loadingForm.IsDisposed && loadingForm.IsHandleCreated && !loadingForm.Disposing)
                {
                    loadingForm.Invoke(new CloseDelegate(frmLoading.CloseFormInternal));
                }                                      
            }
            catch (Exception ex)
            {

                //Not sure what to do here! :( 
                //going to log exception, and then monitor when it happens and check stack trace etc. so that I can try resolve
               //LogNotification.Instance.Log("** ERROR CLOSING LOADING FORM **",applicationName:"SIS.REA.Actinium", ex: ex);
            }
                    
        }

        static private void CloseFormInternal()
        {
            try
            {
                if (loadingForm != null)
                {
                    if(loadingForm.IsHandleCreated)
                    {
                        loadingForm.Close();
                        loadingForm = null;
                    }                   
                }
            }
            catch (Exception)
            {

                //throw;
            }
                    
        }
    }
}
