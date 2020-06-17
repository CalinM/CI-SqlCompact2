namespace Utils
{
    partial class FrmSiteGenParams
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
            this.btnFolderSelector = new System.Windows.Forms.Button();
            this.tbFilesLocation = new System.Windows.Forms.TextBox();
            this.lbLocation = new System.Windows.Forms.Label();
            this.cbSavePosters = new System.Windows.Forms.CheckBox();
            this.cbSaveMoviesThumbnals = new System.Windows.Forms.CheckBox();
            this.cbSaveEpisodesThumbnals = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbPreserveMarkesForExistingThumbnails = new System.Windows.Forms.CheckBox();
            this.cbMinifyScriptFiles = new System.Windows.Forms.CheckBox();
            this.cbMinifyDataFiles = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnFolderSelector
            // 
            this.btnFolderSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderSelector.Location = new System.Drawing.Point(617, 25);
            this.btnFolderSelector.Name = "btnFolderSelector";
            this.btnFolderSelector.Size = new System.Drawing.Size(53, 20);
            this.btnFolderSelector.TabIndex = 11;
            this.btnFolderSelector.Text = "Folder";
            this.btnFolderSelector.UseVisualStyleBackColor = true;
            this.btnFolderSelector.Click += new System.EventHandler(this.BtnFolderSelector_Click);
            // 
            // tbFilesLocation
            // 
            this.tbFilesLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilesLocation.Location = new System.Drawing.Point(123, 25);
            this.tbFilesLocation.Name = "tbFilesLocation";
            this.tbFilesLocation.Size = new System.Drawing.Size(487, 20);
            this.tbFilesLocation.TabIndex = 10;
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(21, 27);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(48, 13);
            this.lbLocation.TabIndex = 9;
            this.lbLocation.Text = "Location";
            // 
            // cbSavePosters
            // 
            this.cbSavePosters.AutoSize = true;
            this.cbSavePosters.Checked = true;
            this.cbSavePosters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSavePosters.Location = new System.Drawing.Point(123, 49);
            this.cbSavePosters.Name = "cbSavePosters";
            this.cbSavePosters.Size = new System.Drawing.Size(156, 17);
            this.cbSavePosters.TabIndex = 12;
            this.cbSavePosters.Text = "Save movies/series posters";
            this.cbSavePosters.UseVisualStyleBackColor = true;
            // 
            // cbSaveMoviesThumbnals
            // 
            this.cbSaveMoviesThumbnals.AutoSize = true;
            this.cbSaveMoviesThumbnals.Enabled = false;
            this.cbSaveMoviesThumbnals.Location = new System.Drawing.Point(123, 71);
            this.cbSaveMoviesThumbnals.Name = "cbSaveMoviesThumbnals";
            this.cbSaveMoviesThumbnals.Size = new System.Drawing.Size(138, 17);
            this.cbSaveMoviesThumbnals.TabIndex = 13;
            this.cbSaveMoviesThumbnals.Text = "Save movies thumbnals";
            this.cbSaveMoviesThumbnals.UseVisualStyleBackColor = true;
            // 
            // cbSaveEpisodesThumbnals
            // 
            this.cbSaveEpisodesThumbnals.AutoSize = true;
            this.cbSaveEpisodesThumbnals.Checked = true;
            this.cbSaveEpisodesThumbnals.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSaveEpisodesThumbnals.Location = new System.Drawing.Point(123, 93);
            this.cbSaveEpisodesThumbnals.Name = "cbSaveEpisodesThumbnals";
            this.cbSaveEpisodesThumbnals.Size = new System.Drawing.Size(149, 17);
            this.cbSaveEpisodesThumbnals.TabIndex = 14;
            this.cbSaveEpisodesThumbnals.Text = "Save episodes thumbnails";
            this.cbSaveEpisodesThumbnals.UseVisualStyleBackColor = true;
            this.cbSaveEpisodesThumbnals.CheckedChanged += new System.EventHandler(this.CbSaveEpisodesThumbnals_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(595, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(515, 179);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // cbPreserveMarkesForExistingThumbnails
            // 
            this.cbPreserveMarkesForExistingThumbnails.AutoSize = true;
            this.cbPreserveMarkesForExistingThumbnails.Checked = true;
            this.cbPreserveMarkesForExistingThumbnails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPreserveMarkesForExistingThumbnails.Enabled = false;
            this.cbPreserveMarkesForExistingThumbnails.Location = new System.Drawing.Point(141, 115);
            this.cbPreserveMarkesForExistingThumbnails.Name = "cbPreserveMarkesForExistingThumbnails";
            this.cbPreserveMarkesForExistingThumbnails.Size = new System.Drawing.Size(259, 17);
            this.cbPreserveMarkesForExistingThumbnails.TabIndex = 17;
            this.cbPreserveMarkesForExistingThumbnails.Text = "Preserve existence markes for existing thumbnails";
            this.cbPreserveMarkesForExistingThumbnails.UseVisualStyleBackColor = true;
            // 
            // cbMinifyScriptFiles
            // 
            this.cbMinifyScriptFiles.AutoSize = true;
            this.cbMinifyScriptFiles.Checked = true;
            this.cbMinifyScriptFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMinifyScriptFiles.Location = new System.Drawing.Point(123, 138);
            this.cbMinifyScriptFiles.Name = "cbMinifyScriptFiles";
            this.cbMinifyScriptFiles.Size = new System.Drawing.Size(102, 17);
            this.cbMinifyScriptFiles.TabIndex = 18;
            this.cbMinifyScriptFiles.Text = "Minify script files";
            this.cbMinifyScriptFiles.UseVisualStyleBackColor = true;
            this.cbMinifyScriptFiles.CheckedChanged += new System.EventHandler(this.cbMinifyScriptFiles_CheckedChanged);
            // 
            // cbMinifyDataFiles
            // 
            this.cbMinifyDataFiles.AutoSize = true;
            this.cbMinifyDataFiles.Checked = true;
            this.cbMinifyDataFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMinifyDataFiles.Location = new System.Drawing.Point(141, 160);
            this.cbMinifyDataFiles.Name = "cbMinifyDataFiles";
            this.cbMinifyDataFiles.Size = new System.Drawing.Size(98, 17);
            this.cbMinifyDataFiles.TabIndex = 19;
            this.cbMinifyDataFiles.Text = "Minify data files";
            this.cbMinifyDataFiles.UseVisualStyleBackColor = true;
            // 
            // FrmSiteGenParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(691, 217);
            this.Controls.Add(this.cbMinifyDataFiles);
            this.Controls.Add(this.cbMinifyScriptFiles);
            this.Controls.Add(this.cbPreserveMarkesForExistingThumbnails);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbSaveEpisodesThumbnals);
            this.Controls.Add(this.cbSaveMoviesThumbnals);
            this.Controls.Add(this.cbSavePosters);
            this.Controls.Add(this.btnFolderSelector);
            this.Controls.Add(this.tbFilesLocation);
            this.Controls.Add(this.lbLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSiteGenParams";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HTML Site generation parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFolderSelector;
        private System.Windows.Forms.TextBox tbFilesLocation;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.CheckBox cbSavePosters;
        private System.Windows.Forms.CheckBox cbSaveMoviesThumbnals;
        private System.Windows.Forms.CheckBox cbSaveEpisodesThumbnals;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbPreserveMarkesForExistingThumbnails;
        private System.Windows.Forms.CheckBox cbMinifyScriptFiles;
        private System.Windows.Forms.CheckBox cbMinifyDataFiles;
    }
}