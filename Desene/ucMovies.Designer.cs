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
            this.components = new System.ComponentModel.Container();
            this.scMovies = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pDummyMenuForShortCutKeys = new System.Windows.Forms.Panel();
            this.lbDoNotDelete = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miSave_ForShortCutOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.miUndo_ForShortCutOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMoviesList = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Calitate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pAdvFilter = new System.Windows.Forms.Panel();
            this.gbAdvFilterWrapper = new System.Windows.Forms.GroupBox();
            this.chkIncludeUnknownRec = new System.Windows.Forms.CheckBox();
            this.cbAdvFilter_Theme = new Utils.SeparatorComboBox();
            this.lbAdvFilterReset = new System.Windows.Forms.Label();
            this.cbAdvFilter_Audio = new Utils.SeparatorComboBox();
            this.lbSwitchToSimpleFilter = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAdvFilter_Rec = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pBasicFilter = new System.Windows.Forms.Panel();
            this.lbSwitchToAdvFilters = new System.Windows.Forms.LinkLabel();
            this.tbFilter = new Utils.FilterTextBox();
            this.lbFilter = new System.Windows.Forms.Label();
            this.scSeriesDetails = new System.Windows.Forms.SplitContainer();
            this.pSeriesSecondToolbar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnImportMovies = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefreshMovieData = new System.Windows.Forms.ToolStripButton();
            this.btnLoadPoster = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.bntSort = new System.Windows.Forms.ToolStripDropDownButton();
            this.miSortByName = new System.Windows.Forms.ToolStripMenuItem();
            this.miSortByInsertDate = new System.Windows.Forms.ToolStripMenuItem();
            this.miSortByLastChangedDate = new System.Windows.Forms.ToolStripMenuItem();
            this.pMovieDetailsContainer = new System.Windows.Forms.Panel();
            this.cmMoviesNavGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miHighlightNoPoster = new System.Windows.Forms.ToolStripMenuItem();
            this.miHighlightNoSynopsis = new System.Windows.Forms.ToolStripMenuItem();
            this.miHighlightNoCSM = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scMovies)).BeginInit();
            this.scMovies.Panel1.SuspendLayout();
            this.scMovies.Panel2.SuspendLayout();
            this.scMovies.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pDummyMenuForShortCutKeys.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoviesList)).BeginInit();
            this.pAdvFilter.SuspendLayout();
            this.gbAdvFilterWrapper.SuspendLayout();
            this.pBasicFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSeriesDetails)).BeginInit();
            this.scSeriesDetails.Panel1.SuspendLayout();
            this.scSeriesDetails.Panel2.SuspendLayout();
            this.scSeriesDetails.SuspendLayout();
            this.pSeriesSecondToolbar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.cmMoviesNavGrid.SuspendLayout();
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
            this.scMovies.Panel1.Controls.Add(this.pAdvFilter);
            this.scMovies.Panel1.Controls.Add(this.pBasicFilter);
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
            this.panel1.Location = new System.Drawing.Point(0, 170);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(300, 554);
            this.panel1.TabIndex = 108;
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
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSave_ForShortCutOnly,
            this.miUndo_ForShortCutOnly});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(185, 60);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miSave_ForShortCutOnly
            // 
            this.miSave_ForShortCutOnly.Image = global::Desene.Properties.Resources.save;
            this.miSave_ForShortCutOnly.Name = "miSave_ForShortCutOnly";
            this.miSave_ForShortCutOnly.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave_ForShortCutOnly.Size = new System.Drawing.Size(180, 28);
            // 
            // miUndo_ForShortCutOnly
            // 
            this.miUndo_ForShortCutOnly.Image = global::Desene.Properties.Resources.undo;
            this.miUndo_ForShortCutOnly.Name = "miUndo_ForShortCutOnly";
            this.miUndo_ForShortCutOnly.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.miUndo_ForShortCutOnly.Size = new System.Drawing.Size(180, 28);
            // 
            // dgvMoviesList
            // 
            this.dgvMoviesList.AllowUserToAddRows = false;
            this.dgvMoviesList.AllowUserToDeleteRows = false;
            this.dgvMoviesList.AllowUserToOrderColumns = true;
            this.dgvMoviesList.AllowUserToResizeColumns = false;
            this.dgvMoviesList.AllowUserToResizeRows = false;
            this.dgvMoviesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMoviesList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMoviesList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMoviesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMoviesList.ColumnHeadersVisible = false;
            this.dgvMoviesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colTitle,
            this.Calitate});
            this.dgvMoviesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMoviesList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvMoviesList.Location = new System.Drawing.Point(15, 0);
            this.dgvMoviesList.MultiSelect = false;
            this.dgvMoviesList.Name = "dgvMoviesList";
            this.dgvMoviesList.ReadOnly = true;
            this.dgvMoviesList.RowHeadersVisible = false;
            this.dgvMoviesList.RowHeadersWidth = 62;
            this.dgvMoviesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMoviesList.Size = new System.Drawing.Size(285, 554);
            this.dgvMoviesList.TabIndex = 105;
            this.dgvMoviesList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvMoviesList_CellPainting);
            this.dgvMoviesList.SelectionChanged += new System.EventHandler(this.dgvMoviesList_SelectionChanged);
            this.dgvMoviesList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvMoviesList_KeyPress);
            this.dgvMoviesList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMoviesList_MouseClick);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "Id";
            this.colId.MinimumWidth = 8;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colTitle
            // 
            this.colTitle.DataPropertyName = "FileName";
            this.colTitle.FillWeight = 179.6954F;
            this.colTitle.HeaderText = "FileName";
            this.colTitle.MinimumWidth = 8;
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            // 
            // Calitate
            // 
            this.Calitate.DataPropertyName = "Quality";
            this.Calitate.FillWeight = 20.30457F;
            this.Calitate.HeaderText = "Quality";
            this.Calitate.MinimumWidth = 8;
            this.Calitate.Name = "Calitate";
            this.Calitate.ReadOnly = true;
            this.Calitate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // pAdvFilter
            // 
            this.pAdvFilter.Controls.Add(this.gbAdvFilterWrapper);
            this.pAdvFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pAdvFilter.Location = new System.Drawing.Point(0, 46);
            this.pAdvFilter.Name = "pAdvFilter";
            this.pAdvFilter.Size = new System.Drawing.Size(300, 124);
            this.pAdvFilter.TabIndex = 107;
            this.pAdvFilter.Visible = false;
            // 
            // gbAdvFilterWrapper
            // 
            this.gbAdvFilterWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAdvFilterWrapper.Controls.Add(this.chkIncludeUnknownRec);
            this.gbAdvFilterWrapper.Controls.Add(this.cbAdvFilter_Theme);
            this.gbAdvFilterWrapper.Controls.Add(this.lbAdvFilterReset);
            this.gbAdvFilterWrapper.Controls.Add(this.cbAdvFilter_Audio);
            this.gbAdvFilterWrapper.Controls.Add(this.lbSwitchToSimpleFilter);
            this.gbAdvFilterWrapper.Controls.Add(this.label3);
            this.gbAdvFilterWrapper.Controls.Add(this.tbAdvFilter_Rec);
            this.gbAdvFilterWrapper.Controls.Add(this.label2);
            this.gbAdvFilterWrapper.Controls.Add(this.label1);
            this.gbAdvFilterWrapper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAdvFilterWrapper.Location = new System.Drawing.Point(15, 6);
            this.gbAdvFilterWrapper.Name = "gbAdvFilterWrapper";
            this.gbAdvFilterWrapper.Size = new System.Drawing.Size(285, 114);
            this.gbAdvFilterWrapper.TabIndex = 17;
            this.gbAdvFilterWrapper.TabStop = false;
            this.gbAdvFilterWrapper.Text = "Advanced filters";
            // 
            // chkIncludeUnknownRec
            // 
            this.chkIncludeUnknownRec.AutoSize = true;
            this.chkIncludeUnknownRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeUnknownRec.Location = new System.Drawing.Point(167, 53);
            this.chkIncludeUnknownRec.Name = "chkIncludeUnknownRec";
            this.chkIncludeUnknownRec.Size = new System.Drawing.Size(108, 17);
            this.chkIncludeUnknownRec.TabIndex = 26;
            this.chkIncludeUnknownRec.Text = "Include unknown";
            this.chkIncludeUnknownRec.UseVisualStyleBackColor = true;
            this.chkIncludeUnknownRec.CheckedChanged += new System.EventHandler(this.chkIncludeUnknownRec_CheckedChanged);
            // 
            // cbAdvFilter_Theme
            // 
            this.cbAdvFilter_Theme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAdvFilter_Theme.AutoAdjustItemHeight = true;
            this.cbAdvFilter_Theme.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbAdvFilter_Theme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAdvFilter_Theme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAdvFilter_Theme.FormattingEnabled = true;
            this.cbAdvFilter_Theme.Location = new System.Drawing.Point(112, 77);
            this.cbAdvFilter_Theme.Name = "cbAdvFilter_Theme";
            this.cbAdvFilter_Theme.SeparatorColor = System.Drawing.Color.Black;
            this.cbAdvFilter_Theme.SeparatorMargin = 1;
            this.cbAdvFilter_Theme.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbAdvFilter_Theme.SeparatorWidth = 1;
            this.cbAdvFilter_Theme.Size = new System.Drawing.Size(137, 21);
            this.cbAdvFilter_Theme.TabIndex = 25;
            this.cbAdvFilter_Theme.SelectionChangeCommitted += new System.EventHandler(this.cbAdvFilter_Audio_SelectionChangeCommitted);
            // 
            // lbAdvFilterReset
            // 
            this.lbAdvFilterReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAdvFilterReset.AutoSize = true;
            this.lbAdvFilterReset.BackColor = System.Drawing.Color.Transparent;
            this.lbAdvFilterReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbAdvFilterReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdvFilterReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbAdvFilterReset.Location = new System.Drawing.Point(256, 0);
            this.lbAdvFilterReset.Name = "lbAdvFilterReset";
            this.lbAdvFilterReset.Size = new System.Drawing.Size(33, 31);
            this.lbAdvFilterReset.TabIndex = 24;
            this.lbAdvFilterReset.Text = "↺";
            this.lbAdvFilterReset.Click += new System.EventHandler(this.lbAdvFilterReset_Click);
            this.lbAdvFilterReset.MouseEnter += new System.EventHandler(this.lbAdvFilterReset_MouseEnter);
            this.lbAdvFilterReset.MouseLeave += new System.EventHandler(this.lbAdvFilterReset_MouseLeave);
            // 
            // cbAdvFilter_Audio
            // 
            this.cbAdvFilter_Audio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAdvFilter_Audio.AutoAdjustItemHeight = true;
            this.cbAdvFilter_Audio.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbAdvFilter_Audio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAdvFilter_Audio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAdvFilter_Audio.FormattingEnabled = true;
            this.cbAdvFilter_Audio.Location = new System.Drawing.Point(112, 24);
            this.cbAdvFilter_Audio.Name = "cbAdvFilter_Audio";
            this.cbAdvFilter_Audio.SeparatorColor = System.Drawing.Color.Black;
            this.cbAdvFilter_Audio.SeparatorMargin = 1;
            this.cbAdvFilter_Audio.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbAdvFilter_Audio.SeparatorWidth = 1;
            this.cbAdvFilter_Audio.Size = new System.Drawing.Size(137, 21);
            this.cbAdvFilter_Audio.TabIndex = 21;
            this.cbAdvFilter_Audio.SelectionChangeCommitted += new System.EventHandler(this.cbAdvFilter_Audio_SelectionChangeCommitted);
            // 
            // lbSwitchToSimpleFilter
            // 
            this.lbSwitchToSimpleFilter.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lbSwitchToSimpleFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSwitchToSimpleFilter.AutoSize = true;
            this.lbSwitchToSimpleFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSwitchToSimpleFilter.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lbSwitchToSimpleFilter.Location = new System.Drawing.Point(252, 98);
            this.lbSwitchToSimpleFilter.Name = "lbSwitchToSimpleFilter";
            this.lbSwitchToSimpleFilter.Size = new System.Drawing.Size(32, 13);
            this.lbSwitchToSimpleFilter.TabIndex = 20;
            this.lbSwitchToSimpleFilter.TabStop = true;
            this.lbSwitchToSimpleFilter.Text = "close";
            this.lbSwitchToSimpleFilter.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lbSwitchToSimpleFilter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbSwitchToSimpleFilter_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Theme";
            // 
            // tbAdvFilter_Rec
            // 
            this.tbAdvFilter_Rec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAdvFilter_Rec.Location = new System.Drawing.Point(112, 51);
            this.tbAdvFilter_Rec.Name = "tbAdvFilter_Rec";
            this.tbAdvFilter_Rec.Size = new System.Drawing.Size(48, 20);
            this.tbAdvFilter_Rec.TabIndex = 4;
            this.tbAdvFilter_Rec.TextChanged += new System.EventHandler(this.tbAdvFilter_Rec_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rec. age (max)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Audio language";
            // 
            // pBasicFilter
            // 
            this.pBasicFilter.Controls.Add(this.lbSwitchToAdvFilters);
            this.pBasicFilter.Controls.Add(this.tbFilter);
            this.pBasicFilter.Controls.Add(this.lbFilter);
            this.pBasicFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pBasicFilter.Location = new System.Drawing.Point(0, 0);
            this.pBasicFilter.Name = "pBasicFilter";
            this.pBasicFilter.Size = new System.Drawing.Size(300, 46);
            this.pBasicFilter.TabIndex = 104;
            // 
            // lbSwitchToAdvFilters
            // 
            this.lbSwitchToAdvFilters.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lbSwitchToAdvFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSwitchToAdvFilters.AutoSize = true;
            this.lbSwitchToAdvFilters.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lbSwitchToAdvFilters.Location = new System.Drawing.Point(218, 30);
            this.lbSwitchToAdvFilters.Name = "lbSwitchToAdvFilters";
            this.lbSwitchToAdvFilters.Size = new System.Drawing.Size(82, 13);
            this.lbSwitchToAdvFilters.TabIndex = 19;
            this.lbSwitchToAdvFilters.TabStop = true;
            this.lbSwitchToAdvFilters.Text = "advanced filters";
            this.lbSwitchToAdvFilters.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lbSwitchToAdvFilters.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbSwitchToAdvFilters_LinkClicked);
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
            this.btnImportMovies,
            this.toolStripSeparator2,
            this.btnRefreshMovieData,
            this.btnLoadPoster,
            this.toolStripSeparator1,
            this.btnSaveChanges,
            this.btnUndo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(25, 684);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnImportMovies
            // 
            this.btnImportMovies.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImportMovies.Image = global::Desene.Properties.Resources.import;
            this.btnImportMovies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportMovies.Name = "btnImportMovies";
            this.btnImportMovies.Size = new System.Drawing.Size(20, 20);
            this.btnImportMovies.Text = "Import movies data from files";
            this.btnImportMovies.ToolTipText = "Import movies data from files";
            this.btnImportMovies.Click += new System.EventHandler(this.btnImportMovies_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(20, 6);
            // 
            // btnRefreshMovieData
            // 
            this.btnRefreshMovieData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshMovieData.Enabled = false;
            this.btnRefreshMovieData.Image = global::Desene.Properties.Resources.refresh;
            this.btnRefreshMovieData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshMovieData.Name = "btnRefreshMovieData";
            this.btnRefreshMovieData.Size = new System.Drawing.Size(20, 20);
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
            this.btnLoadPoster.Size = new System.Drawing.Size(20, 20);
            this.btnLoadPoster.Text = "Load Movie poster";
            this.btnLoadPoster.Click += new System.EventHandler(this.btnLoadPoster_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(20, 6);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.Image = global::Desene.Properties.Resources.save;
            this.btnSaveChanges.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(20, 20);
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
            this.btnUndo.Size = new System.Drawing.Size(20, 20);
            this.btnUndo.Text = "Undo";
            this.btnUndo.ToolTipText = "Undo changes\r\nCTRL+Z";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel2.Size = new System.Drawing.Size(25, 40);
            this.panel2.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bntSort});
            this.toolStrip2.Location = new System.Drawing.Point(0, 8);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip2.Size = new System.Drawing.Size(22, 32);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // bntSort
            // 
            this.bntSort.AutoSize = false;
            this.bntSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bntSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSortByName,
            this.miSortByInsertDate,
            this.miSortByLastChangedDate});
            this.bntSort.Image = global::Desene.Properties.Resources.sort;
            this.bntSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntSort.Name = "bntSort";
            this.bntSort.ShowDropDownArrow = false;
            this.bntSort.Size = new System.Drawing.Size(20, 20);
            this.bntSort.Text = "Sort the list by ...";
            // 
            // miSortByName
            // 
            this.miSortByName.Checked = true;
            this.miSortByName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miSortByName.Name = "miSortByName";
            this.miSortByName.Size = new System.Drawing.Size(170, 22);
            this.miSortByName.Tag = "FileName COLLATE NOCASE ASC";
            this.miSortByName.Text = "Name (default)";
            this.miSortByName.Click += new System.EventHandler(this.SortMoviesGrid);
            // 
            // miSortByInsertDate
            // 
            this.miSortByInsertDate.Name = "miSortByInsertDate";
            this.miSortByInsertDate.Size = new System.Drawing.Size(170, 22);
            this.miSortByInsertDate.Tag = "InsertedDate DESC";
            this.miSortByInsertDate.Text = "Insert date";
            this.miSortByInsertDate.ToolTipText = "Reverse order (last inserted is the first)";
            this.miSortByInsertDate.Click += new System.EventHandler(this.SortMoviesGrid);
            // 
            // miSortByLastChangedDate
            // 
            this.miSortByLastChangedDate.Name = "miSortByLastChangedDate";
            this.miSortByLastChangedDate.Size = new System.Drawing.Size(170, 22);
            this.miSortByLastChangedDate.Tag = "LastChangeDate  DESC";
            this.miSortByLastChangedDate.Text = "Last changed date";
            this.miSortByLastChangedDate.ToolTipText = "Reverse order (last edited is the first)";
            this.miSortByLastChangedDate.Click += new System.EventHandler(this.SortMoviesGrid);
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
            // cmMoviesNavGrid
            // 
            this.cmMoviesNavGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHighlightNoPoster,
            this.miHighlightNoSynopsis,
            this.miHighlightNoCSM});
            this.cmMoviesNavGrid.Name = "cmMoviesNavGrid";
            this.cmMoviesNavGrid.Size = new System.Drawing.Size(353, 92);
            // 
            // miHighlightNoPoster
            // 
            this.miHighlightNoPoster.Name = "miHighlightNoPoster";
            this.miHighlightNoPoster.Size = new System.Drawing.Size(352, 22);
            this.miHighlightNoPoster.Text = "Highlight movies without poster";
            this.miHighlightNoPoster.Click += new System.EventHandler(this.miHighlightNoPoster_Click);
            // 
            // miHighlightNoSynopsis
            // 
            this.miHighlightNoSynopsis.Name = "miHighlightNoSynopsis";
            this.miHighlightNoSynopsis.Size = new System.Drawing.Size(352, 22);
            this.miHighlightNoSynopsis.Text = "Highlight movies without synopsis";
            this.miHighlightNoSynopsis.Click += new System.EventHandler(this.miHighlightNoSynopsis_Click);
            // 
            // miHighlightNoCSM
            // 
            this.miHighlightNoCSM.Name = "miHighlightNoCSM";
            this.miHighlightNoCSM.Size = new System.Drawing.Size(352, 22);
            this.miHighlightNoCSM.Text = "Highlight movies without CommonSenseMedia data";
            this.miHighlightNoCSM.Click += new System.EventHandler(this.miHighlightNoCSM_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FileName";
            this.dataGridViewTextBoxColumn2.FillWeight = 179.6954F;
            this.dataGridViewTextBoxColumn2.HeaderText = "FileName";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 259;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Quality";
            this.dataGridViewTextBoxColumn3.FillWeight = 20.30457F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Quality";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.Width = 23;
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
            this.pDummyMenuForShortCutKeys.ResumeLayout(false);
            this.pDummyMenuForShortCutKeys.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoviesList)).EndInit();
            this.pAdvFilter.ResumeLayout(false);
            this.gbAdvFilterWrapper.ResumeLayout(false);
            this.gbAdvFilterWrapper.PerformLayout();
            this.pBasicFilter.ResumeLayout(false);
            this.pBasicFilter.PerformLayout();
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.cmMoviesNavGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMovies;
        private System.Windows.Forms.Panel pBasicFilter;
        private System.Windows.Forms.Label lbFilter;
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
        private System.Windows.Forms.ToolStripButton btnImportMovies;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripDropDownButton bntSort;
        private System.Windows.Forms.ToolStripMenuItem miSortByName;
        private System.Windows.Forms.ToolStripMenuItem miSortByInsertDate;
        private System.Windows.Forms.ToolStripMenuItem miSortByLastChangedDate;
        private System.Windows.Forms.Panel pAdvFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pDummyMenuForShortCutKeys;
        private System.Windows.Forms.Label lbDoNotDelete;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSave_ForShortCutOnly;
        private System.Windows.Forms.ToolStripMenuItem miUndo_ForShortCutOnly;
        private System.Windows.Forms.DataGridView dgvMoviesList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Calitate;
        private System.Windows.Forms.GroupBox gbAdvFilterWrapper;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAdvFilter_Rec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lbSwitchToAdvFilters;
        private System.Windows.Forms.LinkLabel lbSwitchToSimpleFilter;
        private Utils.SeparatorComboBox cbAdvFilter_Audio;
        private System.Windows.Forms.Label lbAdvFilterReset;
        private Utils.SeparatorComboBox cbAdvFilter_Theme;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.CheckBox chkIncludeUnknownRec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip cmMoviesNavGrid;
        private System.Windows.Forms.ToolStripMenuItem miHighlightNoPoster;
        private System.Windows.Forms.ToolStripMenuItem miHighlightNoSynopsis;
        private System.Windows.Forms.ToolStripMenuItem miHighlightNoCSM;
    }
}
