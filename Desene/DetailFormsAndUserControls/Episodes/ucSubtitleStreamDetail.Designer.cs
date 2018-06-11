namespace Desene.DetailFormsAndUserControls.Episodes
{
    partial class ucSubtitleStreamDetail
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
            this.components = new System.ComponentModel.Container();
            this.cbTitle = new System.Windows.Forms.CheckBox();
            this.tbFormat = new System.Windows.Forms.TextBox();
            this.lbFormat = new System.Windows.Forms.Label();
            this.lbIndexResolution = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.tbStreamSize = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // cbTitle
            // 
            this.cbTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbTitle.AutoSize = true;
            this.cbTitle.Location = new System.Drawing.Point(370, 18);
            this.cbTitle.Name = "cbTitle";
            this.cbTitle.Size = new System.Drawing.Size(15, 14);
            this.cbTitle.TabIndex = 37;
            this.toolTip.SetToolTip(this.cbTitle, "Has title specified!");
            this.cbTitle.UseVisualStyleBackColor = true;
            // 
            // tbFormat
            // 
            this.tbFormat.Location = new System.Drawing.Point(147, 43);
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(149, 20);
            this.tbFormat.TabIndex = 23;
            // 
            // lbFormat
            // 
            this.lbFormat.AutoSize = true;
            this.lbFormat.Location = new System.Drawing.Point(23, 45);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(107, 13);
            this.lbFormat.TabIndex = 22;
            this.lbFormat.Text = "Format / Stream size:";
            // 
            // lbIndexResolution
            // 
            this.lbIndexResolution.AutoSize = true;
            this.lbIndexResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIndexResolution.Location = new System.Drawing.Point(23, 16);
            this.lbIndexResolution.Name = "lbIndexResolution";
            this.lbIndexResolution.Size = new System.Drawing.Size(59, 17);
            this.lbIndexResolution.TabIndex = 21;
            this.lbIndexResolution.Text = "lbIndex";
            // 
            // cbLanguage
            // 
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(147, 15);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(149, 21);
            this.cbLanguage.TabIndex = 38;
            // 
            // tbStreamSize
            // 
            this.tbStreamSize.Location = new System.Drawing.Point(300, 42);
            this.tbStreamSize.Name = "tbStreamSize";
            this.tbStreamSize.Size = new System.Drawing.Size(85, 20);
            this.tbStreamSize.TabIndex = 24;
            // 
            // ucSubtitleStreamDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.cbTitle);
            this.Controls.Add(this.tbStreamSize);
            this.Controls.Add(this.tbFormat);
            this.Controls.Add(this.lbFormat);
            this.Controls.Add(this.lbIndexResolution);
            this.Name = "ucSubtitleStreamDetail";
            this.Size = new System.Drawing.Size(410, 81);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbTitle;
        private System.Windows.Forms.TextBox tbFormat;
        private System.Windows.Forms.Label lbFormat;
        private System.Windows.Forms.Label lbIndexResolution;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.TextBox tbStreamSize;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
