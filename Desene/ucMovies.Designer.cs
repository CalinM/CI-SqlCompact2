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
            this.scSeriesDetails = new System.Windows.Forms.SplitContainer();
            this.pSeriesSecondToolbar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefreshMovieData = new System.Windows.Forms.ToolStripButton();
            this.btnLoadPoster = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pMovieDetailsContainer = new System.Windows.Forms.Panel();
            this.pDummyMenuForShortCutKeys = new System.Windows.Forms.Panel();
            this.lbDoNotDelete = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miSave_ForShortCutOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.miUndo_ForShortCutOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.tbFilter = new Utils.FilterTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.scMovies)).BeginInit();
            this.scMovies.Panel1.SuspendLayout();
            this.scMovies.Panel2.SuspendLayout();
            this.scMovies.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoviesList)).BeginInit();
            this.pFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSeriesDetails)).BeginInit();
            this.scSeriesDetails.Panel1.SuspendLayout();
            this.scSeriesDetails.Panel2.SuspendLayout();
            this.scSeriesDetails.SuspendLayout();
            this.pSeriesSecondToolbar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pDummyMenuForShortCutKeys.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.scMovies.Panel1MinSize = 300;
            // 
            // scMovies.Panel2
            // 
            this.scMovies.Panel2.AutoScroll = true;
            this.scMovies.Panel2.Controls.Add(this.scSeriesDetails);
            this.scMovies.Size = new System.Drawing.Size(977, 724);
            this.scMovies.SplitterDistance = 300;
            this.scMovies.TabIndex = 0;
            this.scMovies.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scMovies_MouseDown);
            this.scMovies.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scMovies_MouseUp);
            this.scMovies.Resize += new System.EventHandler(this.scMovies_Resize);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pDummyMenuForShortCutKeys);
            this.panel1.Controls.Add(this.dgvMoviesList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 684);
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
            this.dgvMoviesList.Size = new System.Drawing.Size(285, 684);
            this.dgvMoviesList.TabIndex = 105;
            this.dgvMoviesList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvMoviesList_CellPainting);
            this.dgvMoviesList.SelectionChanged += new System.EventHandler(this.dgvMoviesList_SelectionChanged);
            this.dgvMoviesList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvMoviesList_KeyPress);
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
            this.pFilters.Controls.Add(this.tbFilter);
            this.pFilters.Controls.Add(this.lbFilter);
            this.pFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilters.Location = new System.Drawing.Point(0, 0);
            this.pFilters.Name = "pFilters";
            this.pFilters.Size = new System.Drawing.Size(300, 40);
            this.pFilters.TabIndex = 104;
            // 
            // lbFilter
            // 
            this.lbFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lbFilter.AutoSize = true;
            this.lbFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbFilter.Location = new System.Drawing.Point(12, 12);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(32, 13);
            this.lbFilter.TabIndex = 15;
            this.lbFilter.Text = "Filter:";
            // 
            // scSeriesDetails
            // 
            this.scSeriesDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSeriesDetails.IsSplitterFixed = true;
            this.scSeriesDetails.Location = new System.Drawing.Point(0, 0);
            this.scSeriesDetails.Name = "scSeriesDetails";
            // 
            // scSeriesDetails.Panel1
            // 
            this.scSeriesDetails.Panel1.Controls.Add(this.pSeriesSecondToolbar);
            // 
            // scSeriesDetails.Panel2
            // 
            this.scSeriesDetails.Panel2.Controls.Add(this.pMovieDetailsContainer);
            this.scSeriesDetails.Size = new System.Drawing.Size(673, 724);
            this.scSeriesDetails.SplitterDistance = 25;
            this.scSeriesDetails.TabIndex = 1;
            // 
            // pSeriesSecondToolbar
            // 
            this.pSeriesSecondToolbar.Controls.Add(this.panel3);
            this.pSeriesSecondToolbar.Controls.Add(this.panel2);
            this.pSeriesSecondToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSeriesSecondToolbar.Location = new System.Drawing.Point(0, 0);
            this.pSeriesSecondToolbar.Name = "pSeriesSecondToolbar";
            this.pSeriesSecondToolbar.Size = new System.Drawing.Size(25, 724);
            this.pSeriesSecondToolbar.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(25, 684);
            this.panel3.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshMovieData,
            this.btnLoadPoster,
            this.toolStripSeparator1,
            this.btnSaveChanges,
            this.btnUndo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 684);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefreshMovieData
            // 
            this.btnRefreshMovieData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshMovieData.Enabled = false;
            this.btnRefreshMovieData.Image = global::Desene.Properties.Resources.refresh;
            this.btnRefreshMovieData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshMovieData.Name = "btnRefreshMovieData";
            this.btnRefreshMovieData.Size = new System.Drawing.Size(21, 20);
            this.btnRefreshMovieData.Text = "Refresh movie details from file";
            this.btnRefreshMovieData.Click += new System.EventHandler(this.btnRefreshMovieData_Click);
            // 
            // btnLoadPoster
            // 
            this.btnLoadPoster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadPoster.Enabled = false;
            this.btnLoadPoster.Image = global::Desene.Properties.Resources.image;
            this.btnLoadPoster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadPoster.Margin = new System.Windows.Forms.Padding(0, 5, 0, 2);
            this.btnLoadPoster.Name = "btnLoadPoster";
            this.btnLoadPoster.Size = new System.Drawing.Size(21, 20);
            this.btnLoadPoster.Text = "Load Movie poster";
            this.btnLoadPoster.Click += new System.EventHandler(this.btnLoadPoster_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(21, 6);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.Image = global::Desene.Properties.Resources.save;
            this.btnSaveChanges.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(21, 20);
            this.btnSaveChanges.Text = "Save changes";
            this.btnSaveChanges.ToolTipText = "Save changes\r\nCTRL+S";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Enabled = false;
            this.btnUndo.Image = global::Desene.Properties.Resources.undo;
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(21, 20);
            this.btnUndo.Text = "Undo";
            this.btnUndo.ToolTipText = "Undo changes\r\nCTRL+Z";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(25, 40);
            this.panel2.TabIndex = 0;
            // 
            // pMovieDetailsContainer
            // 
            this.pMovieDetailsContainer.AutoScroll = true;
            this.pMovieDetailsContainer.AutoSize = true;
            this.pMovieDetailsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pMovieDetailsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMovieDetailsContainer.Location = new System.Drawing.Point(0, 0);
            this.pMovieDetailsContainer.Name = "pMovieDetailsContainer";
            this.pMovieDetailsContainer.Size = new System.Drawing.Size(644, 724);
            this.pMovieDetailsContainer.TabIndex = 0;
            // 
            // pDummyMenuForShortCutKeys
            // 
            this.pDummyMenuForShortCutKeys.Controls.Add(this.lbDoNotDelete);
            this.pDummyMenuForShortCutKeys.Controls.Add(this.menuStrip1);
            this.pDummyMenuForShortCutKeys.Location = new System.Drawing.Point(58, 295);
            this.pDummyMenuForShortCutKeys.Name = "pDummyMenuForShortCutKeys";
            this.pDummyMenuForShortCutKeys.Size = new System.Drawing.Size(185, 94);
            this.pDummyMenuForShortCutKeys.TabIndex = 120;
            // 
            // lbDoNotDelete
            // 
            this.lbDoNotDelete.AutoSize = true;
            this.lbDoNotDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDoNotDelete.ForeColor = System.Drawing.Color.Red;
            this.lbDoNotDelete.Location = new System.Drawing.Point(15, 57);
            this.lbDoNotDelete.Name = "lbDoNotDelete";
            this.lbDoNotDelete.Size = new System.Drawing.Size(153, 13);
            this.lbDoNotDelete.TabIndex = 1;
            this.lbDoNotDelete.Text = "Do Not Delete (design) !!!";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSave_ForShortCutOnly,
            this.miUndo_ForShortCutOnly});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(185, 46);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miSave_ForShortCutOnly
            // 
            this.miSave_ForShortCutOnly.Image = global::Desene.Properties.Resources.save;
            this.miSave_ForShortCutOnly.Name = "miSave_ForShortCutOnly";
            this.miSave_ForShortCutOnly.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave_ForShortCutOnly.Size = new System.Drawing.Size(178, 20);
            this.miSave_ForShortCutOnly.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // miUndo_ForShortCutOnly
            // 
            this.miUndo_ForShortCutOnly.Image = global::Desene.Properties.Resources.undo;
            this.miUndo_ForShortCutOnly.Name = "miUndo_ForShortCutOnly";
            this.miUndo_ForShortCutOnly.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.miUndo_ForShortCutOnly.Size = new System.Drawing.Size(178, 20);
            this.miUndo_ForShortCutOnly.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbFilter.ForeColor = System.Drawing.Color.Silver;
            this.tbFilter.Location = new System.Drawing.Point(47, 9);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(253, 20);
            this.tbFilter.TabIndex = 17;
            this.tbFilter.ButtonClick += new System.EventHandler(this.tbFilter_ButtonClick);
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
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
            this.scSeriesDetails.Panel1.ResumeLayout(false);
            this.scSeriesDetails.Panel2.ResumeLayout(false);
            this.scSeriesDetails.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSeriesDetails)).EndInit();
            this.scSeriesDetails.ResumeLayout(false);
            this.pSeriesSecondToolbar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pDummyMenuForShortCutKeys.ResumeLayout(false);
            this.pDummyMenuForShortCutKeys.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMovies;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvMoviesList;
        private System.Windows.Forms.Panel pFilters;
        private System.Windows.Forms.Label lbFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Calitate;
        private System.Windows.Forms.SplitContainer scSeriesDetails;
        private System.Windows.Forms.Panel pSeriesSecondToolbar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLoadPoster;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSaveChanges;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRefreshMovieData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pMovieDetailsContainer;
        private Utils.FilterTextBox tbFilter;
        private System.Windows.Forms.Panel pDummyMenuForShortCutKeys;
        private System.Windows.Forms.Label lbDoNotDelete;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSave_ForShortCutOnly;
        private System.Windows.Forms.ToolStripMenuItem miUndo_ForShortCutOnly;
    }
}
