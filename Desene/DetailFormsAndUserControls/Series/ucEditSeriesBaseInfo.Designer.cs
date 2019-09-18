namespace Desene.EditUserControls
{
    partial class ucEditSeriesBaseInfo
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
            this.pbCover = new System.Windows.Forms.PictureBox();
            this.tbNotes = new Utils.CustomTextBox();
            this.tbRecommendedLink = new Utils.CustomTextBox();
            this.tbRecommended = new Utils.CustomTextBox();
            this.lbRecommended = new System.Windows.Forms.Label();
            this.tbDescriptionLink = new Utils.CustomTextBox();
            this.pbDescriptionLink = new System.Windows.Forms.Label();
            this.tbTitle = new Utils.CustomTextBox();
            this.lbSeriesTitle = new System.Windows.Forms.Label();
            this.lbNotes = new System.Windows.Forms.Label();
            this.tbTrailer = new Utils.CustomTextBox();
            this.lbTrailer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCover
            // 
            this.pbCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCover.Location = new System.Drawing.Point(587, 24);
            this.pbCover.Name = "pbCover";
            this.pbCover.Size = new System.Drawing.Size(212, 318);
            this.pbCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCover.TabIndex = 193;
            this.pbCover.TabStop = false;
            // 
            // tbNotes
            // 
            this.tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNotes.Location = new System.Drawing.Point(128, 102);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(435, 219);
            this.tbNotes.TabIndex = 4;
            // 
            // tbRecommendedLink
            // 
            this.tbRecommendedLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRecommendedLink.Location = new System.Drawing.Point(184, 76);
            this.tbRecommendedLink.Name = "tbRecommendedLink";
            this.tbRecommendedLink.Size = new System.Drawing.Size(379, 20);
            this.tbRecommendedLink.TabIndex = 3;
            // 
            // tbRecommended
            // 
            this.tbRecommended.Location = new System.Drawing.Point(128, 76);
            this.tbRecommended.Name = "tbRecommended";
            this.tbRecommended.Size = new System.Drawing.Size(50, 20);
            this.tbRecommended.TabIndex = 2;
            // 
            // lbRecommended
            // 
            this.lbRecommended.AutoSize = true;
            this.lbRecommended.Location = new System.Drawing.Point(23, 79);
            this.lbRecommended.Name = "lbRecommended";
            this.lbRecommended.Size = new System.Drawing.Size(82, 13);
            this.lbRecommended.TabIndex = 189;
            this.lbRecommended.Text = "Recommended:";
            // 
            // tbDescriptionLink
            // 
            this.tbDescriptionLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescriptionLink.Location = new System.Drawing.Point(128, 50);
            this.tbDescriptionLink.Name = "tbDescriptionLink";
            this.tbDescriptionLink.Size = new System.Drawing.Size(435, 20);
            this.tbDescriptionLink.TabIndex = 1;
            // 
            // pbDescriptionLink
            // 
            this.pbDescriptionLink.AutoSize = true;
            this.pbDescriptionLink.Location = new System.Drawing.Point(23, 53);
            this.pbDescriptionLink.Name = "pbDescriptionLink";
            this.pbDescriptionLink.Size = new System.Drawing.Size(82, 13);
            this.pbDescriptionLink.TabIndex = 187;
            this.pbDescriptionLink.Text = "Description link:";
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(128, 24);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(435, 20);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.TextChanged += new System.EventHandler(this.tbTitle_TextChanged);
            // 
            // lbSeriesTitle
            // 
            this.lbSeriesTitle.AutoSize = true;
            this.lbSeriesTitle.Location = new System.Drawing.Point(23, 27);
            this.lbSeriesTitle.Name = "lbSeriesTitle";
            this.lbSeriesTitle.Size = new System.Drawing.Size(27, 13);
            this.lbSeriesTitle.TabIndex = 185;
            this.lbSeriesTitle.Text = "Title";
            // 
            // lbNotes
            // 
            this.lbNotes.AutoSize = true;
            this.lbNotes.Location = new System.Drawing.Point(23, 105);
            this.lbNotes.Name = "lbNotes";
            this.lbNotes.Size = new System.Drawing.Size(38, 13);
            this.lbNotes.TabIndex = 184;
            this.lbNotes.Text = "Notes:";
            // 
            // tbTrailer
            // 
            this.tbTrailer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTrailer.Location = new System.Drawing.Point(128, 325);
            this.tbTrailer.Name = "tbTrailer";
            this.tbTrailer.Size = new System.Drawing.Size(435, 20);
            this.tbTrailer.TabIndex = 5;
            // 
            // lbTrailer
            // 
            this.lbTrailer.AutoSize = true;
            this.lbTrailer.Location = new System.Drawing.Point(23, 327);
            this.lbTrailer.Name = "lbTrailer";
            this.lbTrailer.Size = new System.Drawing.Size(39, 13);
            this.lbTrailer.TabIndex = 195;
            this.lbTrailer.Text = "Trailer:";
            // 
            // ucEditSeriesBaseInfo
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTrailer);
            this.Controls.Add(this.tbTrailer);
            this.Controls.Add(this.pbCover);
            this.Controls.Add(this.tbNotes);
            this.Controls.Add(this.tbRecommendedLink);
            this.Controls.Add(this.tbRecommended);
            this.Controls.Add(this.lbRecommended);
            this.Controls.Add(this.tbDescriptionLink);
            this.Controls.Add(this.pbDescriptionLink);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.lbSeriesTitle);
            this.Controls.Add(this.lbNotes);
            this.Name = "ucEditSeriesBaseInfo";
            this.Size = new System.Drawing.Size(830, 364);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.UcEditSeriesBaseInfo_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.UcEditSeriesBaseInfo_DragOver);
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCover;
        private Utils.CustomTextBox tbNotes;
        private Utils.CustomTextBox tbRecommendedLink;
        private Utils.CustomTextBox tbRecommended;
        private System.Windows.Forms.Label lbRecommended;
        private Utils.CustomTextBox tbDescriptionLink;
        private System.Windows.Forms.Label pbDescriptionLink;
        private Utils.CustomTextBox tbTitle;
        private System.Windows.Forms.Label lbSeriesTitle;
        private System.Windows.Forms.Label lbNotes;
        private Utils.CustomTextBox tbTrailer;
        private System.Windows.Forms.Label lbTrailer;
    }
}
