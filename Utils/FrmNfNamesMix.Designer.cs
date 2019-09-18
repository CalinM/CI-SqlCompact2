namespace Utils
{
    partial class FrmNfNamesMix
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tcNfNamesMix = new System.Windows.Forms.TabControl();
            this.tpLanguage1 = new System.Windows.Forms.TabPage();
            this.rtbLanguage1 = new System.Windows.Forms.RichTextBox();
            this.tpLanguage2 = new System.Windows.Forms.TabPage();
            this.rtbLanguage2 = new System.Windows.Forms.RichTextBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.lbFilesExt = new System.Windows.Forms.Label();
            this.cbFilesExt = new System.Windows.Forms.ComboBox();
            this.lbNamingType = new System.Windows.Forms.Label();
            this.cbNamingType = new System.Windows.Forms.ComboBox();
            this.tcNfNamesMix.SuspendLayout();
            this.tpLanguage1.SuspendLayout();
            this.tpLanguage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Enabled = false;
            this.btnConfirm.Location = new System.Drawing.Point(402, 252);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // tcNfNamesMix
            // 
            this.tcNfNamesMix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcNfNamesMix.Controls.Add(this.tpLanguage1);
            this.tcNfNamesMix.Controls.Add(this.tpLanguage2);
            this.tcNfNamesMix.Location = new System.Drawing.Point(12, 12);
            this.tcNfNamesMix.Name = "tcNfNamesMix";
            this.tcNfNamesMix.SelectedIndex = 0;
            this.tcNfNamesMix.Size = new System.Drawing.Size(550, 234);
            this.tcNfNamesMix.TabIndex = 5;
            // 
            // tpLanguage1
            // 
            this.tpLanguage1.Controls.Add(this.rtbLanguage1);
            this.tpLanguage1.Location = new System.Drawing.Point(4, 22);
            this.tpLanguage1.Name = "tpLanguage1";
            this.tpLanguage1.Padding = new System.Windows.Forms.Padding(3);
            this.tpLanguage1.Size = new System.Drawing.Size(542, 208);
            this.tpLanguage1.TabIndex = 0;
            this.tpLanguage1.Text = "Language 1 (translated)";
            this.tpLanguage1.UseVisualStyleBackColor = true;
            // 
            // rtbLanguage1
            // 
            this.rtbLanguage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLanguage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLanguage1.Location = new System.Drawing.Point(3, 3);
            this.rtbLanguage1.Name = "rtbLanguage1";
            this.rtbLanguage1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLanguage1.Size = new System.Drawing.Size(536, 202);
            this.rtbLanguage1.TabIndex = 2;
            this.rtbLanguage1.Text = "";
            this.rtbLanguage1.TextChanged += new System.EventHandler(this.RtbLanguage1_TextChanged);
            // 
            // tpLanguage2
            // 
            this.tpLanguage2.Controls.Add(this.rtbLanguage2);
            this.tpLanguage2.Location = new System.Drawing.Point(4, 22);
            this.tpLanguage2.Name = "tpLanguage2";
            this.tpLanguage2.Padding = new System.Windows.Forms.Padding(3);
            this.tpLanguage2.Size = new System.Drawing.Size(472, 163);
            this.tpLanguage2.TabIndex = 1;
            this.tpLanguage2.Text = "Language 2 (original)";
            this.tpLanguage2.UseVisualStyleBackColor = true;
            // 
            // rtbLanguage2
            // 
            this.rtbLanguage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLanguage2.DetectUrls = false;
            this.rtbLanguage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLanguage2.Location = new System.Drawing.Point(3, 3);
            this.rtbLanguage2.Name = "rtbLanguage2";
            this.rtbLanguage2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLanguage2.Size = new System.Drawing.Size(466, 157);
            this.rtbLanguage2.TabIndex = 3;
            this.rtbLanguage2.Text = "";
            this.rtbLanguage2.TextChanged += new System.EventHandler(this.RtbLanguage1_TextChanged);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Enabled = false;
            this.btnPreview.Location = new System.Drawing.Point(483, 252);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 6;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // lbFilesExt
            // 
            this.lbFilesExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbFilesExt.AutoSize = true;
            this.lbFilesExt.Location = new System.Drawing.Point(16, 257);
            this.lbFilesExt.Name = "lbFilesExt";
            this.lbFilesExt.Size = new System.Drawing.Size(76, 13);
            this.lbFilesExt.TabIndex = 7;
            this.lbFilesExt.Text = "Files extension";
            // 
            // cbFilesExt
            // 
            this.cbFilesExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFilesExt.FormattingEnabled = true;
            this.cbFilesExt.Items.AddRange(new object[] {
            ".mkv",
            ".mp4"});
            this.cbFilesExt.Location = new System.Drawing.Point(98, 254);
            this.cbFilesExt.Name = "cbFilesExt";
            this.cbFilesExt.Size = new System.Drawing.Size(77, 21);
            this.cbFilesExt.TabIndex = 8;
            this.cbFilesExt.SelectedIndexChanged += new System.EventHandler(this.CbFilesExt_SelectedIndexChanged);
            // 
            // lbNamingType
            // 
            this.lbNamingType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbNamingType.AutoSize = true;
            this.lbNamingType.Location = new System.Drawing.Point(194, 257);
            this.lbNamingType.Name = "lbNamingType";
            this.lbNamingType.Size = new System.Drawing.Size(66, 13);
            this.lbNamingType.TabIndex = 9;
            this.lbNamingType.Text = "Naming type";
            // 
            // cbNamingType
            // 
            this.cbNamingType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbNamingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNamingType.FormattingEnabled = true;
            this.cbNamingType.Items.AddRange(new object[] {
            ". ",
            " - "});
            this.cbNamingType.Location = new System.Drawing.Point(266, 254);
            this.cbNamingType.Name = "cbNamingType";
            this.cbNamingType.Size = new System.Drawing.Size(77, 21);
            this.cbNamingType.TabIndex = 10;
            this.cbNamingType.SelectedIndexChanged += new System.EventHandler(this.CbFilesExt_SelectedIndexChanged);
            // 
            // FrmNfNamesMix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 281);
            this.Controls.Add(this.cbNamingType);
            this.Controls.Add(this.lbNamingType);
            this.Controls.Add(this.cbFilesExt);
            this.Controls.Add(this.lbFilesExt);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.tcNfNamesMix);
            this.Controls.Add(this.btnConfirm);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 320);
            this.Name = "FrmNfNamesMix";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Text Input";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNfNamesMix_FormClosed);
            this.Load += new System.EventHandler(this.FrmNfNamesMix_Load);
            this.tcNfNamesMix.ResumeLayout(false);
            this.tpLanguage1.ResumeLayout(false);
            this.tpLanguage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TabControl tcNfNamesMix;
        private System.Windows.Forms.TabPage tpLanguage1;
        private System.Windows.Forms.TabPage tpLanguage2;
        private System.Windows.Forms.RichTextBox rtbLanguage1;
        private System.Windows.Forms.RichTextBox rtbLanguage2;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label lbFilesExt;
        private System.Windows.Forms.ComboBox cbFilesExt;
        private System.Windows.Forms.Label lbNamingType;
        private System.Windows.Forms.ComboBox cbNamingType;
    }
}