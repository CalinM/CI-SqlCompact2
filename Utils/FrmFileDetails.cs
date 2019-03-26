using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Common;
using Common.ExtensionMethods;

namespace Utils
{
    public partial class FrmFileDetails : Form
    {
        private DataTable _fileDetails;

        public FrmFileDetails()
        {
            InitializeComponent();
        }

        public FrmFileDetails(List<dynamic> fileDetails)
        {
            InitializeComponent();

            _fileDetails = ToDataTable(fileDetails);

            dgvFilesDetails.AutoGenerateColumns = false;
            dgvFilesDetails.DataSource = _fileDetails;

            var x = fileDetails.Where(x2 => x2.StartsWith("Audio"));
            foreach (DataColumn tCol in _fileDetails.Columns)
            {
                if (tCol.ColumnName == "Filename" || tCol.ColumnName == "Error" || tCol.ColumnName.StartsWith("Audio"))
                {
                    dgvFilesDetails.Columns.Add(
                        new DataGridViewTextBoxColumn
                            {
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                ValueType = typeof(string),
                                DataPropertyName = tCol.ColumnName,
                                Name = "col" + tCol.ColumnName.Replace(" ", ""),
                                HeaderText =  tCol.ColumnName,
                                Width = tCol.ColumnName == "Filename" ? 300 : tCol.ColumnName == "Error" ? 100 : 50
                            });
                }
                else
                //if (tCol.ColumnName.StartsWith("Default") || tCol.ColumnName.StartsWith("Forced"))
                {
                    dgvFilesDetails.Columns.Add(
                        new DataGridViewCheckBoxColumn
                            {
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                ValueType = typeof(bool),
                                DataPropertyName = tCol.ColumnName,
                                Name = "col" + tCol.ColumnName.Replace(" ", ""),
                                HeaderText = tCol.ColumnName,
                                Width = 50
                            });
                }
            }
        }

        private static DataTable ToDataTable(/*this*/ IEnumerable<dynamic> items)
        {
            if (!items.Any()) return null;

            var table = new DataTable { TableName = "FilesDetails" } ;

            var keyCollection = new List<string>();

            items.Cast<IDictionary<string, object>>().ToList().ForEach(x =>
                {
                    if (x.Keys.Count > keyCollection.Count)
                        keyCollection = x.Keys.ToList();
                });

            keyCollection.Select(y => table.Columns.Add(y)).ToList();

            foreach (var kv in items.Cast<IDictionary<string, object>>().ToList())
            {
                var row = table.NewRow();
                foreach (var keyName in keyCollection)
                {
                    if (kv.Keys.Contains(keyName))
                    {
                        row[keyName] = kv[keyName];
                    }
                }

                table.Rows.Add(row);
            }

            return table;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miExportToXML_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Title = "Select files details as ..";
                    saveDialog.Filter = "XML files (*.xml)|*.xml";

                    if (saveDialog.ShowDialog() != DialogResult.OK) return;

                    using (var sw = new StringWriter())
                    {
                        _fileDetails.WriteXml(sw);

                        var sr = File.CreateText(saveDialog.FileName);
			            sr.WriteLine(sw.ToString());
			            sr.Close();
                    }
                }

                MsgBox.Show("Save complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MsgBox.Show(OperationResult.GetErrorMessage(ex), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void miExportToCSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Title = "Select files details as ..";
                    saveDialog.Filter = "CSV files (*.csv)|*.csv";

                    if (saveDialog.ShowDialog() != DialogResult.OK) return;

                    using (var sw = new StringWriter())
                    {
                        _fileDetails.WriteXml(sw);

                        var sr = File.CreateText(saveDialog.FileName);
			            sr.WriteLine(_fileDetails.ToCsv());
			            sr.Close();
                    }
                }

                MsgBox.Show("Save complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MsgBox.Show(OperationResult.GetErrorMessage(ex), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
