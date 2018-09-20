namespace Utils
{
    partial class FrmMoviesInfoFromFiles
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
            this.lbWarning = new System.Windows.Forms.Label();
            this.cbGenerateThumbnails = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnFolderSelector = new System.Windows.Forms.Button();
            this.cbFileExtensions = new System.Windows.Forms.ComboBox();
            this.lbFilesExtensions = new System.Windows.Forms.Label();
            this.tbFilesLocation = new System.Windows.Forms.TextBox();
            this.lbLocation = new System.Windows.Forms.Label();
            this.cbForceAddMissingMovies = new System.Windows.Forms.CheckBox();
            this.cbPreserveManuallySetData = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbWarning
            // 
            this.lbWarning.AutoSize = true;
            this.lbWarning.ForeColor = System.Drawing.Color.Red;
            this.lbWarning.Location = new System.Drawing.Point(122, 141);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(283, 13);
            this.lbWarning.TabIndex = 21;
            this.lbWarning.Text = "Warning! The import operation will take significantly longer!";
            this.lbWarning.Visible = false;
            // 
            // cbGenerateThumbnails
            // 
            this.cbGenerateThumbnails.AutoSize = true;
            this.cbGenerateThumbnails.Location = new System.Drawing.Point(125, 124);
            this.cbGenerateThumbnails.Name = "cbGenerateThumbnails";
            this.cbGenerateThumbnails.Size = new System.Drawing.Size(302, 17);
            this.cbGenerateThumbnails.TabIndex = 20;
            this.cbGenerateThumbnails.Text = "Use the first frame at 25-50-75% from duration as thumbnail";
            this.cbGenerateThumbnails.UseVisualStyleBackColor = true;
            this.cbGenerateThumbnails.CheckedChanged += new System.EventHandler(this.cbGenerateThumbnails_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(598, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(517, 170);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnFolderSelector
            // 
            this.btnFolderSelector.Location = new System.Drawing.Point(620, 24);
            this.btnFolderSelector.Name = "btnFolderSelector";
            this.btnFolderSelector.Size = new System.Drawing.Size(53, 20);
            this.btnFolderSelector.TabIndex = 17;
            this.btnFolderSelector.Text = "Folder";
            this.btnFolderSelector.UseVisualStyleBackColor = true;
            this.btnFolderSelector.Click += new System.EventHandler(this.btnFolderSelector_Click);
            // 
            // cbFileExtensions
            // 
            this.cbFileExtensions.FormattingEnabled = true;
            this.cbFileExtensions.Items.AddRange(new object[] {
            "*.mkv",
            "*.mp4",
            "*.avi"});
            this.cbFileExtensions.Location = new System.Drawing.Point(127, 50);
            this.cbFileExtensions.Name = "cbFileExtensions";
            this.cbFileExtensions.Size = new System.Drawing.Size(183, 21);
            this.cbFileExtensions.TabIndex = 16;
            this.cbFileExtensions.SelectedIndexChanged += new System.EventHandler(this.cbFileExtensions_SelectedIndexChanged);
            // 
            // lbFilesExtensions
            // 
            this.lbFilesExtensions.AutoSize = true;
            this.lbFilesExtensions.Location = new System.Drawing.Point(26, 53);
            this.lbFilesExtensions.Name = "lbFilesExtensions";
            this.lbFilesExtensions.Size = new System.Drawing.Size(81, 13);
            this.lbFilesExtensions.TabIndex = 15;
            this.lbFilesExtensions.Text = "Files extensions";
            // 
            // tbFilesLocation
            // 
            this.tbFilesLocation.Location = new System.Drawing.Point(127, 24);
            this.tbFilesLocation.Name = "tbFilesLocation";
            this.tbFilesLocation.Size = new System.Drawing.Size(487, 20);
            this.tbFilesLocation.TabIndex = 14;
            this.tbFilesLocation.TextChanged += new System.EventHandler(this.tbFilesLocation_TextChanged);
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(26, 28);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(48, 13);
            this.lbLocation.TabIndex = 13;
            this.lbLocation.Text = "Location";
            // 
            // cbForceAddMissingMovies
            // 
            this.cbForceAddMissingMovies.AutoSize = true;
            this.cbForceAddMissingMovies.Location = new System.Drawing.Point(125, 78);
            this.cbForceAddMissingMovies.Name = "cbForceAddMissingMovies";
            this.cbForceAddMissingMovies.Size = new System.Drawing.Size(414, 17);
            this.cbForceAddMissingMovies.TabIndex = 22;
            this.cbForceAddMissingMovies.Text = "Force-add missing movies (the ones that were not found in the database, by name)";
            this.cbForceAddMissingMovies.UseVisualStyleBackColor = true;
            // 
            // cbPreserveManuallySetData
            // 
            this.cbPreserveManuallySetData.AutoSize = true;
            this.cbPreserveManuallySetData.Checked = true;
            this.cbPreserveManuallySetData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPreserveManuallySetData.Location = new System.Drawing.Point(125, 101);
            this.cbPreserveManuallySetData.Name = "cbPreserveManuallySetData";
            this.cbPreserveManuallySetData.Size = new System.Drawing.Size(397, 17);
            this.cbPreserveManuallySetData.TabIndex = 23;
            this.cbPreserveManuallySetData.Text = "Preserve manually set data (Descriptions, Recomandations, movie Theme, etc)";
            this.cbPreserveManuallySetData.UseVisualStyleBackColor = true;
            // 
            // FrmMoviesInfoFromFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 206);
            this.Controls.Add(this.cbPreserveManuallySetData);
            this.Controls.Add(this.cbForceAddMissingMovies);
            this.Controls.Add(this.lbWarning);
            this.Controls.Add(this.cbGenerateThumbnails);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnFolderSelector);
            this.Controls.Add(this.cbFileExtensions);
            this.Controls.Add(this.lbFilesExtensions);
            this.Controls.Add(this.tbFilesLocation);
            this.Controls.Add(this.lbLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMoviesInfoFromFiles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import/Update Movies from Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbWarning;
        private System.Windows.Forms.CheckBox cbGenerateThumbnails;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnFolderSelector;
        private System.Windows.Forms.ComboBox cbFileExtensions;
        private System.Windows.Forms.Label lbFilesExtensions;
        private System.Windows.Forms.TextBox tbFilesLocation;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.CheckBox cbForceAddMissingMovies;
        private System.Windows.Forms.CheckBox cbPreserveManuallySetData;
    }
}