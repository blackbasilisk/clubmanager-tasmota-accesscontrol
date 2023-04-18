
namespace SM.ClubManager.AccessControl
{
    partial class frmNewLoading
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtStatusMessage = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDelayCounter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(78, 112);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(426, 255);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtStatusMessage
            // 
            this.txtStatusMessage.BackColor = System.Drawing.Color.White;
            this.txtStatusMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatusMessage.Enabled = false;
            this.txtStatusMessage.Font = new System.Drawing.Font("Open Sans Light", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatusMessage.ForeColor = System.Drawing.Color.Black;
            this.txtStatusMessage.Location = new System.Drawing.Point(11, 4);
            this.txtStatusMessage.Name = "txtStatusMessage";
            this.txtStatusMessage.Size = new System.Drawing.Size(344, 66);
            this.txtStatusMessage.TabIndex = 13;
            this.txtStatusMessage.Text = "One moment...";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtStatusMessage);
            this.panel3.Controls.Add(this.txtDelayCounter);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(491, 75);
            this.panel3.TabIndex = 6;
            // 
            // txtDelayCounter
            // 
            this.txtDelayCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDelayCounter.BackColor = System.Drawing.Color.White;
            this.txtDelayCounter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDelayCounter.Enabled = false;
            this.txtDelayCounter.Font = new System.Drawing.Font("Open Sans Light", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDelayCounter.ForeColor = System.Drawing.Color.Black;
            this.txtDelayCounter.Location = new System.Drawing.Point(375, 4);
            this.txtDelayCounter.Name = "txtDelayCounter";
            this.txtDelayCounter.Size = new System.Drawing.Size(103, 66);
            this.txtDelayCounter.TabIndex = 15;
            this.txtDelayCounter.Text = "99";
            this.txtDelayCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDelayCounter.Visible = false;
            // 
            // frmNewLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 75);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNewLoading";
            this.Text = "frmNewLoading";
            this.Shown += new System.EventHandler(this.frmNewLoading_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox txtStatusMessage;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.TextBox txtDelayCounter;
    }
}