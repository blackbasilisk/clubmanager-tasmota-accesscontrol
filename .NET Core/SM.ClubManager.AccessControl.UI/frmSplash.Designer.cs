namespace SM.ClubManager.AccessControl
{
    partial class frmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            panel3 = new Panel();
            picSplashImage = new PictureBox();
            txtStatusMessage = new TextBox();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSplashImage).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(picSplashImage);
            panel3.Controls.Add(txtStatusMessage);
            panel3.Dock = DockStyle.Fill;
            panel3.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Italic, GraphicsUnit.Point);
            panel3.ForeColor = Color.FromArgb(64, 64, 64);
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(465, 199);
            panel3.TabIndex = 5;
            // 
            // picSplashImage
            // 
            picSplashImage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            picSplashImage.ErrorImage = null;
            picSplashImage.Image = (Image)resources.GetObject("picSplashImage.Image");
            picSplashImage.InitialImage = null;
            picSplashImage.Location = new Point(4, 3);
            picSplashImage.Margin = new Padding(4, 3, 4, 3);
            picSplashImage.Name = "picSplashImage";
            picSplashImage.Size = new Size(453, 160);
            picSplashImage.SizeMode = PictureBoxSizeMode.Zoom;
            picSplashImage.TabIndex = 13;
            picSplashImage.TabStop = false;
            // 
            // txtStatusMessage
            // 
            txtStatusMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtStatusMessage.BackColor = Color.White;
            txtStatusMessage.BorderStyle = BorderStyle.None;
            txtStatusMessage.Enabled = false;
            txtStatusMessage.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            txtStatusMessage.ForeColor = Color.Black;
            txtStatusMessage.Location = new Point(4, 171);
            txtStatusMessage.Margin = new Padding(4, 3, 4, 3);
            txtStatusMessage.Name = "txtStatusMessage";
            txtStatusMessage.Size = new Size(453, 15);
            txtStatusMessage.TabIndex = 12;
            txtStatusMessage.Text = "One moment please...";
            txtStatusMessage.TextAlign = HorizontalAlignment.Center;
            // 
            // frmSplash
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(465, 199);
            ControlBox = false;
            Controls.Add(panel3);
            DoubleBuffered = true;
            DropShadow = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new Size(477, 211);
            MinimumSize = new Size(477, 211);
            Name = "frmSplash";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picSplashImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtStatusMessage;
        private System.Windows.Forms.PictureBox picSplashImage;
    }
}