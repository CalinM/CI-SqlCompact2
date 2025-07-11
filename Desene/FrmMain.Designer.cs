﻿using Utils;

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
            this.ddbTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.miCleanupOrphanFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheckFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportSynopsis = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportCommonSenseMediaData = new System.Windows.Forms.ToolStripMenuItem();
            this.miSQLmanagement = new System.Windows.Forms.ToolStripMenuItem();
            this.miOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.sslbStatistics = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbAdditionalInfo1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbClick = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbAdditionalInfo2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbInfo2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pMainContainer = new System.Windows.Forms.Panel();
            this.buttonEdit1 = new Utils.ButtonEdit();
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
            this.btnBuildFileNames,
            this.ddbTools});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mainMenu.Size = new System.Drawing.Size(1284, 31);
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
            this.btnCategory.Size = new System.Drawing.Size(68, 28);
            this.btnCategory.Text = "Category";
            // 
            // miMovies
            // 
            this.miMovies.Name = "miMovies";
            this.miMovies.Size = new System.Drawing.Size(133, 22);
            this.miMovies.Tag = "1";
            this.miMovies.Text = "Movies";
            this.miMovies.Click += new System.EventHandler(this.miMovies_Click);
            // 
            // miMoviesList
            // 
            this.miMoviesList.Image = global::Desene.Properties.Resources.moviesList;
            this.miMoviesList.Name = "miMoviesList";
            this.miMoviesList.Size = new System.Drawing.Size(133, 22);
            this.miMoviesList.Tag = "1";
            this.miMoviesList.Text = "Movies list";
            this.miMoviesList.Click += new System.EventHandler(this.miMoviesList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // miCollections
            // 
            this.miCollections.Name = "miCollections";
            this.miCollections.Size = new System.Drawing.Size(133, 22);
            this.miCollections.Tag = "1";
            this.miCollections.Text = "Collections";
            this.miCollections.Click += new System.EventHandler(this.miCollections_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(130, 6);
            // 
            // miSeries
            // 
            this.miSeries.Name = "miSeries";
            this.miSeries.Size = new System.Drawing.Size(133, 22);
            this.miSeries.Tag = "1";
            this.miSeries.Text = "Series";
            this.miSeries.Click += new System.EventHandler(this.miSeries_Click);
            // 
            // miRecordings
            // 
            this.miRecordings.Name = "miRecordings";
            this.miRecordings.Size = new System.Drawing.Size(133, 22);
            this.miRecordings.Tag = "1";
            this.miRecordings.Text = "Recordings";
            this.miRecordings.Click += new System.EventHandler(this.miRecordings_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(130, 6);
            // 
            // miExit
            // 
            this.miExit.Image = global::Desene.Properties.Resources.exit;
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(133, 22);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // separatorMainButtons
            // 
            this.separatorMainButtons.Name = "separatorMainButtons";
            this.separatorMainButtons.Size = new System.Drawing.Size(6, 31);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Desene.Properties.Resources.add;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 28);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::Desene.Properties.Resources.delete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete movie/series";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
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
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(67, 28);
            this.toolStripDropDownButton1.Text = "Generate";
            // 
            // btnGenerateHtml
            // 
            this.btnGenerateHtml.Image = global::Desene.Properties.Resources.generateHtml;
            this.btnGenerateHtml.Name = "btnGenerateHtml";
            this.btnGenerateHtml.Size = new System.Drawing.Size(156, 22);
            this.btnGenerateHtml.Text = "Generate HTML";
            this.btnGenerateHtml.Click += new System.EventHandler(this.btnGenerateHtml_Click);
            // 
            // miGeneratePdf
            // 
            this.miGeneratePdf.Image = global::Desene.Properties.Resources.generatePdf;
            this.miGeneratePdf.Name = "miGeneratePdf";
            this.miGeneratePdf.Size = new System.Drawing.Size(156, 22);
            this.miGeneratePdf.Text = "Generate PDF";
            this.miGeneratePdf.Click += new System.EventHandler(this.btnGenerateCatalog_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // btnFilesDetails
            // 
            this.btnFilesDetails.Image = global::Desene.Properties.Resources.info1;
            this.btnFilesDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilesDetails.Name = "btnFilesDetails";
            this.btnFilesDetails.Size = new System.Drawing.Size(95, 28);
            this.btnFilesDetails.Text = "Files details";
            this.btnFilesDetails.Click += new System.EventHandler(this.btnFilesDetails_Click);
            // 
            // btnBuildFileNames
            // 
            this.btnBuildFileNames.Image = global::Desene.Properties.Resources.combineNames;
            this.btnBuildFileNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuildFileNames.Name = "btnBuildFileNames";
            this.btnBuildFileNames.Size = new System.Drawing.Size(155, 28);
            this.btnBuildFileNames.Text = "Mix Netflix files names";
            this.btnBuildFileNames.Click += new System.EventHandler(this.BtnBuildFileNames_Click);
            // 
            // ddbTools
            // 
            this.ddbTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCleanupOrphanFiles,
            this.miCheckFiles,
            this.miImportSynopsis,
            this.miImportCommonSenseMediaData,
            this.miSQLmanagement,
            this.miOptions});
            this.ddbTools.Image = global::Desene.Properties.Resources.tools;
            this.ddbTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbTools.Name = "ddbTools";
            this.ddbTools.Size = new System.Drawing.Size(71, 28);
            this.ddbTools.Text = "Tools";
            // 
            // miCleanupOrphanFiles
            // 
            this.miCleanupOrphanFiles.Name = "miCleanupOrphanFiles";
            this.miCleanupOrphanFiles.Size = new System.Drawing.Size(279, 30);
            this.miCleanupOrphanFiles.Text = "Cleanup orphan files";
            this.miCleanupOrphanFiles.Click += new System.EventHandler(this.miCleanupOrphanFiles_Click);
            // 
            // miCheckFiles
            // 
            this.miCheckFiles.Image = global::Desene.Properties.Resources.CheckFileExistence;
            this.miCheckFiles.Name = "miCheckFiles";
            this.miCheckFiles.Size = new System.Drawing.Size(279, 30);
            this.miCheckFiles.Text = "Check file existence";
            this.miCheckFiles.Click += new System.EventHandler(this.btnCheckFiles_Click);
            // 
            // miImportSynopsis
            // 
            this.miImportSynopsis.Image = global::Desene.Properties.Resources.accessories_text_editor_icon;
            this.miImportSynopsis.Name = "miImportSynopsis";
            this.miImportSynopsis.Size = new System.Drawing.Size(279, 30);
            this.miImportSynopsis.Text = "Import synopsis from description link";
            this.miImportSynopsis.Click += new System.EventHandler(this.btnImportSynopsis_Click);
            // 
            // miImportCommonSenseMediaData
            // 
            this.miImportCommonSenseMediaData.Name = "miImportCommonSenseMediaData";
            this.miImportCommonSenseMediaData.Size = new System.Drawing.Size(279, 30);
            this.miImportCommonSenseMediaData.Text = "Import CommonSenseMedia data";
            this.miImportCommonSenseMediaData.Click += new System.EventHandler(this.miImportCommonSenseMediaData_Click);
            // 
            // miSQLmanagement
            // 
            this.miSQLmanagement.Name = "miSQLmanagement";
            this.miSQLmanagement.Size = new System.Drawing.Size(279, 30);
            this.miSQLmanagement.Text = "SQL Management";
            this.miSQLmanagement.Click += new System.EventHandler(this.miSQLmanagement_Click);
            // 
            // miOptions
            // 
            this.miOptions.Name = "miOptions";
            this.miOptions.Size = new System.Drawing.Size(279, 30);
            this.miOptions.Text = "Options";
            this.miOptions.Visible = false;
            this.miOptions.Click += new System.EventHandler(this.miOptions_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslbStatistics,
            this.sslbAdditionalInfo1,
            this.sslbClick,
            this.sslbAdditionalInfo2,
            this.toolStripStatusLabel1,
            this.sslbInfo2});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1284, 22);
            this.statusStrip.TabIndex = 109;
            this.statusStrip.Text = "statusStrip1";
            // 
            // sslbStatistics
            // 
            this.sslbStatistics.Name = "sslbStatistics";
            this.sslbStatistics.Size = new System.Drawing.Size(0, 17);
            // 
            // sslbAdditionalInfo1
            // 
            this.sslbAdditionalInfo1.Margin = new System.Windows.Forms.Padding(-3, 3, 0, 2);
            this.sslbAdditionalInfo1.Name = "sslbAdditionalInfo1";
            this.sslbAdditionalInfo1.Size = new System.Drawing.Size(33, 17);
            this.sslbAdditionalInfo1.Text = "Click";
            this.sslbAdditionalInfo1.Visible = false;
            // 
            // sslbClick
            // 
            this.sslbClick.IsLink = true;
            this.sslbClick.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.sslbClick.Margin = new System.Windows.Forms.Padding(-3, 3, 0, 2);
            this.sslbClick.Name = "sslbClick";
            this.sslbClick.Size = new System.Drawing.Size(30, 17);
            this.sslbClick.Text = "here";
            this.sslbClick.Visible = false;
            this.sslbClick.Click += new System.EventHandler(this.sslbClick_Click);
            // 
            // sslbAdditionalInfo2
            // 
            this.sslbAdditionalInfo2.Margin = new System.Windows.Forms.Padding(-3, 3, 0, 2);
            this.sslbAdditionalInfo2.Name = "sslbAdditionalInfo2";
            this.sslbAdditionalInfo2.Size = new System.Drawing.Size(71, 17);
            this.sslbAdditionalInfo2.Text = "for details ...";
            this.sslbAdditionalInfo2.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1269, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // sslbInfo2
            // 
            this.sslbInfo2.Name = "sslbInfo2";
            this.sslbInfo2.Size = new System.Drawing.Size(0, 17);
            // 
            // pMainContainer
            // 
            this.pMainContainer.Controls.Add(this.buttonEdit1);
            this.pMainContainer.Controls.Add(this.button5);
            this.pMainContainer.Controls.Add(this.button3);
            this.pMainContainer.Controls.Add(this.button4);
            this.pMainContainer.Controls.Add(this.button2);
            this.pMainContainer.Controls.Add(this.button1);
            this.pMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainContainer.Location = new System.Drawing.Point(0, 31);
            this.pMainContainer.Name = "pMainContainer";
            this.pMainContainer.Size = new System.Drawing.Size(1284, 508);
            this.pMainContainer.TabIndex = 110;
            this.pMainContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.PMainContainer_Paint);
            this.pMainContainer.Resize += new System.EventHandler(this.PMainContainer_Resize);
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.ButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEdit1.ButtonImage = global::Desene.Properties.Resources.search1;
            this.buttonEdit1.ButtonImageForceWidth = 16;
            this.buttonEdit1.ButtonImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonEdit1.ButtonToolTip = "";
            this.buttonEdit1.ButtonVisible = true;
            this.buttonEdit1.Location = new System.Drawing.Point(456, 352);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Size = new System.Drawing.Size(244, 20);
            this.buttonEdit1.TabIndex = 113;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(855, 168);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(141, 23);
            this.button5.TabIndex = 112;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(676, 295);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 111;
            this.button3.Text = "Tests";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(855, 129);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 23);
            this.button4.TabIndex = 110;
            this.button4.Text = "Convert Nl Audio";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(855, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 28);
            this.button2.TabIndex = 108;
            this.button2.Text = "Import Old Series";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(855, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 28);
            this.button1.TabIndex = 107;
            this.button1.Text = "Import Old HD Movies";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.pMainContainer);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1296, 588);
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
            this.pMainContainer.PerformLayout();
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
        private ButtonEdit buttonEdit1;
        private System.Windows.Forms.ToolStripDropDownButton ddbTools;
        private System.Windows.Forms.ToolStripMenuItem miCheckFiles;
        private System.Windows.Forms.ToolStripMenuItem miImportSynopsis;
        private System.Windows.Forms.ToolStripMenuItem miImportCommonSenseMediaData;
        private System.Windows.Forms.ToolStripMenuItem miSQLmanagement;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel sslbInfo2;
        private System.Windows.Forms.ToolStripMenuItem miOptions;
        private System.Windows.Forms.ToolStripMenuItem miCleanupOrphanFiles;
    }
}

