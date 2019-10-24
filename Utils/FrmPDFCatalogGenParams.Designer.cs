namespace Utils
{
    partial class FrmPDFCatalogGenParams
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
            this.btnFolderSelector = new System.Windows.Forms.Button();
            this.tbFilesLocation = new System.Windows.Forms.TextBox();
            this.lbLocation = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbHelloween = new System.Windows.Forms.RadioButton();
            this.rbChristmas = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbSeries = new System.Windows.Forms.RadioButton();
            this.rbMovies = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFolderSelector
            // 
            this.btnFolderSelector.Location = new System.Drawing.Point(617, 25);
            this.btnFolderSelector.Name = "btnFolderSelector";
            this.btnFolderSelector.Size = new System.Drawing.Size(68, 20);
            this.btnFolderSelector.TabIndex = 14;
            this.btnFolderSelector.Text = "Save as ...";
            this.btnFolderSelector.UseVisualStyleBackColor = true;
            this.btnFolderSelector.Click += new System.EventHandler(this.btnFolderSelector_Click);
            // 
            // tbFilesLocation
            // 
            this.tbFilesLocation.Location = new System.Drawing.Point(123, 25);
            this.tbFilesLocation.Name = "tbFilesLocation";
            this.tbFilesLocation.Size = new System.Drawing.Size(487, 20);
            this.tbFilesLocation.TabIndex = 13;
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(21, 27);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(48, 13);
            this.lbLocation.TabIndex = 12;
            this.lbLocation.Text = "Location";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(610, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(530, 122);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbHelloween);
            this.panel1.Controls.Add(this.rbChristmas);
            this.panel1.Controls.Add(this.rbAll);
            this.panel1.Location = new System.Drawing.Point(214, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(84, 72);
            this.panel1.TabIndex = 22;
            // 
            // rbHelloween
            // 
            this.rbHelloween.AutoSize = true;
            this.rbHelloween.Location = new System.Drawing.Point(3, 49);
            this.rbHelloween.Name = "rbHelloween";
            this.rbHelloween.Size = new System.Drawing.Size(75, 17);
            this.rbHelloween.TabIndex = 20;
            this.rbHelloween.Text = "Helloween";
            this.rbHelloween.UseVisualStyleBackColor = true;
            // 
            // rbChristmas
            // 
            this.rbChristmas.AutoSize = true;
            this.rbChristmas.Location = new System.Drawing.Point(3, 26);
            this.rbChristmas.Name = "rbChristmas";
            this.rbChristmas.Size = new System.Drawing.Size(70, 17);
            this.rbChristmas.TabIndex = 19;
            this.rbChristmas.Text = "Christmas";
            this.rbChristmas.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(3, 3);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 18;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbSeries);
            this.panel2.Controls.Add(this.rbMovies);
            this.panel2.Location = new System.Drawing.Point(123, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(85, 72);
            this.panel2.TabIndex = 23;
            // 
            // rbSeries
            // 
            this.rbSeries.AutoSize = true;
            this.rbSeries.Location = new System.Drawing.Point(3, 26);
            this.rbSeries.Name = "rbSeries";
            this.rbSeries.Size = new System.Drawing.Size(54, 17);
            this.rbSeries.TabIndex = 23;
            this.rbSeries.Text = "Series";
            this.rbSeries.UseVisualStyleBackColor = true;
            this.rbSeries.CheckedChanged += new System.EventHandler(this.rbSeries_CheckedChanged);
            // 
            // rbMovies
            // 
            this.rbMovies.AutoSize = true;
            this.rbMovies.Checked = true;
            this.rbMovies.Location = new System.Drawing.Point(3, 3);
            this.rbMovies.Name = "rbMovies";
            this.rbMovies.Size = new System.Drawing.Size(59, 17);
            this.rbMovies.TabIndex = 22;
            this.rbMovies.Text = "Movies";
            this.rbMovies.UseVisualStyleBackColor = true;
            this.rbMovies.CheckedChanged += new System.EventHandler(this.rbMovies_CheckedChanged);
            // 
            // FrmPDFCatalogGenParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(713, 157);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnFolderSelector);
            this.Controls.Add(this.tbFilesLocation);
            this.Controls.Add(this.lbLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPDFCatalogGenParams";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PDF Catalog generation parameters";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFolderSelector;
        private System.Windows.Forms.TextBox tbFilesLocation;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbHelloween;
        private System.Windows.Forms.RadioButton rbChristmas;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbSeries;
        private System.Windows.Forms.RadioButton rbMovies;
    }
}