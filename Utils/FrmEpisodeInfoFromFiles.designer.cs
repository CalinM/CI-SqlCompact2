namespace Utils
{
    partial class FrmEpisodeInfoFromFiles
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
            this.lbLocation = new System.Windows.Forms.Label();
            this.tbFilesLocation = new System.Windows.Forms.TextBox();
            this.lbFilesExtensions = new System.Windows.Forms.Label();
            this.lbSeason = new System.Windows.Forms.Label();
            this.lbYear = new System.Windows.Forms.Label();
            this.cbFileExtensions = new System.Windows.Forms.ComboBox();
            this.tbSeason = new System.Windows.Forms.TextBox();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.btnFolderSelector = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbGenerateThumbnails = new System.Windows.Forms.CheckBox();
            this.lbWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(26, 28);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(48, 13);
            this.lbLocation.TabIndex = 0;
            this.lbLocation.Text = "Location";
            // 
            // tbFilesLocation
            // 
            this.tbFilesLocation.Location = new System.Drawing.Point(127, 24);
            this.tbFilesLocation.Name = "tbFilesLocation";
            this.tbFilesLocation.Size = new System.Drawing.Size(487, 20);
            this.tbFilesLocation.TabIndex = 1;
            this.tbFilesLocation.TextChanged += new System.EventHandler(this.tbFilesLocation_TextChanged);
            // 
            // lbFilesExtensions
            // 
            this.lbFilesExtensions.AutoSize = true;
            this.lbFilesExtensions.Location = new System.Drawing.Point(26, 53);
            this.lbFilesExtensions.Name = "lbFilesExtensions";
            this.lbFilesExtensions.Size = new System.Drawing.Size(81, 13);
            this.lbFilesExtensions.TabIndex = 2;
            this.lbFilesExtensions.Text = "Files extensions";
            // 
            // lbSeason
            // 
            this.lbSeason.AutoSize = true;
            this.lbSeason.Location = new System.Drawing.Point(26, 81);
            this.lbSeason.Name = "lbSeason";
            this.lbSeason.Size = new System.Drawing.Size(43, 13);
            this.lbSeason.TabIndex = 3;
            this.lbSeason.Text = "Season";
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.Location = new System.Drawing.Point(26, 108);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(29, 13);
            this.lbYear.TabIndex = 4;
            this.lbYear.Text = "Year";
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
            this.cbFileExtensions.TabIndex = 5;
            this.cbFileExtensions.SelectedIndexChanged += new System.EventHandler(this.cbFileExtensions_SelectedIndexChanged);
            // 
            // tbSeason
            // 
            this.tbSeason.Location = new System.Drawing.Point(127, 78);
            this.tbSeason.Name = "tbSeason";
            this.tbSeason.Size = new System.Drawing.Size(63, 20);
            this.tbSeason.TabIndex = 6;
            this.tbSeason.TextChanged += new System.EventHandler(this.tbSeason_TextChanged);
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(127, 105);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(63, 20);
            this.tbYear.TabIndex = 7;
            // 
            // btnFolderSelector
            // 
            this.btnFolderSelector.Location = new System.Drawing.Point(620, 24);
            this.btnFolderSelector.Name = "btnFolderSelector";
            this.btnFolderSelector.Size = new System.Drawing.Size(53, 20);
            this.btnFolderSelector.TabIndex = 8;
            this.btnFolderSelector.Text = "Folder";
            this.btnFolderSelector.UseVisualStyleBackColor = true;
            this.btnFolderSelector.Click += new System.EventHandler(this.btnFolderSelector_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(517, 170);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(598, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbGenerateThumbnails
            // 
            this.cbGenerateThumbnails.AutoSize = true;
            this.cbGenerateThumbnails.Location = new System.Drawing.Point(127, 132);
            this.cbGenerateThumbnails.Name = "cbGenerateThumbnails";
            this.cbGenerateThumbnails.Size = new System.Drawing.Size(302, 17);
            this.cbGenerateThumbnails.TabIndex = 11;
            this.cbGenerateThumbnails.Text = "Use the first frame at 25-50-75% from duration as thumbnail";
            this.cbGenerateThumbnails.UseVisualStyleBackColor = true;
            this.cbGenerateThumbnails.CheckedChanged += new System.EventHandler(this.cbGenerateThumbnails_CheckedChanged);
            // 
            // lbWarning
            // 
            this.lbWarning.AutoSize = true;
            this.lbWarning.ForeColor = System.Drawing.Color.Red;
            this.lbWarning.Location = new System.Drawing.Point(124, 149);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(283, 13);
            this.lbWarning.TabIndex = 12;
            this.lbWarning.Text = "Warning! The import operation will take significantly longer!";
            this.lbWarning.Visible = false;
            // 
            // FrmEpisodeInfoFromFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 205);
            this.Controls.Add(this.lbWarning);
            this.Controls.Add(this.cbGenerateThumbnails);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnFolderSelector);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.tbSeason);
            this.Controls.Add(this.cbFileExtensions);
            this.Controls.Add(this.lbYear);
            this.Controls.Add(this.lbSeason);
            this.Controls.Add(this.lbFilesExtensions);
            this.Controls.Add(this.tbFilesLocation);
            this.Controls.Add(this.lbLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEpisodeInfoFromFiles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import episodes from files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.TextBox tbFilesLocation;
        private System.Windows.Forms.Label lbFilesExtensions;
        private System.Windows.Forms.Label lbSeason;
        private System.Windows.Forms.Label lbYear;
        private System.Windows.Forms.ComboBox cbFileExtensions;
        private System.Windows.Forms.TextBox tbSeason;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.Button btnFolderSelector;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbGenerateThumbnails;
        private System.Windows.Forms.Label lbWarning;
    }
}