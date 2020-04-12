using Utils;

namespace Desene
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.mainMenu = new System.Windows.Forms.ToolStrip();
            this.btnCategory = new System.Windows.Forms.ToolStripDropDownButton();
            this.miMovies = new System.Windows.Forms.ToolStripMenuItem();
            this.miMoviesList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miCollections = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.miSeries = new System.Windows.Forms.ToolStripMenuItem();
            this.miRecordings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorMainButtons = new System.Windows.Forms.ToolStripSeparator();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnGenerateHtml = new System.Windows.Forms.ToolStripMenuItem();
            this.miGeneratePdf = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFilesDetails = new System.Windows.Forms.ToolStripButton();
            this.btnBuildFileNames = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.sslbStatistics = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbAdditionalInfo1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbClick = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbAdditionalInfo2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pMainContainer = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pMainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCategory,
            this.separatorMainButtons,
            this.btnAdd,
            this.btnDelete,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this.btnFilesDetails,
            this.btnBuildFileNames});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.mainMenu.Size = new System.Drawing.Size(1926, 34);
            this.mainMenu.TabIndex = 108;
            // 
            // btnCategory
            // 
            this.btnCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCategory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMovies,
            this.miMoviesList,
            this.toolStripSeparator1,
            this.miCollections,
            this.toolStripSeparator4,
            this.miSeries,
            this.miRecordings,
            this.toolStripMenuItem1,
            this.miExit});
            this.btnCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnCategory.Image")));
            this.btnCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(102, 29);
            this.btnCategory.Text = "Category";
            // 
            // miMovies
            // 
            this.miMovies.Name = "miMovies";
            this.miMovies.Size = new System.Drawing.Size(202, 34);
            this.miMovies.Tag = "1";
            this.miMovies.Text = "Movies";
            this.miMovies.Click += new System.EventHandler(this.miMovies_Click);
            // 
            // miMoviesList
            // 
            this.miMoviesList.Image = global::Desene.Properties.Resources.moviesList;
            this.miMoviesList.Name = "miMoviesList";
            this.miMoviesList.Size = new System.Drawing.Size(202, 34);
            this.miMoviesList.Tag = "1";
            this.miMoviesList.Text = "Movies list";
            this.miMoviesList.Click += new System.EventHandler(this.miMoviesList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // miCollections
            // 
            this.miCollections.Name = "miCollections";
            this.miCollections.Size = new System.Drawing.Size(202, 34);
            this.miCollections.Tag = "1";
            this.miCollections.Text = "Collections";
            this.miCollections.Click += new System.EventHandler(this.miCollections_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(199, 6);
            // 
            // miSeries
            // 
            this.miSeries.Name = "miSeries";
            this.miSeries.Size = new System.Drawing.Size(202, 34);
            this.miSeries.Tag = "1";
            this.miSeries.Text = "Series";
            this.miSeries.Click += new System.EventHandler(this.miSeries_Click);
            // 
            // miRecordings
            // 
            this.miRecordings.Name = "miRecordings";
            this.miRecordings.Size = new System.Drawing.Size(202, 34);
            this.miRecordings.Tag = "1";
            this.miRecordings.Text = "Recordings";
            this.miRecordings.Click += new System.EventHandler(this.miRecordings_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
            // 
            // miExit
            // 
            this.miExit.Image = global::Desene.Properties.Resources.exit;
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(202, 34);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // separatorMainButtons
            // 
            this.separatorMainButtons.Name = "separatorMainButtons";
            this.separatorMainButtons.Size = new System.Drawing.Size(6, 34);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Desene.Properties.Resources.add;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 29);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::Desene.Properties.Resources.delete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 29);
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete movie/series";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenerateHtml,
            this.miGeneratePdf});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(100, 29);
            this.toolStripDropDownButton1.Text = "Generate";
            // 
            // btnGenerateHtml
            // 
            this.btnGenerateHtml.Image = global::Desene.Properties.Resources.generateHtml;
            this.btnGenerateHtml.Name = "btnGenerateHtml";
            this.btnGenerateHtml.Size = new System.Drawing.Size(515, 34);
            this.btnGenerateHtml.Text = "Generate HTML";
            this.btnGenerateHtml.Click += new System.EventHandler(this.btnGenerateHtml_Click);
            // 
            // miGeneratePdf
            // 
            this.miGeneratePdf.Image = global::Desene.Properties.Resources.generatePdf;
            this.miGeneratePdf.Name = "miGeneratePdf";
            this.miGeneratePdf.Size = new System.Drawing.Size(515, 34);
            this.miGeneratePdf.Text = "Generate PDF";
            this.miGeneratePdf.Click += new System.EventHandler(this.btnGenerateCatalog_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // btnFilesDetails
            // 
            this.btnFilesDetails.Image = global::Desene.Properties.Resources.info1;
            this.btnFilesDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilesDetails.Name = "btnFilesDetails";
            this.btnFilesDetails.Size = new System.Drawing.Size(130, 29);
            this.btnFilesDetails.Text = "Files details";
            this.btnFilesDetails.Click += new System.EventHandler(this.btnFilesDetails_Click);
            // 
            // btnBuildFileNames
            // 
            this.btnBuildFileNames.Image = global::Desene.Properties.Resources.combineNames;
            this.btnBuildFileNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuildFileNames.Name = "btnBuildFileNames";
            this.btnBuildFileNames.Size = new System.Drawing.Size(216, 29);
            this.btnBuildFileNames.Text = "Mix Netflix files names";
            this.btnBuildFileNames.Click += new System.EventHandler(this.BtnBuildFileNames_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslbStatistics,
            this.sslbAdditionalInfo1,
            this.sslbClick,
            this.sslbAdditionalInfo2});
            this.statusStrip.Location = new System.Drawing.Point(0, 841);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1926, 22);
            this.statusStrip.TabIndex = 109;
            this.statusStrip.Text = "statusStrip1";
            // 
            // sslbStatistics
            // 
            this.sslbStatistics.Name = "sslbStatistics";
            this.sslbStatistics.Size = new System.Drawing.Size(0, 15);
            // 
            // sslbAdditionalInfo1
            // 
            this.sslbAdditionalInfo1.Margin = new System.Windows.Forms.Padding(-3, 3, 0, 2);
            this.sslbAdditionalInfo1.Name = "sslbAdditionalInfo1";
            this.sslbAdditionalInfo1.Size = new System.Drawing.Size(48, 25);
            this.sslbAdditionalInfo1.Text = "Click";
            this.sslbAdditionalInfo1.Visible = false;
            // 
            // sslbClick
            // 
            this.sslbClick.IsLink = true;
            this.sslbClick.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.sslbClick.Margin = new System.Windows.Forms.Padding(-3, 3, 0, 2);
            this.sslbClick.Name = "sslbClick";
            this.sslbClick.Size = new System.Drawing.Size(46, 25);
            this.sslbClick.Text = "here";
            this.sslbClick.Visible = false;
            this.sslbClick.Click += new System.EventHandler(this.sslbClick_Click);
            // 
            // sslbAdditionalInfo2
            // 
            this.sslbAdditionalInfo2.Margin = new System.Windows.Forms.Padding(-3, 3, 0, 2);
            this.sslbAdditionalInfo2.Name = "sslbAdditionalInfo2";
            this.sslbAdditionalInfo2.Size = new System.Drawing.Size(108, 25);
            this.sslbAdditionalInfo2.Text = "for details ...";
            this.sslbAdditionalInfo2.Visible = false;
            // 
            // pMainContainer
            // 
            this.pMainContainer.Controls.Add(this.button5);
            this.pMainContainer.Controls.Add(this.button3);
            this.pMainContainer.Controls.Add(this.button4);
            this.pMainContainer.Controls.Add(this.button2);
            this.pMainContainer.Controls.Add(this.button1);
            this.pMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainContainer.Location = new System.Drawing.Point(0, 34);
            this.pMainContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pMainContainer.Name = "pMainContainer";
            this.pMainContainer.Size = new System.Drawing.Size(1926, 807);
            this.pMainContainer.TabIndex = 110;
            this.pMainContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.PMainContainer_Paint);
            this.pMainContainer.Resize += new System.EventHandler(this.PMainContainer_Resize);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1282, 258);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(212, 35);
            this.button5.TabIndex = 112;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1014, 454);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 35);
            this.button3.TabIndex = 111;
            this.button3.Text = "Tests";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(1282, 198);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(212, 35);
            this.button4.TabIndex = 110;
            this.button4.Text = "Convert Nl Audio";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1282, 125);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(212, 43);
            this.button2.TabIndex = 108;
            this.button2.Text = "Import Old Series";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1282, 55);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 43);
            this.button1.TabIndex = 107;
            this.button1.Text = "Import Old HD Movies";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1926, 863);
            this.Controls.Add(this.pMainContainer);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1939, 893);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CartoonsRepo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResizeBegin += new System.EventHandler(this.FrmMain_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.FrmMain_ResizeEnd);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragEnter);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pMainContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip mainMenu;
        private System.Windows.Forms.ToolStripDropDownButton btnCategory;
        private System.Windows.Forms.ToolStripMenuItem miMovies;
        private System.Windows.Forms.ToolStripMenuItem miSeries;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripSeparator separatorMainButtons;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel pMainContainer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel sslbStatistics;
        private System.Windows.Forms.ToolStripStatusLabel sslbClick;
        private System.Windows.Forms.ToolStripStatusLabel sslbAdditionalInfo1;
        private System.Windows.Forms.ToolStripStatusLabel sslbAdditionalInfo2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripButton btnFilesDetails;
        private System.Windows.Forms.ToolStripButton btnBuildFileNames;
        private System.Windows.Forms.ToolStripMenuItem miCollections;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem btnGenerateHtml;
        private System.Windows.Forms.ToolStripMenuItem miGeneratePdf;
        private System.Windows.Forms.ToolStripMenuItem miMoviesList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem miRecordings;
        private System.Windows.Forms.Button button5;
    }
}

