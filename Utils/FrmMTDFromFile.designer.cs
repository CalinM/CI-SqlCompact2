namespace Utils
{
    partial class FrmMTDFromFile
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
            this.lbFilename = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnFileSelector = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbGenerateThumbnails = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip();
            this.lbWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.Location = new System.Drawing.Point(26, 28);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(52, 13);
            this.lbFilename.TabIndex = 0;
            this.lbFilename.Text = "File name";
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(84, 24);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(530, 20);
            this.tbFileName.TabIndex = 1;
            this.tbFileName.TextChanged += new System.EventHandler(this.tbFilesLocation_TextChanged);
            // 
            // btnFileSelector
            // 
            this.btnFileSelector.Location = new System.Drawing.Point(620, 24);
            this.btnFileSelector.Name = "btnFileSelector";
            this.btnFileSelector.Size = new System.Drawing.Size(53, 20);
            this.btnFileSelector.TabIndex = 8;
            this.btnFileSelector.TabStop = false;
            this.btnFileSelector.Text = "File";
            this.btnFileSelector.UseVisualStyleBackColor = true;
            this.btnFileSelector.Click += new System.EventHandler(this.btnFolderSelector_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(517, 100);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(598, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbGenerateThumbnails
            // 
            this.cbGenerateThumbnails.AutoSize = true;
            this.cbGenerateThumbnails.Checked = true;
            this.cbGenerateThumbnails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGenerateThumbnails.Location = new System.Drawing.Point(84, 50);
            this.cbGenerateThumbnails.Name = "cbGenerateThumbnails";
            this.cbGenerateThumbnails.Size = new System.Drawing.Size(302, 17);
            this.cbGenerateThumbnails.TabIndex = 2;
            this.cbGenerateThumbnails.Text = "Use the first frame at 25-50-75% from duration as thumbnail";
            this.cbGenerateThumbnails.UseVisualStyleBackColor = true;
            this.cbGenerateThumbnails.CheckedChanged += new System.EventHandler(this.cbGenerateThumbnails_CheckedChanged);
            // 
            // lbWarning
            // 
            this.lbWarning.AutoSize = true;
            this.lbWarning.ForeColor = System.Drawing.Color.Red;
            this.lbWarning.Location = new System.Drawing.Point(81, 66);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(283, 13);
            this.lbWarning.TabIndex = 13;
            this.lbWarning.Text = "Warning! The import operation will take significantly longer!";
            // 
            // FrmMTDFromFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 135);
            this.Controls.Add(this.lbWarning);
            this.Controls.Add(this.cbGenerateThumbnails);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnFileSelector);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.lbFilename);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMTDFromFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Refresh episode data from file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFilename;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button btnFileSelector;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbGenerateThumbnails;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lbWarning;
    }
}