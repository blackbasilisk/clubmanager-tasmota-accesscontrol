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
            this.panel3 = new System.Windows.Forms.Panel();
            this.picSplashImage = new System.Windows.Forms.PictureBox();
            this.txtStatusMessage = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSplashImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.picSplashImage);
            this.panel3.Controls.Add(this.txtStatusMessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(397, 171);
            this.panel3.TabIndex = 5;
            // 
            // picSplashImage
            // 
            this.picSplashImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picSplashImage.ErrorImage = null;
            this.picSplashImage.Image = ((System.Drawing.Image)(resources.GetObject("picSplashImage.Image")));
            this.picSplashImage.InitialImage = null;
            this.picSplashImage.Location = new System.Drawing.Point(3, 3);
            this.picSplashImage.Name = "picSplashImage";
            this.picSplashImage.Size = new System.Drawing.Size(387, 139);
            this.picSplashImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSplashImage.TabIndex = 13;
            this.picSplashImage.TabStop = false;
            // 
            // txtStatusMessage
            // 
            this.txtStatusMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatusMessage.BackColor = System.Drawing.Color.White;
            this.txtStatusMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatusMessage.Enabled = false;
            this.txtStatusMessage.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Italic);
            this.txtStatusMessage.ForeColor = System.Drawing.Color.Black;
            this.txtStatusMessage.Location = new System.Drawing.Point(3, 148);
            this.txtStatusMessage.Name = "txtStatusMessage";
            this.txtStatusMessage.Size = new System.Drawing.Size(387, 18);
            this.txtStatusMessage.TabIndex = 12;
            this.txtStatusMessage.Text = "One moment please...";
            this.txtStatusMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 171);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.DropShadow = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(409, 183);
            this.MinimumSize = new System.Drawing.Size(409, 183);
            this.Name = "frmSplash";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSplashImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtStatusMessage;
        private System.Windows.Forms.PictureBox picSplashImage;
    }
}