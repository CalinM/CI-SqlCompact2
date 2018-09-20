namespace Desene.DetailFormsAndUserControls.Shared
{
    partial class ucAudioStreamDetail
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbBitRate = new System.Windows.Forms.Label();
            this.lbFormat = new System.Windows.Forms.Label();
            this.lbIndex = new System.Windows.Forms.Label();
            this.ttTitleContent = new System.Windows.Forms.ToolTip(this.components);
            this.tbVideoDelay = new Utils.CustomTextBox();
            this.tbStreamSize = new Utils.CustomTextBox();
            this.tbDelay = new Utils.CustomTextBox();
            this.tbResolution = new Utils.CustomTextBox();
            this.tbSamplingRate = new Utils.CustomTextBox();
            this.tbChannelsPosition = new Utils.CustomTextBox();
            this.tbChannels = new Utils.CustomTextBox();
            this.tbBitRate = new Utils.CustomTextBox();
            this.tbFormat = new Utils.CustomTextBox();
            this.cbLanguage = new Utils.SeparatorComboBox();
            this.cbSource = new Utils.SeparatorComboBox();
            this.SuspendLayout();
            // 
            // chbTitle
            // 
            this.chbTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chbTitle.AutoSize = true;
            this.chbTitle.Location = new System.Drawing.Point(677, 275);
            this.chbTitle.Margin = new System.Windows.Forms.Padding(6);
            this.chbTitle.Name = "chbTitle";
            this.chbTitle.Size = new System.Drawing.Size(22, 21);
            this.chbTitle.TabIndex = 10;
            this.chbTitle.TabStop = false;
            this.ttTitleContent.SetToolTip(this.chbTitle, "Has title specified!");
            this.chbTitle.UseVisualStyleBackColor = true;
            this.chbTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chbTitle_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 275);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 25);
            this.label2.TabIndex = 33;
            this.label2.Text = "Stream size:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 227);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 25);
            this.label3.TabIndex = 31;
            this.label3.Text = "Delay / video delay:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 179);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 25);
            this.label1.TabIndex = 28;
            this.label1.Text = "Sampling / Resolution:";
            // 
            // lbBitRate
            // 
            this.lbBitRate.AutoSize = true;
            this.lbBitRate.Location = new System.Drawing.Point(42, 131);
            this.lbBitRate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbBitRate.Name = "lbBitRate";
            this.lbBitRate.Size = new System.Drawing.Size(216, 25);
            this.lbBitRate.TabIndex = 25;
            this.lbBitRate.Text = "Channels / Ch.position:";
            // 
            // lbFormat
            // 
            this.lbFormat.AutoSize = true;
            this.lbFormat.Location = new System.Drawing.Point(42, 83);
            this.lbFormat.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(150, 25);
            this.lbFormat.TabIndex = 22;
            this.lbFormat.Text = "Format / Bitrate:";
            // 
            // lbIndex
            // 
            this.lbIndex.AutoSize = true;
            this.lbIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIndex.Location = new System.Drawing.Point(42, 32);
            this.lbIndex.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbIndex.Name = "lbIndex";
            this.lbIndex.Size = new System.Drawing.Size(88, 25);
            this.lbIndex.TabIndex = 21;
            this.lbIndex.Text = "lbIndex";
            // 
            // tbVideoDelay
            // 
            this.tbVideoDelay.Location = new System.Drawing.Point(550, 222);
            this.tbVideoDelay.Margin = new System.Windows.Forms.Padding(6);
            this.tbVideoDelay.Name = "tbVideoDelay";
            this.tbVideoDelay.Size = new System.Drawing.Size(153, 29);
            this.tbVideoDelay.TabIndex = 9;
            // 
            // tbStreamSize
            // 
            this.tbStreamSize.Location = new System.Drawing.Point(270, 270);
            this.tbStreamSize.Margin = new System.Windows.Forms.Padding(6);
            this.tbStreamSize.Name = "tbStreamSize";
            this.tbStreamSize.Size = new System.Drawing.Size(162, 29);
            this.tbStreamSize.TabIndex = 10;
            // 
            // tbDelay
            // 
            this.tbDelay.Location = new System.Drawing.Point(270, 222);
            this.tbDelay.Margin = new System.Windows.Forms.Padding(6);
            this.tbDelay.Name = "tbDelay";
            this.tbDelay.Size = new System.Drawing.Size(266, 29);
            this.tbDelay.TabIndex = 8;
            // 
            // tbResolution
            // 
            this.tbResolution.Location = new System.Drawing.Point(550, 174);
            this.tbResolution.Margin = new System.Windows.Forms.Padding(6);
            this.tbResolution.Name = "tbResolution";
            this.tbResolution.Size = new System.Drawing.Size(153, 29);
            this.tbResolution.TabIndex = 7;
            // 
            // tbSamplingRate
            // 
            this.tbSamplingRate.Location = new System.Drawing.Point(270, 174);
            this.tbSamplingRate.Margin = new System.Windows.Forms.Padding(6);
            this.tbSamplingRate.Name = "tbSamplingRate";
            this.tbSamplingRate.Size = new System.Drawing.Size(266, 29);
            this.tbSamplingRate.TabIndex = 6;
            // 
            // tbChannelsPosition
            // 
            this.tbChannelsPosition.Location = new System.Drawing.Point(550, 126);
            this.tbChannelsPosition.Margin = new System.Windows.Forms.Padding(6);
            this.tbChannelsPosition.Name = "tbChannelsPosition";
            this.tbChannelsPosition.Size = new System.Drawing.Size(153, 29);
            this.tbChannelsPosition.TabIndex = 5;
            // 
            // tbChannels
            // 
            this.tbChannels.Location = new System.Drawing.Point(270, 126);
            this.tbChannels.Margin = new System.Windows.Forms.Padding(6);
            this.tbChannels.Name = "tbChannels";
            this.tbChannels.Size = new System.Drawing.Size(266, 29);
            this.tbChannels.TabIndex = 4;
            // 
            // tbBitRate
            // 
            this.tbBitRate.Location = new System.Drawing.Point(550, 78);
            this.tbBitRate.Margin = new System.Windows.Forms.Padding(6);
            this.tbBitRate.Name = "tbBitRate";
            this.tbBitRate.Size = new System.Drawing.Size(153, 29);
            this.tbBitRate.TabIndex = 3;
            // 
            // tbFormat
            // 
            this.tbFormat.Location = new System.Drawing.Point(270, 79);
            this.tbFormat.Margin = new System.Windows.Forms.Padding(6);
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(270, 29);
            this.tbFormat.TabIndex = 2;
            // 
            // cbLanguage
            // 
            this.cbLanguage.AutoAdjustItemHeight = true;
            this.cbLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(270, 30);
            this.cbLanguage.Margin = new System.Windows.Forms.Padding(6);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.SeparatorColor = System.Drawing.Color.Black;
            this.cbLanguage.SeparatorMargin = 1;
            this.cbLanguage.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbLanguage.SeparatorWidth = 1;
            this.cbLanguage.Size = new System.Drawing.Size(270, 30);
            this.cbLanguage.TabIndex = 0;
            this.cbLanguage.SelectionChangeCommitted += new System.EventHandler(this.cbLanguage_SelectionChangeCommitted);
            // 
            // cbSource
            // 
            this.cbSource.AutoAdjustItemHeight = true;
            this.cbSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Location = new System.Drawing.Point(550, 29);
            this.cbSource.Margin = new System.Windows.Forms.Padding(6);
            this.cbSource.Name = "cbSource";
            this.cbSource.SeparatorColor = System.Drawing.Color.Black;
            this.cbSource.SeparatorMargin = 1;
            this.cbSource.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbSource.SeparatorWidth = 1;
            this.cbSource.Size = new System.Drawing.Size(153, 30);
            this.cbSource.TabIndex = 1;
            this.cbSource.SelectionChangeCommitted += new System.EventHandler(this.cbLanguage_SelectionChangeCommitted);
            // 
            // ucAudioStreamDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cbSource);
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.tbVideoDelay);
            this.Controls.Add(this.chbTitle);
            this.Controls.Add(this.tbStreamSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDelay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbResolution);
            this.Controls.Add(this.tbSamplingRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbChannelsPosition);
            this.Controls.Add(this.tbChannels);
            this.Controls.Add(this.lbBitRate);
            this.Controls.Add(this.tbBitRate);
            this.Controls.Add(this.tbFormat);
            this.Controls.Add(this.lbFormat);
            this.Controls.Add(this.lbIndex);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ucAudioStreamDetail";
            this.Size = new System.Drawing.Size(752, 332);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chbTitle;
        private Utils.CustomTextBox tbStreamSize;
        private System.Windows.Forms.Label label2;
        private Utils.CustomTextBox tbDelay;
        private System.Windows.Forms.Label label3;
        private Utils.CustomTextBox tbResolution;
        private Utils.CustomTextBox tbSamplingRate;
        private System.Windows.Forms.Label label1;
        private Utils.CustomTextBox tbChannels;
        private System.Windows.Forms.Label lbBitRate;
        private Utils.CustomTextBox tbFormat;
        private System.Windows.Forms.Label lbFormat;
        private System.Windows.Forms.Label lbIndex;
        private Utils.CustomTextBox tbChannelsPosition;
        private Utils.CustomTextBox tbBitRate;
        private Utils.CustomTextBox tbVideoDelay;
        private System.Windows.Forms.ToolTip ttTitleContent;
        private Utils.SeparatorComboBox cbLanguage;
        private Utils.SeparatorComboBox cbSource;
    }
}
