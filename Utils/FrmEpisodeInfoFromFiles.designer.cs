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
            this.pRercordingSpecifics = new System.Windows.Forms.Panel();
            this.lbSkipMultiVersion = new System.Windows.Forms.Label();
            this.cbSkipMultiVersion = new System.Windows.Forms.CheckBox();
            this.cbLanguages = new Utils.SeparatorComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSeason = new System.Windows.Forms.TextBox();
            this.pRercordingSpecifics.SuspendLayout();
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
            "*.avi",
            "*.ts"});
            this.cbFileExtensions.Location = new System.Drawing.Point(127, 50);
            this.cbFileExtensions.Name = "cbFileExtensions";
            this.cbFileExtensions.Size = new System.Drawing.Size(89, 21);
            this.cbFileExtensions.TabIndex = 2;
            this.cbFileExtensions.SelectedIndexChanged += new System.EventHandler(this.cbFileExtensions_SelectedIndexChanged);
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(127, 105);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(89, 20);
            this.tbYear.TabIndex = 4;
            this.tbYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbYear_KeyDown);
            // 
            // btnFolderSelector
            // 
            this.btnFolderSelector.Location = new System.Drawing.Point(620, 24);
            this.btnFolderSelector.Name = "btnFolderSelector";
            this.btnFolderSelector.Size = new System.Drawing.Size(53, 20);
            this.btnFolderSelector.TabIndex = 8;
            this.btnFolderSelector.TabStop = false;
            this.btnFolderSelector.Text = "Folder";
            this.btnFolderSelector.UseVisualStyleBackColor = true;
            this.btnFolderSelector.Click += new System.EventHandler(this.btnFolderSelector_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(517, 170);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(598, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbGenerateThumbnails
            // 
            this.cbGenerateThumbnails.AutoSize = true;
            this.cbGenerateThumbnails.Checked = true;
            this.cbGenerateThumbnails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGenerateThumbnails.Location = new System.Drawing.Point(127, 132);
            this.cbGenerateThumbnails.Name = "cbGenerateThumbnails";
            this.cbGenerateThumbnails.Size = new System.Drawing.Size(302, 17);
            this.cbGenerateThumbnails.TabIndex = 5;
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
            // 
            // pRercordingSpecifics
            // 
            this.pRercordingSpecifics.Controls.Add(this.lbSkipMultiVersion);
            this.pRercordingSpecifics.Controls.Add(this.cbSkipMultiVersion);
            this.pRercordingSpecifics.Controls.Add(this.cbLanguages);
            this.pRercordingSpecifics.Controls.Add(this.label1);
            this.pRercordingSpecifics.Location = new System.Drawing.Point(371, 46);
            this.pRercordingSpecifics.Margin = new System.Windows.Forms.Padding(2);
            this.pRercordingSpecifics.Name = "pRercordingSpecifics";
            this.pRercordingSpecifics.Size = new System.Drawing.Size(251, 75);
            this.pRercordingSpecifics.TabIndex = 18;
            // 
            // lbSkipMultiVersion
            // 
            this.lbSkipMultiVersion.AutoSize = true;
            this.lbSkipMultiVersion.Location = new System.Drawing.Point(7, 35);
            this.lbSkipMultiVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSkipMultiVersion.Name = "lbSkipMultiVersion";
            this.lbSkipMultiVersion.Size = new System.Drawing.Size(134, 13);
            this.lbSkipMultiVersion.TabIndex = 21;
            this.lbSkipMultiVersion.Text = "Skip multi-version episodes";
            // 
            // cbSkipMultiVersion
            // 
            this.cbSkipMultiVersion.AutoSize = true;
            this.cbSkipMultiVersion.Checked = true;
            this.cbSkipMultiVersion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkipMultiVersion.Location = new System.Drawing.Point(147, 36);
            this.cbSkipMultiVersion.Margin = new System.Windows.Forms.Padding(2);
            this.cbSkipMultiVersion.Name = "cbSkipMultiVersion";
            this.cbSkipMultiVersion.Size = new System.Drawing.Size(15, 14);
            this.cbSkipMultiVersion.TabIndex = 6;
            this.cbSkipMultiVersion.UseVisualStyleBackColor = true;
            // 
            // cbLanguages
            // 
            this.cbLanguages.AutoAdjustItemHeight = false;
            this.cbLanguages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLanguages.DropDownWidth = 250;
            this.cbLanguages.FormattingEnabled = true;
            this.cbLanguages.Location = new System.Drawing.Point(147, 5);
            this.cbLanguages.Margin = new System.Windows.Forms.Padding(2);
            this.cbLanguages.Name = "cbLanguages";
            this.cbLanguages.SeparatorColor = System.Drawing.Color.Black;
            this.cbLanguages.SeparatorMargin = 1;
            this.cbLanguages.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbLanguages.SeparatorWidth = 1;
            this.cbLanguages.Size = new System.Drawing.Size(96, 21);
            this.cbLanguages.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Default audio language";
            // 
            // tbSeason
            // 
            this.tbSeason.Location = new System.Drawing.Point(127, 77);
            this.tbSeason.Name = "tbSeason";
            this.tbSeason.Size = new System.Drawing.Size(89, 20);
            this.tbSeason.TabIndex = 3;
            this.tbSeason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbYear_KeyDown);
            // 
            // FrmEpisodeInfoFromFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 206);
            this.Controls.Add(this.tbSeason);
            this.Controls.Add(this.pRercordingSpecifics);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEpisodeInfoFromFiles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Episodes from Files";
            this.pRercordingSpecifics.ResumeLayout(false);
            this.pRercordingSpecifics.PerformLayout();
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
        private System.Windows.Forms.Panel pRercordingSpecifics;
        private SeparatorComboBox cbLanguages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSkipMultiVersion;
        private System.Windows.Forms.CheckBox cbSkipMultiVersion;
        private System.Windows.Forms.TextBox tbSeason;
    }
}