namespace SM.ClubManager.AccessControl.UI
{
    partial class frmSerial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            txtSerialKey = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtSerialKey);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(800, 400);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // txtSerialKey
            // 
            txtSerialKey.Dock = DockStyle.Fill;
            txtSerialKey.Location = new Point(3, 19);
            txtSerialKey.Multiline = true;
            txtSerialKey.Name = "txtSerialKey";
            txtSerialKey.ReadOnly = true;
            txtSerialKey.Size = new Size(794, 378);
            txtSerialKey.TabIndex = 0;
            // 
            // frmSerial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 400);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            MaximumSize = new Size(816, 439);
            MinimizeBox = false;
            MinimumSize = new Size(816, 439);
            Name = "frmSerial";
            ShowIcon = false;
            TopMost = true;
            Load += frmSerial_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtSerialKey;
    }
}