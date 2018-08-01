namespace Utils
{
    partial class FrmImportErrors
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbWarningMessage = new System.Windows.Forms.Label();
            this.dgvErrors = new System.Windows.Forms.DataGridView();
            this.colFileAndError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbWarningMessage2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrors)).BeginInit();
            this.SuspendLayout();
            //
            // lbWarningMessage
            //
            this.lbWarningMessage.AutoSize = true;
            this.lbWarningMessage.Location = new System.Drawing.Point(12, 25);
            this.lbWarningMessage.Name = "lbWarningMessage";
            this.lbWarningMessage.Size = new System.Drawing.Size(324, 13);
            this.lbWarningMessage.TabIndex = 0;
            this.lbWarningMessage.Text = "The following errors occurred while determining the files details:";
            //
            // dgvErrors
            //
            this.dgvErrors.AllowUserToAddRows = false;
            this.dgvErrors.AllowUserToDeleteRows = false;
            this.dgvErrors.AllowUserToResizeColumns = false;
            this.dgvErrors.AllowUserToResizeRows = false;
            this.dgvErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvErrors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvErrors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvErrors.BackgroundColor = System.Drawing.Color.White;
            this.dgvErrors.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrors.ColumnHeadersVisible = false;
            this.dgvErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileAndError});
            this.dgvErrors.Location = new System.Drawing.Point(15, 46);
            this.dgvErrors.MultiSelect = false;
            this.dgvErrors.Name = "dgvErrors";
            this.dgvErrors.ReadOnly = true;
            this.dgvErrors.RowHeadersVisible = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvErrors.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvErrors.Size = new System.Drawing.Size(657, 197);
            this.dgvErrors.TabIndex = 1;
            //
            // colFileAndError
            //
            this.colFileAndError.HeaderText = "FileAndError";
            this.colFileAndError.Name = "colFileAndError";
            this.colFileAndError.ReadOnly = true;
            //
            // btnConfirm
            //
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(516, 259);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Continue";
            this.btnConfirm.UseVisualStyleBackColor = true;
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(597, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // lbWarningMessage2
            //
            this.lbWarningMessage2.AutoSize = true;
            this.lbWarningMessage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWarningMessage2.ForeColor = System.Drawing.Color.DarkRed;
            this.lbWarningMessage2.Location = new System.Drawing.Point(15, 263);
            this.lbWarningMessage2.Name = "lbWarningMessage2";
            this.lbWarningMessage2.Size = new System.Drawing.Size(455, 13);
            this.lbWarningMessage2.TabIndex = 4;
            this.lbWarningMessage2.Text = "Errors occured for all files in the selected folder. The process cannot continue!" +
    "";
            //
            // FrmImportErrors
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 294);
            this.Controls.Add(this.lbWarningMessage2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.dgvErrors);
            this.Controls.Add(this.lbWarningMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImportErrors";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FIles import errors summary";
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbWarningMessage;
        private System.Windows.Forms.DataGridView dgvErrors;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileAndError;
        private System.Windows.Forms.Label lbWarningMessage2;
    }
}