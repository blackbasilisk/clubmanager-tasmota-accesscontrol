using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM.ClubManager.AccessControl.UI
{
    public partial class frmSerial : Form
    {
        public frmSerial()
        {
            InitializeComponent();
        }

        private void frmSerial_Load(object sender, EventArgs e)
        {
            //get path for serial key
            string serialFolderName = "VSPE_Serial";

            //build the path to the serial key

            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(executingPath, serialFolderName, "VSPE_64_KEY.txt");
            string serialKey = File.ReadAllText(filePath);

            txtSerialKey.Text = serialKey;

            // Ensure the text isn't selected
            txtSerialKey.SelectionStart = 0; // Move caret to the start
            txtSerialKey.SelectionLength = 0; // Ensure no text is selected

        }
    }
}
