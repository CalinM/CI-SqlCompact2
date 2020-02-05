namespace Desene
{
    partial class ucSeries
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
            this.tvSeries = new Aga.Controls.Tree.TreeViewAdv();
            this.pFilters = new System.Windows.Forms.Panel();
            this.lbFilter = new System.Windows.Forms.Label();
            this.tbFilter = new Utils.FilterTextBox();
            this.pSeriesDetailsWrapper = new System.Windows.Forms.Panel();
            this.scSeriesDetails = new System.Windows.Forms.SplitContainer();
            this.pSeriesSecondToolbar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnImportEpisodes = new System.Windows.Forms.ToolStripButton();
            this.btnLoadPoster = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshEpisodeData = new System.Windows.Forms.ToolStripButton();
            this.btnBulkEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteSeasonEpisode = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pSeriesDetailsContainer = new Utils.CustomPanel();
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
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pSeriesTreeWrapper
            // 
            this.pSeriesTreeWrapper.Controls.Add(this.panel2);
            this.pSeriesTreeWrapper.Controls.Add(this.pFilters);
            this.pSeriesTreeWrapper.Dock = System.Windows.Forms.DockStyle.Left;
            this.pSeriesTreeWrapper.Location = new System.Drawing.Point(0, 0);
            this.pSeriesTreeWrapper.Name = "pSeriesTreeWrapper";
            this.pSeriesTreeWrapper.Size = new System.Drawing.Size(400, 580);
            this.pSeriesTreeWrapper.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pDummyMenuForShortCutKeys);
            this.panel2.Controls.Add(this.tvSeries);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 540);
            this.panel2.TabIndex = 107;
            // 
            // pDummyMenuForShortCutKeys
            // 
            this.pDummyMenuForShortCutKeys.Controls.Add(this.lbDoNotDelete);
            this.pDummyMenuForShortCutKeys.Controls.Add(this.menuStrip1);
            this.pDummyMenuForShortCutKeys.Location = new System.Drawing.Point(98, 414);
            this.pDummyMenuForShortCutKeys.Name = "pDummyMenuForShortCutKeys";
            this.pDummyMenuForShortCutKeys.Size = new System.Drawing.Size(185, 94);
            this.pDummyMenuForShortCutKeys.TabIndex = 119;
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
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(185, 62);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miSave_ForShortCutOnly
            // 
            this.miSave_ForShortCutOnly.Image = global::Desene.Properties.Resources.save;
            this.miSave_ForShortCutOnly.Name = "miSave_ForShortCutOnly";
            this.miSave_ForShortCutOnly.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave_ForShortCutOnly.Size = new System.Drawing.Size(178, 28);
            this.miSave_ForShortCutOnly.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // miUndo_ForShortCutOnly
            // 
            this.miUndo_ForShortCutOnly.Image = global::Desene.Properties.Resources.undo;
            this.miUndo_ForShortCutOnly.Name = "miUndo_ForShortCutOnly";
            this.miUndo_ForShortCutOnly.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.miUndo_ForShortCutOnly.Size = new System.Drawing.Size(178, 28);
            // 
            // tvSeries
            // 
            this.tvSeries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSeries.BackColor = System.Drawing.SystemColors.Window;
            this.tvSeries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvSeries.DefaultToolTipProvider = null;
            this.tvSeries.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvSeries.FullRowSelect = true;
            this.tvSeries.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvSeries.LoadOnDemand = true;
            this.tvSeries.Location = new System.Drawing.Point(15, 0);
            this.tvSeries.Model = null;
            this.tvSeries.Name = "tvSeries";
            this.tvSeries.SelectedNode = null;
            this.tvSeries.Size = new System.Drawing.Size(379, 541);
            this.tvSeries.TabIndex = 4;
            this.tvSeries.Text = "treeViewAdv1";
            this.tvSeries.SelectionChanged += new System.EventHandler(this.tvSeries_SelectionChanged);
            // 
            // pFilters
            // 
            this.pFilters.Controls.Add(this.lbFilter);
            this.pFilters.Controls.Add(this.tbFilter);
            this.pFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilters.Location = new System.Drawing.Point(0, 0);
            this.pFilters.Name = "pFilters";
            this.pFilters.Size = new System.Drawing.Size(400, 40);
            this.pFilters.TabIndex = 106;
            // 
            // lbFilter
            // 
            this.lbFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lbFilter.AutoSize = true;
            this.lbFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbFilter.Location = new System.Drawing.Point(12, 12);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(29, 13);
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
            this.tbFilter.Location = new System.Drawing.Point(47, 9);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(347, 20);
            this.tbFilter.TabIndex = 14;
            this.tbFilter.ButtonClick += new System.EventHandler(this.tbFilter_ButtonClick);
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // pSeriesDetailsWrapper
            // 
            this.pSeriesDetailsWrapper.AutoScroll = true;
            this.pSeriesDetailsWrapper.Controls.Add(this.scSeriesDetails);
            this.pSeriesDetailsWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSeriesDetailsWrapper.Location = new System.Drawing.Point(400, 0);
            this.pSeriesDetailsWrapper.Name = "pSeriesDetailsWrapper";
            this.pSeriesDetailsWrapper.Size = new System.Drawing.Size(710, 580);
            this.pSeriesDetailsWrapper.TabIndex = 1;
            // 
            // scSeriesDetails
            // 
            this.scSeriesDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSeriesDetails.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.scSeriesDetails.Panel2.Controls.Add(this.pSeriesDetailsContainer);
            this.scSeriesDetails.Size = new System.Drawing.Size(710, 580);
            this.scSeriesDetails.SplitterDistance = 25;
            this.scSeriesDetails.TabIndex = 0;
            // 
            // pSeriesSecondToolbar
            // 
            this.pSeriesSecondToolbar.Controls.Add(this.panel3);
            this.pSeriesSecondToolbar.Controls.Add(this.panel1);
            this.pSeriesSecondToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSeriesSecondToolbar.Location = new System.Drawing.Point(0, 0);
            this.pSeriesSecondToolbar.Name = "pSeriesSecondToolbar";
            this.pSeriesSecondToolbar.Size = new System.Drawing.Size(25, 580);
            this.pSeriesSecondToolbar.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(25, 540);
            this.panel3.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImportEpisodes,
            this.btnLoadPoster,
            this.toolStripSeparator1,
            this.btnSaveChanges,
            this.btnUndo,
            this.btnRefreshEpisodeData,
            this.btnBulkEdit,
            this.toolStripSeparator2,
            this.btnDeleteSeasonEpisode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 540);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnImportEpisodes
            // 
            this.btnImportEpisodes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImportEpisodes.Enabled = false;
            this.btnImportEpisodes.Image = global::Desene.Properties.Resources.import;
            this.btnImportEpisodes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportEpisodes.Name = "btnImportEpisodes";
            this.btnImportEpisodes.Size = new System.Drawing.Size(21, 20);
            this.btnImportEpisodes.Text = "Import episodes data from files";
            this.btnImportEpisodes.Click += new System.EventHandler(this.btnImportEpisodes_Click);
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
            this.btnLoadPoster.Text = "Load Series poster";
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
            // btnRefreshEpisodeData
            // 
            this.btnRefreshEpisodeData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshEpisodeData.Enabled = false;
            this.btnRefreshEpisodeData.Image = global::Desene.Properties.Resources.refresh;
            this.btnRefreshEpisodeData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshEpisodeData.Name = "btnRefreshEpisodeData";
            this.btnRefreshEpisodeData.Size = new System.Drawing.Size(21, 20);
            this.btnRefreshEpisodeData.Text = "Refresh episode data from file";
            this.btnRefreshEpisodeData.Click += new System.EventHandler(this.btnRefreshEpisodeData_Click);
            // 
            // btnBulkEdit
            // 
            this.btnBulkEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBulkEdit.Enabled = false;
            this.btnBulkEdit.Image = global::Desene.Properties.Resources.bulkEdit;
            this.btnBulkEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBulkEdit.Name = "btnBulkEdit";
            this.btnBulkEdit.Size = new System.Drawing.Size(21, 20);
            this.btnBulkEdit.Text = "Bulk Edit";
            this.btnBulkEdit.ToolTipText = "Bulk Edit\r\n(must have more than one episode selected)";
            this.btnBulkEdit.Click += new System.EventHandler(this.BtnBulkEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(21, 6);
            // 
            // btnDeleteSeasonEpisode
            // 
            this.btnDeleteSeasonEpisode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteSeasonEpisode.Enabled = false;
            this.btnDeleteSeasonEpisode.Image = global::Desene.Properties.Resources.delete;
            this.btnDeleteSeasonEpisode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteSeasonEpisode.Name = "btnDeleteSeasonEpisode";
            this.btnDeleteSeasonEpisode.Size = new System.Drawing.Size(21, 20);
            this.btnDeleteSeasonEpisode.Text = "Delete";
            this.btnDeleteSeasonEpisode.ToolTipText = "Delete the selected Season/Episode";
            this.btnDeleteSeasonEpisode.Click += new System.EventHandler(this.btnDeleteSeasonEpisode_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(25, 40);
            this.panel1.TabIndex = 0;
            // 
            // pSeriesDetailsContainer
            // 
            this.pSeriesDetailsContainer.AutoScroll = true;
            this.pSeriesDetailsContainer.AutoSize = true;
            this.pSeriesDetailsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pSeriesDetailsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSeriesDetailsContainer.Location = new System.Drawing.Point(0, 0);
            this.pSeriesDetailsContainer.Name = "pSeriesDetailsContainer";
            this.pSeriesDetailsContainer.Size = new System.Drawing.Size(681, 580);
            this.pSeriesDetailsContainer.TabIndex = 0;
            // 
            // ucSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pSeriesDetailsWrapper);
            this.Controls.Add(this.pSeriesTreeWrapper);
            this.Name = "ucSeries";
            this.Size = new System.Drawing.Size(1110, 580);
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pSeriesTreeWrapper;
        private System.Windows.Forms.Panel panel2;
        private Aga.Controls.Tree.TreeViewAdv tvSeries;
        private System.Windows.Forms.Panel pFilters;
        private System.Windows.Forms.Label lbFilter;
        private Utils.FilterTextBox tbFilter;
        private System.Windows.Forms.Panel pSeriesDetailsWrapper;
        private System.Windows.Forms.SplitContainer scSeriesDetails;
        private System.Windows.Forms.Panel pSeriesSecondToolbar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveChanges;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton btnLoadPoster;
        private System.Windows.Forms.ToolStripButton btnRefreshEpisodeData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImportEpisodes;
        private System.Windows.Forms.ToolStripButton btnDeleteSeasonEpisode;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel pDummyMenuForShortCutKeys;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSave_ForShortCutOnly;
        private System.Windows.Forms.ToolStripMenuItem miUndo_ForShortCutOnly;
        private System.Windows.Forms.Label lbDoNotDelete;
        private System.Windows.Forms.ToolStripButton btnBulkEdit;
        private Utils.CustomPanel pSeriesDetailsContainer;
    }
}
