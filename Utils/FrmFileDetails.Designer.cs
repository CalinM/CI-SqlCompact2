namespace Utils
{
    partial class FrmFileDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFileDetails));
            this.dgvFilesDetails = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripDropDownButton();
            this.miExportToXML = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportToCSV = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesDetails)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFilesDetails
            // 
            this.dgvFilesDetails.AllowUserToAddRows = false;
            this.dgvFilesDetails.AllowUserToDeleteRows = false;
            this.dgvFilesDetails.AllowUserToResizeRows = false;
            this.dgvFilesDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFilesDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvFilesDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilesDetails.Location = new System.Drawing.Point(12, 46);
            this.dgvFilesDetails.MultiSelect = false;
            this.dgvFilesDetails.Name = "dgvFilesDetails";
            this.dgvFilesDetails.ReadOnly = true;
            this.dgvFilesDetails.RowHeadersVisible = false;
            this.dgvFilesDetails.RowHeadersWidth = 62;
            this.dgvFilesDetails.RowTemplate.Height = 20;
            this.dgvFilesDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilesDetails.Size = new System.Drawing.Size(1274, 409);
            this.dgvFilesDetails.TabIndex = 113;
            this.dgvFilesDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvFilesDetails_CellFormatting);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1298, 34);
            this.toolStrip1.TabIndex = 114;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 29);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExportToXML,
            this.miExportToCSV});
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(105, 29);
            this.btnExport.Text = "Export";
            // 
            // miExportToXML
            // 
            this.miExportToXML.Name = "miExportToXML";
            this.miExportToXML.Size = new System.Drawing.Size(171, 34);
            this.miExportToXML.Text = "to XML";
            this.miExportToXML.Click += new System.EventHandler(this.miExportToXML_Click);
            // 
            // miExportToCSV
            // 
            this.miExportToCSV.Name = "miExportToCSV";
            this.miExportToCSV.Size = new System.Drawing.Size(171, 34);
            this.miExportToCSV.Text = "to CSV";
            this.miExportToCSV.Click += new System.EventHandler(this.miExportToCSV_Click);
            // 
            // FrmFileDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 465);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvFilesDetails);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFileDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Files details";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesDetails)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFilesDetails;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripDropDownButton btnExport;
        private System.Windows.Forms.ToolStripMenuItem miExportToXML;
        private System.Windows.Forms.ToolStripMenuItem miExportToCSV;
    }
}