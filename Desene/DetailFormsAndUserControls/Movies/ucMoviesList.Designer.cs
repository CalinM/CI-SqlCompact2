using DataGridViewAutoFilter;

namespace Desene.DetailFormsAndUserControls.Movies
{
    partial class ucMoviesList
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
            this.dgvMoviesList = new System.Windows.Forms.DataGridView();
            this.lbSearch = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn8 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn9 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.tbFilter = new Utils.FilterTextBox();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colQuality = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colSize = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colDuration = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colAudio = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colSubtitles = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colRecommended = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colTheme = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoviesList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMoviesList
            // 
            this.dgvMoviesList.AllowUserToAddRows = false;
            this.dgvMoviesList.AllowUserToDeleteRows = false;
            this.dgvMoviesList.AllowUserToResizeRows = false;
            this.dgvMoviesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMoviesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMoviesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colFileName,
            this.colYear,
            this.colQuality,
            this.colSize,
            this.colDuration,
            this.colAudio,
            this.colSubtitles,
            this.colRecommended,
            this.colTheme});
            this.dgvMoviesList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMoviesList.Location = new System.Drawing.Point(42, 71);
            this.dgvMoviesList.MultiSelect = false;
            this.dgvMoviesList.Name = "dgvMoviesList";
            this.dgvMoviesList.RowHeadersVisible = false;
            this.dgvMoviesList.RowTemplate.Height = 28;
            this.dgvMoviesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMoviesList.Size = new System.Drawing.Size(1252, 589);
            this.dgvMoviesList.TabIndex = 113;
            this.dgvMoviesList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMoviesList_DataBindingComplete);
            // 
            // lbSearch
            // 
            this.lbSearch.AutoSize = true;
            this.lbSearch.Location = new System.Drawing.Point(38, 32);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(162, 20);
            this.lbSearch.TabIndex = 115;
            this.lbSearch.Text = "Search in all columns:";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 59;
            // 
            // dataGridViewAutoFilterTextBoxColumn1
            // 
            this.dataGridViewAutoFilterTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewAutoFilterTextBoxColumn1.DataPropertyName = "FN";
            this.dataGridViewAutoFilterTextBoxColumn1.HeaderText = "File name";
            this.dataGridViewAutoFilterTextBoxColumn1.Name = "dataGridViewAutoFilterTextBoxColumn1";
            // 
            // dataGridViewAutoFilterTextBoxColumn2
            // 
            this.dataGridViewAutoFilterTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewAutoFilterTextBoxColumn2.DataPropertyName = "Y";
            this.dataGridViewAutoFilterTextBoxColumn2.HeaderText = "Year";
            this.dataGridViewAutoFilterTextBoxColumn2.Name = "dataGridViewAutoFilterTextBoxColumn2";
            this.dataGridViewAutoFilterTextBoxColumn2.Width = 102;
            // 
            // dataGridViewAutoFilterTextBoxColumn3
            // 
            this.dataGridViewAutoFilterTextBoxColumn3.DataPropertyName = "Q";
            this.dataGridViewAutoFilterTextBoxColumn3.HeaderText = "Quality";
            this.dataGridViewAutoFilterTextBoxColumn3.Name = "dataGridViewAutoFilterTextBoxColumn3";
            // 
            // dataGridViewAutoFilterTextBoxColumn4
            // 
            this.dataGridViewAutoFilterTextBoxColumn4.DataPropertyName = "S";
            this.dataGridViewAutoFilterTextBoxColumn4.HeaderText = "Size";
            this.dataGridViewAutoFilterTextBoxColumn4.Name = "dataGridViewAutoFilterTextBoxColumn4";
            // 
            // dataGridViewAutoFilterTextBoxColumn5
            // 
            this.dataGridViewAutoFilterTextBoxColumn5.DataPropertyName = "L";
            this.dataGridViewAutoFilterTextBoxColumn5.HeaderText = "Duration";
            this.dataGridViewAutoFilterTextBoxColumn5.Name = "dataGridViewAutoFilterTextBoxColumn5";
            // 
            // dataGridViewAutoFilterTextBoxColumn6
            // 
            this.dataGridViewAutoFilterTextBoxColumn6.DataPropertyName = "A";
            this.dataGridViewAutoFilterTextBoxColumn6.HeaderText = "Audio";
            this.dataGridViewAutoFilterTextBoxColumn6.Name = "dataGridViewAutoFilterTextBoxColumn6";
            // 
            // dataGridViewAutoFilterTextBoxColumn7
            // 
            this.dataGridViewAutoFilterTextBoxColumn7.DataPropertyName = "SU";
            this.dataGridViewAutoFilterTextBoxColumn7.HeaderText = "Subtitles";
            this.dataGridViewAutoFilterTextBoxColumn7.Name = "dataGridViewAutoFilterTextBoxColumn7";
            // 
            // dataGridViewAutoFilterTextBoxColumn8
            // 
            this.dataGridViewAutoFilterTextBoxColumn8.DataPropertyName = "R";
            this.dataGridViewAutoFilterTextBoxColumn8.HeaderText = "Recommended";
            this.dataGridViewAutoFilterTextBoxColumn8.Name = "dataGridViewAutoFilterTextBoxColumn8";
            // 
            // dataGridViewAutoFilterTextBoxColumn9
            // 
            this.dataGridViewAutoFilterTextBoxColumn9.DataPropertyName = "T";
            this.dataGridViewAutoFilterTextBoxColumn9.HeaderText = "Theme";
            this.dataGridViewAutoFilterTextBoxColumn9.Name = "dataGridViewAutoFilterTextBoxColumn9";
            // 
            // tbFilter
            // 
            this.tbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbFilter.ForeColor = System.Drawing.Color.Silver;
            this.tbFilter.Location = new System.Drawing.Point(207, 29);
            this.tbFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(415, 26);
            this.tbFilter.TabIndex = 116;
            this.tbFilter.ButtonClick += new System.EventHandler(this.tbFilter_ButtonClick);
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // colId
            // 
            this.colId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colId.DataPropertyName = "Id";
            this.colId.Frozen = true;
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.Width = 59;
            // 
            // colFileName
            // 
            this.colFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFileName.DataPropertyName = "FN";
            this.colFileName.HeaderText = "File name";
            this.colFileName.Name = "colFileName";
            this.colFileName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colFileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colYear
            // 
            this.colYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colYear.DataPropertyName = "Y";
            this.colYear.HeaderText = "Year";
            this.colYear.Name = "colYear";
            this.colYear.Width = 102;
            // 
            // colQuality
            // 
            this.colQuality.DataPropertyName = "Q";
            this.colQuality.HeaderText = "Quality";
            this.colQuality.Name = "colQuality";
            // 
            // colSize
            // 
            this.colSize.DataPropertyName = "S";
            this.colSize.HeaderText = "Size";
            this.colSize.Name = "colSize";
            // 
            // colDuration
            // 
            this.colDuration.DataPropertyName = "L";
            this.colDuration.HeaderText = "Duration";
            this.colDuration.Name = "colDuration";
            // 
            // colAudio
            // 
            this.colAudio.DataPropertyName = "A";
            this.colAudio.HeaderText = "Audio";
            this.colAudio.Name = "colAudio";
            // 
            // colSubtitles
            // 
            this.colSubtitles.DataPropertyName = "SU";
            this.colSubtitles.HeaderText = "Subtitles";
            this.colSubtitles.Name = "colSubtitles";
            // 
            // colRecommended
            // 
            this.colRecommended.DataPropertyName = "R";
            this.colRecommended.HeaderText = "Recommended";
            this.colRecommended.Name = "colRecommended";
            // 
            // colTheme
            // 
            this.colTheme.DataPropertyName = "T";
            this.colTheme.HeaderText = "Theme";
            this.colTheme.Name = "colTheme";
            // 
            // ucMoviesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.lbSearch);
            this.Controls.Add(this.dgvMoviesList);
            this.Name = "ucMoviesList";
            this.Size = new System.Drawing.Size(1333, 685);
            this.Load += new System.EventHandler(this.ucMoviesList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoviesList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMoviesList;
        private System.Windows.Forms.Label lbSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn1;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn2;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn3;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn4;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn5;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn6;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn7;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn8;
        private DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn9;
        private Utils.FilterTextBox tbFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private DataGridViewAutoFilterTextBoxColumn colYear;
        private DataGridViewAutoFilterTextBoxColumn colQuality;
        private DataGridViewAutoFilterTextBoxColumn colSize;
        private DataGridViewAutoFilterTextBoxColumn colDuration;
        private DataGridViewAutoFilterTextBoxColumn colAudio;
        private DataGridViewAutoFilterTextBoxColumn colSubtitles;
        private DataGridViewAutoFilterTextBoxColumn colRecommended;
        private DataGridViewAutoFilterTextBoxColumn colTheme;
    }
}
