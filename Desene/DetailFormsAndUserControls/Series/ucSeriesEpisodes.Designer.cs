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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pSeparator_Caption = new System.Windows.Forms.Panel();
            this.ftbFilterEpisodes = new Utils.FilterTextBox();
            this.lbSeriesEpisodesCaption = new System.Windows.Forms.Label();
            this.lbNoEpisodeWarning = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pEdpisodesGridWrapper = new System.Windows.Forms.Panel();
            this.dgvEpisodes = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFIleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBitRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFrameRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAutio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSeparator_Caption.SuspendLayout();
            this.pEdpisodesGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).BeginInit();
            this.SuspendLayout();
            // 
            // pSeparator_Caption
            // 
            this.pSeparator_Caption.BackColor = System.Drawing.Color.DimGray;
            this.pSeparator_Caption.Controls.Add(this.ftbFilterEpisodes);
            this.pSeparator_Caption.Controls.Add(this.lbSeriesEpisodesCaption);
            this.pSeparator_Caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSeparator_Caption.Location = new System.Drawing.Point(0, 0);
            this.pSeparator_Caption.Name = "pSeparator_Caption";
            this.pSeparator_Caption.Size = new System.Drawing.Size(826, 25);
            this.pSeparator_Caption.TabIndex = 0;
            // 
            // ftbFilterEpisodes
            // 
            this.ftbFilterEpisodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ftbFilterEpisodes.ForeColor = System.Drawing.Color.Silver;
            this.ftbFilterEpisodes.Location = new System.Drawing.Point(587, 3);
            this.ftbFilterEpisodes.Name = "ftbFilterEpisodes";
            this.ftbFilterEpisodes.Size = new System.Drawing.Size(209, 20);
            this.ftbFilterEpisodes.TabIndex = 2;
            this.ftbFilterEpisodes.ButtonClick += new System.EventHandler(this.ftbFilterEpisodes_ButtonClick);
            this.ftbFilterEpisodes.TextChanged += new System.EventHandler(this.ftbFilterEpisodes_TextChanged);
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
            // lbNoEpisodeWarning
            // 
            this.lbNoEpisodeWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbNoEpisodeWarning.ForeColor = System.Drawing.Color.Gray;
            this.lbNoEpisodeWarning.Location = new System.Drawing.Point(0, 25);
            this.lbNoEpisodeWarning.Name = "lbNoEpisodeWarning";
            this.lbNoEpisodeWarning.Size = new System.Drawing.Size(826, 19);
            this.lbNoEpisodeWarning.TabIndex = 2;
            this.lbNoEpisodeWarning.Text = "The selected series doesn\'t have episodes added!";
            this.lbNoEpisodeWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbNoEpisodeWarning.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Season";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Season";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "FileName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "File name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Year";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn4.HeaderText = "Year";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "FileSize2";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.HeaderText = "Size";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Duration";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "HH:mm:ss";
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn6.HeaderText = "Duration";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Quality";
            this.dataGridViewTextBoxColumn7.HeaderText = "Quality";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 40;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "AudioLanguages";
            this.dataGridViewTextBoxColumn8.HeaderText = "Audio";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 200;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "AudioLanguages";
            this.dataGridViewTextBoxColumn9.HeaderText = "Audio";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 200;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "AudioLanguages";
            this.dataGridViewTextBoxColumn10.HeaderText = "Audio";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 200;
            // 
            // pEdpisodesGridWrapper
            // 
            this.pEdpisodesGridWrapper.Controls.Add(this.dgvEpisodes);
            this.pEdpisodesGridWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pEdpisodesGridWrapper.Location = new System.Drawing.Point(0, 44);
            this.pEdpisodesGridWrapper.Name = "pEdpisodesGridWrapper";
            this.pEdpisodesGridWrapper.Padding = new System.Windows.Forms.Padding(0, 5, 30, 10);
            this.pEdpisodesGridWrapper.Size = new System.Drawing.Size(826, 302);
            this.pEdpisodesGridWrapper.TabIndex = 4;
            // 
            // dgvEpisodes
            // 
            this.dgvEpisodes.AllowUserToAddRows = false;
            this.dgvEpisodes.AllowUserToDeleteRows = false;
            this.dgvEpisodes.AllowUserToResizeRows = false;
            this.dgvEpisodes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvEpisodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEpisodes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEpisodes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvEpisodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEpisodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.colSeason,
            this.colFIleName,
            this.colYear,
            this.colFileSize,
            this.colDuration,
            this.colQuality,
            this.colBitRate,
            this.colFrameRate,
            this.colAutio});
            this.dgvEpisodes.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEpisodes.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvEpisodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEpisodes.EnableHeadersVisualStyles = false;
            this.dgvEpisodes.Location = new System.Drawing.Point(0, 5);
            this.dgvEpisodes.MultiSelect = false;
            this.dgvEpisodes.Name = "dgvEpisodes";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEpisodes.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvEpisodes.RowHeadersVisible = false;
            this.dgvEpisodes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvEpisodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEpisodes.Size = new System.Drawing.Size(796, 287);
            this.dgvEpisodes.TabIndex = 2;
            this.dgvEpisodes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEpisodes_CellClick);
            this.dgvEpisodes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEpisodes_CellDoubleClick);
            this.dgvEpisodes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvEpisodes_CellValueChanged);
            this.dgvEpisodes.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DgvEpisodes_CellValueNeeded);
            this.dgvEpisodes.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DgvEpisodes_CellValuePushed);
            this.dgvEpisodes.CurrentCellDirtyStateChanged += new System.EventHandler(this.DgvEpisodes_CurrentCellDirtyStateChanged);
            this.dgvEpisodes.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvEpisodes_DataBindingComplete);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.FillWeight = 50F;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 50;
            // 
            // colSeason
            // 
            this.colSeason.DataPropertyName = "Season";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSeason.DefaultCellStyle = dataGridViewCellStyle7;
            this.colSeason.HeaderText = "Season";
            this.colSeason.Name = "colSeason";
            this.colSeason.ReadOnly = true;
            this.colSeason.Width = 50;
            // 
            // colFIleName
            // 
            this.colFIleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFIleName.DataPropertyName = "FileName";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colFIleName.DefaultCellStyle = dataGridViewCellStyle8;
            this.colFIleName.HeaderText = "File name";
            this.colFIleName.Name = "colFIleName";
            this.colFIleName.ReadOnly = true;
            // 
            // colYear
            // 
            this.colYear.DataPropertyName = "Year";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colYear.DefaultCellStyle = dataGridViewCellStyle9;
            this.colYear.HeaderText = "Year";
            this.colYear.Name = "colYear";
            this.colYear.ReadOnly = true;
            this.colYear.Width = 50;
            // 
            // colFileSize
            // 
            this.colFileSize.DataPropertyName = "FileSize2";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colFileSize.DefaultCellStyle = dataGridViewCellStyle10;
            this.colFileSize.HeaderText = "Size";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.ReadOnly = true;
            this.colFileSize.Width = 60;
            // 
            // colDuration
            // 
            this.colDuration.DataPropertyName = "Duration";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "HH:mm:ss";
            this.colDuration.DefaultCellStyle = dataGridViewCellStyle11;
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
            this.colQuality.Width = 45;
            // 
            // colBitRate
            // 
            this.colBitRate.DataPropertyName = "BitRate";
            this.colBitRate.HeaderText = "BitRate";
            this.colBitRate.Name = "colBitRate";
            this.colBitRate.ReadOnly = true;
            this.colBitRate.Width = 70;
            // 
            // colFrameRate
            // 
            this.colFrameRate.DataPropertyName = "FrameRate";
            this.colFrameRate.HeaderText = "FrameRate";
            this.colFrameRate.Name = "colFrameRate";
            this.colFrameRate.ReadOnly = true;
            this.colFrameRate.Width = 75;
            // 
            // colAutio
            // 
            this.colAutio.DataPropertyName = "AudioLanguages";
            this.colAutio.HeaderText = "Audio";
            this.colAutio.Name = "colAutio";
            this.colAutio.ReadOnly = true;
            this.colAutio.Width = 70;
            // 
            // ucSeriesEpisodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pEdpisodesGridWrapper);
            this.Controls.Add(this.lbNoEpisodeWarning);
            this.Controls.Add(this.pSeparator_Caption);
            this.Name = "ucSeriesEpisodes";
            this.Size = new System.Drawing.Size(826, 346);
            this.pSeparator_Caption.ResumeLayout(false);
            this.pSeparator_Caption.PerformLayout();
            this.pEdpisodesGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pSeparator_Caption;
        private System.Windows.Forms.Label lbSeriesEpisodesCaption;
        private System.Windows.Forms.Label lbNoEpisodeWarning;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private Utils.FilterTextBox ftbFilterEpisodes;
        private System.Windows.Forms.Panel pEdpisodesGridWrapper;
        private System.Windows.Forms.DataGridView dgvEpisodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFIleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuality;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBitRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrameRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAutio;
    }
}
