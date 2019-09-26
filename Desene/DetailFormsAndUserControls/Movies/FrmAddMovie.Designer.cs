using Desene.DetailFormsAndUserControls;

namespace Desene
{
    partial class FrmAddMovie
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
            this.btnImportMovieData = new System.Windows.Forms.ToolStripButton();
            this.btnLoadPoster = new System.Windows.Forms.ToolStripButton();
            this.ucMovieInfo1 = new Desene.DetailFormsAndUserControls.ucMovieInfo(true);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // toolStrip1
            //
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnCancel,
            this.toolStripSeparator1,
            this.btnImportMovieData,
            this.btnLoadPoster});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(884, 25);
            this.toolStrip1.TabIndex = 185;
            this.toolStrip1.Text = "toolStrip1";
            //
            // btnSave
            //
            this.btnSave.Enabled = false;
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
            // btnImportMovieData
            //
            this.btnImportMovieData.Image = global::Desene.Properties.Resources.import;
            this.btnImportMovieData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportMovieData.Name = "btnImportMovieData";
            this.btnImportMovieData.Size = new System.Drawing.Size(111, 22);
            this.btnImportMovieData.Text = "Import from file";
            this.btnImportMovieData.ToolTipText = "Import movie data from file";
            this.btnImportMovieData.Click += new System.EventHandler(this.btnImportMovieData_Click);
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
            // ucMovieInfo1
            //
            this.ucMovieInfo1.AllowDrop = true;
            this.ucMovieInfo1.AutoSize = true;
            this.ucMovieInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMovieInfo1.Location = new System.Drawing.Point(0, 25);
            this.ucMovieInfo1.Margin = new System.Windows.Forms.Padding(5);
            this.ucMovieInfo1.Name = "ucMovieInfo1";
            this.ucMovieInfo1.Size = new System.Drawing.Size(884, 366);
            this.ucMovieInfo1.TabIndex = 186;
            //
            // FrmAddMovie
            //
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 391);
            this.Controls.Add(this.ucMovieInfo1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddMovie";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New movie ...";
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
        private System.Windows.Forms.ToolStripButton btnImportMovieData;
        private ucMovieInfo ucMovieInfo1;
    }
}