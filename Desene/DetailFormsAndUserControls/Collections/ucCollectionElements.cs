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
    public partial class ucCollectionElements : UserControl
    {
        private BindingSource _bsEpisodesGridData;
        private ucCollections _parent;
        private Dictionary<int, bool> _checkState;
        private CheckBox _headerColCheckBox;
        private int _lastSelectedRowIndex = -1;
        private bool _preventEvent = false;

        public ucCollectionElements()
        {
            InitializeComponent();
        }

        public ucCollectionElements(SeriesEpisodesShortInfo sesInfo, ucCollections parent)
        {
            InitializeComponent();

            _parent = parent;

            dgvElements.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvElements, true, null);
            dgvElements.AutoGenerateColumns = false;

            _bsEpisodesGridData = new BindingSource();
            dgvElements.DataSource = _bsEpisodesGridData;

            // The check box column will be virtual.
            dgvElements.VirtualMode = true;

            dgvElements.Columns.Insert(0, new DataGridViewCheckBoxColumn() { Width = 40, Resizable = DataGridViewTriState.False });

            _headerColCheckBox = ColumnHeaderCheckBox(dgvElements);
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
                ((header.Size.Height - result.Size.Height) / 2) - 2);

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
            if (_preventEvent) return;
            try
            {
                dgvElements.SuspendLayout();
                dgvElements.CurrentCell = null; //when the grid selection is left in place, the checkbox appears not selected

                _checkState = new Dictionary<int, bool>();

                if (((CheckBox)sender).Checked)
                {
                    foreach (var episodeRow in (List<MovieTechnicalDetails>)_bsEpisodesGridData.DataSource)
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
                dgvElements.Invalidate();
                dgvElements.ResumeLayout();

                SetBulkEditButtonStateInParent();
            }
        }

        public void LoadControls(SeriesEpisodesShortInfo sesInfo)
        {
            _checkState = new Dictionary<int, bool>();
            _headerColCheckBox.Checked = false;

            SetBulkEditButtonStateInParent();
            _lastSelectedRowIndex = -1;

            var episodesInSeries = DAL.GetCollectionElements(sesInfo);

            if (episodesInSeries.Any())
            {
                lbCollectionElementsCaption.Text = string.Format("Elements ({0})", episodesInSeries.Count);

                dgvElements.Visible = true;
                lbNoEpisodeWarning.Visible = false;

                _bsEpisodesGridData.DataSource = episodesInSeries;
                _bsEpisodesGridData.ResetBindings(false);

                //dgvEpisodes.ClearSelection();
            }
            else
            {
                lbCollectionElementsCaption.Text = "Elements";
                dgvElements.Visible = false;
                lbNoEpisodeWarning.Visible = true;
            }
        }

        private void dgvElements_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //https://stackoverflow.com/questions/7412098/fit-datagridview-size-to-rows-and-columnss-total-size
            var fullGridHeight  = dgvElements.Rows.GetRowsHeight(DataGridViewElementStates.None) + dgvElements.ColumnHeadersHeight; //dgvElements.Height

            //alternative:
            //var proposedSize = dgvElements.GetPreferredSize(new Size(0, 0));
            //dgvElements.Height = proposedSize.Height;

            //do not size the grid!

            Size = new Size
            {
                Height = fullGridHeight + dgvElements.Location.Y + 10,
                Width = 150
            };
        }

        private void dgvElements_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var rowObj = (MovieTechnicalDetails)dgvElements.Rows[e.RowIndex].DataBoundItem;

                if (rowObj != null)
                {
                    _parent.TryLocateEpisodeInTree(rowObj.Id);
                }
            }
        }

        private void dgvElements_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1 /*&& dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value != null*/)
            {
                // Get the episodeId
                var episodeId = (int)dgvElements.Rows[e.RowIndex].Cells["Id"].Value;
                _checkState[episodeId] = (bool)dgvElements.Rows[e.RowIndex].Cells[0].Value;

                SetHeaderCheckBoxState();
            }
        }

        private void SetHeaderCheckBoxState()
        {
            try
            {
                _preventEvent = true;

                if (_checkState.Count(e => e.Value) == dgvElements.Rows.Count)
                    _headerColCheckBox.CheckState = CheckState.Checked;
                else
                if (_checkState.Any(e => e.Value))
                    _headerColCheckBox.CheckState = CheckState.Indeterminate;
                else
                    _headerColCheckBox.CheckState = CheckState.Unchecked;
            }
            finally
            {
                _preventEvent = false;
            }
        }

        private void dgvElements_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            // Handle the notification that the value for a cell in the virtual column
            // is needed. Get the value from the dictionary if the key exists.
            if (_checkState.Count == 0)
                return;

            if (e.ColumnIndex == 0/* && dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value != null*/)
            {
                var episodeId = (int)dgvElements.Rows[e.RowIndex].Cells["Id"].Value;
                if (_checkState.ContainsKey(episodeId))
                    e.Value = _checkState[episodeId];
                else
                    e.Value = false;
            }
        }

        private void dgvElements_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            // Handle the notification that the value for a cell in the virtual column
            // needs to be pushed back to the dictionary.

            if (e.ColumnIndex == 0/* && dgvEpisodes.Rows[e.RowIndex].Cells["Id"].Value != null*/)
            {
                // Get the episodeId
                int episodeId = (int)dgvElements.Rows[e.RowIndex].Cells["Id"].Value;

                // Add or update the checked value to the dictionary depending on if the key already exists.
                if (!_checkState.ContainsKey(episodeId))
                {
                    _checkState.Add(episodeId, (bool)e.Value);
                }
                else
                    _checkState[episodeId] = (bool)e.Value;

                SetBulkEditButtonStateInParent();
            }
        }

        private void SetBulkEditButtonStateInParent()
        {
            //_parent.SetBulkEditButtonState(_checkState.Where(item => item.Value).Select(item => item.Key).ToList());
        }

        //MSDN says here that CellValueChanged won't fire until the cell has lost focus.
        //BUT ...
        //The CurrentCellDirtyStateChanged event commits the changes immediately when the cell is clicked.
        //You manually raise the CellValueChanged event when calling the CommitEdit method.
        private void dgvElements_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvElements.IsCurrentCellDirty)
            {
                dgvElements.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvElements_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                var selectedIndexes = new List<int>();

                for (var i = 0; i < dgvElements.Rows.Count; i++)
                {
                    if (_checkState.ContainsKey((int)dgvElements.Rows[i].Cells["Id"].Value))
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
                    dgvElements.SuspendLayout();

                    for (var i = fromindex; i <= toIndex; i++)
                    {
                        _checkState.Add((int)dgvElements.Rows[i].Cells["Id"].Value, true);
                    }
                }
                finally
                {
                    dgvElements.Invalidate();
                    dgvElements.ResumeLayout();

                    SetHeaderCheckBoxState();
                    SetBulkEditButtonStateInParent();
                }
            }
            else
            {
                _lastSelectedRowIndex = e.RowIndex;
            }
        }
    }
}
