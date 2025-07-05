using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Desene
{
    public partial class ucSQLmanagement : UserControl
    {
        private FrmMain _parent;
        private DataTable _fileDetails;

        public ucSQLmanagement(FrmMain parent)
        {
            InitializeComponent();
            //typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            //    | BindingFlags.Instance | BindingFlags.NonPublic, null,
            //    pMovieDetailsContainer, new object[] { true });

            _parent = parent;


            ToolStripMenuItem button = new ToolStripMenuItem();

            button.Size = new Size(0, 0);

            button.ShortcutKeys = (Keys)Shortcut.F5;
            button.Click += btnExecute_Click;

            mainMenu2.Items.Add(button);
        }

        private void ucSQLmanagement_Load(object sender, EventArgs e)
        {
            var tcName = new TreeColumn { Header = "Name", Width = 250 };
            var tbName = new NodeTextBox { DataPropertyName = "Name", ParentColumn = tcName };
            tvDbStructure.Columns.Add(tcName);
            tvDbStructure.NodeControls.Add(tbName);

            var tcType = new TreeColumn { Header = "Type", Width = 150 };
            var tbType = new NodeTextBox { DataPropertyName = "ColumnType", ParentColumn = tcType };
            //tbType.Font = new Font(tbType.Font.Name, 40);
            tvDbStructure.Columns.Add(tcType);
            tvDbStructure.NodeControls.Add(tbType);


            tvDbStructure.FullRowSelect = true;
            tvDbStructure.GridLineStyle = GridLineStyle.HorizontalAndVertical;
            tvDbStructure.UseColumns = true;

            var treeModel = new DbStructureTreeModel();
            tvDbStructure.Model = treeModel;

            _parent.SetStatistics(false, "");
            _parent.SetInfo2(false, "");

            eSqlEdit.SelectionIndent = 5;

            RefreshSQLHistory();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            dvgQueryResult.DataSource = null;

            var toExecute =
                string.IsNullOrEmpty(eSqlEdit.SelectedText)
                    ? eSqlEdit.Text.Trim()
                    : eSqlEdit.SelectedText.Trim();

            if (!string.IsNullOrEmpty(toExecute))
            {
                var opRes = DAL.ExecuteSQLcommand(toExecute);

                if (!opRes.Success)
                {
                    tcResult.SelectedIndex = 1;
                    richTextBox1.Text = opRes.CustomErrorMessage;

                    _parent.SetStatistics(true, "Query completed with errors.");
                }
                else
                {
                    _parent.SetStatistics(true, "Query executed successfully.");

                    if (toExecute.ToLower().StartsWith("select"))
                    {
                        var resultList = (List<dynamic>)opRes.AdditionalDataReturn;

                        _parent.SetInfo2(true, string.Format("{0} {1}", resultList.Count, resultList.Count > 1 ? "rows" : "row"));

                        tcResult.SelectedIndex = 0;
                        //dvgQueryResult.DataSource


                        _fileDetails = Desene.DAL.ToDataTable(resultList);

                        dvgQueryResult.AutoGenerateColumns = true;
                        dvgQueryResult.DataSource = _fileDetails;
                    }
                    else
                    {
                        _parent.SetInfo2(true, "0 rows");

                        tcResult.SelectedIndex = 1;
                        richTextBox1.Text = opRes.AdditionalDataReturn.ToString();
                    }
                }

                WriteSqlExecution(toExecute, opRes.CustomErrorMessage);
            }
        }

        private void RefreshSQLHistory()
        {
            try
            {
                var sqlHistory = DAL.GetSqlHistory();
                if (sqlHistory != null && sqlHistory.Count > 0)
                {
                    dgvSQLHistory.DataSource = sqlHistory;
                    dgvSQLHistory.Columns["Id"].Visible = false;
                    dgvSQLHistory.Columns["DateTimeStamp"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    dgvSQLHistory.Columns["SQLExecuted"].HeaderText = "Execution Date";
                    dgvSQLHistory.Columns["ErrorMessage"].HeaderText = "Error Message";
                }
                else
                {
                    dgvSQLHistory.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("The following error occurred while loading the SQL history:{0}{0}{1}", Environment.NewLine, OperationResult.GetErrorMessage(ex, false)),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WriteSqlExecution(string toExecute, string errorMessage)
        {
            try
            {
                DAL.PersistExecution(toExecute, errorMessage);
                RefreshSQLHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("The following error occurred while persisting the SQL history:{0}{0}{1}", Environment.NewLine, OperationResult.GetErrorMessage(ex, false)),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
            
        private void tvDbStructure_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) return;

            var clickPoint = new Point(e.X, e.Y);
            var clickNode = tvDbStructure.GetNodeAt(clickPoint);
            if (clickNode == null || clickNode.Level > 1) return;

            // Convert from Tree coordinates to Screen coordinates    
            var screenPoint = tvDbStructure.PointToScreen(clickPoint);
            // Convert from Screen coordinates to Form coordinates    
            var formPoint = this.PointToClient(screenPoint);
            
            // Show context menu   
            pmTreeView.Show(this, formPoint);
        }

        private void pmTreeView_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "miGenerateSelect":

                    var tableName = ((DbStructureModel)tvDbStructure.SelectedNode.Tag).Name;
                    var columns = DAL.ListColumns2(tableName);
                    var selectStatement =
                        string.Format("select{0}[{1}]{0}from {2}",
                            Environment.NewLine,
                            string.Join("], [", columns.Where(x => x.ColumnType != "Byte[]").Select(x => x.Name)),
                            tableName);

                    eSqlEdit.AppendText(
                        (eSqlEdit.Text.Trim() == string.Empty ? string.Empty : (Environment.NewLine + Environment.NewLine)) +
                        selectStatement);

                    break;
            }
        }

        private void dgvSQLHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvSQLHistory.Rows[e.RowIndex];
            var textValue = row.Cells["SQLExecuted"].Value?.ToString() ?? "";
        
            eSqlEdit.Text = textValue;
        }
    }
}
