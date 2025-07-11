﻿namespace Desene
{
    partial class FrmEditSeriesBaseInfo
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLoadPoster = new System.Windows.Forms.ToolStripButton();
            this.ucEditSeriesBaseInfo = new Desene.EditUserControls.ucEditSeriesBaseInfo(true);
            this.btnOpenPages = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnCancel,
            this.toolStripSeparator1,
            this.btnLoadPoster,
            this.btnOpenPages});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(812, 25);
            this.toolStrip1.TabIndex = 184;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Desene.Properties.Resources.save_as;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Desene.Properties.Resources.cancel;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 22);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLoadPoster
            // 
            this.btnLoadPoster.Image = global::Desene.Properties.Resources.image;
            this.btnLoadPoster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadPoster.Name = "btnLoadPoster";
            this.btnLoadPoster.Size = new System.Drawing.Size(89, 22);
            this.btnLoadPoster.Text = "Load poster";
            this.btnLoadPoster.Click += new System.EventHandler(this.btnLoadPoster_Click);
            // 
            // ucEditSeriesBaseInfo
            // 
            this.ucEditSeriesBaseInfo.AllowDrop = true;
            this.ucEditSeriesBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEditSeriesBaseInfo.Location = new System.Drawing.Point(0, 25);
            this.ucEditSeriesBaseInfo.Name = "ucEditSeriesBaseInfo";
            this.ucEditSeriesBaseInfo.Size = new System.Drawing.Size(812, 362);
            this.ucEditSeriesBaseInfo.TabIndex = 185;
            // 
            // btnOpenPages
            // 
            this.btnOpenPages.Image = global::Desene.Properties.Resources.www;
            this.btnOpenPages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenPages.Name = "btnOpenPages";
            this.btnOpenPages.Size = new System.Drawing.Size(117, 22);
            this.btnOpenPages.Text = "Open base pages";
            this.btnOpenPages.Click += new System.EventHandler(this.btnOpenPages_Click);
            // 
            // FrmEditSeriesBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 387);
            this.Controls.Add(this.ucEditSeriesBaseInfo);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditSeriesBaseInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New series";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmEditSeriesBaseInfo_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnLoadPoster;
        private EditUserControls.ucEditSeriesBaseInfo ucEditSeriesBaseInfo;
        private System.Windows.Forms.ToolStripButton btnOpenPages;
    }
}