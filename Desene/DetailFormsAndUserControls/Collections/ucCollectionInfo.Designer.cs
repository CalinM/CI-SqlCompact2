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
            this.lbNotes = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbSiteSectionType = new System.Windows.Forms.Label();
            this.cbSectionType = new System.Windows.Forms.ComboBox();
            this.tbTitle = new Utils.CustomTextBox();
            this.tbNotes = new Utils.CustomTextBox();
            this.SuspendLayout();
            // 
            // lbNotes
            // 
            this.lbNotes.AutoSize = true;
            this.lbNotes.Location = new System.Drawing.Point(19, 41);
            this.lbNotes.Name = "lbNotes";
            this.lbNotes.Size = new System.Drawing.Size(35, 13);
            this.lbNotes.TabIndex = 193;
            this.lbNotes.Text = "Notes";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(19, 15);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(27, 13);
            this.lbTitle.TabIndex = 191;
            this.lbTitle.Text = "Title";
            // 
            // lbSiteSectionType
            // 
            this.lbSiteSectionType.AutoSize = true;
            this.lbSiteSectionType.Location = new System.Drawing.Point(19, 211);
            this.lbSiteSectionType.Name = "lbSiteSectionType";
            this.lbSiteSectionType.Size = new System.Drawing.Size(43, 26);
            this.lbSiteSectionType.TabIndex = 195;
            this.lbSiteSectionType.Text = "Section\r\n type";
            this.lbSiteSectionType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbSectionType
            // 
            this.cbSectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSectionType.FormattingEnabled = true;
            this.cbSectionType.Items.AddRange(new object[] {
            "Movies-style",
            "Series-style"});
            this.cbSectionType.Location = new System.Drawing.Point(74, 211);
            this.cbSectionType.Name = "cbSectionType";
            this.cbSectionType.Size = new System.Drawing.Size(133, 21);
            this.cbSectionType.TabIndex = 2;
            this.cbSectionType.SelectedIndexChanged += new System.EventHandler(this.cbSectionType_SelectedIndexChanged);
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(74, 12);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(385, 20);
            this.tbTitle.TabIndex = 0;
            // 
            // tbNotes
            // 
            this.tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNotes.Location = new System.Drawing.Point(74, 38);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(385, 167);
            this.tbNotes.TabIndex = 1;
            // 
            // ucCollectionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbNotes);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.cbSectionType);
            this.Controls.Add(this.lbSiteSectionType);
            this.Controls.Add(this.lbNotes);
            this.Controls.Add(this.lbTitle);
            this.Name = "ucCollectionInfo";
            this.Size = new System.Drawing.Size(480, 254);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbNotes;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbSiteSectionType;
        private System.Windows.Forms.ComboBox cbSectionType;
        public Utils.CustomTextBox tbTitle;
        private Utils.CustomTextBox tbNotes;
    }
}
