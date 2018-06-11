namespace Desene.DetailFormsAndUserControls
{
    partial class ucSeriesEpisodes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pSeparator_Caption = new System.Windows.Forms.Panel();
            this.lbSeriesEpisodesCaption = new System.Windows.Forms.Label();
            this.dgvEpisodes = new System.Windows.Forms.DataGridView();
            this.colSeason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFIleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAutio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSeparator_Caption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).BeginInit();
            this.SuspendLayout();
            // 
            // pSeparator_Caption
            // 
            this.pSeparator_Caption.BackColor = System.Drawing.Color.DimGray;
            this.pSeparator_Caption.Controls.Add(this.lbSeriesEpisodesCaption);
            this.pSeparator_Caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSeparator_Caption.Location = new System.Drawing.Point(0, 0);
            this.pSeparator_Caption.Name = "pSeparator_Caption";
            this.pSeparator_Caption.Size = new System.Drawing.Size(826, 25);
            this.pSeparator_Caption.TabIndex = 0;
            // 
            // lbSeriesEpisodesCaption
            // 
            this.lbSeriesEpisodesCaption.AutoSize = true;
            this.lbSeriesEpisodesCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeriesEpisodesCaption.ForeColor = System.Drawing.Color.White;
            this.lbSeriesEpisodesCaption.Location = new System.Drawing.Point(9, 6);
            this.lbSeriesEpisodesCaption.Name = "lbSeriesEpisodesCaption";
            this.lbSeriesEpisodesCaption.Size = new System.Drawing.Size(58, 13);
            this.lbSeriesEpisodesCaption.TabIndex = 0;
            this.lbSeriesEpisodesCaption.Text = "Episodes";
            // 
            // dgvEpisodes
            // 
            this.dgvEpisodes.AllowUserToAddRows = false;
            this.dgvEpisodes.AllowUserToDeleteRows = false;
            this.dgvEpisodes.AllowUserToResizeRows = false;
            this.dgvEpisodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEpisodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEpisodes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEpisodes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEpisodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEpisodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSeason,
            this.colFIleName,
            this.colYear,
            this.colFileSize,
            this.colDuration,
            this.colQuality,
            this.colAutio});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEpisodes.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvEpisodes.EnableHeadersVisualStyles = false;
            this.dgvEpisodes.Location = new System.Drawing.Point(0, 31);
            this.dgvEpisodes.MultiSelect = false;
            this.dgvEpisodes.Name = "dgvEpisodes";
            this.dgvEpisodes.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEpisodes.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvEpisodes.RowHeadersVisible = false;
            this.dgvEpisodes.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvEpisodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEpisodes.Size = new System.Drawing.Size(795, 107);
            this.dgvEpisodes.TabIndex = 1;
            this.dgvEpisodes.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvEpisodes_DataBindingComplete);
            // 
            // colSeason
            // 
            this.colSeason.DataPropertyName = "Season";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSeason.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSeason.HeaderText = "Season";
            this.colSeason.Name = "colSeason";
            this.colSeason.ReadOnly = true;
            this.colSeason.Width = 50;
            // 
            // colFIleName
            // 
            this.colFIleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFIleName.DataPropertyName = "FileName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colFIleName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colFIleName.HeaderText = "File name";
            this.colFIleName.Name = "colFIleName";
            this.colFIleName.ReadOnly = true;
            // 
            // colYear
            // 
            this.colYear.DataPropertyName = "Year";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colYear.DefaultCellStyle = dataGridViewCellStyle4;
            this.colYear.HeaderText = "Year";
            this.colYear.Name = "colYear";
            this.colYear.ReadOnly = true;
            this.colYear.Width = 50;
            // 
            // colFileSize
            // 
            this.colFileSize.DataPropertyName = "FileSize2";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colFileSize.DefaultCellStyle = dataGridViewCellStyle5;
            this.colFileSize.HeaderText = "Size";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.ReadOnly = true;
            this.colFileSize.Width = 60;
            // 
            // colDuration
            // 
            this.colDuration.DataPropertyName = "Duration";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "HH:mm:ss";
            this.colDuration.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDuration.HeaderText = "Duration";
            this.colDuration.Name = "colDuration";
            this.colDuration.ReadOnly = true;
            this.colDuration.Width = 60;
            // 
            // colQuality
            // 
            this.colQuality.DataPropertyName = "Quality";
            this.colQuality.HeaderText = "Quality";
            this.colQuality.Name = "colQuality";
            this.colQuality.ReadOnly = true;
            this.colQuality.Width = 40;
            // 
            // colAutio
            // 
            this.colAutio.DataPropertyName = "AudioLanguages";
            this.colAutio.HeaderText = "Audio";
            this.colAutio.Name = "colAutio";
            this.colAutio.ReadOnly = true;
            this.colAutio.Width = 200;
            // 
            // ucSeriesEpisodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.dgvEpisodes);
            this.Controls.Add(this.pSeparator_Caption);
            this.Name = "ucSeriesEpisodes";
            this.Size = new System.Drawing.Size(826, 141);
            this.pSeparator_Caption.ResumeLayout(false);
            this.pSeparator_Caption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pSeparator_Caption;
        private System.Windows.Forms.Label lbSeriesEpisodesCaption;
        private System.Windows.Forms.DataGridView dgvEpisodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFIleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuality;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAutio;
    }
}
