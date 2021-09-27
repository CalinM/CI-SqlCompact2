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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNfNamesMix));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.miOpt_FileExt = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpt_FileExt_mkv = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpt_FileExt_mp4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpt_NameType = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpt_NameType_Dash = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpt_NameType_Dot = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpt_FNproc = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpt_FNproc_SentCase = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpt_FNproc_TitleCase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miSampleData = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslCurrentOpt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tcNfNamesMix = new System.Windows.Forms.TabControl();
            this.tpLanguage1 = new System.Windows.Forms.TabPage();
            this.rtbLanguage1 = new System.Windows.Forms.RichTextBox();
            this.tpLanguage2 = new System.Windows.Forms.TabPage();
            this.rtbLanguage2 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tcNfNamesMix.SuspendLayout();
            this.tpLanguage1.SuspendLayout();
            this.tpLanguage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.toolStripSeparator1,
            this.btnSave,
            this.btnPreview,
            this.btnReset,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(851, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 22);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Utils.Properties.Resources.save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Image = global::Utils.Properties.Resources.Merge;
            this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(68, 22);
            this.btnPreview.Text = "Preview";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = global::Utils.Properties.Resources.Reset;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(55, 22);
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpt_FileExt,
            this.miOpt_NameType,
            this.miOpt_FNproc,
            this.toolStripMenuItem1,
            this.miSampleData});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(78, 22);
            this.toolStripDropDownButton1.Text = "Options";
            // 
            // miOpt_FileExt
            // 
            this.miOpt_FileExt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpt_FileExt_mkv,
            this.miOpt_FileExt_mp4});
            this.miOpt_FileExt.Name = "miOpt_FileExt";
            this.miOpt_FileExt.Size = new System.Drawing.Size(171, 22);
            this.miOpt_FileExt.Text = "File extension";
            // 
            // miOpt_FileExt_mkv
            // 
            this.miOpt_FileExt_mkv.Checked = true;
            this.miOpt_FileExt_mkv.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miOpt_FileExt_mkv.Name = "miOpt_FileExt_mkv";
            this.miOpt_FileExt_mkv.Size = new System.Drawing.Size(101, 22);
            this.miOpt_FileExt_mkv.Tag = "0";
            this.miOpt_FileExt_mkv.Text = ".mkv";
            this.miOpt_FileExt_mkv.Click += new System.EventHandler(this.miOpt_FileExt_SubItems_Click);
            // 
            // miOpt_FileExt_mp4
            // 
            this.miOpt_FileExt_mp4.Name = "miOpt_FileExt_mp4";
            this.miOpt_FileExt_mp4.Size = new System.Drawing.Size(101, 22);
            this.miOpt_FileExt_mp4.Tag = "1";
            this.miOpt_FileExt_mp4.Text = ".mp4";
            this.miOpt_FileExt_mp4.Click += new System.EventHandler(this.miOpt_FileExt_SubItems_Click);
            // 
            // miOpt_NameType
            // 
            this.miOpt_NameType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpt_NameType_Dash,
            this.miOpt_NameType_Dot});
            this.miOpt_NameType.Name = "miOpt_NameType";
            this.miOpt_NameType.Size = new System.Drawing.Size(171, 22);
            this.miOpt_NameType.Text = "Naming type";
            // 
            // miOpt_NameType_Dash
            // 
            this.miOpt_NameType_Dash.Checked = true;
            this.miOpt_NameType_Dash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miOpt_NameType_Dash.Name = "miOpt_NameType_Dash";
            this.miOpt_NameType_Dash.Size = new System.Drawing.Size(82, 22);
            this.miOpt_NameType_Dash.Tag = "0";
            this.miOpt_NameType_Dash.Text = " -";
            this.miOpt_NameType_Dash.Click += new System.EventHandler(this.miOpt_NameType_SubItems_Click);
            // 
            // miOpt_NameType_Dot
            // 
            this.miOpt_NameType_Dot.Name = "miOpt_NameType_Dot";
            this.miOpt_NameType_Dot.Size = new System.Drawing.Size(82, 22);
            this.miOpt_NameType_Dot.Tag = "1";
            this.miOpt_NameType_Dot.Text = " .";
            this.miOpt_NameType_Dot.Click += new System.EventHandler(this.miOpt_NameType_SubItems_Click);
            // 
            // miOpt_FNproc
            // 
            this.miOpt_FNproc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpt_FNproc_SentCase,
            this.miOpt_FNproc_TitleCase});
            this.miOpt_FNproc.Name = "miOpt_FNproc";
            this.miOpt_FNproc.Size = new System.Drawing.Size(171, 22);
            this.miOpt_FNproc.Text = "Process file names";
            // 
            // miOpt_FNproc_SentCase
            // 
            this.miOpt_FNproc_SentCase.Name = "miOpt_FNproc_SentCase";
            this.miOpt_FNproc_SentCase.Size = new System.Drawing.Size(159, 22);
            this.miOpt_FNproc_SentCase.Tag = "1";
            this.miOpt_FNproc_SentCase.Text = "ToSentenceCase";
            this.miOpt_FNproc_SentCase.Click += new System.EventHandler(this.miOpt_FNproc_SubItems_Click);
            // 
            // miOpt_FNproc_TitleCase
            // 
            this.miOpt_FNproc_TitleCase.Name = "miOpt_FNproc_TitleCase";
            this.miOpt_FNproc_TitleCase.Size = new System.Drawing.Size(159, 22);
            this.miOpt_FNproc_TitleCase.Tag = "2";
            this.miOpt_FNproc_TitleCase.Text = "ToTitleCase";
            this.miOpt_FNproc_TitleCase.Click += new System.EventHandler(this.miOpt_FNproc_SubItems_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 6);
            // 
            // miSampleData
            // 
            this.miSampleData.Name = "miSampleData";
            this.miSampleData.Size = new System.Drawing.Size(171, 22);
            this.miSampleData.Text = "Sample data";
            this.miSampleData.Click += new System.EventHandler(this.miSampleData_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCurrentOpt});
            this.statusStrip1.Location = new System.Drawing.Point(0, 486);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(851, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCurrentOpt
            // 
            this.tsslCurrentOpt.Name = "tsslCurrentOpt";
            this.tsslCurrentOpt.Size = new System.Drawing.Size(12, 17);
            this.tsslCurrentOpt.Text = "-";
            // 
            // tcNfNamesMix
            // 
            this.tcNfNamesMix.Controls.Add(this.tpLanguage1);
            this.tcNfNamesMix.Controls.Add(this.tpLanguage2);
            this.tcNfNamesMix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcNfNamesMix.Location = new System.Drawing.Point(0, 25);
            this.tcNfNamesMix.Name = "tcNfNamesMix";
            this.tcNfNamesMix.SelectedIndex = 0;
            this.tcNfNamesMix.Size = new System.Drawing.Size(851, 461);
            this.tcNfNamesMix.TabIndex = 17;
            this.tcNfNamesMix.TabStop = false;
            // 
            // tpLanguage1
            // 
            this.tpLanguage1.Controls.Add(this.rtbLanguage1);
            this.tpLanguage1.Location = new System.Drawing.Point(4, 22);
            this.tpLanguage1.Name = "tpLanguage1";
            this.tpLanguage1.Padding = new System.Windows.Forms.Padding(3);
            this.tpLanguage1.Size = new System.Drawing.Size(843, 435);
            this.tpLanguage1.TabIndex = 0;
            this.tpLanguage1.Text = "Language 1 (translated)";
            this.tpLanguage1.UseVisualStyleBackColor = true;
            // 
            // rtbLanguage1
            // 
            this.rtbLanguage1.BackColor = System.Drawing.SystemColors.Window;
            this.rtbLanguage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLanguage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLanguage1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLanguage1.Location = new System.Drawing.Point(3, 3);
            this.rtbLanguage1.Name = "rtbLanguage1";
            this.rtbLanguage1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLanguage1.Size = new System.Drawing.Size(837, 429);
            this.rtbLanguage1.TabIndex = 2;
            this.rtbLanguage1.Text = resources.GetString("rtbLanguage1.Text");
            this.rtbLanguage1.TextChanged += new System.EventHandler(this.RtbLanguage1_TextChanged);
            // 
            // tpLanguage2
            // 
            this.tpLanguage2.Controls.Add(this.rtbLanguage2);
            this.tpLanguage2.Location = new System.Drawing.Point(4, 22);
            this.tpLanguage2.Name = "tpLanguage2";
            this.tpLanguage2.Padding = new System.Windows.Forms.Padding(3);
            this.tpLanguage2.Size = new System.Drawing.Size(843, 435);
            this.tpLanguage2.TabIndex = 1;
            this.tpLanguage2.Text = "Language 2 (original)";
            this.tpLanguage2.UseVisualStyleBackColor = true;
            // 
            // rtbLanguage2
            // 
            this.rtbLanguage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLanguage2.DetectUrls = false;
            this.rtbLanguage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLanguage2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLanguage2.Location = new System.Drawing.Point(3, 3);
            this.rtbLanguage2.Name = "rtbLanguage2";
            this.rtbLanguage2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLanguage2.Size = new System.Drawing.Size(837, 429);
            this.rtbLanguage2.TabIndex = 3;
            this.rtbLanguage2.Text = resources.GetString("rtbLanguage2.Text");
            this.rtbLanguage2.TextChanged += new System.EventHandler(this.RtbLanguage1_TextChanged);
            // 
            // FrmNfNamesMix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 508);
            this.Controls.Add(this.tcNfNamesMix);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 450);
            this.Name = "FrmNfNamesMix";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Text Input";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNfNamesMix_FormClosed);
            this.Load += new System.EventHandler(this.FrmNfNamesMix_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tcNfNamesMix.ResumeLayout(false);
            this.tpLanguage1.ResumeLayout(false);
            this.tpLanguage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnPreview;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem miOpt_FileExt;
        private System.Windows.Forms.ToolStripMenuItem miOpt_FileExt_mkv;
        private System.Windows.Forms.ToolStripMenuItem miOpt_FileExt_mp4;
        private System.Windows.Forms.ToolStripMenuItem miOpt_NameType;
        private System.Windows.Forms.ToolStripMenuItem miOpt_NameType_Dash;
        private System.Windows.Forms.ToolStripMenuItem miOpt_NameType_Dot;
        private System.Windows.Forms.ToolStripMenuItem miOpt_FNproc;
        private System.Windows.Forms.ToolStripMenuItem miOpt_FNproc_SentCase;
        private System.Windows.Forms.ToolStripMenuItem miOpt_FNproc_TitleCase;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentOpt;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miSampleData;
        private System.Windows.Forms.TabControl tcNfNamesMix;
        private System.Windows.Forms.TabPage tpLanguage1;
        private System.Windows.Forms.RichTextBox rtbLanguage1;
        private System.Windows.Forms.TabPage tpLanguage2;
        private System.Windows.Forms.RichTextBox rtbLanguage2;
    }
}