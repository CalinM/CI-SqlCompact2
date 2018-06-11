namespace Desene
{
    partial class ucMovies
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
            this.scMovies = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvMoviesList = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Calitate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pFilters = new System.Windows.Forms.Panel();
            this.lbFilter = new System.Windows.Forms.Label();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.ucFileDetail1 = new Desene.ucFileDetail();
            ((System.ComponentModel.ISupportInitialize)(this.scMovies)).BeginInit();
            this.scMovies.Panel1.SuspendLayout();
            this.scMovies.Panel2.SuspendLayout();
            this.scMovies.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoviesList)).BeginInit();
            this.pFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMovies
            // 
            this.scMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMovies.Location = new System.Drawing.Point(0, 0);
            this.scMovies.Name = "scMovies";
            // 
            // scMovies.Panel1
            // 
            this.scMovies.Panel1.Controls.Add(this.panel1);
            this.scMovies.Panel1.Controls.Add(this.pFilters);
            this.scMovies.Panel1MinSize = 250;
            // 
            // scMovies.Panel2
            // 
            this.scMovies.Panel2.AutoScroll = true;
            this.scMovies.Panel2.Controls.Add(this.ucFileDetail1);
            this.scMovies.Size = new System.Drawing.Size(977, 724);
            this.scMovies.SplitterDistance = 250;
            this.scMovies.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvMoviesList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 684);
            this.panel1.TabIndex = 105;
            // 
            // dgvMoviesList
            // 
            this.dgvMoviesList.AllowUserToAddRows = false;
            this.dgvMoviesList.AllowUserToDeleteRows = false;
            this.dgvMoviesList.AllowUserToOrderColumns = true;
            this.dgvMoviesList.AllowUserToResizeColumns = false;
            this.dgvMoviesList.AllowUserToResizeRows = false;
            this.dgvMoviesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMoviesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMoviesList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMoviesList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMoviesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMoviesList.ColumnHeadersVisible = false;
            this.dgvMoviesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colTitle,
            this.Calitate});
            this.dgvMoviesList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvMoviesList.Location = new System.Drawing.Point(15, 0);
            this.dgvMoviesList.MultiSelect = false;
            this.dgvMoviesList.Name = "dgvMoviesList";
            this.dgvMoviesList.ReadOnly = true;
            this.dgvMoviesList.RowHeadersVisible = false;
            this.dgvMoviesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMoviesList.Size = new System.Drawing.Size(222, 684);
            this.dgvMoviesList.TabIndex = 105;
            this.dgvMoviesList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvMoviesList_CellPainting);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colTitle
            // 
            this.colTitle.DataPropertyName = "FileName";
            this.colTitle.FillWeight = 179.6954F;
            this.colTitle.HeaderText = "FileName";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            // 
            // Calitate
            // 
            this.Calitate.DataPropertyName = "Quality";
            this.Calitate.FillWeight = 20.30457F;
            this.Calitate.HeaderText = "Quality";
            this.Calitate.Name = "Calitate";
            this.Calitate.ReadOnly = true;
            this.Calitate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // pFilters
            // 
            this.pFilters.Controls.Add(this.lbFilter);
            this.pFilters.Controls.Add(this.tbFilter);
            this.pFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilters.Location = new System.Drawing.Point(0, 0);
            this.pFilters.Name = "pFilters";
            this.pFilters.Size = new System.Drawing.Size(250, 40);
            this.pFilters.TabIndex = 104;
            // 
            // lbFilter
            // 
            this.lbFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lbFilter.AutoSize = true;
            this.lbFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbFilter.Location = new System.Drawing.Point(12, 12);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(55, 13);
            this.lbFilter.TabIndex = 15;
            this.lbFilter.Text = "Filter by ▼";
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(76, 9);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(161, 20);
            this.tbFilter.TabIndex = 14;
            // 
            // ucFileDetail1
            // 
            this.ucFileDetail1.AutoScroll = true;
            this.ucFileDetail1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFileDetail1.Location = new System.Drawing.Point(0, 0);
            this.ucFileDetail1.Name = "ucFileDetail1";
            this.ucFileDetail1.Size = new System.Drawing.Size(723, 605);
            this.ucFileDetail1.TabIndex = 0;
            // 
            // ucMovies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMovies);
            this.Name = "ucMovies";
            this.Size = new System.Drawing.Size(977, 724);
            this.Load += new System.EventHandler(this.ucMovies_Load);
            this.scMovies.Panel1.ResumeLayout(false);
            this.scMovies.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMovies)).EndInit();
            this.scMovies.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoviesList)).EndInit();
            this.pFilters.ResumeLayout(false);
            this.pFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMovies;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvMoviesList;
        private System.Windows.Forms.Panel pFilters;
        private System.Windows.Forms.Label lbFilter;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Calitate;
        private ucFileDetail ucFileDetail1;
    }
}
