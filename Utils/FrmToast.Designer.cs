namespace Utils
{
    partial class FrmToast
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
            this.toastImage = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbText = new System.Windows.Forms.Label();
            this.lbAppName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.toastImage)).BeginInit();
            this.SuspendLayout();
            //
            // toastImage
            //
            this.toastImage.BackColor = System.Drawing.Color.Transparent;
            this.toastImage.Location = new System.Drawing.Point(24, 27);
            this.toastImage.Name = "toastImage";
            this.toastImage.Size = new System.Drawing.Size(40, 40);
            this.toastImage.TabIndex = 1;
            this.toastImage.TabStop = false;
            //
            // lbTitle
            //
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Location = new System.Drawing.Point(82, 22);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(267, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "Title";
            this.lbTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmToast_MouseDown);
            //
            // lbText
            //
            this.lbText.BackColor = System.Drawing.Color.Transparent;
            this.lbText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbText.Location = new System.Drawing.Point(82, 44);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(267, 23);
            this.lbText.TabIndex = 3;
            this.lbText.Text = "Text";
            this.lbText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmToast_MouseDown);
            //
            // lbAppName
            //
            this.lbAppName.AutoSize = true;
            this.lbAppName.BackColor = System.Drawing.Color.Transparent;
            this.lbAppName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppName.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbAppName.Location = new System.Drawing.Point(82, 67);
            this.lbAppName.Name = "lbAppName";
            this.lbAppName.Size = new System.Drawing.Size(28, 15);
            this.lbAppName.TabIndex = 4;
            this.lbAppName.Text = "Text";
            this.lbAppName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmToast_MouseDown);
            //
            // FrmToast
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 113);
            this.Controls.Add(this.lbAppName);
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.toastImage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmToast";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmToast";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FrmToast_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmToast_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmToast_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.toastImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox toastImage;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Label lbAppName;
    }
}