namespace Desene.DetailFormsAndUserControls.Collections
{
    partial class ucCollectionInfo
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pMainControls = new System.Windows.Forms.Panel();
            this.tbNotes = new Utils.CustomTextBox();
            this.tbTitle = new Utils.CustomTextBox();
            this.lbNotes = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.cbSectionType = new System.Windows.Forms.ComboBox();
            this.lbSiteSectionType = new System.Windows.Forms.Label();
            this.pPosterWrapper = new System.Windows.Forms.Panel();
            this.pbCover = new System.Windows.Forms.PictureBox();
            this.pMainControls.SuspendLayout();
            this.pPosterWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).BeginInit();
            this.SuspendLayout();
            // 
            // pMainControls
            // 
            this.pMainControls.Controls.Add(this.tbNotes);
            this.pMainControls.Controls.Add(this.tbTitle);
            this.pMainControls.Controls.Add(this.lbNotes);
            this.pMainControls.Controls.Add(this.lbTitle);
            this.pMainControls.Controls.Add(this.cbSectionType);
            this.pMainControls.Controls.Add(this.lbSiteSectionType);
            this.pMainControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainControls.Location = new System.Drawing.Point(0, 0);
            this.pMainControls.Name = "pMainControls";
            this.pMainControls.Size = new System.Drawing.Size(458, 387);
            this.pMainControls.TabIndex = 196;
            // 
            // tbNotes
            // 
            this.tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNotes.Location = new System.Drawing.Point(70, 49);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(371, 292);
            this.tbNotes.TabIndex = 1;
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(70, 23);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(371, 20);
            this.tbTitle.TabIndex = 0;
            // 
            // lbNotes
            // 
            this.lbNotes.AutoSize = true;
            this.lbNotes.Location = new System.Drawing.Point(15, 52);
            this.lbNotes.Name = "lbNotes";
            this.lbNotes.Size = new System.Drawing.Size(35, 13);
            this.lbNotes.TabIndex = 201;
            this.lbNotes.Text = "Notes";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(15, 26);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(27, 13);
            this.lbTitle.TabIndex = 200;
            this.lbTitle.Text = "Title";
            // 
            // cbSectionType
            // 
            this.cbSectionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSectionType.FormattingEnabled = true;
            this.cbSectionType.Items.AddRange(new object[] {
            "Movies-style",
            "Series-style"});
            this.cbSectionType.Location = new System.Drawing.Point(70, 347);
            this.cbSectionType.Name = "cbSectionType";
            this.cbSectionType.Size = new System.Drawing.Size(133, 21);
            this.cbSectionType.TabIndex = 2;
            this.cbSectionType.SelectedIndexChanged += new System.EventHandler(this.cbSectionType_SelectedIndexChanged);
            // 
            // lbSiteSectionType
            // 
            this.lbSiteSectionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSiteSectionType.AutoSize = true;
            this.lbSiteSectionType.Location = new System.Drawing.Point(15, 343);
            this.lbSiteSectionType.Name = "lbSiteSectionType";
            this.lbSiteSectionType.Size = new System.Drawing.Size(43, 26);
            this.lbSiteSectionType.TabIndex = 197;
            this.lbSiteSectionType.Text = "Section\r\n type";
            this.lbSiteSectionType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pPosterWrapper
            // 
            this.pPosterWrapper.Controls.Add(this.pbCover);
            this.pPosterWrapper.Dock = System.Windows.Forms.DockStyle.Right;
            this.pPosterWrapper.Location = new System.Drawing.Point(458, 0);
            this.pPosterWrapper.Name = "pPosterWrapper";
            this.pPosterWrapper.Size = new System.Drawing.Size(231, 387);
            this.pPosterWrapper.TabIndex = 197;
            // 
            // pbCover
            // 
            this.pbCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCover.Location = new System.Drawing.Point(1, 23);
            this.pbCover.Name = "pbCover";
            this.pbCover.Size = new System.Drawing.Size(212, 318);
            this.pbCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCover.TabIndex = 268;
            this.pbCover.TabStop = false;
            // 
            // ucCollectionInfo
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pMainControls);
            this.Controls.Add(this.pPosterWrapper);
            this.Name = "ucCollectionInfo";
            this.Size = new System.Drawing.Size(689, 387);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ucCollectionInfo_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.ucCollectionInfo_DragOver);
            this.pMainControls.ResumeLayout(false);
            this.pMainControls.PerformLayout();
            this.pPosterWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pMainControls;
        private Utils.CustomTextBox tbNotes;
        public Utils.CustomTextBox tbTitle;
        private System.Windows.Forms.Label lbNotes;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.ComboBox cbSectionType;
        private System.Windows.Forms.Label lbSiteSectionType;
        private System.Windows.Forms.Panel pPosterWrapper;
        private System.Windows.Forms.PictureBox pbCover;
    }
}
