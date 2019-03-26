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
            this.miSeries = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMoviesList = new System.Windows.Forms.ToolStripButton();
            this.separatorMainButtons = new System.Windows.Forms.ToolStripSeparator();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerateHtml = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFilesDetails = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.sslbStatistics = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbAdditionalInfo1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbClick = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbAdditionalInfo2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pMainContainer = new System.Windows.Forms.Panel();
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
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCategory,
            this.btnMoviesList,
            this.separatorMainButtons,
            this.btnAdd,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnGenerateHtml,
            this.toolStripSeparator3,
            this.btnFilesDetails});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mainMenu.Size = new System.Drawing.Size(1926, 35);
            this.mainMenu.TabIndex = 108;
            // 
            // btnCategory
            // 
            this.btnCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCategory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMovies,
            this.miSeries,
            this.toolStripMenuItem1,
            this.miExit});
            this.btnCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnCategory.Image")));
            this.btnCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(102, 32);
            this.btnCategory.Text = "Category";
            // 
            // miMovies
            // 
            this.miMovies.Name = "miMovies";
            this.miMovies.Size = new System.Drawing.Size(153, 30);
            this.miMovies.Tag = "1";
            this.miMovies.Text = "Movies";
            this.miMovies.Click += new System.EventHandler(this.miMovies_Click);
            // 
            // miSeries
            // 
            this.miSeries.Name = "miSeries";
            this.miSeries.Size = new System.Drawing.Size(153, 30);
            this.miSeries.Tag = "1";
            this.miSeries.Text = "Series";
            this.miSeries.Click += new System.EventHandler(this.miSeries_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 6);
            // 
            // miExit
            // 
            this.miExit.Image = global::Desene.Properties.Resources.exit;
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(153, 30);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // btnMoviesList
            // 
            this.btnMoviesList.Image = global::Desene.Properties.Resources.moviesList;
            this.btnMoviesList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoviesList.Name = "btnMoviesList";
            this.btnMoviesList.Size = new System.Drawing.Size(128, 32);
            this.btnMoviesList.Text = "Movies list";
            this.btnMoviesList.Click += new System.EventHandler(this.btnMoviesList_Click);
            // 
            // separatorMainButtons
            // 
            this.separatorMainButtons.Name = "separatorMainButtons";
            this.separatorMainButtons.Size = new System.Drawing.Size(6, 35);
            this.separatorMainButtons.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Desene.Properties.Resources.add;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 32);
            this.btnAdd.Text = "Add";
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::Desene.Properties.Resources.delete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 32);
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete movie/series";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // btnGenerateHtml
            // 
            this.btnGenerateHtml.Image = global::Desene.Properties.Resources.generateHtml;
            this.btnGenerateHtml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerateHtml.Name = "btnGenerateHtml";
            this.btnGenerateHtml.Size = new System.Drawing.Size(165, 32);
            this.btnGenerateHtml.Text = "Generate HTML";
            this.btnGenerateHtml.Click += new System.EventHandler(this.btnGenerateHtml_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 35);
            // 
            // btnFilesDetails
            // 
            this.btnFilesDetails.Image = global::Desene.Properties.Resources.info1;
            this.btnFilesDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilesDetails.Name = "btnFilesDetails";
            this.btnFilesDetails.Size = new System.Drawing.Size(134, 32);
            this.btnFilesDetails.Text = "Files details";
            this.btnFilesDetails.Click += new System.EventHandler(this.btnFilesDetails_Click);
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
            this.sslbStatistics.Size = new System.Drawing.Size(0, 17);
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
            this.pMainContainer.Controls.Add(this.button3);
            this.pMainContainer.Controls.Add(this.button4);
            this.pMainContainer.Controls.Add(this.button2);
            this.pMainContainer.Controls.Add(this.button1);
            this.pMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainContainer.Location = new System.Drawing.Point(0, 35);
            this.pMainContainer.Margin = new System.Windows.Forms.Padding(5);
            this.pMainContainer.Name = "pMainContainer";
            this.pMainContainer.Size = new System.Drawing.Size(1926, 806);
            this.pMainContainer.TabIndex = 110;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1014, 454);
            this.button3.Margin = new System.Windows.Forms.Padding(5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 35);
            this.button3.TabIndex = 111;
            this.button3.Text = "Tests";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(1283, 198);
            this.button4.Margin = new System.Windows.Forms.Padding(5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(211, 35);
            this.button4.TabIndex = 110;
            this.button4.Text = "Convert Nl Audio";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1283, 125);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 43);
            this.button2.TabIndex = 108;
            this.button2.Text = "Import Old Series";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1283, 55);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 43);
            this.button1.TabIndex = 107;
            this.button1.Text = "Import Old HD Movies";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1926, 863);
            this.Controls.Add(this.pMainContainer);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.statusStrip);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(1937, 888);
            this.Name = "FrmMain";
            this.Text = "CartoonsRepo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
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
        private System.Windows.Forms.ToolStripButton btnGenerateHtml;
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
        private System.Windows.Forms.ToolStripButton btnMoviesList;
        private System.Windows.Forms.ToolStripButton btnFilesDetails;
    }
}

