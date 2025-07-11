﻿using DAL;
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
        private string _currentSortField = "FileName COLLATE NOCASE ASC";
        private string _advancedFilter = string.Empty;
        private IniFile _iniFile = new IniFile();

        public ucMovies(FrmMain parent)
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic, null,
                pMovieDetailsContainer, new object[] { true });

            _parent = parent;
            _parent.OnAddButtonPress += AddMovie;
            _parent.OnDeleteButtonPress += DeleteMovie;

            Helpers.GenericSetButtonsState2 = SetSaveButtonState;
            pDummyMenuForShortCutKeys.SendToBack();

            dgvMoviesList.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvMoviesList, true, null);
            dgvMoviesList.AutoGenerateColumns = false;

            miHighlightNoPoster.Checked = _iniFile.ReadBool("HighlightNoPoster", "MoviesSection");
            miHighlightNoSynopsis.Checked = _iniFile.ReadBool("HighlightNoSynopsis", "MoviesSection");
            miHighlightNoCSM.Checked = _iniFile.ReadBool("HighlightNoCSM", "MoviesSection");
        }

        private void ucMovies_Load(object sender, EventArgs e)
        {
            _preventEvent = true;
            ReloadData();
            _preventEvent = false;
        }

        public void ReloadData(bool resetCachedstills = false)
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

        private List<HighlightData> _highlightTempList;

        public List<int> PartitionMeInto(int value, int count)
        {
            if (count <= 0) throw new ArgumentException("count must be greater than zero.", "count");
            var result = new int[count];

            int runningTotal = 0;
            for (int i = 0; i < count; i++)
            {
                var remainder = value - runningTotal;
                var share = remainder > 0 ? remainder / (count - i) : 0;
                result[i] = share;
                runningTotal += share;
            }

            if (runningTotal < value) result[count - 1] += value - runningTotal;

            return result.ToList();
        }

        private void dgvMoviesList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1 && e.Value != null && e.ColumnIndex == 1 && e.RowIndex != dgvMoviesList.SelectedRows[0].Index)
            {

                var rowObj = ((DataGridView)sender).Rows[e.RowIndex].DataBoundItem;
                if (rowObj == null) return;

                if (_highlightTempList == null)
                    _highlightTempList = new List<HighlightData>();

                if (!((MovieShortInfo)rowObj).HasPoster && miHighlightNoPoster.Checked)
                    _highlightTempList.Add(new HighlightData(MoviesGridsHighlights.NoPoster, Color.LightPink));

                if (!((MovieShortInfo)rowObj).HasSynopsis && miHighlightNoSynopsis.Checked)
                    _highlightTempList.Add(new HighlightData(MoviesGridsHighlights.NoSynopsis, Color.LightSkyBlue));

                if (!((MovieShortInfo)rowObj).HasCsmData && miHighlightNoCSM.Checked)
                    _highlightTempList.Add(new HighlightData(MoviesGridsHighlights.NoCSM, Color.Bisque));

                if (!_highlightTempList.Any()) return;


                var sectionWidths = PartitionMeInto(e.CellBounds.Width, _highlightTempList.Count);
                for (var i = 0; i < sectionWidths.Count(); i++)
                {
                    _highlightTempList[i].SectionWidth = sectionWidths[i];
                }

                //var sectionWidth = e.CellBounds.Width / _highlightTempList.Count;
                var sectionWidthDrawn = 0;
                for (int i = 0; i < _highlightTempList.Count; i++)
                {
                    var htEl = _highlightTempList[i];
                    var backColorBrush = new SolidBrush(htEl.Color);
                    var tmpRect = new Rectangle(e.CellBounds.Left + sectionWidthDrawn, e.CellBounds.Top, htEl.SectionWidth, e.CellBounds.Height);
                    e.Graphics.FillRectangle(backColorBrush, tmpRect);

                    sectionWidthDrawn += htEl.SectionWidth;
                    //backColorBrush.Dispose;
                }

                //e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                //e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                //e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
                //e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;


                // here you force paint of content
                e.PaintContent(e.ClipBounds);
                e.Handled = true;

                _highlightTempList.Clear();
            }
        }

        private void dgvMoviesList_SelectionChanged(object sender, EventArgs e)
        {
            //((DataGridView)sender).Invalidate();
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
                //dgvMoviesList.DataSource = DAL.MoviesData.Where(x => x.FileName.ToLower().Contains(tbFilter.Text.ToLower())).ToList();
                dgvMoviesList.DataSource = DAL.GetMoviesGridData2(_currentSortField, tbFilter.Text);
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
                HasPoster = DAL.CurrentMTD.Poster != null,
                HasSynopsis = !string.IsNullOrEmpty(DAL.CurrentMTD.Synopsis),
                InsertedDate = (DateTime)DAL.CurrentMTD.InsertedDate,
                LastChangedDate = (DateTime)DAL.CurrentMTD.InsertedDate,
            };

            var x = DAL.GetQualityStrFromSize(DAL.CurrentMTD);

            DAL.MoviesData.Add(msi);

            ResortGrid();

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

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            var prevInstance = pMovieDetailsContainer.Controls.Find("ucMovieInfo", false);
            if (!prevInstance.Any())
            {
                MsgBox.Show("The previous UserControl instance could not be found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedMovieData = (MovieShortInfo)dgvMoviesList.SelectedRows[0].DataBoundItem;

            var dialog = new CustomDialogs
            {
                Title = string.Format("Choose a poster for movie '{0}'", selectedMovieData.FileName),
                DialogType = DialogType.OpenFile,
                InitialDirectory = _iniFile.ReadString("LastCoverPath", "General"),
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*",
                FileNameLabel = "FileName or URL",
                //ConfirmButtonText = "Confirm"
            };

            if (!dialog.Show(Handle)) return;

            _iniFile.Write("LastCoverPath", Path.GetFullPath(dialog.FileName), "General");

            using (var file = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);

                ((ucMovieInfo)prevInstance[0]).SetPoster(bytes);
            }

            Helpers.UnsavedChanges = true;
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

                OperationResult opRes = null;
                CSMScrapeResult csmData = null;

                if (DAL.CurrentMTD.HasRecommendedDataSaved)
                {
                    opRes = DAL.LoadCSMData(DAL.CurrentMTD.Id);
                    if (opRes.Success)
                    {
                        csmData = (CSMScrapeResult)opRes.AdditionalDataReturn;
                    }
                }

                opRes = DAL.RemoveMovieOrEpisode(_previousSelectedMsi.Id);

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
                if (csmData != null)
                    DAL.SaveCommonSenseMediaData(rParam.mtd.Id, csmData);

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
                        HasPoster = DAL.CurrentMTD.Poster != null,
                        HasSynopsis = !string.IsNullOrEmpty(DAL.CurrentMTD.Synopsis),
                        InsertedDate = (DateTime)rParam.mtd.InsertedDate,
                        LastChangedDate = (DateTime)rParam.mtd.LastChangeDate,
                    };

                    DAL.MoviesData.Add(msi);

                    ResortGrid();

                    _previousSelectedMsi = msi; //not needed?
                }
                finally
                {
                    _preventEvent = false;
                    FocusCurrentMovieInGrid(_previousSelectedMsi);
                }
            }
        }

        private void ResortGrid()
        {
            DAL.MoviesData =
                _currentSortField == "FileName COLLATE NOCASE ASC"
                    ? new BindingList<MovieShortInfo>(DAL.MoviesData.OrderBy(o => o.FileName).ToList())
                    : _currentSortField == "InsertedDate DESC"
                        ? new BindingList<MovieShortInfo>(DAL.MoviesData.OrderByDescending(o => o.InsertedDate).ToList())
                        : new BindingList<MovieShortInfo>(DAL.MoviesData.OrderByDescending(o => o.LastChangedDate).ToList());

            RefreshGrid();
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

            if (DAL.TmpPoster != null)
                DAL.CurrentMTD.Poster = DAL.TmpPoster;

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
                var mustRefresh = false;

                if (!selectedMovieData.HasPoster && DAL.CurrentMTD.Poster != null)
                {
                    selectedMovieData.HasPoster = true;
                    mustRefresh = true;
                }

                if ((!selectedMovieData.HasSynopsis && !string.IsNullOrEmpty(DAL.CurrentMTD.Synopsis)) ||
                    (selectedMovieData.HasSynopsis && string.IsNullOrEmpty(DAL.CurrentMTD.Synopsis)))
                {
                    selectedMovieData.HasSynopsis = !string.IsNullOrEmpty(DAL.CurrentMTD.Synopsis);
                    mustRefresh = true;
                }

                if (mustRefresh)
                    dgvMoviesList.Invalidate();
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
            _currentSortField = currentMenuItem.Tag.ToString(); // + (currentMenuItem.Name == "miSortByName" ? " COLLATE NOCASE ASC" : string.Empty);

            ResortGrid();

            //ReloadData();
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
                filterValues.Add(string.Format("(AudioLanguages LIKE '%{0}%')", cbAdvFilter_Audio.SelectedValue));

            if (!string.IsNullOrEmpty(tbAdvFilter_Rec.Text))
                filterValues.Add(string.Format(@"
	                (CASE
		                WHEN
                            REPLACE(REPLACE(Recommended, '+', ''), '?', '') IS NULL OR
                            REPLACE(REPLACE(Recommended, '+', ''), '?', '') = ''
		                THEN {0}             --this dictates the column type (int)
		                ELSE CAST(REPLACE(REPLACE(Recommended, '+', ''), '?', '') AS INT)
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

        private void dgvMoviesList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmMoviesNavGrid.Show(Cursor.Position.X, Cursor.Position.Y);
            }

            /*
                private void dgrdResults_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
                {
                    //handle the row selection on right click
                    if (e.Button == MouseButtons.Right)
                    {
                        try
                        {
                            dgrdResults.CurrentCell = dgrdResults.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            // Can leave these here - doesn't hurt
                            dgrdResults.Rows[e.RowIndex].Selected = true;
                            dgrdResults.Focus();

                            selectedBiodataId = Convert.ToInt32(dgrdResults.Rows[e.RowIndex].Cells[1].Value);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
             */
        }

        private void miHighlightNoPoster_Click(object sender, EventArgs e)
        {
            miHighlightNoPoster.Checked = !miHighlightNoPoster.Checked;
            _iniFile.Write("HighlightNoPoster", miHighlightNoCSM.Checked.ToString(), "MoviesSection");

            dgvMoviesList.Invalidate();
        }

        private void miHighlightNoSynopsis_Click(object sender, EventArgs e)
        {
            miHighlightNoSynopsis.Checked = !miHighlightNoSynopsis.Checked;
            _iniFile.Write("HighlightNoSynopsis", miHighlightNoCSM.Checked.ToString(), "MoviesSection");

            dgvMoviesList.Invalidate();
        }

        private void miHighlightNoCSM_Click(object sender, EventArgs e)
        {
            miHighlightNoCSM.Checked = !miHighlightNoCSM.Checked;
            _iniFile.Write("HighlightNoCSM", miHighlightNoCSM.Checked.ToString(), "MoviesSection");

            dgvMoviesList.Invalidate();
        }
    }

    public class MyReflector
    {
        string myNamespace;
        Assembly myAssembly;
        public MyReflector(string assemblyName, string namespaceName)
        {
            myNamespace = namespaceName;
            myAssembly = null;
            var alist = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (AssemblyName aN in alist)
            {
                if (aN.FullName.StartsWith(assemblyName))
                {
                    myAssembly = Assembly.Load(aN);
                    break;
                }
            }
        }
        public Type GetType(string typeName)
        {
            Type type = null;
            string[] names = typeName.Split('.');

            if (names.Length > 0)
                type = myAssembly.GetType(myNamespace + "." + names[0]);

            for (int i = 1; i < names.Length; ++i)
            {
                type = type.GetNestedType(names[i], BindingFlags.NonPublic);
            }
            return type;
        }

        public object Call(Type type, object obj, string func, object[] parameters)
        {
            MethodInfo methInfo = type.GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return methInfo.Invoke(obj, parameters);
        }

        public object GetField(Type type, object obj, string field)
        {
            FieldInfo fieldInfo = type.GetField(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return fieldInfo.GetValue(obj);
        }
    }
}
