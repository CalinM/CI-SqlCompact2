namespace Desene.DetailFormsAndUserControls.Shared
{
    partial class ucVideoStreamDetail
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
            this.label5 = new System.Windows.Forms.Label();
            this.chbTitle = new System.Windows.Forms.CheckBox();
            this.lbLanguage_Delay = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbBitRate = new System.Windows.Forms.Label();
            this.lbFormat = new System.Windows.Forms.Label();
            this.lbIndex = new System.Windows.Forms.Label();
            this.ttTitleContent = new System.Windows.Forms.ToolTip(this.components);
            this.tbHeight = new Utils.CustomTextBox();
            this.tbWidth = new Utils.CustomTextBox();
            this.tbLanguage = new Utils.CustomTextBox();
            this.tbStreamSize = new Utils.CustomTextBox();
            this.tbDelay = new Utils.CustomTextBox();
            this.tbFrameRateMode = new Utils.CustomTextBox();
            this.tbFrameRate = new Utils.CustomTextBox();
            this.tbBitRateMode = new Utils.CustomTextBox();
            this.tbBitRate = new Utils.CustomTextBox();
            this.tbFormatProfile = new Utils.CustomTextBox();
            this.tbFormat = new Utils.CustomTextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(320, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 25);
            this.label5.TabIndex = 40;
            this.label5.Text = "x";
            // 
            // chbTitle
            // 
            this.chbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbTitle.AutoSize = true;
            this.chbTitle.Location = new System.Drawing.Point(555, 229);
            this.chbTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbTitle.Name = "chbTitle";
            this.chbTitle.Size = new System.Drawing.Size(22, 21);
            this.chbTitle.TabIndex = 11;
            this.ttTitleContent.SetToolTip(this.chbTitle, "Has title specified!");
            this.chbTitle.UseVisualStyleBackColor = true;
            this.chbTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chbTitle_MouseClick);
            // 
            // lbLanguage_Delay
            // 
            this.lbLanguage_Delay.AutoSize = true;
            this.lbLanguage_Delay.Location = new System.Drawing.Point(34, 189);
            this.lbLanguage_Delay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLanguage_Delay.Name = "lbLanguage_Delay";
            this.lbLanguage_Delay.Size = new System.Drawing.Size(137, 20);
            this.lbLanguage_Delay.TabIndex = 35;
            this.lbLanguage_Delay.Text = "Language / Delay:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 229);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Stream size:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 149);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "Framerate / mode:";
            // 
            // lbBitRate
            // 
            this.lbBitRate.AutoSize = true;
            this.lbBitRate.Location = new System.Drawing.Point(34, 109);
            this.lbBitRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbBitRate.Name = "lbBitRate";
            this.lbBitRate.Size = new System.Drawing.Size(112, 20);
            this.lbBitRate.TabIndex = 25;
            this.lbBitRate.Text = "Bitrate / mode:";
            // 
            // lbFormat
            // 
            this.lbFormat.AutoSize = true;
            this.lbFormat.Location = new System.Drawing.Point(34, 69);
            this.lbFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(120, 20);
            this.lbFormat.TabIndex = 22;
            this.lbFormat.Text = "Format / Profile:";
            // 
            // lbIndex
            // 
            this.lbIndex.AutoSize = true;
            this.lbIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIndex.Location = new System.Drawing.Point(34, 25);
            this.lbIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIndex.Name = "lbIndex";
            this.lbIndex.Size = new System.Drawing.Size(82, 25);
            this.lbIndex.TabIndex = 21;
            this.lbIndex.Text = "lbIndex";
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(351, 23);
            this.tbHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(88, 26);
            this.tbHeight.TabIndex = 1;
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(220, 23);
            this.tbWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(88, 26);
            this.tbWidth.TabIndex = 0;
            // 
            // tbLanguage
            // 
            this.tbLanguage.Location = new System.Drawing.Point(220, 185);
            this.tbLanguage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbLanguage.Name = "tbLanguage";
            this.tbLanguage.Size = new System.Drawing.Size(218, 26);
            this.tbLanguage.TabIndex = 8;
            // 
            // tbStreamSize
            // 
            this.tbStreamSize.Location = new System.Drawing.Point(220, 225);
            this.tbStreamSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStreamSize.Name = "tbStreamSize";
            this.tbStreamSize.Size = new System.Drawing.Size(133, 26);
            this.tbStreamSize.TabIndex = 10;
            // 
            // tbDelay
            // 
            this.tbDelay.Location = new System.Drawing.Point(450, 185);
            this.tbDelay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDelay.Name = "tbDelay";
            this.tbDelay.Size = new System.Drawing.Size(126, 26);
            this.tbDelay.TabIndex = 9;
            // 
            // tbFrameRateMode
            // 
            this.tbFrameRateMode.Location = new System.Drawing.Point(450, 145);
            this.tbFrameRateMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFrameRateMode.Name = "tbFrameRateMode";
            this.tbFrameRateMode.Size = new System.Drawing.Size(126, 26);
            this.tbFrameRateMode.TabIndex = 7;
            // 
            // tbFrameRate
            // 
            this.tbFrameRate.Location = new System.Drawing.Point(220, 145);
            this.tbFrameRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFrameRate.Name = "tbFrameRate";
            this.tbFrameRate.Size = new System.Drawing.Size(218, 26);
            this.tbFrameRate.TabIndex = 6;
            // 
            // tbBitRateMode
            // 
            this.tbBitRateMode.Location = new System.Drawing.Point(450, 105);
            this.tbBitRateMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbBitRateMode.Name = "tbBitRateMode";
            this.tbBitRateMode.Size = new System.Drawing.Size(126, 26);
            this.tbBitRateMode.TabIndex = 5;
            // 
            // tbBitRate
            // 
            this.tbBitRate.Location = new System.Drawing.Point(220, 105);
            this.tbBitRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbBitRate.Name = "tbBitRate";
            this.tbBitRate.Size = new System.Drawing.Size(218, 26);
            this.tbBitRate.TabIndex = 4;
            // 
            // tbFormatProfile
            // 
            this.tbFormatProfile.Location = new System.Drawing.Point(450, 65);
            this.tbFormatProfile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFormatProfile.Name = "tbFormatProfile";
            this.tbFormatProfile.Size = new System.Drawing.Size(126, 26);
            this.tbFormatProfile.TabIndex = 3;
            // 
            // tbFormat
            // 
            this.tbFormat.Location = new System.Drawing.Point(220, 65);
            this.tbFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(218, 26);
            this.tbFormat.TabIndex = 2;
            // 
            // ucVideoStreamDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.chbTitle);
            this.Controls.Add(this.tbLanguage);
            this.Controls.Add(this.lbLanguage_Delay);
            this.Controls.Add(this.tbStreamSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDelay);
            this.Controls.Add(this.tbFrameRateMode);
            this.Controls.Add(this.tbFrameRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbBitRateMode);
            this.Controls.Add(this.tbBitRate);
            this.Controls.Add(this.lbBitRate);
            this.Controls.Add(this.tbFormatProfile);
            this.Controls.Add(this.tbFormat);
            this.Controls.Add(this.lbFormat);
            this.Controls.Add(this.lbIndex);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucVideoStreamDetail";
            this.Size = new System.Drawing.Size(615, 277);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private Utils.CustomTextBox tbHeight;
        private Utils.CustomTextBox tbWidth;
        private System.Windows.Forms.CheckBox chbTitle;
        private Utils.CustomTextBox tbLanguage;
        private System.Windows.Forms.Label lbLanguage_Delay;
        private Utils.CustomTextBox tbStreamSize;
        private System.Windows.Forms.Label label2;
        private Utils.CustomTextBox tbDelay;
        private Utils.CustomTextBox tbFrameRateMode;
        private Utils.CustomTextBox tbFrameRate;
        private System.Windows.Forms.Label label1;
        private Utils.CustomTextBox tbBitRateMode;
        private Utils.CustomTextBox tbBitRate;
        private System.Windows.Forms.Label lbBitRate;
        private Utils.CustomTextBox tbFormatProfile;
        private Utils.CustomTextBox tbFormat;
        private System.Windows.Forms.Label lbFormat;
        private System.Windows.Forms.Label lbIndex;
        private System.Windows.Forms.ToolTip ttTitleContent;
    }
}
