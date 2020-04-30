namespace Desene
{
    partial class ucCollections
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
            this.pSeriesTreeWrapper = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pDummyMenuForShortCutKeys = new System.Windows.Forms.Panel();
            this.lbDoNotDelete = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miSave_ForShortCutOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.miUndo_ForShortCutOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.tvCollections = new Aga.Controls.Tree.TreeViewAdv();
            this.pFilters = new System.Windows.Forms.Panel();
            this.lbFilter = new System.Windows.Forms.Label();
            this.tbFilter = new Utils.FilterTextBox();
            this.pSeriesDetailsWrapper = new System.Windows.Forms.Panel();
            this.scSeriesDetails = new System.Windows.Forms.SplitContainer();
            this.pSeriesSecondToolbar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.moduleToolbar = new System.Windows.Forms.ToolStrip();
            this.btnImportElements = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLoadPoster = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshElementData = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteElement = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pCollectionElementDetailsContainer = new System.Windows.Forms.Panel();
            this.btnAddMovieToCollection = new System.Windows.Forms.ToolStripButton();
            this.pSeriesTreeWrapper.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pDummyMenuForShortCutKeys.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pFilters.SuspendLayout();
            this.pSeriesDetailsWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSeriesDetails)).BeginInit();
            this.scSeriesDetails.Panel1.SuspendLayout();
            this.scSeriesDetails.Panel2.SuspendLayout();
            this.scSeriesDetails.SuspendLayout();
            this.pSeriesSecondToolbar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.moduleToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pSeriesTreeWrapper
            // 
            this.pSeriesTreeWrapper.Controls.Add(this.panel2);
            this.pSeriesTreeWrapper.Controls.Add(this.pFilters);
            this.pSeriesTreeWrapper.Dock = System.Windows.Forms.DockStyle.Left;
            this.pSeriesTreeWrapper.Location = new System.Drawing.Point(0, 0);
            this.pSeriesTreeWrapper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pSeriesTreeWrapper.Name = "pSeriesTreeWrapper";
            this.pSeriesTreeWrapper.Size = new System.Drawing.Size(600, 892);
            this.pSeriesTreeWrapper.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pDummyMenuForShortCutKeys);
            this.panel2.Controls.Add(this.tvCollections);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 62);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 830);
            this.panel2.TabIndex = 107;
            // 
            // pDummyMenuForShortCutKeys
            // 
            this.pDummyMenuForShortCutKeys.Controls.Add(this.lbDoNotDelete);
            this.pDummyMenuForShortCutKeys.Controls.Add(this.menuStrip1);
            this.pDummyMenuForShortCutKeys.Location = new System.Drawing.Point(147, 637);
            this.pDummyMenuForShortCutKeys.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pDummyMenuForShortCutKeys.Name = "pDummyMenuForShortCutKeys";
            this.pDummyMenuForShortCutKeys.Size = new System.Drawing.Size(278, 145);
            this.pDummyMenuForShortCutKeys.TabIndex = 119;
            // 
            // lbDoNotDelete
            // 
            this.lbDoNotDelete.AutoSize = true;
            this.lbDoNotDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDoNotDelete.ForeColor = System.Drawing.Color.Red;
            this.lbDoNotDelete.Location = new System.Drawing.Point(22, 88);
            this.lbDoNotDelete.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDoNotDelete.Name = "lbDoNotDelete";
            this.lbDoNotDelete.Size = new System.Drawing.Size(228, 20);
            this.lbDoNotDelete.TabIndex = 1;
            this.lbDoNotDelete.Text = "Do Not Delete (design) !!!";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSave_ForShortCutOnly,
            this.miUndo_ForShortCutOnly});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(278, 64);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miSave_ForShortCutOnly
            // 
            this.miSave_ForShortCutOnly.Image = global::Desene.Properties.Resources.save;
            this.miSave_ForShortCutOnly.Name = "miSave_ForShortCutOnly";
            this.miSave_ForShortCutOnly.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave_ForShortCutOnly.Size = new System.Drawing.Size(268, 28);
            this.miSave_ForShortCutOnly.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // miUndo_ForShortCutOnly
            // 
            this.miUndo_ForShortCutOnly.Image = global::Desene.Properties.Resources.undo;
            this.miUndo_ForShortCutOnly.Name = "miUndo_ForShortCutOnly";
            this.miUndo_ForShortCutOnly.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.miUndo_ForShortCutOnly.Size = new System.Drawing.Size(268, 28);
            // 
            // tvCollections
            // 
            this.tvCollections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCollections.BackColor = System.Drawing.SystemColors.Window;
            this.tvCollections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvCollections.DefaultToolTipProvider = null;
            this.tvCollections.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvCollections.FullRowSelect = true;
            this.tvCollections.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvCollections.LoadOnDemand = true;
            this.tvCollections.Location = new System.Drawing.Point(22, 0);
            this.tvCollections.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tvCollections.Model = null;
            this.tvCollections.Name = "tvCollections";
            this.tvCollections.SelectedNode = null;
            this.tvCollections.Size = new System.Drawing.Size(568, 830);
            this.tvCollections.TabIndex = 4;
            this.tvCollections.Text = "treeViewAdv1";
            this.tvCollections.SelectionChanged += new System.EventHandler(this.tvCollections_SelectionChanged);
            // 
            // pFilters
            // 
            this.pFilters.Controls.Add(this.lbFilter);
            this.pFilters.Controls.Add(this.tbFilter);
            this.pFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilters.Location = new System.Drawing.Point(0, 0);
            this.pFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pFilters.Name = "pFilters";
            this.pFilters.Size = new System.Drawing.Size(600, 62);
            this.pFilters.TabIndex = 106;
            // 
            // lbFilter
            // 
            this.lbFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lbFilter.AutoSize = true;
            this.lbFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbFilter.Location = new System.Drawing.Point(18, 18);
            this.lbFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(44, 20);
            this.lbFilter.TabIndex = 15;
            this.lbFilter.Text = "Filter";
            this.lbFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbFilter.ForeColor = System.Drawing.Color.Silver;
            this.tbFilter.Location = new System.Drawing.Point(70, 14);
            this.tbFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(518, 26);
            this.tbFilter.TabIndex = 14;
            this.tbFilter.ButtonClick += new System.EventHandler(this.tbFilter_ButtonClick);
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // pSeriesDetailsWrapper
            // 
            this.pSeriesDetailsWrapper.AutoScroll = true;
            this.pSeriesDetailsWrapper.Controls.Add(this.scSeriesDetails);
            this.pSeriesDetailsWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSeriesDetailsWrapper.Location = new System.Drawing.Point(600, 0);
            this.pSeriesDetailsWrapper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pSeriesDetailsWrapper.Name = "pSeriesDetailsWrapper";
            this.pSeriesDetailsWrapper.Size = new System.Drawing.Size(1065, 892);
            this.pSeriesDetailsWrapper.TabIndex = 1;
            // 
            // scSeriesDetails
            // 
            this.scSeriesDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSeriesDetails.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scSeriesDetails.IsSplitterFixed = true;
            this.scSeriesDetails.Location = new System.Drawing.Point(0, 0);
            this.scSeriesDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.scSeriesDetails.Name = "scSeriesDetails";
            // 
            // scSeriesDetails.Panel1
            // 
            this.scSeriesDetails.Panel1.Controls.Add(this.pSeriesSecondToolbar);
            // 
            // scSeriesDetails.Panel2
            // 
            this.scSeriesDetails.Panel2.Controls.Add(this.pCollectionElementDetailsContainer);
            this.scSeriesDetails.Size = new System.Drawing.Size(1065, 892);
            this.scSeriesDetails.SplitterDistance = 25;
            this.scSeriesDetails.SplitterWidth = 6;
            this.scSeriesDetails.TabIndex = 0;
            // 
            // pSeriesSecondToolbar
            // 
            this.pSeriesSecondToolbar.Controls.Add(this.panel3);
            this.pSeriesSecondToolbar.Controls.Add(this.panel1);
            this.pSeriesSecondToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSeriesSecondToolbar.Location = new System.Drawing.Point(0, 0);
            this.pSeriesSecondToolbar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pSeriesSecondToolbar.Name = "pSeriesSecondToolbar";
            this.pSeriesSecondToolbar.Size = new System.Drawing.Size(25, 892);
            this.pSeriesSecondToolbar.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.moduleToolbar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 62);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(25, 830);
            this.panel3.TabIndex = 1;
            // 
            // moduleToolbar
            // 
            this.moduleToolbar.BackColor = System.Drawing.SystemColors.Control;
            this.moduleToolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.moduleToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.moduleToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.moduleToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImportElements,
            this.btnAddMovieToCollection,
            this.toolStripSeparator3,
            this.btnLoadPoster,
            this.toolStripSeparator1,
            this.btnSaveChanges,
            this.btnUndo,
            this.btnRefreshElementData,
            this.btnDeleteElement});
            this.moduleToolbar.Location = new System.Drawing.Point(0, 0);
            this.moduleToolbar.Name = "moduleToolbar";
            this.moduleToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.moduleToolbar.Size = new System.Drawing.Size(49, 830);
            this.moduleToolbar.TabIndex = 1;
            this.moduleToolbar.Text = "toolStrip1";
            // 
            // btnImportElements
            // 
            this.btnImportElements.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImportElements.Enabled = false;
            this.btnImportElements.Image = global::Desene.Properties.Resources.import;
            this.btnImportElements.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportElements.Name = "btnImportElements";
            this.btnImportElements.Size = new System.Drawing.Size(42, 28);
            this.btnImportElements.Text = "Import Elements data from files";
            this.btnImportElements.Click += new System.EventHandler(this.btnImportElements_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(42, 6);
            // 
            // btnLoadPoster
            // 
            this.btnLoadPoster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadPoster.Enabled = false;
            this.btnLoadPoster.Image = global::Desene.Properties.Resources.image;
            this.btnLoadPoster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadPoster.Margin = new System.Windows.Forms.Padding(0, 5, 0, 2);
            this.btnLoadPoster.Name = "btnLoadPoster";
            this.btnLoadPoster.Size = new System.Drawing.Size(42, 28);
            this.btnLoadPoster.Text = "Load Series poster";
            this.btnLoadPoster.Click += new System.EventHandler(this.btnLoadPoster_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(42, 6);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.Image = global::Desene.Properties.Resources.save;
            this.btnSaveChanges.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(42, 20);
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
            this.btnUndo.Size = new System.Drawing.Size(42, 28);
            this.btnUndo.Text = "Undo";
            this.btnUndo.ToolTipText = "Undo changes\r\nCTRL+Z";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRefreshElementData
            // 
            this.btnRefreshElementData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshElementData.Enabled = false;
            this.btnRefreshElementData.Image = global::Desene.Properties.Resources.refresh;
            this.btnRefreshElementData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshElementData.Name = "btnRefreshElementData";
            this.btnRefreshElementData.Size = new System.Drawing.Size(42, 28);
            this.btnRefreshElementData.Text = "Refresh Element data from file";
            this.btnRefreshElementData.Click += new System.EventHandler(this.btnRefreshEpisodeData_Click);
            // 
            // btnDeleteElement
            // 
            this.btnDeleteElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteElement.Enabled = false;
            this.btnDeleteElement.Image = global::Desene.Properties.Resources.delete;
            this.btnDeleteElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteElement.Name = "btnDeleteElement";
            this.btnDeleteElement.Size = new System.Drawing.Size(42, 28);
            this.btnDeleteElement.Text = "Delete";
            this.btnDeleteElement.ToolTipText = "Delete the selected Element";
            this.btnDeleteElement.Click += new System.EventHandler(this.btnDeleteSeasonEpisode_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(25, 62);
            this.panel1.TabIndex = 0;
            // 
            // pCollectionElementDetailsContainer
            // 
            this.pCollectionElementDetailsContainer.AutoScroll = true;
            this.pCollectionElementDetailsContainer.AutoSize = true;
            this.pCollectionElementDetailsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pCollectionElementDetailsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCollectionElementDetailsContainer.Location = new System.Drawing.Point(0, 0);
            this.pCollectionElementDetailsContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pCollectionElementDetailsContainer.Name = "pCollectionElementDetailsContainer";
            this.pCollectionElementDetailsContainer.Size = new System.Drawing.Size(1034, 892);
            this.pCollectionElementDetailsContainer.TabIndex = 0;
            // 
            // btnAddMovieToCollection
            // 
            this.btnAddMovieToCollection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddMovieToCollection.Enabled = false;
            this.btnAddMovieToCollection.Image = global::Desene.Properties.Resources.add2;
            this.btnAddMovieToCollection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddMovieToCollection.Name = "btnAddMovieToCollection";
            this.btnAddMovieToCollection.Size = new System.Drawing.Size(42, 28);
            this.btnAddMovieToCollection.Text = "Add Movie to Collection";
            this.btnAddMovieToCollection.Click += new System.EventHandler(this.btnAddMovieToCollection_Click);
            // 
            // ucCollections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pSeriesDetailsWrapper);
            this.Controls.Add(this.pSeriesTreeWrapper);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucCollections";
            this.Size = new System.Drawing.Size(1665, 892);
            this.Load += new System.EventHandler(this.ucSeries_Load);
            this.pSeriesTreeWrapper.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pDummyMenuForShortCutKeys.ResumeLayout(false);
            this.pDummyMenuForShortCutKeys.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pFilters.ResumeLayout(false);
            this.pFilters.PerformLayout();
            this.pSeriesDetailsWrapper.ResumeLayout(false);
            this.scSeriesDetails.Panel1.ResumeLayout(false);
            this.scSeriesDetails.Panel2.ResumeLayout(false);
            this.scSeriesDetails.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSeriesDetails)).EndInit();
            this.scSeriesDetails.ResumeLayout(false);
            this.pSeriesSecondToolbar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.moduleToolbar.ResumeLayout(false);
            this.moduleToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pSeriesTreeWrapper;
        private System.Windows.Forms.Panel panel2;
        private Aga.Controls.Tree.TreeViewAdv tvCollections;
        private System.Windows.Forms.Panel pFilters;
        private System.Windows.Forms.Label lbFilter;
        private Utils.FilterTextBox tbFilter;
        private System.Windows.Forms.Panel pSeriesDetailsWrapper;
        private System.Windows.Forms.SplitContainer scSeriesDetails;
        private System.Windows.Forms.Panel pCollectionElementDetailsContainer;
        private System.Windows.Forms.Panel pSeriesSecondToolbar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip moduleToolbar;
        private System.Windows.Forms.ToolStripButton btnSaveChanges;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton btnLoadPoster;
        private System.Windows.Forms.ToolStripButton btnRefreshElementData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImportElements;
        private System.Windows.Forms.ToolStripButton btnDeleteElement;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.Panel pDummyMenuForShortCutKeys;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSave_ForShortCutOnly;
        private System.Windows.Forms.ToolStripMenuItem miUndo_ForShortCutOnly;
        private System.Windows.Forms.Label lbDoNotDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnAddMovieToCollection;
    }
}
