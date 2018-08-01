using DAL;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Desene.DetailFormsAndUserControls;
using Desene.DetailFormsAndUserControls.Movies;
using Desene.Properties;

using Utils;

using Helpers = Common.Helpers;

namespace Desene
{
    public partial class ucMovies : UserControl
    {
        private FrmMain _parent;
        private BindingList<MovieShortInfo> _moviesData;
        private bool _preventEvent;
        private MovieShortInfo _previousSelectedMsi;
        private Timer _genericTimer;
        private string _lookupStartingWith = string.Empty;

        public ucMovies(FrmMain parent)
        {
            InitializeComponent();

            _parent = parent;
            _parent.OnAddButtonPress += AddMovie;
            _parent.OnDeleteButtonPress += DeleteMovie;

            Helpers.GenericSetButtonsState2 = SetSaveButtonState;
            pDummyMenuForShortCutKeys.SendToBack();

            dgvMoviesList.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvMoviesList, true, null);
            dgvMoviesList.AutoGenerateColumns = false;
        }

        private void ucMovies_Load(object sender, EventArgs e)
        {
            _moviesData = DAL.GetMoviesGridData();

            RefreshGrid();

            _previousSelectedMsi = _moviesData.FirstOrDefault();
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
                    var ucMovieInfo = new ucMovieInfo { Dock = DockStyle.Top };
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
            var index = _moviesData.IndexOf(msi);

            if (index >= 0)
            {
                RefreshGrid();
                dgvMoviesList.Rows[index].Selected = true;
                dgvMoviesList.FirstDisplayedScrollingRowIndex = index;
            }
        }

        private void RefreshGrid()
        {
            dgvMoviesList.DataSource = _moviesData;
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

            var movieObj = _moviesData.FirstOrDefault(f => f.FileName.ToLower().StartsWith(_lookupStartingWith));
            if (movieObj != null)
                index = _moviesData.IndexOf(movieObj);

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
                dgvMoviesList.DataSource = _moviesData.Where(x => x.FileName.ToLower().Contains(tbFilter.Text.ToLower())).ToList();
            }
            else
            {
                dgvMoviesList.DataSource = _moviesData;
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

            dgvMoviesList.DataSource = _moviesData;
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

            if (frmAddMovie.ShowDialog() != DialogResult.OK) return;

            var msi = new MovieShortInfo
            {
                Id = DAL.CurrentMTD.Id,
                FileName = DAL.CurrentMTD.FileName,
                //Cover = DAL.CurrentMTD.Poster,
                HasPoster = DAL.CurrentMTD.Poster != null
            };

            _moviesData.Add(msi);
            _moviesData = new BindingList<MovieShortInfo>(_moviesData.OrderBy(o => o.FileName).ToList());
            RefreshGrid();

            FocusCurrentMovieInGrid(msi);
        }

        private void DeleteMovie(object sender, EventArgs e)
        {
            if (_moviesData.Count == 0) return;

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

                var index = _moviesData.IndexOf(_previousSelectedMsi);
                var newIndex = index == _moviesData.Count-1
                    ? index - 1
                    : index;

                _moviesData.Remove(_previousSelectedMsi);

                RefreshGrid();

                if (_moviesData.Count > 0)
                {
                    _previousSelectedMsi = _moviesData[newIndex];
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

            using (var openFileDialog = new OpenFileDialog())
            {
                var selectedMovieData = (MovieShortInfo)dgvMoviesList.SelectedRows[0].DataBoundItem;

                openFileDialog.Title = string.Format("Choose a poster for series '{0}'", selectedMovieData.FileName);
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Settings.Default.LastPath;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                Settings.Default.LastPath = Path.GetFullPath(openFileDialog.FileName);
                Settings.Default.Save();


                using (var file = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);

                    ((ucMovieInfo)prevInstance[0]).SetPoster(bytes, false);
                }

                Helpers.UnsavedChanges = true;
            }
        }

        private void btnRefreshMovieData_Click(object sender, EventArgs e)
        {
            //if (MsgBox.Show(
            //        "The previous movie details and all changes made by hand will be lost. Are you sure you want to continue?",
            //        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return;
            if (Helpers.UnsavedChanges && !SaveChanges())
                return;

            using (var rParam = new FrmMTDFromFile(true, false) { Owner = _parent })
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

                rParam.mtd.DescriptionLink = DAL.CurrentMTD.DescriptionLink;
                rParam.mtd.Recommended = DAL.CurrentMTD.Recommended;
                rParam.mtd.RecommendedLink = DAL.CurrentMTD.RecommendedLink;
                rParam.mtd.Year = DAL.CurrentMTD.Year;
                rParam.mtd.Theme = DAL.CurrentMTD.Theme;
                rParam.mtd.Notes = DAL.CurrentMTD.Notes;
                rParam.mtd.StreamLink = DAL.CurrentMTD.StreamLink;
                rParam.mtd.Poster = DAL.CurrentMTD.Poster;

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
                    var movieObj = _moviesData.FirstOrDefault(m => m.Id == _previousSelectedMsi.Id);
                    _moviesData.Remove(movieObj);

                    var msi = new MovieShortInfo
                    {
                        Id = DAL.CurrentMTD.Id,
                        FileName = DAL.CurrentMTD.FileName,
                        //Cover = DAL.CurrentMTD.Poster,
                        HasPoster = DAL.CurrentMTD.Poster != null
                    };

                    _moviesData.Add(msi);
                    _moviesData = new BindingList<MovieShortInfo>(_moviesData.OrderBy(o => o.FileName).ToList());
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
            scMovies.Panel2MinSize = scMovies.Width - scMovies.SplitterWidth - 300;
        }

    }
}
