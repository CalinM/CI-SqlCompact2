namespace Desene
{
    partial class ucSQLmanagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pSqlManagement = new System.Windows.Forms.SplitContainer();
            this.tvDbStructure = new Aga.Controls.Tree.TreeViewAdv();
            this.pSeparator_Caption = new System.Windows.Forms.Panel();
            this.lbSeriesEpisodesCaption = new System.Windows.Forms.Label();
            this.customPanel1 = new Utils.CustomPanel();
            this.scSQLwork = new System.Windows.Forms.SplitContainer();
            this.eSqlEdit = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lLine1 = new System.Windows.Forms.Label();
            this.lLine2 = new System.Windows.Forms.Label();
            this.tcResult = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dvgQueryResult = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.mainMenu2 = new System.Windows.Forms.ToolStrip();
            this.btnExecute = new System.Windows.Forms.ToolStripButton();
            this.pmTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miGenerateSelect = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pSqlManagement)).BeginInit();
            this.pSqlManagement.Panel1.SuspendLayout();
            this.pSqlManagement.Panel2.SuspendLayout();
            this.pSqlManagement.SuspendLayout();
            this.pSeparator_Caption.SuspendLayout();
            this.customPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSQLwork)).BeginInit();
            this.scSQLwork.Panel1.SuspendLayout();
            this.scSQLwork.Panel2.SuspendLayout();
            this.scSQLwork.SuspendLayout();
            this.tcResult.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgQueryResult)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.mainMenu2.SuspendLayout();
            this.pmTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // pSqlManagement
            // 
            this.pSqlManagement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pSqlManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSqlManagement.Location = new System.Drawing.Point(0, 0);
            this.pSqlManagement.Name = "pSqlManagement";
            // 
            // pSqlManagement.Panel1
            // 
            this.pSqlManagement.Panel1.Controls.Add(this.tvDbStructure);
            this.pSqlManagement.Panel1.Controls.Add(this.pSeparator_Caption);
            // 
            // pSqlManagement.Panel2
            // 
            this.pSqlManagement.Panel2.Controls.Add(this.customPanel1);
            this.pSqlManagement.Size = new System.Drawing.Size(939, 514);
            this.pSqlManagement.SplitterDistance = 313;
            this.pSqlManagement.TabIndex = 0;
            // 
            // tvDbStructure
            // 
            this.tvDbStructure.BackColor = System.Drawing.SystemColors.Window;
            this.tvDbStructure.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvDbStructure.DefaultToolTipProvider = null;
            this.tvDbStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDbStructure.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvDbStructure.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvDbStructure.Location = new System.Drawing.Point(0, 25);
            this.tvDbStructure.Model = null;
            this.tvDbStructure.Name = "tvDbStructure";
            this.tvDbStructure.SelectedNode = null;
            this.tvDbStructure.Size = new System.Drawing.Size(311, 487);
            this.tvDbStructure.TabIndex = 111;
            this.tvDbStructure.Text = "treeViewAdv1";
            this.tvDbStructure.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvDbStructure_MouseUp);
            // 
            // pSeparator_Caption
            // 
            this.pSeparator_Caption.BackColor = System.Drawing.SystemColors.Control;
            this.pSeparator_Caption.Controls.Add(this.lbSeriesEpisodesCaption);
            this.pSeparator_Caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSeparator_Caption.ForeColor = System.Drawing.Color.Cornsilk;
            this.pSeparator_Caption.Location = new System.Drawing.Point(0, 0);
            this.pSeparator_Caption.Name = "pSeparator_Caption";
            this.pSeparator_Caption.Size = new System.Drawing.Size(311, 25);
            this.pSeparator_Caption.TabIndex = 1;
            // 
            // lbSeriesEpisodesCaption
            // 
            this.lbSeriesEpisodesCaption.AutoSize = true;
            this.lbSeriesEpisodesCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeriesEpisodesCaption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbSeriesEpisodesCaption.Location = new System.Drawing.Point(9, 6);
            this.lbSeriesEpisodesCaption.Name = "lbSeriesEpisodesCaption";
            this.lbSeriesEpisodesCaption.Size = new System.Drawing.Size(45, 13);
            this.lbSeriesEpisodesCaption.TabIndex = 0;
            this.lbSeriesEpisodesCaption.Text = "Tables";
            // 
            // customPanel1
            // 
            this.customPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.customPanel1.Controls.Add(this.scSQLwork);
            this.customPanel1.Controls.Add(this.mainMenu2);
            this.customPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customPanel1.Location = new System.Drawing.Point(0, 0);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(620, 512);
            this.customPanel1.TabIndex = 1;
            // 
            // scSQLwork
            // 
            this.scSQLwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSQLwork.Location = new System.Drawing.Point(0, 25);
            this.scSQLwork.Name = "scSQLwork";
            this.scSQLwork.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scSQLwork.Panel1
            // 
            this.scSQLwork.Panel1.Controls.Add(this.eSqlEdit);
            this.scSQLwork.Panel1.Controls.Add(this.label1);
            this.scSQLwork.Panel1.Controls.Add(this.lLine1);
            // 
            // scSQLwork.Panel2
            // 
            this.scSQLwork.Panel2.Controls.Add(this.lLine2);
            this.scSQLwork.Panel2.Controls.Add(this.tcResult);
            this.scSQLwork.Size = new System.Drawing.Size(620, 487);
            this.scSQLwork.SplitterDistance = 243;
            this.scSQLwork.TabIndex = 112;
            // 
            // eSqlEdit
            // 
            this.eSqlEdit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.eSqlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eSqlEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eSqlEdit.HideSelection = false;
            this.eSqlEdit.Location = new System.Drawing.Point(0, 5);
            this.eSqlEdit.Name = "eSqlEdit";
            this.eSqlEdit.Size = new System.Drawing.Size(620, 237);
            this.eSqlEdit.TabIndex = 114;
            this.eSqlEdit.Text = "";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(620, 5);
            this.label1.TabIndex = 113;
            // 
            // lLine1
            // 
            this.lLine1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lLine1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lLine1.Location = new System.Drawing.Point(0, 242);
            this.lLine1.Name = "lLine1";
            this.lLine1.Size = new System.Drawing.Size(620, 1);
            this.lLine1.TabIndex = 112;
            this.lLine1.Text = "label1";
            // 
            // lLine2
            // 
            this.lLine2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lLine2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lLine2.Location = new System.Drawing.Point(0, 0);
            this.lLine2.Name = "lLine2";
            this.lLine2.Size = new System.Drawing.Size(620, 1);
            this.lLine2.TabIndex = 113;
            this.lLine2.Text = "label1";
            // 
            // tcResult
            // 
            this.tcResult.Controls.Add(this.tabPage1);
            this.tcResult.Controls.Add(this.tabPage2);
            this.tcResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcResult.Location = new System.Drawing.Point(0, 0);
            this.tcResult.Name = "tcResult";
            this.tcResult.SelectedIndex = 0;
            this.tcResult.Size = new System.Drawing.Size(620, 240);
            this.tcResult.TabIndex = 112;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dvgQueryResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 214);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Result";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dvgQueryResult
            // 
            this.dvgQueryResult.AllowUserToAddRows = false;
            this.dvgQueryResult.AllowUserToDeleteRows = false;
            this.dvgQueryResult.AllowUserToResizeRows = false;
            this.dvgQueryResult.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgQueryResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dvgQueryResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgQueryResult.DefaultCellStyle = dataGridViewCellStyle8;
            this.dvgQueryResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgQueryResult.Location = new System.Drawing.Point(3, 3);
            this.dvgQueryResult.Name = "dvgQueryResult";
            this.dvgQueryResult.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgQueryResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dvgQueryResult.RowHeadersVisible = false;
            this.dvgQueryResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dvgQueryResult.Size = new System.Drawing.Size(606, 208);
            this.dvgQueryResult.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 214);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Messages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(606, 208);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // mainMenu2
            // 
            this.mainMenu2.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainMenu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExecute});
            this.mainMenu2.Location = new System.Drawing.Point(0, 0);
            this.mainMenu2.Name = "mainMenu2";
            this.mainMenu2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mainMenu2.Size = new System.Drawing.Size(620, 25);
            this.mainMenu2.TabIndex = 109;
            // 
            // btnExecute
            // 
            this.btnExecute.Image = global::Desene.Properties.Resources.play;
            this.btnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(68, 22);
            this.btnExecute.Text = "Execute";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // pmTreeView
            // 
            this.pmTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miGenerateSelect});
            this.pmTreeView.Name = "pmTreeView";
            this.pmTreeView.Size = new System.Drawing.Size(181, 48);
            this.pmTreeView.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.pmTreeView_ItemClicked);
            // 
            // miGenerateSelect
            // 
            this.miGenerateSelect.Name = "miGenerateSelect";
            this.miGenerateSelect.Size = new System.Drawing.Size(154, 22);
            this.miGenerateSelect.Text = "Generate select";
            // 
            // ucSQLmanagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pSqlManagement);
            this.Name = "ucSQLmanagement";
            this.Size = new System.Drawing.Size(939, 514);
            this.Load += new System.EventHandler(this.ucSQLmanagement_Load);
            this.pSqlManagement.Panel1.ResumeLayout(false);
            this.pSqlManagement.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pSqlManagement)).EndInit();
            this.pSqlManagement.ResumeLayout(false);
            this.pSeparator_Caption.ResumeLayout(false);
            this.pSeparator_Caption.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.scSQLwork.Panel1.ResumeLayout(false);
            this.scSQLwork.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scSQLwork)).EndInit();
            this.scSQLwork.ResumeLayout(false);
            this.tcResult.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgQueryResult)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.mainMenu2.ResumeLayout(false);
            this.mainMenu2.PerformLayout();
            this.pmTreeView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer pSqlManagement;
        private System.Windows.Forms.Panel pSeparator_Caption;
        private System.Windows.Forms.Label lbSeriesEpisodesCaption;
        private Utils.CustomPanel customPanel1;
        private System.Windows.Forms.ToolStrip mainMenu2;
        private System.Windows.Forms.ToolStripButton btnExecute;
        private Aga.Controls.Tree.TreeViewAdv tvDbStructure;
        private System.Windows.Forms.SplitContainer scSQLwork;
        private System.Windows.Forms.TabControl tcResult;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dvgQueryResult;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lLine1;
        private System.Windows.Forms.Label lLine2;
        private System.Windows.Forms.RichTextBox eSqlEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip pmTreeView;
        private System.Windows.Forms.ToolStripMenuItem miGenerateSelect;
    }
}
