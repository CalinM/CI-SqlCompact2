namespace Desene.DetailFormsAndUserControls.Shared
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
            this.chbTitle = new System.Windows.Forms.CheckBox();
            this.tbFormat = new Utils.CustomTextBox();
            this.lbFormat = new System.Windows.Forms.Label();
            this.lbIndex = new System.Windows.Forms.Label();
            this.tbStreamSize = new Utils.CustomTextBox();
            this.ttTitleContent = new System.Windows.Forms.ToolTip(this.components);
            this.cbLanguage = new Utils.SeparatorComboBox();
            this.SuspendLayout();
            //
            // chbTitle
            //
            this.chbTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chbTitle.AutoSize = true;
            this.chbTitle.Location = new System.Drawing.Point(370, 46);
            this.chbTitle.Name = "chbTitle";
            this.chbTitle.Size = new System.Drawing.Size(15, 14);
            this.chbTitle.TabIndex = 1;
            this.ttTitleContent.SetToolTip(this.chbTitle, "Has title specified!");
            this.chbTitle.UseVisualStyleBackColor = true;
            this.chbTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chbTitle_MouseClick);
            //
            // tbFormat
            //
            this.tbFormat.Location = new System.Drawing.Point(147, 43);
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(90, 20);
            this.tbFormat.TabIndex = 2;
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
            // lbIndex
            //
            this.lbIndex.AutoSize = true;
            this.lbIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIndex.Location = new System.Drawing.Point(23, 16);
            this.lbIndex.Name = "lbIndex";
            this.lbIndex.Size = new System.Drawing.Size(59, 17);
            this.lbIndex.TabIndex = 21;
            this.lbIndex.Text = "lbIndex";
            //
            // tbStreamSize
            //
            this.tbStreamSize.Location = new System.Drawing.Point(243, 43);
            this.tbStreamSize.Name = "tbStreamSize";
            this.tbStreamSize.Size = new System.Drawing.Size(90, 20);
            this.tbStreamSize.TabIndex = 3;
            //
            // cbLanguage
            //
            this.cbLanguage.AutoAdjustItemHeight = true;
            this.cbLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(147, 16);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.SeparatorColor = System.Drawing.Color.Black;
            this.cbLanguage.SeparatorMargin = 1;
            this.cbLanguage.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbLanguage.SeparatorWidth = 1;
            this.cbLanguage.Size = new System.Drawing.Size(238, 21);
            this.cbLanguage.TabIndex = 35;
            this.cbLanguage.SelectionChangeCommitted += new System.EventHandler(this.cbLanguage_SelectionChangeCommitted);
            //
            // ucSubtitleStreamDetail
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.chbTitle);
            this.Controls.Add(this.tbStreamSize);
            this.Controls.Add(this.tbFormat);
            this.Controls.Add(this.lbFormat);
            this.Controls.Add(this.lbIndex);
            this.Name = "ucSubtitleStreamDetail";
            this.Size = new System.Drawing.Size(410, 79);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chbTitle;
        private Utils.CustomTextBox tbFormat;
        private System.Windows.Forms.Label lbFormat;
        private System.Windows.Forms.Label lbIndex;
        private Utils.CustomTextBox tbStreamSize;
        private System.Windows.Forms.ToolTip ttTitleContent;
        private Utils.SeparatorComboBox cbLanguage;
    }
}
