using DAL;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Desene.DetailFormsAndUserControls;
using Desene.Properties;

using Utils;

using Helpers = Common.Helpers;
using System.Collections.Generic;

using Common;

namespace Desene
{
    public partial class ucMovies : UserControl
    {
        private FrmMain _parent;
        private bool _preventEvent;
        private MovieShortInfo _previousSelectedMsi;
        private Timer _genericTimer;
        private string _lookupStartingWith = string.Empty;
        private string _currentSortField = "FileName";
        private string _advancedFilter = string.Empty;

        public ucMovies(FrmMain parent)
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic, null,
                pMovieDetailsContainer, new object[] { true });

            _parent = parent;
            _parent.OnAddButtonPress += AddMovie;
            _parent.OnDeleteButtonPress += DeleteMovie;
            _parent.OnCloseModule += CloseModule;

            Helpers.GenericSetButtonsState2 = SetSaveButtonState;
            pDummyMenuForShortCutKeys.SendToBack();

            dgvMoviesList.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvMoviesList, true, null);
            dgvMoviesList.AutoGenerateColumns = false;
        }

        private void ucMovies_Load(object sender, EventArgs e)
        {
            _preventEvent = true;
            ReloadData();
            _preventEvent = false;
        }

        private void ReloadData(bool resetCachedstills = false)
        {
            DAL.MoviesData = DAL.GetMoviesGridData(_currentSortField, _advancedFilter);

            if (resetCachedstills)
                DAL.CachedMoviesStills = new List<CachedMovieStills>();

            RefreshGrid();

            _previousSelectedMsi = DAL.MoviesData.FirstOrDefault();
        }

        private void LoadSelectionDetails(int scrollAt = -1)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMovieDetailsContainer);

                var selectedMovieData = (MovieShortInfo)dgvMoviesList.SelectedRows[0].DataBoundItem;

                var opRes = DAL.LoadMTD(selectedMovieData.Id, true);

                if (!opRes.Success)
                {
                    MsgBox.Show(string.Format("The following error occurred while loading the file details:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var prevInstance = pMovieDetailsContainer.Controls.Find("ucMovieInfo", false);

                if (prevInstance.Any())
                    ((ucMovieInfo)prevInstance[0]).RefreshControls(DAL.CurrentMTD);
                else
                {
                    var ucMovieInfo = new ucMovieInfo(false) { Dock = DockStyle.Top };
                    ucMovieInfo.RefreshControls(DAL.CurrentMTD);

                    pMovieDetailsContainer.Controls.Add(ucMovieInfo);
                    ucMovieInfo.BringToFront();
                }
            }
            finally
            {
                if (scrollAt > -1)
                    pMovieDetailsContainer.VerticalScroll.Value = scrollAt;

                SetSaveButtonState(false);

                DrawingControl.ResumeDrawing(pMovieDetailsContainer);
                Cursor = Cursors.Default;
            }
        }

        #region ******** DataGridView code

        #region Basic events

        private void dgvMoviesList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var rowObj = ((DataGridView)sender).Rows[e.RowIndex].DataBoundItem;
            if (rowObj == null) return;

            dgvMoviesList.Rows[e.RowIndex].DefaultCellStyle.BackColor = ((MovieShortInfo)rowObj).HasPoster ? Color.White : Color.LightPink;

            if (e.ColumnIndex == 2 && ((MovieShortInfo)rowObj).Quality == "sd?")
            {
                e.CellStyle.BackColor = Color.Lavender;
            }

        }

        private void dgvMoviesList_SelectionChanged(object sender, EventArgs e)
        {
            if (_preventEvent || dgvMoviesList.SelectedRows.Count == 0) return;

            if (!Utils.Helpers.ConfirmDiscardChanges())
            {
                _preventEvent = true;
                FocusCurrentMovieInGrid(_previousSelectedMsi);
                _preventEvent = false;

                return;
            }

            LoadSelectionDetails();

            _previousSelectedMsi = (MovieShortInfo)dgvMoviesList.SelectedRows[0].DataBoundItem;

            btnLoadPoster.Enabled = true;
            btnRefreshMovieData.Enabled = true;
        }

        #endregion

        #region Helper methods

        private void FocusCurrentMovieInGrid(MovieShortInfo msi)
        {
            var index = DAL.MoviesData.IndexOf(msi);

            if (index >= 0)
            {
                RefreshGrid();
                dgvMoviesList.Rows[index].Selected = true;
                dgvMoviesList.FirstDisplayedScrollingRowIndex = index;
            }
        }

        private void RefreshGrid()
        {
            dgvMoviesList.DataSource = DAL.MoviesData;
            dgvMoviesList.Refresh();
        }


        #endregion

        #region Inline search

        private void dgvMoviesList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_genericTimer != null)
            {
                _genericTimer.Enabled = false;
                _genericTimer = null;
            }

            _genericTimer = new Timer
            {
                Interval = 2000,
            };

            _genericTimer.Tick += ClearLookupTypedKeys;
            _genericTimer.Enabled = true;

            _lookupStartingWith += e.KeyChar;
            var index = -1;

            var movieObj = DAL.MoviesData.FirstOrDefault(f => f.FileName.ToLower().StartsWith(_lookupStartingWith));
            if (movieObj != null)
                index = DAL.MoviesData.IndexOf(movieObj);

            if (index >= 0)
            {
                dgvMoviesList.Rows[index].Selected = true;
                dgvMoviesList.FirstDisplayedScrollingRowIndex = index;
            }
        }

        private void ClearLookupTypedKeys(object sender, EventArgs e)
        {
            _genericTimer.Enabled = false;
            _genericTimer = null;

            _lookupStartingWith = string.Empty;
        }

        #endregion

        #region Filter

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (_preventEvent) return;

            if (_genericTimer != null)
            {
                _genericTimer.Enabled = false;
                _genericTimer = null;
            }

            _genericTimer = new Timer
            {
                Interval = 1000
            };

            _genericTimer.Tick += FilterMovieTiles;
            _genericTimer.Enabled = true;
        }

        private void FilterMovieTiles(object sender, EventArgs e)
        {
            _genericTimer.Enabled = false;
            _genericTimer = null;

            if (!string.IsNullOrEmpty(tbFilter.Text))
            {
                dgvMoviesList.DataSource = DAL.MoviesData.Where(x => x.FileName.ToLower().Contains(tbFilter.Text.ToLower())).ToList();
            }
            else
            {
                dgvMoviesList.DataSource = DAL.MoviesData;
            }

            dgvMoviesList.Invalidate();
        }

        private void tbFilter_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                _preventEvent = true;

                tbFilter.Text = "";
            }
            finally
            {
                _preventEvent = false;
            }

            dgvMoviesList.DataSource = DAL.MoviesData;
            dgvMoviesList.Invalidate();
        }

        #endregion

        #endregion

        #region ******** Crud code

        private void SetSaveButtonState(bool b)
        {
            btnSaveChanges.Enabled = b;
            btnUndo.Enabled = b;
        }

        private void AddMovie(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            var frmAddMovie = new FrmAddMovie { Owner = _parent };

            if (frmAddMovie.ShowDialog() != DialogResult.OK)
            {
                Helpers.UnsavedChanges = false;
                return;
            }

            var msi = new MovieShortInfo
            {
                Id = DAL.CurrentMTD.Id,
                FileName = DAL.CurrentMTD.FileName,
                //Cover = DAL.CurrentMTD.Poster,
                HasPoster = DAL.CurrentMTD.Poster != null
            };

            DAL.MoviesData.Add(msi);
            DAL.MoviesData = new BindingList<MovieShortInfo>(DAL.MoviesData.OrderBy(o => o.FileName).ToList());
            RefreshGrid();

            FocusCurrentMovieInGrid(msi);
        }

        private void DeleteMovie(object sender, EventArgs e)
        {
            if (DAL.MoviesData.Count == 0) return;

            if (MsgBox.Show(
                    string.Format("Are you sure you want to remove{0}{0}{1}{0}{0}from your collection?",
                        Environment.NewLine, _previousSelectedMsi.FileName),
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var opRes = DAL.RemoveMovieOrEpisode(_previousSelectedMsi.Id);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while removing the selection:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Helpers.UnsavedChanges = false; //this setter takes care of the buttons states!

                var index = DAL.MoviesData.IndexOf(_previousSelectedMsi);
                var newIndex = index == DAL.MoviesData.Count - 1
                    ? index - 1
                    : index;

                DAL.MoviesData.Remove(_previousSelectedMsi);

                RefreshGrid();

                if (DAL.MoviesData.Count > 0)
                {
                    _previousSelectedMsi = DAL.MoviesData[newIndex];
                    FocusCurrentMovieInGrid(_previousSelectedMsi);
                }
            }
        }

        private void CloseModule(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            _preventEvent = true;
        }

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            var prevInstance = pMovieDetailsContainer.Controls.Find("ucMovieInfo", false);
            if (!prevInstance.Any())
            {
                MsgBox.Show("The previous UserControl instance could not be found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var openFileDialog = new OpenFileDialog())
            {
                var selectedMovieData = (MovieShortInfo)dgvMoviesList.SelectedRows[0].DataBoundItem;

                openFileDialog.Title = string.Format("Choose a poster for series '{0}'", selectedMovieData.FileName);
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Settings.Default.LastCoverPath;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                Settings.Default.LastCoverPath = Path.GetFullPath(openFileDialog.FileName);
                Settings.Default.Save();


                using (var file = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);

                    ((ucMovieInfo)prevInstance[0]).SetPoster(bytes);
                }

                Helpers.UnsavedChanges = true;
            }
        }

        private void btnImportMovies_Click(object sender, EventArgs e)
        {
            var iParams = new FrmMoviesInfoFromFiles { Owner = _parent };

            if (iParams.ShowDialog() != DialogResult.OK)
                return;

            var files = Directory.GetFiles(iParams.MoviesImportParams.Location, iParams.MoviesImportParams.FilesExtension);

            if (files.Length == 0)
            {
                MsgBox.Show("There are no files with the specified extension in the selected folder!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MsgBox.Show(string.Format("Are you sure you want to import {0} Movies?", files.Length), "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var opRes = FilesMetaData.GetFilesTechnicalDetails(files, iParams.MoviesImportParams);

            if (!opRes.Success)
            {
                MsgBox.Show(opRes.CustomErrorMessage, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var importErrors = (List<TechnicalDetailsImportError>)opRes.AdditionalDataReturn;

            if (importErrors.Any())
            {
                var frmIE = new FrmImportErrors(importErrors, true);
                frmIE.ShowDialog();
            }

            ReloadData(true);
        }

        private void btnRefreshMovieData_Click(object sender, EventArgs e)
        {
            if (Helpers.UnsavedChanges && !SaveChanges())
                return;

            using (var rParam = new FrmMTDFromFile(false, false) { Owner = _parent })
            {
                if (rParam.ShowDialog() != DialogResult.OK)
                    return;

                if (rParam.mtd == null)
                {
                    MsgBox.Show("An error occurred while determining the file (movie) details. No additional data available!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                var opRes = DAL.RemoveMovieOrEpisode(_previousSelectedMsi.Id);

                if (!opRes.Success)
                {
                    MsgBox.Show(
                        string.Format("The following error occurred while removing the previous movie details:{0}{0}{1}{0}{0}The current operation cannot continue!", Environment.NewLine, opRes.CustomErrorMessage),
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                //not using UpdateMTD in order to avoud additional check regarding the number of streams
                opRes = DAL.InsertMTD(rParam.mtd, null);

                if (!opRes.Success)
                {
                    MsgBox.Show(
                        string.Format("The following error occurred while inserting the new movie details into the database:{0}{0}{1}{0}{0}", Environment.NewLine, opRes.CustomErrorMessage),
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                rParam.mtd.Id = (int)opRes.AdditionalDataReturn;

                try
                {
                    _preventEvent = true;

                    DAL.CurrentMTD = rParam.mtd;
                    var movieObj = DAL.MoviesData.FirstOrDefault(m => m.Id == _previousSelectedMsi.Id);
                    DAL.MoviesData.Remove(movieObj);

                    var msi = new MovieShortInfo
                    {
                        Id = DAL.CurrentMTD.Id,
                        FileName = DAL.CurrentMTD.FileName,
                        //Cover = DAL.CurrentMTD.Poster,
                        HasPoster = DAL.CurrentMTD.Poster != null
                    };

                    DAL.MoviesData.Add(msi);
                    DAL.MoviesData = new BindingList<MovieShortInfo>(DAL.MoviesData.OrderBy(o => o.FileName).ToList());
                    RefreshGrid();

                    _previousSelectedMsi = msi; //not needed?
                }
                finally
                {
                    _preventEvent = false;
                    FocusCurrentMovieInGrid(_previousSelectedMsi);
                }
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            SaveChanges();

            var prevInstance = pMovieDetailsContainer.Controls.Find("ucMovieInfo", false);

            if (prevInstance.Any())
                ((ucMovieInfo)prevInstance[0]).RefreshAfterSave();
        }

        private bool SaveChanges()
        {
            var prevInstance = pMovieDetailsContainer.Controls.Find("ucMovieInfo", false);
            ((ucMovieInfo)prevInstance[0]).tbDummyForFocus.Focus();

            var opRes = DAL.SaveMTD();

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while saving the changes made to the movie details:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var selectedMovieData = (MovieShortInfo)dgvMoviesList.SelectedRows[0].DataBoundItem;

                if (!selectedMovieData.HasPoster && DAL.CurrentMTD.Poster != null)
                {
                    selectedMovieData.HasPoster = true;
                    dgvMoviesList.Invalidate();
                }
            }

            return opRes.Success;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            LoadSelectionDetails(pMovieDetailsContainer.VerticalScroll.Value);
            Helpers.UnsavedChanges = false;
        }

        #endregion

        #region ******** Attempt to prevent the focus rect on the splitter control, fails the fist time, but works after

        // Temp variable to store a previously focused control
        private Control focused;

        private void scMovies_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the focused control before the splitter is focused
            focused = getFocused(Controls);
        }

        private Control getFocused(ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c.Focused)
                {
                    // Return the focused control
                    return c;
                }

                if (c.ContainsFocus)
                {
                    // If the focus is contained inside a control's children
                    // return the child
                    return getFocused(c.Controls);
                }
            }

            // No control on the form has focus
            return null;
        }

        private void scMovies_MouseUp(object sender, MouseEventArgs e)
        {
            // If a previous control had focus
            if (focused != null)
            {
                // Return focus and clear the temp variable for
                // garbage collection
                focused.Focus();
                focused = null;
            }
        }

        #endregion

        private void scMovies_Resize(object sender, EventArgs e)
        {
            //scMovies.Panel2MinSize = scMovies.Width - scMovies.SplitterWidth - 300;
        }

        private void SortMoviesGrid(object sender, EventArgs e)
        {
            //uncheck all item
            foreach (ToolStripMenuItem item in bntSort.DropDownItems)
            {
                item.Checked = false;
            }

            var currentMenuItem = (ToolStripMenuItem)sender;
            currentMenuItem.Checked = true;
            _currentSortField = currentMenuItem.Tag.ToString();
            ReloadData();
        }

        #region Advanced filters

        private void lbSwitchToAdvFilters_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DrawingControl.SuspendDrawing(scMovies.Panel1);

                if (cbAdvFilter_Audio.DataSource == null)
                {
                    var listOfLanguagesWithAny = new List<LanguageObject>(Languages.Iso639);
                    listOfLanguagesWithAny.Insert(0, new LanguageObject()
                    {
                        Name = "<Any>"
                    });

                    cbAdvFilter_Audio.DataSource = listOfLanguagesWithAny;
                    cbAdvFilter_Audio.ValueMember = "Code";
                    cbAdvFilter_Audio.DisplayMember = "Name";
                    cbAdvFilter_Audio.SetSeparator(4);

                    var listOfThemesWithAny = new List<string>(DAL.MovieThemes);
                    listOfThemesWithAny.Insert(0, "<Any>");
                    cbAdvFilter_Theme.DataSource = listOfThemesWithAny;

                    var toolTip = new ToolTip();
                    toolTip.SetToolTip(lbAdvFilterReset, "Reset the advanced filter values");
                    toolTip.SetToolTip(chkIncludeUnknownRec, "Include the movies with missing or unknown recommendation");
                }

                pBasicFilter.Visible = false;
                pAdvFilter.Visible = true;
            }
            finally
            {
                DrawingControl.ResumeDrawing(scMovies.Panel1);
            }
        }

        private void lbSwitchToSimpleFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DrawingControl.SuspendDrawing(scMovies.Panel1);

                pBasicFilter.Visible = true;
                pAdvFilter.Visible = false;

                ResetAdvancedFilter();
            }
            finally
            {
                DrawingControl.ResumeDrawing(scMovies.Panel1);
            }
        }

        private void lbAdvFilterReset_MouseEnter(object sender, EventArgs e)
        {
            var lbl = (Label)sender;
            lbl.ForeColor = Color.Red;
        }

        private void lbAdvFilterReset_MouseLeave(object sender, EventArgs e)
        {
            var lbl = (Label)sender;
            lbl.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void lbAdvFilterReset_Click(object sender, EventArgs e)
        {
            ResetAdvancedFilter();
        }

        private void ResetAdvancedFilter()
        {
            cbAdvFilter_Audio.SelectedIndex = 0;
            tbAdvFilter_Rec.Text = "";
            cbAdvFilter_Theme.SelectedIndex = 0;
            chkIncludeUnknownRec.Checked = false;

            BuildAdvancedFilter();
        }

        private void cbAdvFilter_Audio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BuildAdvancedFilter();
        }

        private void tbAdvFilter_Rec_TextChanged(object sender, EventArgs e)
        {
            BuildAdvancedFilter();
        }

        private void chkIncludeUnknownRec_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbAdvFilter_Rec.Text))
                BuildAdvancedFilter();
        }

        private void BuildAdvancedFilter()
        {
            var filterValues = new List<string>();

            if (cbAdvFilter_Audio.SelectedIndex > 0)
                filterValues.Add(string.Format("(CHARINDEX('{0}', AudioLanguages) > 0)", cbAdvFilter_Audio.SelectedValue));

            if (!string.IsNullOrEmpty(tbAdvFilter_Rec.Text))
                filterValues.Add(string.Format(@"
	                (CASE
		                WHEN
                            REPLACE(REPLACE(Recommended, '+', ''), '?', '') IS NULL OR
                            REPLACE(REPLACE(Recommended, '+', ''), '?', '') = ''
		                THEN {0}             --this dictates the column type (int)
		                ELSE REPLACE(REPLACE(Recommended, '+', ''), '?', '')
	                END <= {1})",
                    chkIncludeUnknownRec.Checked ? "0" : "99",
                    tbAdvFilter_Rec.Text));

            if (cbAdvFilter_Theme.SelectedIndex > 0)
                filterValues.Add(string.Format("(Theme = '{0}')", cbAdvFilter_Theme.Text.Replace("'", "''")));

            _advancedFilter =
                filterValues.Any()
                    ? string.Format("AND ({0})", string.Join(" AND ", filterValues))
                    : string.Empty;

            ReloadData();

            gbAdvFilterWrapper.Text =
                string.IsNullOrEmpty(_advancedFilter)
                    ? "Advanced filters"
                    : string.Format("Advanced filters ({0} results)", DAL.MoviesData.Count);
        }

        #endregion
    }
}
