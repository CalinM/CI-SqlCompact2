namespace Desene.DetailFormsAndUserControls.Shared
{
    partial class FrmALotOrALittle_Explanation
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
            this.lbExplanation = new System.Windows.Forms.Label();
            this.pbCategory = new System.Windows.Forms.PictureBox();
            this.pbRating = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbSectionTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRating)).BeginInit();
            this.SuspendLayout();
            // 
            // lbExplanation
            // 
            this.lbExplanation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExplanation.Location = new System.Drawing.Point(12, 95);
            this.lbExplanation.Name = "lbExplanation";
            this.lbExplanation.Size = new System.Drawing.Size(431, 200);
            this.lbExplanation.TabIndex = 0;
            this.lbExplanation.Text = "label1";
            // 
            // pbCategory
            // 
            this.pbCategory.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbCategory.Image = global::Desene.Properties.Resources.educational_value;
            this.pbCategory.InitialImage = null;
            this.pbCategory.Location = new System.Drawing.Point(15, 24);
            this.pbCategory.Name = "pbCategory";
            this.pbCategory.Size = new System.Drawing.Size(20, 20);
            this.pbCategory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCategory.TabIndex = 10;
            this.pbCategory.TabStop = false;
            // 
            // pbRating
            // 
            this.pbRating.BackColor = System.Drawing.Color.White;
            this.pbRating.Location = new System.Drawing.Point(41, 18);
            this.pbRating.Name = "pbRating";
            this.pbRating.Size = new System.Drawing.Size(115, 26);
            this.pbRating.TabIndex = 19;
            this.pbRating.TabStop = false;
            this.pbRating.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(41, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "not present";
            // 
            // lbSectionTitle
            // 
            this.lbSectionTitle.AutoSize = true;
            this.lbSectionTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSectionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbSectionTitle.Location = new System.Drawing.Point(12, 64);
            this.lbSectionTitle.Name = "lbSectionTitle";
            this.lbSectionTitle.Size = new System.Drawing.Size(51, 20);
            this.lbSectionTitle.TabIndex = 21;
            this.lbSectionTitle.Text = "label2";
            // 
            // FrmALotOrALittle_Explanation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(455, 304);
            this.Controls.Add(this.lbSectionTitle);
            this.Controls.Add(this.pbRating);
            this.Controls.Add(this.pbCategory);
            this.Controls.Add(this.lbExplanation);
            this.Controls.Add(this.label5);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmALotOrALittle_Explanation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Explanation";
            ((System.ComponentModel.ISupportInitialize)(this.pbCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbExplanation;
        private System.Windows.Forms.PictureBox pbCategory;
        private System.Windows.Forms.PictureBox pbRating;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbSectionTitle;
    }
}