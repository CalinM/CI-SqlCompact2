namespace Desene.DetailFormsAndUserControls.Episodes
{
    partial class ucGenericStreamsWrapper
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
            this.flpWrapper = new Utils.CustomFLP();
            this.SuspendLayout();
            // 
            // flpWrapper
            // 
            this.flpWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpWrapper.AutoSize = true;
            this.flpWrapper.Location = new System.Drawing.Point(0, 3);
            this.flpWrapper.Name = "flpWrapper";
            this.flpWrapper.Size = new System.Drawing.Size(811, 154);
            this.flpWrapper.TabIndex = 0;
            // 
            // ucGenericStreamsWrapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.flpWrapper);
            this.DoubleBuffered = true;
            this.Name = "ucGenericStreamsWrapper";
            this.Size = new System.Drawing.Size(814, 173);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Utils.CustomFLP flpWrapper;
    }
}
