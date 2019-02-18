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
            this.tbYear = new System.Windows.Forms.TextBox();
            this.btnFolderSelector = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbGenerateThumbnails = new System.Windows.Forms.CheckBox();
            this.lbWarning = new System.Windows.Forms.Label();
            this.cbSeason = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(39, 43);
            this.lbLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(70, 20);
            this.lbLocation.TabIndex = 0;
            this.lbLocation.Text = "Location";
            // 
            // tbFilesLocation
            // 
            this.tbFilesLocation.Location = new System.Drawing.Point(190, 37);
            this.tbFilesLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFilesLocation.Name = "tbFilesLocation";
            this.tbFilesLocation.Size = new System.Drawing.Size(728, 26);
            this.tbFilesLocation.TabIndex = 1;
            this.tbFilesLocation.TextChanged += new System.EventHandler(this.tbFilesLocation_TextChanged);
            // 
            // lbFilesExtensions
            // 
            this.lbFilesExtensions.AutoSize = true;
            this.lbFilesExtensions.Location = new System.Drawing.Point(39, 82);
            this.lbFilesExtensions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFilesExtensions.Name = "lbFilesExtensions";
            this.lbFilesExtensions.Size = new System.Drawing.Size(122, 20);
            this.lbFilesExtensions.TabIndex = 2;
            this.lbFilesExtensions.Text = "Files extensions";
            // 
            // lbSeason
            // 
            this.lbSeason.AutoSize = true;
            this.lbSeason.Location = new System.Drawing.Point(39, 125);
            this.lbSeason.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSeason.Name = "lbSeason";
            this.lbSeason.Size = new System.Drawing.Size(64, 20);
            this.lbSeason.TabIndex = 3;
            this.lbSeason.Text = "Season";
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.Location = new System.Drawing.Point(39, 166);
            this.lbYear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(43, 20);
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
            this.cbFileExtensions.Location = new System.Drawing.Point(190, 77);
            this.cbFileExtensions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbFileExtensions.Name = "cbFileExtensions";
            this.cbFileExtensions.Size = new System.Drawing.Size(132, 28);
            this.cbFileExtensions.TabIndex = 5;
            this.cbFileExtensions.SelectedIndexChanged += new System.EventHandler(this.cbFileExtensions_SelectedIndexChanged);
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(190, 162);
            this.tbYear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(132, 26);
            this.tbYear.TabIndex = 7;
            // 
            // btnFolderSelector
            // 
            this.btnFolderSelector.Location = new System.Drawing.Point(930, 37);
            this.btnFolderSelector.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFolderSelector.Name = "btnFolderSelector";
            this.btnFolderSelector.Size = new System.Drawing.Size(80, 31);
            this.btnFolderSelector.TabIndex = 8;
            this.btnFolderSelector.Text = "Folder";
            this.btnFolderSelector.UseVisualStyleBackColor = true;
            this.btnFolderSelector.Click += new System.EventHandler(this.btnFolderSelector_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(776, 262);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 35);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(897, 262);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbGenerateThumbnails
            // 
            this.cbGenerateThumbnails.AutoSize = true;
            this.cbGenerateThumbnails.Location = new System.Drawing.Point(190, 203);
            this.cbGenerateThumbnails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGenerateThumbnails.Name = "cbGenerateThumbnails";
            this.cbGenerateThumbnails.Size = new System.Drawing.Size(458, 24);
            this.cbGenerateThumbnails.TabIndex = 11;
            this.cbGenerateThumbnails.Text = "Use the first frame at 25-50-75% from duration as thumbnail";
            this.cbGenerateThumbnails.UseVisualStyleBackColor = true;
            this.cbGenerateThumbnails.CheckedChanged += new System.EventHandler(this.cbGenerateThumbnails_CheckedChanged);
            // 
            // lbWarning
            // 
            this.lbWarning.AutoSize = true;
            this.lbWarning.ForeColor = System.Drawing.Color.Red;
            this.lbWarning.Location = new System.Drawing.Point(186, 229);
            this.lbWarning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(418, 20);
            this.lbWarning.TabIndex = 12;
            this.lbWarning.Text = "Warning! The import operation will take significantly longer!";
            this.lbWarning.Visible = false;
            // 
            // cbSeason
            // 
            this.cbSeason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSeason.FormattingEnabled = true;
            this.cbSeason.Location = new System.Drawing.Point(190, 122);
            this.cbSeason.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSeason.Name = "cbSeason";
            this.cbSeason.Size = new System.Drawing.Size(132, 28);
            this.cbSeason.TabIndex = 14;
            this.cbSeason.SelectedIndexChanged += new System.EventHandler(this.cbSeason_SelectedIndexChanged);
            // 
            // FrmEpisodeInfoFromFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 317);
            this.Controls.Add(this.cbSeason);
            this.Controls.Add(this.lbWarning);
            this.Controls.Add(this.cbGenerateThumbnails);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnFolderSelector);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.cbFileExtensions);
            this.Controls.Add(this.lbYear);
            this.Controls.Add(this.lbSeason);
            this.Controls.Add(this.lbFilesExtensions);
            this.Controls.Add(this.tbFilesLocation);
            this.Controls.Add(this.lbLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEpisodeInfoFromFiles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Episodes from Files";
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
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.Button btnFolderSelector;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbGenerateThumbnails;
        private System.Windows.Forms.Label lbWarning;
        private System.Windows.Forms.ComboBox cbSeason;
    }
}