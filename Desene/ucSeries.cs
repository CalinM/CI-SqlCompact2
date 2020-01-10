using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Common;
using DAL;
using Desene.DetailFormsAndUserControls;
using Desene.DetailFormsAndUserControls.Series;
using Desene.EditUserControls;
using Desene.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Utils;

using Helpers = Common.Helpers;

namespace Desene
{
    public partial class ucSeries : UserControl
    {
        private FrmMain _parent;
        private int _prevSelectedSeriesId = -2;
        private TreeNodeAdv _prevSelectedNode;
        private bool _preventEvent;
        private bool _isFiltered;
        private Timer _genericTimer;

        public ucSeries(FrmMain parent, SeriesType st)
        {
            InitializeComponent();

            _parent = parent;
            _parent.OnAddButtonPress += AddSeries;
            _parent.OnDeleteButtonPress += DeleteSeries;

            DAL.SeriesType = st;
            Helpers.GenericSetButtonsState2 = SetSaveButtonState;

            pDummyMenuForShortCutKeys.SendToBack();
        }

        private void ucSeries_Load(object sender, EventArgs e)
        {
            //AutoSizeColumns? Where?
            //https://sourceforge.net/p/treeviewadv/discussion/568369/thread/b9e687fa/

            var tcTitle = new TreeColumn { Header = "Title", Width = 240 };
            var tbTitle = new NodeTextBox { DataPropertyName = "FileName", ParentColumn = tcTitle };
            tvSeries.Columns.Add(tcTitle);
            tvSeries.NodeControls.Add(tbTitle);

            var tcTheme = new TreeColumn { Header = "Theme", Width = 75 };
            var tbTheme = new NodeTextBox { DataPropertyName = "Theme", ParentColumn = tcTheme };
            tvSeries.Columns.Add(tcTheme);
            tvSeries.NodeControls.Add(tbTheme);

            var tcQuality = new TreeColumn { Header = "Quality", Width = 35 };
            var tbQuality = new NodeTextBox { DataPropertyName = "Quality", ParentColumn = tcQuality };
            tvSeries.Columns.Add(tcQuality);
            tvSeries.NodeControls.Add(tbQuality);

            tvSeries.FullRowSelect = true;
            tvSeries.GridLineStyle = GridLineStyle.HorizontalAndVertical;
            tvSeries.UseColumns = true;

            var treeModel = new SeriesTreeModel();
            tvSeries.Model = treeModel;

            if (tvSeries.AllNodes.Any())
                tvSeries.SelectedNode = tvSeries.AllNodes.First();
        }

        private void LoadSelectionDetails(int scrollAt = -1)
        {
            if (tvSeries.SelectedNode == null) return; //why??

            try
            {
                Cursor = Cursors.WaitCursor;
                pSeriesDetailsContainer.SuspendLayout();

                var seShortInfo = (SeriesEpisodesShortInfo)tvSeries.SelectedNode.Tag;

                var opRes = DAL.LoadMTD(seShortInfo.Id, seShortInfo.IsEpisode);

                if (!opRes.Success)
                {
                    MsgBox.Show(string.Format("The following error occurred while loading the file details:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (seShortInfo.IsSeason || seShortInfo.IsSeries)
                {
                    //when moving focus from EpisodeDetail to SeasonDetail, the "ucEpisodeDetails" must be removed
                    var prevInstance = pSeriesDetailsContainer.Controls.Find("ucEpisodeDetails", false);
                    if (prevInstance.Any())
                        pSeriesDetailsContainer.Controls.Remove(prevInstance[0]);

                    if (_prevSelectedSeriesId != seShortInfo.Id)
                    {
                        //attempting to reuse the "ucEditSeriesBaseInfo" user control
                        prevInstance = pSeriesDetailsContainer.Controls.Find("ucEditSeriesBaseInfo", false);

                        if (prevInstance.Any())
                        {
                            if (_prevSelectedSeriesId != seShortInfo.Id)
                                ((ucEditSeriesBaseInfo)prevInstance[0]).RefreshControls();
                        }
                        else
                        {
                            pSeriesDetailsContainer.Controls.Add(new ucEditSeriesBaseInfo(false) { Dock = DockStyle.Top });
                        }
                    }
                    else //when returning to the Series/Season node from an episode from the same Series
                    {
                        prevInstance = pSeriesDetailsContainer.Controls.Find("ucEditSeriesBaseInfo", false);
                        if (!prevInstance.Any())
                        {
                            pSeriesDetailsContainer.Controls.Add(new ucEditSeriesBaseInfo(false) { Dock = DockStyle.Top });
                        }
                    }


                    prevInstance = pSeriesDetailsContainer.Controls.Find("ucSeriesEpisodes", false);

                    //refresh the episode list ... but only if needed
                    if (_prevSelectedSeriesId != seShortInfo.Id || !prevInstance.Any()) //when the selection moved from Episode to Series/Season, the "ucSeriesEpisodes" is not there, but the SeriesId remains the same!
                    {
                        //attempting to reuse the "ucSeriesEpisodes" user control
                        if (prevInstance.Any())
                        {
                            ((ucSeriesEpisodes)prevInstance[0]).LoadControls(seShortInfo.Id);
                        }
                        else
                        {
                            if (!_isFiltered)
                            {
                                var ucSeriesEpisodes = new ucSeriesEpisodes(seShortInfo.Id, this) { Dock = DockStyle.Top };
                                pSeriesDetailsContainer.Controls.Add(ucSeriesEpisodes);
                                ucSeriesEpisodes.BringToFront();
                            }
                        }
                    }
                    else
                    {
                        if (_isFiltered && prevInstance.Any())
                            pSeriesDetailsContainer.Controls.Remove(prevInstance[0]);
                    }


                    _prevSelectedSeriesId = seShortInfo.Id;
                }
                else
                {
                    //when moving focus from SeasonDetail to EpisodeDetail, the "ucSeriesEpisodes" must be removed
                    var prevInstance = pSeriesDetailsContainer.Controls.Find("ucSeriesEpisodes", false);
                    if (prevInstance.Any())
                        pSeriesDetailsContainer.Controls.Remove(prevInstance[0]);

                    //attempting to reuse the "ucEditSeriesBaseInfo" user control
                    prevInstance = pSeriesDetailsContainer.Controls.Find("ucEditSeriesBaseInfo", false);
                    if (prevInstance.Any())
                        pSeriesDetailsContainer.Controls.Remove(prevInstance[0]);


                    var seriesNode = tvSeries.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == seShortInfo.SeriesId && ((SeriesEpisodesShortInfo)x.Tag).IsSeries);
                    var seriesName = seriesNode == null
                        ? "unknown !!!"
                        : ((SeriesEpisodesShortInfo)seriesNode.Tag).FileName;

                    prevInstance = pSeriesDetailsContainer.Controls.Find("ucEpisodeDetails", false);
                    if (prevInstance.Any())
                        ((ucEpisodeDetails)prevInstance[0]).LoadControls(seShortInfo.Id, null, seriesName);
                    else
                    {
                        var ucEpisodeDetails = new ucEpisodeDetails(seShortInfo.Id, _prevSelectedSeriesId, seriesName) { Dock = DockStyle.Top };
                        pSeriesDetailsContainer.Controls.Add(ucEpisodeDetails);
                        ucEpisodeDetails.BringToFront();
                    }
                }
            }
            finally
            {
                if (scrollAt > -1)
                    pSeriesDetailsContainer.VerticalScroll.Value = scrollAt;

                pSeriesDetailsContainer.ResumeLayout();
                Cursor = Cursors.Default;
            }
        }

        #region ******** Treeview code

        #region Basic Events

        private void tvSeries_SelectionChanged(object sender, EventArgs e)
        {
            if (_preventEvent || tvSeries.SelectedNode == null) return;

            if (!Utils.Helpers.ConfirmDiscardChanges())
            {
                _preventEvent = true;
                tvSeries.SelectedNode = _prevSelectedNode;//tvSeries.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == _prevSelectedSeriesId);
                _preventEvent = false;

                return;
            }

            LoadSelectionDetails();

            _prevSelectedNode = tvSeries.SelectedNode;


            var sesi = (SeriesEpisodesShortInfo)_prevSelectedNode.Tag;

            btnImportEpisodes.Enabled = sesi != null && !sesi.IsEpisode;
            btnLoadPoster.Enabled = btnImportEpisodes.Enabled;

            btnRefreshEpisodeData.Enabled = sesi != null && !sesi.IsSeries;
            btnDeleteSeasonEpisode.Enabled = sesi != null && !sesi.IsSeries;
        }

        #endregion

        #region Helper methods

        private void ReloadTreeView(int? selectId)
        {
            //https://sourceforge.net/p/treeviewadv/discussion/568369/thread/9f164a97/
            //^^ not working?

            var treeModel = new SeriesTreeModel(); //to refresh the tree
            tvSeries.Model = treeModel;

            //tvSeries.FindNodeByTag()
            //no support for finding based on a single property?

            if (selectId != null)
                tvSeries.SelectedNode = tvSeries.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == selectId);
            else
                tvSeries.SelectedNode = tvSeries.AllNodes.FirstOrDefault();
            //treeViewAdv1.Focus();
        }

        public void TryLocateEpisodeInTree(int episodeId)
        {
            if (tvSeries.SelectedNode != null)
            {
                tvSeries.SelectedNode.ExpandAll();
                tvSeries.SelectedNode = tvSeries.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == episodeId);
                tvSeries.Focus();
            }
        }

        #endregion

        #endregion

        #region ******** Crud code

        private void SetSaveButtonState(bool b)
        {
            btnSaveChanges.Enabled = b;
            btnUndo.Enabled = b;
        }

        private void AddSeries(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            var frmEditSeriesBaseInfo = new FrmEditSeriesBaseInfo { Owner = _parent };

            if (frmEditSeriesBaseInfo.ShowDialog() != DialogResult.OK) return;

            ReloadTreeView(frmEditSeriesBaseInfo.NewId);
        }

        private void DeleteSeries(object sender, EventArgs e)
        {
            if (tvSeries.AllNodes.Count() == 0) return;

            var selectedNodeData = (SeriesEpisodesShortInfo)tvSeries.SelectedNode.Tag;

            if (MsgBox.Show(
                    string.Format("Are you sure you want to remove the Series{0}{0}{1}{0}{0}with all it's Episodes?",
                        Environment.NewLine, DAL.GetSeriesTitleFromId(selectedNodeData.SeriesId)),
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var opRes = DAL.RemoveSeries(selectedNodeData.SeriesId);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while removing the selection:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Helpers.UnsavedChanges = false; //this setter takes care of the buttons states!

                ReloadTreeView(null);
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            SaveChanges();

            var prevInstance = pSeriesDetailsContainer.Controls.Find("ucEpisodeDetails", false);
            if (prevInstance.Any())
            {
                //((ucEpisodeDetails)prevInstance[0]).LoadThemesInControl();
                ((ucEpisodeDetails)prevInstance[0]).RefreshAfterSave();
            }
        }

        private OperationResult SaveChanges()
        {
            var selectedNodeData = (SeriesEpisodesShortInfo)tvSeries.SelectedNode.Tag;
            tvSeries.Focus(); //required to push all changes from editors into the DAL.CurrentMTD object

            var opRes = DAL.SaveMTD(selectedNodeData.IsEpisode);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while saving the changes:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Helpers.UnsavedChanges = false;

                selectedNodeData.FileName = DAL.CurrentMTD.FileName;
                selectedNodeData.Quality = DAL.CurrentMTD.Quality;
                selectedNodeData.Theme = DAL.CurrentMTD.Theme;
            }

            return opRes;
        }

        private void btnImportEpisodes_Click(object sender, EventArgs e)
        {
            var sesi = (SeriesEpisodesShortInfo)tvSeries.SelectedNode.Tag;

            var iParams = new FrmEpisodeInfoFromFiles(sesi.Id, sesi.IsSeason ? sesi.Season : (int?)null) { Owner = _parent };

            if (iParams.ShowDialog() != DialogResult.OK)
                return;


            var files = Directory.GetFiles(iParams.EpisodesImportParams.Location, iParams.EpisodesImportParams.FilesExtension);

            var opRes = FilesMetaData.GetFilesTechnicalDetails(files, iParams.EpisodesImportParams);

            if (!opRes.Success)
            {
                MsgBox.Show(opRes.CustomErrorMessage, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var importResult = (KeyValuePair<List<TechnicalDetailsImportError>, List<MovieTechnicalDetails>>)opRes.AdditionalDataReturn;

            if (importResult.Key.Any())
            {
                var frmIE = new FrmImportErrors(importResult.Key, importResult.Value.Any());

                if (frmIE.ShowDialog() != DialogResult.OK)
                    return;
            }

            opRes = FilesMetaData.SaveImportedEpisodes(new KeyValuePair<FilesImportParams, List<MovieTechnicalDetails>>(
                iParams.EpisodesImportParams, importResult.Value));

            var saveErrors = (List<TechnicalDetailsImportError>)opRes.AdditionalDataReturn;
            if (saveErrors != null && saveErrors.Any())
            {
                var frmIE = new FrmImportErrors(saveErrors, true);
                frmIE.ShowDialog();
            }

            ReloadTreeView(sesi.Id);
            tvSeries.SelectedNode.ExpandAll();

            LoadSelectionDetails();
        }

        private void btnRefreshEpisodeData_Click(object sender, EventArgs e)
        {
            var sesi = (SeriesEpisodesShortInfo)_prevSelectedNode.Tag;
            if (!sesi.IsEpisode)
            {
                MsgBox.Show("There are no episode related information that need to be preserved, so please remove the season and re-import.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Utils.Helpers.ConfirmDiscardChanges()) return;

            using (var rParam = new FrmMTDFromFile(false, true) { Owner = _parent })
            {
                if (rParam.ShowDialog() != DialogResult.OK)
                    return;

                var prevInstance = pSeriesDetailsContainer.Controls.Find("ucEpisodeDetails", false);
                if (prevInstance.Any())
                {
                    var opRes = SaveChanges();

                    if (opRes.Success)
                        ((ucEpisodeDetails)prevInstance[0]).LoadControls2(true);
                }
                else
                    MsgBox.Show("The previous UserControl instance could not be found!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            var prevInstance = pSeriesDetailsContainer.Controls.Find("ucEditSeriesBaseInfo", false);
            if (!prevInstance.Any())
            {
                MsgBox.Show("The previous UserControl instance could not be found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var openFileDialog = new OpenFileDialog())
            {
                var selectedNodeData = (SeriesEpisodesShortInfo)tvSeries.SelectedNode.Tag;

                openFileDialog.Title = string.Format("Choose a poster for series '{0}'", selectedNodeData.FileName);
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Settings.Default.LastCoverPath;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                Settings.Default.LastCoverPath = Path.GetFullPath(openFileDialog.FileName);
                Settings.Default.Save();


                using (var file = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);

                    ((ucEditSeriesBaseInfo)prevInstance[0]).SetPoster(bytes);
                }

                Helpers.UnsavedChanges = true;
            }
        }

        private void btnDeleteSeasonEpisode_Click(object sender, EventArgs e)
        {
            var sesi = (SeriesEpisodesShortInfo)tvSeries.SelectedNode.Tag;

            var msg =
                sesi.IsSeason
                    ? "Are you sure you want to remove all episodes in the selected season?"
                    : "Are you sure you want to remove the selected episode?";

            if (MsgBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var opRes =
                sesi.IsSeason
                    ? DAL.RemoveSeason(sesi.Id, sesi.Season)
                    : DAL.RemoveMovieOrEpisode(sesi.Id);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while removing the selection:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Helpers.UnsavedChanges = false; //this setter takes care of the buttons states!

                ReloadTreeView(sesi.SeriesId);

                if (!sesi.IsSeason)
                    tvSeries.SelectedNode.ExpandAll();
                else
                    _prevSelectedSeriesId = -2;

                LoadSelectionDetails();
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            LoadSelectionDetails(pSeriesDetailsContainer.VerticalScroll.Value);
            Helpers.UnsavedChanges = false;
        }

        public void SetBulkEditButtonState(List<int> selectedIds)
        {
            btnBulkEdit.Enabled = selectedIds.Count() > 1;
            btnBulkEdit.Tag = selectedIds;
        }

        #endregion

        #region Filter

        private void tbFilter_ButtonClick(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges()) return;

            CancelFilter();
        }

        private void CancelFilter()
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

            _isFiltered = false;

            //var ses = (SeriesEpisodesShortInfo)tvSeries.SelectedNode.Tag;

            var treeModel = new SeriesTreeModel();
            tvSeries.Model = treeModel;

            tvSeries.SelectedNode = tvSeries.AllNodes.FirstOrDefault();

            //tvSeries.SelectedNode = ses.IsSeason

            //    ? tvSeries.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == ses.Id && ((SeriesEpisodesShortInfo)x.Tag).Season == ses.Season)
            //    : tvSeries.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == ses.Id);
            //todo: this ... ^^ ... has only the roots, the selection must be remade from the original path
        }

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

            if (!Utils.Helpers.ConfirmDiscardChanges()) return;

            if (!string.IsNullOrEmpty(tbFilter.Text))
            {
                _isFiltered = true;
                var treeModel = new FilteredTreeModel(tbFilter.Text);
                tvSeries.Model = treeModel;
                tvSeries.ExpandAll();

                tvSeries.SelectedNode = tvSeries.AllNodes.FirstOrDefault();
                _prevSelectedNode = tvSeries.SelectedNode;
            }
            else
            {
                CancelFilter();
            }
        }

        private void BtnBulkEdit_Click(object sender, EventArgs e)
        {
            var selectedEpisodes = (List<int>)btnBulkEdit.Tag;

            var frmBulkEpisodeEdit = new FrmBulkEpisodeEdit(selectedEpisodes.Count) { Owner = _parent };

            if (frmBulkEpisodeEdit.ShowDialog() != DialogResult.OK)
                return;

            var opRes = DAL.SaveBulkChanges(selectedEpisodes, frmBulkEpisodeEdit.SelectedBulkValues);

            if (!opRes.Success)
            {
                MsgBox.Show(string.Format("The following error occurred while saving the builk changes:{0}{0}{1}{0}{0}.No values were changed in the database!", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sesi = (SeriesEpisodesShortInfo)tvSeries.SelectedNode.Tag;

            var prevInstance = pSeriesDetailsContainer.Controls.Find("ucSeriesEpisodes", false);

            if (prevInstance.Any())
            {
                //this resets the grid selection and disable the button
                ((ucSeriesEpisodes)prevInstance[0]).LoadControls(sesi.Id);
            }            
        }

        #endregion

        //public IEnumerable<Control> GetAll(Control control,Type type)
        //{
        //    var controls = control.Controls.Cast<Control>();

        //    return controls.SelectMany(ctrl => GetAll(ctrl,type))
        //                              .Concat(controls)
        //                              .Where(c => c.GetType() == type);
    }
}