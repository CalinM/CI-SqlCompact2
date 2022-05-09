using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Common;
using Common.ExtensionMethods;

namespace Utils
{
    public partial class FrmFileDetails : EscapeForm
    {
        private DataTable _fileDetails;

        public FrmFileDetails()
        {
            InitializeComponent();
        }

        public FrmFileDetails(List<dynamic> fileDetails)
        {
            InitializeComponent();

            //for an operation started by a drag & drop action, the parent is unknown
            this.Owner = (Form)(FromHandle(Common.Helpers.MainFormHandle));

            var dgvType = dgvFilesDetails.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgvFilesDetails, true, null);

            _fileDetails = Desene.DAL.ToDataTable(fileDetails);

            dgvFilesDetails.AutoGenerateColumns = false;
            dgvFilesDetails.DataSource = _fileDetails;

            var x = fileDetails.Where(x2 => x2.StartsWith("Audio"));
            foreach (DataColumn tCol in _fileDetails.Columns)
            {
                var colWidth = 50;

                switch (tCol.ColumnName)
                {
                    case "Filename":
                        colWidth = 300;
                        break;

                    case "Resolution":
                        colWidth = 70;
                        break;

                   case "BitRate":
                        colWidth = 120;
                        break;
                }

                if (tCol.ColumnName == "Filename"
                    || tCol.ColumnName == "Resolution"
                    || tCol.ColumnName == "BitRate"
                    || tCol.ColumnName == "Error" //no longer provided here
                    || tCol.ColumnName.StartsWith("Audio")
                    || tCol.ColumnName.StartsWith("Channels"))
                {
                    dgvFilesDetails.Columns.Add(
                        new DataGridViewTextBoxColumn
                        {
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                            ValueType = typeof(string),
                            DataPropertyName = tCol.ColumnName,
                            Name = "col" + tCol.ColumnName.Replace(" ", ""),
                            HeaderText = tCol.ColumnName,
                            Width = colWidth
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

        private void DgvFilesDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (dgvFilesDetails.Columns[e.ColumnIndex].DataPropertyName == "BitRate")
            //{
            //    SetDGVTextBoxColumnWidth(dgvFilesDetails.Columns[e.ColumnIndex] as DataGridViewTextBoxColumn);
            //}

            var numericPartOfFieldName = new string(dgvFilesDetails.Columns[e.ColumnIndex].DataPropertyName.Where(c => char.IsDigit(c)).ToArray());

            if (numericPartOfFieldName.Length == 0) return;

            int numericPartOfFieldName2 = -1;
            if (int.TryParse(numericPartOfFieldName, out numericPartOfFieldName2))
            {
                e.CellStyle.BackColor =
                    numericPartOfFieldName2 % 2 == 0 //even
                        ? System.Drawing.Color.White
                        : System.Drawing.Color.FromArgb(229, 228, 228);
            }
        }

        //private void SetDGVTextBoxColumnWidth(DataGridViewTextBoxColumn column)
        //{
        //    if (column != null)
        //    {
        //        DataGridView dgv = column.DataGridView;
        //        Graphics g = dgv.CreateGraphics();
        //        Font font = dgv.Font;

        //        // Acquire all the relevant cells - the whole column's collection of cells:
        //        List<DataGridViewTextBoxCell> cells = new List<DataGridViewTextBoxCell>();
        //        foreach (DataGridViewRow row in column.DataGridView.Rows)
        //        {
        //            cells.Add(row.Cells[column.Index] as DataGridViewTextBoxCell);
        //        }

        //        // Now find the widest cell:
        //        int widestCellWidth = g.MeasureString(column.HeaderText, font).ToSize().Width;  // Start with the header text, but for some reason this seems a bit short.
        //        bool foundNewline = false;
        //        foreach (DataGridViewTextBoxCell cell in cells)
        //        {
        //            font = ((cell.Style.Font != null) ? cell.Style.Font : dgv.Font);  // The font may change between cells.
        //            string cellText = cell.Value.ToString().Replace("\r", "");  // Ignore any carriage return characters.  
        //            //if (cellText.Contains('\n'))
        //            if (cellText.Contains(" - "))
        //            {
        //                foundNewline = true;
        //                cell.Style.WrapMode = DataGridViewTriState.True;  // This allows newlines in the cell's text to be recognised.

        //                string[] lines = cellText.Split(new[] { " - " }, StringSplitOptions.None);
        //                foreach (string line in lines)
        //                {
        //                    int textWidth = g.MeasureString(line + "_", font).ToSize().Width;  // A simple way to ensure that there is room for this text.
        //                    widestCellWidth = Math.Max(widestCellWidth, textWidth);
        //                }
        //            }
        //            else
        //            {
        //                int textWidth = g.MeasureString(cellText + "_", font).ToSize().Width;
        //                widestCellWidth = Math.Max(widestCellWidth, textWidth);
        //            }
        //        }
        //        if (foundNewline)
        //        {
        //            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;  // Allows us to programatically modify the column width.
        //            column.Width = widestCellWidth;  // Simply set the desired width.
        //        }
        //        else
        //        {
        //            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;  // Allow the system to do the work for us.  This does a better job with cell headers.
        //        }
        //        column.Resizable = DataGridViewTriState.False;  // We don't wish the User to modify the width of this column manually.
        //    }
        //}
    }
}
