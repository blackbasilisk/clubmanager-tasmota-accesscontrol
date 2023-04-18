
using SM.ClubManager.AccessControl.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM.ClubManager.AccessControl
{   
    public partial class frmNewLoading : Form
    {
        
        BackgroundWorker worker;

        public frmNewLoading()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged +=
                        new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted +=
                       new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtDelayCounter.Visible = false;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int counter = (int)e.UserState;
            if(counter > 0)
            {
                txtStatusMessage.InvokeIfRequired(t => t.TextAlign = HorizontalAlignment.Left);
                txtDelayCounter.InvokeIfRequired(t => t.Text = counter.ToString());
            }
            else
            {
                txtStatusMessage.InvokeIfRequired(t => t.TextAlign = HorizontalAlignment.Center);
                txtDelayCounter.InvokeIfRequired(t => t.Text = "");
            }             
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = (int)e.Argument;
            while (!worker.CancellationPending && counter > 0)
            {               
                worker.ReportProgress(0,counter);
                counter--;
                System.Threading.Thread.Sleep(1000);               
            }
            e.Result = counter;
        }

        private void frmNewLoading_Shown(object sender, EventArgs e)
        {
           
        }

        public void RunCounter(int delayCounter)
        {
            if (delayCounter > 0)
            {
                txtDelayCounter.Visible = true;
                txtDelayCounter.Text = delayCounter.ToString();
                worker.RunWorkerAsync(delayCounter);
            }
            else
            {
                txtDelayCounter.Text = "";
                txtDelayCounter.Visible = false;
            }
        }
    }
}
