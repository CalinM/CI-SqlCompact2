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
            this.SuspendLayout();
            // 
            // btnFolderSelector
            // 
            this.btnFolderSelector.Location = new System.Drawing.Point(925, 39);
            this.btnFolderSelector.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFolderSelector.Name = "btnFolderSelector";
            this.btnFolderSelector.Size = new System.Drawing.Size(80, 31);
            this.btnFolderSelector.TabIndex = 11;
            this.btnFolderSelector.Text = "Folder";
            this.btnFolderSelector.UseVisualStyleBackColor = true;
            this.btnFolderSelector.Click += new System.EventHandler(this.BtnFolderSelector_Click);
            // 
            // tbFilesLocation
            // 
            this.tbFilesLocation.Location = new System.Drawing.Point(185, 39);
            this.tbFilesLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFilesLocation.Name = "tbFilesLocation";
            this.tbFilesLocation.Size = new System.Drawing.Size(728, 26);
            this.tbFilesLocation.TabIndex = 10;
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(32, 42);
            this.lbLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(70, 20);
            this.lbLocation.TabIndex = 9;
            this.lbLocation.Text = "Location";
            // 
            // cbSavePosters
            // 
            this.cbSavePosters.AutoSize = true;
            this.cbSavePosters.Location = new System.Drawing.Point(185, 75);
            this.cbSavePosters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSavePosters.Name = "cbSavePosters";
            this.cbSavePosters.Size = new System.Drawing.Size(227, 24);
            this.cbSavePosters.TabIndex = 12;
            this.cbSavePosters.Text = "Save movies/series posters";
            this.cbSavePosters.UseVisualStyleBackColor = true;
            // 
            // cbSaveMoviesThumbnals
            // 
            this.cbSaveMoviesThumbnals.AutoSize = true;
            this.cbSaveMoviesThumbnals.Enabled = false;
            this.cbSaveMoviesThumbnals.Location = new System.Drawing.Point(185, 109);
            this.cbSaveMoviesThumbnals.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSaveMoviesThumbnals.Name = "cbSaveMoviesThumbnals";
            this.cbSaveMoviesThumbnals.Size = new System.Drawing.Size(202, 24);
            this.cbSaveMoviesThumbnals.TabIndex = 13;
            this.cbSaveMoviesThumbnals.Text = "Save movies thumbnals";
            this.cbSaveMoviesThumbnals.UseVisualStyleBackColor = true;
            // 
            // cbSaveEpisodesThumbnals
            // 
            this.cbSaveEpisodesThumbnals.AutoSize = true;
            this.cbSaveEpisodesThumbnals.Location = new System.Drawing.Point(185, 143);
            this.cbSaveEpisodesThumbnals.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSaveEpisodesThumbnals.Name = "cbSaveEpisodesThumbnals";
            this.cbSaveEpisodesThumbnals.Size = new System.Drawing.Size(220, 24);
            this.cbSaveEpisodesThumbnals.TabIndex = 14;
            this.cbSaveEpisodesThumbnals.Text = "Save episodes thumbnails";
            this.cbSaveEpisodesThumbnals.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(893, 197);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(772, 197);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 35);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // FrmSiteGenParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 256);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbSaveEpisodesThumbnals);
            this.Controls.Add(this.cbSaveMoviesThumbnals);
            this.Controls.Add(this.cbSavePosters);
            this.Controls.Add(this.btnFolderSelector);
            this.Controls.Add(this.tbFilesLocation);
            this.Controls.Add(this.lbLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSiteGenParams";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmSiteGenParams";
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
    }
}