namespace Desene.DetailFormsAndUserControls.Series
{
    partial class ucBulkEditFieldValue
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
            this.lbField = new System.Windows.Forms.Label();
            this.cbFields = new System.Windows.Forms.ComboBox();
            this.lbEqual = new System.Windows.Forms.Label();
            this.lbNewValue = new System.Windows.Forms.Label();
            this.tbNewValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbField
            // 
            this.lbField.AutoSize = true;
            this.lbField.Location = new System.Drawing.Point(57, 6);
            this.lbField.Name = "lbField";
            this.lbField.Size = new System.Drawing.Size(29, 13);
            this.lbField.TabIndex = 0;
            this.lbField.Text = "Field";
            // 
            // cbFields
            // 
            this.cbFields.FormattingEnabled = true;
            this.cbFields.Location = new System.Drawing.Point(60, 23);
            this.cbFields.Name = "cbFields";
            this.cbFields.Size = new System.Drawing.Size(159, 21);
            this.cbFields.TabIndex = 1;
            // 
            // lbEqual
            // 
            this.lbEqual.AutoSize = true;
            this.lbEqual.Location = new System.Drawing.Point(225, 26);
            this.lbEqual.Name = "lbEqual";
            this.lbEqual.Size = new System.Drawing.Size(13, 13);
            this.lbEqual.TabIndex = 2;
            this.lbEqual.Text = "=";
            // 
            // lbNewValue
            // 
            this.lbNewValue.AutoSize = true;
            this.lbNewValue.Location = new System.Drawing.Point(241, 6);
            this.lbNewValue.Name = "lbNewValue";
            this.lbNewValue.Size = new System.Drawing.Size(58, 13);
            this.lbNewValue.TabIndex = 3;
            this.lbNewValue.Text = "New value";
            // 
            // tbNewValue
            // 
            this.tbNewValue.Location = new System.Drawing.Point(244, 24);
            this.tbNewValue.Name = "tbNewValue";
            this.tbNewValue.Size = new System.Drawing.Size(195, 20);
            this.tbNewValue.TabIndex = 4;
            this.tbNewValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNewValue_KeyDown);
            // 
            // ucBulkEditFieldValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbNewValue);
            this.Controls.Add(this.lbNewValue);
            this.Controls.Add(this.lbEqual);
            this.Controls.Add(this.cbFields);
            this.Controls.Add(this.lbField);
            this.Name = "ucBulkEditFieldValue";
            this.Size = new System.Drawing.Size(460, 52);
            this.Load += new System.EventHandler(this.ucBulkEditFieldValue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbField;
        private System.Windows.Forms.ComboBox cbFields;
        private System.Windows.Forms.Label lbEqual;
        private System.Windows.Forms.Label lbNewValue;
        private System.Windows.Forms.TextBox tbNewValue;
    }
}
