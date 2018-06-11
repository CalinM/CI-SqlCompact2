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
            this.tvSeries = new Aga.Controls.Tree.TreeViewAdv();
            this.pFilters = new System.Windows.Forms.Panel();
            this.lbFilter = new System.Windows.Forms.Label();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.pSeriesDetailsWrapper = new System.Windows.Forms.Panel();
            this.scSeriesDetails = new System.Windows.Forms.SplitContainer();
            this.pSeriesSecondToolbar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.btnLoadPoster = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pSeriesDetailsContainer = new System.Windows.Forms.Panel();
            this.pSeriesTreeWrapper.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panel2.Controls.Add(this.tvSeries);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 540);
            this.panel2.TabIndex = 107;
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
            this.tvSeries.Size = new System.Drawing.Size(379, 540);
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
            this.tbFilter.Location = new System.Drawing.Point(47, 9);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(347, 20);
            this.tbFilter.TabIndex = 14;
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
            this.btnSaveChanges,
            this.btnLoadPoster});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(32, 540);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveChanges.Image = global::Desene.Properties.Resources.save;
            this.btnSaveChanges.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(29, 20);
            this.btnSaveChanges.Text = "Save changes";
            this.btnSaveChanges.ToolTipText = "Save changes";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnLoadPoster
            // 
            this.btnLoadPoster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadPoster.Enabled = false;
            this.btnLoadPoster.Image = global::Desene.Properties.Resources.image;
            this.btnLoadPoster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadPoster.Margin = new System.Windows.Forms.Padding(0, 5, 0, 2);
            this.btnLoadPoster.Name = "btnLoadPoster";
            this.btnLoadPoster.Size = new System.Drawing.Size(29, 20);
            this.btnLoadPoster.Text = "Load poster";
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
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Panel pSeriesDetailsWrapper;
        private System.Windows.Forms.SplitContainer scSeriesDetails;
        private System.Windows.Forms.Panel pSeriesDetailsContainer;
        private System.Windows.Forms.Panel pSeriesSecondToolbar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveChanges;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton btnLoadPoster;
    }
}
