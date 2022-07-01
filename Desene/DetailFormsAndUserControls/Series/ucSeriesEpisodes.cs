using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Utils;

namespace Desene.DetailFormsAndUserControls
{
    public partial class ucSeriesEpisodes : UserControl
    {
        private BindingSource _bsEpisodesGridData;
        private ucSeries _parent;
        private Dictionary<int, bool> _checkState;
        private CheckBox _headerColCheckBox;
        private int _lastSelectedRowIndex = -1;
        private bool _forceHeightCalculation = true;
        private List<EpisodeTechnicalDetails> _episodesInSeries;
        private List<EpisodeTechnicalDetails> _episodesInSeriesOrig;
        private Timer _genericTimer;
        private bool _preventHeaderCode = false;


        public ucSeriesEpisodes()
        {
            InitializeComponent();
        }

        public ucSeriesEpisodes(SeriesEpisodesShortInfo sesInfo, ucSeries parent)
        {
            InitializeComponent();

            _parent = parent;

            dgvEpisodes.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvEpisodes, true, null);
            dgvEpisodes.AutoGenerateColumns = false;

            _bsEpisodesGridData = new BindingSource();
            dgvEpisodes.DataSource = _bsEpisodesGridData;

            // The check box column will be virtual.
            dgvEpisodes.VirtualMode = true;

            dgvEpisodes.Columns.Insert(0, new DataGridViewCheckBoxColumn() { Width = 40, Resizable = DataGridViewTriState.False });

            _headerColCheckBox = ColumnHeaderCheckBox(dgvEpisodes);
            _headerColCheckBox.CheckedChanged += HeaderColCheckBox_CheckedChanged;

            // Initialize the dictionary that contains the boolean check state.
            _checkState = new Dictionary<int, bool>();

            LoadControls(sesInfo);
        }

        private CheckBox ColumnHeaderCheckBox(DataGridView dgv)
        {
            var result = new CheckBox
            {
                Size = new Size(15, 15),
                BackColor = Color.Transparent,

                // Reset properties
                Padding = new Padding(0),
                Margin = new Padding(0),
                Text = ""
            };

            // Add checkbox to datagrid cell
            dgv.Controls.Add(result);
            var header = dgv.Columns[0].HeaderCell;

            result.Location = new Point(
                ((header.Size.Width - result.Size.Width) / 2) + 2,
                ((header.Size.Height - result.Size.Height) / 2));

            /*
             * ContentBounds values are 0 due to missing header?
            checkbox.Location = new Point(
                        (header.ContentBounds.Left +
                         (header.ContentBounds.Right - header.ContentBounds.Left + checkbox.Size.Width)
                         /2) - 2,
                        (header.ContentBounds.Top +
                         (header.ContentBounds.Bottom - header.ContentBounds.Top + checkbox.Size.Height)
                         /2) - 2);
            */

            return result;
        }

        private void HeaderColCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_preventHeaderCode) return;

            try
            {
                dgvEpisodes.SuspendLayout();
                dgvEpisodes.CurrentCell = null; //when the grid selection is left in place, the checkbox appears not selected

                _checkState = new Dictionary<int, bool>();

                if (((CheckBox)sender).Checked)
                {
                    foreach (var episodeRow in (List<EpisodeTechnicalDetails>)_bsEpisodesGridData.DataSource)
                    {
                        //var epId = (int)episodeRow.ItemArray[0]; //0 is the checkbox column?
                        if (_checkState.ContainsKey(episodeRow.Id))
                            continue;

                        _checkState.Add(episodeRow.Id, true);
                    }
                }
            }
            finally
            {
                dgvEpisodes.Invalidate();
                dgvEpisodes.ResumeLayout();

                SetBulkEditButtonStateInParent();
            }
        }

        public void LoadControls(SeriesEpisodesShortInfo sesInfo)
        {
            _checkState = new Dictionary<int, bool>();
            _headerColCheckBox.Checked = false;

            SetBulkEditButtonStateInParent();
            _lastSelectedRowIndex = -1;

            _episodesInSeries = DAL.GetEpisodesInSeries(sesInfo);
            _episodesInSeriesOrig = null;

            if (_episodesInSeries.Count > 0)
            {
                lbSeriesEpisodesCaption.Text = string.Format("Episodes ({0})", _episodesInSeries.Count(_ => _.Id > 0));

                _bsEpisodesGridData.DataSource = _episodesInSeries;
                _bsEpisodesGridData.ResetBindings(false);

                var maxBitRateObj = _episodesInSeries.Where(s => !string.IsNullOrEmpty(s.BitRate)).OrderByDescending(s => s.BitRate.Length).FirstOrDefault();
                var bitRateColWidth =
                    maxBitRateObj == null
                        ? 70
                        : maxBitRateObj.BitRate.Length > 15
                            ? 130
                            : 70;

                dgvEpisodes.Columns["colBitRate"].Width = bitRateColWidth;

                //dgvEpisodes.ClearSelection();

                dgvEpisodes.Visible = true;
                ftbFilterEpisodes.Visible = true;
                lbNoEpisodeWarning.Visible = false;
                dgvEpisodes.Location = new Point(0, pSeparator_Caption.Height + 7);

                //on first render the height is not correct
                //selecting something will produce the desired results
                if (_forceHeightCalculation)
                {
                    SetGridHeight();
                    _forceHeightCalculation = false;
                }
            }
            else
            {
                lbSeriesEpisodesCaption.Text = "Episodes";

                dgvEpisodes.Visible = false;
                ftbFilterEpisodes.Visible = false;
                lbNoEpisodeWarning.Visible = true;
            }
        }

        private void dgvEpisodes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetGridHeight();
        }

        private void SetGridHeight()
        {
            ////https://stackoverflow.com/questions/7412098/fit-datagridview-size-to-rows-and-columnss-total-size
            //dgvEpisodes.Height = dgvEpisodes.Rows.GetRowsHeight(DataGridViewElementStates.None) + dgvEpisodes.ColumnHeadersHeight;

            //Size = new Size
            //{
            //    Height = dgvEpisodes.Height + dgvEpisodes.Location.Y + 10, //10 to have a space under grid
            //    Width = 150
            //};
        }

        private void dgvEpisodes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var mtd = (MovieTechnicalDetails)dgvEpisodes.Rows[e.RowIndex].DataBoundItem;

                if (mtd != null && mtd.Id > 0)
                {
                    _parent.TryLocateEpisodeInTree(mtd.Id);
                }
            }
        }

        private void DgvEpisodes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0 && e.RowIndex != -1 /*&& dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value != null*/)
            //{
            //    // Get the episodeId
            //    var episodeId = (int)dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value;
            //    _checkState[episodeId] = (bool)dgvEpisodes.Rows[e.RowIndex].Cells[0].Value;
            //}
        }

        private void DgvEpisodes_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            // Handle the notification that the value for a cell in the virtual column
            // is needed. Get the value from the dictionary if the key exists.
            if (_checkState.Count == 0)
                return;

            if (e.ColumnIndex == 0/* && dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value != null*/)
            {
                var episodeId = (int)dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value;
                if (_checkState.ContainsKey(episodeId))
                    e.Value = _checkState[episodeId];
                else
                    e.Value = false;
            }
        }

        private void DgvEpisodes_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            // Handle the notification that the value for a cell in the virtual column
            // needs to be pushed back to the dictionary.

            if (e.ColumnIndex == 0/* && dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value != null*/)
            {
                // Get the episodeId
                var episodeId = (int)dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value;
                var b = (bool)e.Value;

                // Add or update the checked value to the dictionary depending on if the key already exists.
                if (!_checkState.ContainsKey(episodeId))
                {
                    _checkState.Add(episodeId, b);
                }
                else
                {
                    _checkState.Remove(episodeId);
                }

                SetBulkEditButtonStateInParent();

                _preventHeaderCode = true;
                if (_checkState.Count(x => x.Value && x.Key > 0) == _episodesInSeries.Count(x => x.Id > 0))
                    _headerColCheckBox.CheckState = CheckState.Checked;
                else
                if (_checkState.Count(x => !x.Value) == _episodesInSeries.Count(x => x.Id > 0))
                    _headerColCheckBox.CheckState = CheckState.Unchecked;
                else
                    _headerColCheckBox.CheckState = CheckState.Indeterminate;
                _preventHeaderCode = false;
            }
        }

        private void SetBulkEditButtonStateInParent()
        {
            _parent.SetBulkEditButtonState(_checkState.Where(item => item.Value).Select(item => item.Key).ToList());
        }

        //MSDN says here that CellValueChanged won't fire until the cell has lost focus.
        //BUT ...
        //The CurrentCellDirtyStateChanged event commits the changes immediately when the cell is clicked.
        //You manually raise the CellValueChanged event when calling the CommitEdit method.
        private void DgvEpisodes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvEpisodes.IsCurrentCellDirty)
            {
                dgvEpisodes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvEpisodes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                var selectedIndexes = new List<int>();

                for (var i = 0; i < dgvEpisodes.Rows.Count; i++)
                {
                    if (_checkState.ContainsKey((int)dgvEpisodes.Rows[i].Cells["Id"].Value))
                        selectedIndexes.Add(i);
                }

                int fromindex = -1;
                int toIndex = -1;

                if (selectedIndexes.Any())
                {
                    var currentSelMin = selectedIndexes.Min();
                    var currentSelMax = selectedIndexes.Max();

                    if (e.RowIndex < currentSelMin)
                    {
                        fromindex = e.RowIndex;
                        toIndex = currentSelMin - 1;
                    }
                    else
                    if (e.RowIndex > currentSelMax)
                    {
                        fromindex = currentSelMax + 1;
                        toIndex = e.RowIndex;
                    }
                    else
                    {
                        fromindex = Math.Min(e.RowIndex, _lastSelectedRowIndex+1);
                        toIndex = Math.Max(e.RowIndex, _lastSelectedRowIndex-1);
                    }
                }
                else
                {
                    fromindex = 0;
                    toIndex = e.RowIndex;
                }

                _lastSelectedRowIndex = e.RowIndex;

                if (fromindex < 0 || toIndex < 0)
                {
                    MsgBox.Show("Selection range could not be determined!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    dgvEpisodes.SuspendLayout();

                    for (var i = fromindex; i <= toIndex; i++)
                    {
                        var id = (int)dgvEpisodes.Rows[i].Cells["Id"].Value;

                        if (_checkState.ContainsKey(id))
                        {
                            _checkState[id] = !_checkState[id];
                        }
                        else
                        {
                            _checkState.Add(id, true);
                        } 
                    }
                }
                finally
                {
                    dgvEpisodes.Invalidate();
                    dgvEpisodes.ResumeLayout();

                    SetBulkEditButtonStateInParent();
                }
            }
            else
            {
                _lastSelectedRowIndex = e.RowIndex;
            }
        }

        private void ftbFilterEpisodes_TextChanged(object sender, EventArgs e)
        {
            if (_genericTimer != null)
            {
                _genericTimer.Enabled = false;
                _genericTimer = null;
            }

            _genericTimer = new Timer
            {
                Interval = 1000
            };

            _genericTimer.Tick += FilterEpisodes;
            _genericTimer.Enabled = true;
        }

        private void ftbFilterEpisodes_ButtonClick(object sender, EventArgs e)
        {
            ftbFilterEpisodes.Text = "";
            ApplyFilter();
        }

        private void FilterEpisodes(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (_genericTimer != null)
            {
                _genericTimer.Enabled = false;
                _genericTimer = null;
            }

            if (!string.IsNullOrEmpty(ftbFilterEpisodes.Text))
            {
                if (_episodesInSeriesOrig == null)
                {
                    _episodesInSeriesOrig = _episodesInSeries.ToList();
                }

                _episodesInSeries =
                    _episodesInSeriesOrig
                        .Where(x => x.FileName.IndexOf(ftbFilterEpisodes.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();

                lbSeriesEpisodesCaption.Text = string.Format("Filtered episodes list ({0}/{1})", _episodesInSeries.Count, _episodesInSeriesOrig.Count);
            }
            else
            {
                _episodesInSeries = _episodesInSeriesOrig.ToList();
                lbSeriesEpisodesCaption.Text = string.Format("Episodes ({0})", _episodesInSeries.Count);
            }

            _bsEpisodesGridData.DataSource = _episodesInSeries;
            _bsEpisodesGridData.ResetBindings(false);

            //SetGridHeight();
            ///dgvEpisodes.Height = dgvEpisodes.Rows.GetRowsHeight(DataGridViewElementStates.None) + dgvEpisodes.ColumnHeadersHeight;
        }

        private void dgvEpisodes_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex > -1))
            {
                if ((int)dgvEpisodes.Rows[e.RowIndex].Cells[1].Value < 0)
                {
                    e.Paint(e.ClipBounds, DataGridViewPaintParts.Border | DataGridViewPaintParts.Background);  //put what to draw
                    e.Handled = true;   //skip rest of painting event
                }
            }
        }

        private void dgvEpisodes_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var currentRow = dgvEpisodes.Rows[e.RowIndex];

            try
            {
                Color bgColor;
                Color seltextColor;
                Color selbgColor;

                switch ((int)currentRow.Cells[1].Value)
                {
                    case -1:
                        bgColor = Color.AliceBlue;
                        selbgColor = bgColor;
                        seltextColor = Color.Black;
                        break;

                    case -2:
                        bgColor = Color.FromArgb(255, 255, 238, 198);
                        selbgColor = bgColor;
                        seltextColor = Color.Black;
                        break;

                    default:
                        bgColor = Color.White;
                        selbgColor = Color.FromArgb(255, 197, 224, 229); 
                        seltextColor = Color.Black;
                        break;
                }

                currentRow.DefaultCellStyle.BackColor = bgColor;
                //currentRow.DefaultCellStyle.Font = new Font(dgvEpisodes.Font, FontStyle.Bold);
                currentRow.DefaultCellStyle.SelectionBackColor = selbgColor;
                currentRow.DefaultCellStyle.SelectionForeColor = seltextColor;
            }
            catch (Exception exception)
            {
                //onLogs?.Invoke(exception.Message, EventArgs.Empty);
            }
        }
    }
}
