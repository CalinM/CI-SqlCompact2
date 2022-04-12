
namespace Desene
{
    partial class FrmOptions
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnFolderSelectorMkvValidator = new System.Windows.Forms.Button();
            this.tbMkValidatorPath = new System.Windows.Forms.TextBox();
            this.lbMkValidatorPath = new System.Windows.Forms.Label();
            this.cbCheckMkvDuringFileDetails = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(526, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(446, 124);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnFolderSelectorMkvValidator
            // 
            this.btnFolderSelectorMkvValidator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderSelectorMkvValidator.Location = new System.Drawing.Point(548, 67);
            this.btnFolderSelectorMkvValidator.Name = "btnFolderSelectorMkvValidator";
            this.btnFolderSelectorMkvValidator.Size = new System.Drawing.Size(53, 20);
            this.btnFolderSelectorMkvValidator.TabIndex = 19;
            this.btnFolderSelectorMkvValidator.Text = "Folder";
            this.btnFolderSelectorMkvValidator.UseVisualStyleBackColor = true;
            this.btnFolderSelectorMkvValidator.Click += new System.EventHandler(this.btnFolderSelectorMkvValidator_Click);
            // 
            // tbMkValidatorPath
            // 
            this.tbMkValidatorPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMkValidatorPath.Location = new System.Drawing.Point(56, 67);
            this.tbMkValidatorPath.Name = "tbMkValidatorPath";
            this.tbMkValidatorPath.Size = new System.Drawing.Size(486, 20);
            this.tbMkValidatorPath.TabIndex = 18;
            this.tbMkValidatorPath.TextChanged += new System.EventHandler(this.tbMkValidatorPath_TextChanged);
            // 
            // lbMkValidatorPath
            // 
            this.lbMkValidatorPath.AutoSize = true;
            this.lbMkValidatorPath.Location = new System.Drawing.Point(53, 51);
            this.lbMkValidatorPath.Name = "lbMkValidatorPath";
            this.lbMkValidatorPath.Size = new System.Drawing.Size(132, 13);
            this.lbMkValidatorPath.TabIndex = 17;
            this.lbMkValidatorPath.Text = "MkvValidator.exe location:";
            // 
            // cbCheckMkvDuringFileDetails
            // 
            this.cbCheckMkvDuringFileDetails.AutoSize = true;
            this.cbCheckMkvDuringFileDetails.Location = new System.Drawing.Point(35, 30);
            this.cbCheckMkvDuringFileDetails.Name = "cbCheckMkvDuringFileDetails";
            this.cbCheckMkvDuringFileDetails.Size = new System.Drawing.Size(284, 17);
            this.cbCheckMkvDuringFileDetails.TabIndex = 22;
            this.cbCheckMkvDuringFileDetails.Text = "Check MKV containers during FileDetails determination";
            this.cbCheckMkvDuringFileDetails.UseVisualStyleBackColor = true;
            this.cbCheckMkvDuringFileDetails.CheckedChanged += new System.EventHandler(this.cbCheckMkvDuringFileDetails_CheckedChanged);
            // 
            // FrmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 168);
            this.Controls.Add(this.cbCheckMkvDuringFileDetails);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnFolderSelectorMkvValidator);
            this.Controls.Add(this.tbMkValidatorPath);
            this.Controls.Add(this.lbMkValidatorPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmOptions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnFolderSelectorMkvValidator;
        private System.Windows.Forms.TextBox tbMkValidatorPath;
        private System.Windows.Forms.Label lbMkValidatorPath;
        private System.Windows.Forms.CheckBox cbCheckMkvDuringFileDetails;
    }
}