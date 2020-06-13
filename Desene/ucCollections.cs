using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Common;
using DAL;
using Desene.DetailFormsAndUserControls;
using Desene.DetailFormsAndUserControls.Collections;
using Desene.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Utils;

using Helpers = Common.Helpers;

namespace Desene
{
    public partial class ucCollections : UserControl
    {
        private FrmMain _parent;
        private int _prevSelectedSeriesId = -2;
        private TreeNodeAdv _prevSelectedNode;
        private bool _preventEvent;
        private bool _isFiltered;
        private Timer _genericTimer;

        public ucCollections(FrmMain parent)
        {
            InitializeComponent();

            moduleToolbar.ImageScalingSize = new Size(16, 16);

            _parent = parent;
            _parent.OnAddButtonPress += AddCollection;
            _parent.OnDeleteButtonPress += DeleteCollection;

            Helpers.GenericSetButtonsState2 = SetSaveButtonState;

            pDummyMenuForShortCutKeys.SendToBack();
        }

        private void ucSeries_Load(object sender, EventArgs e)
        {
            //AutoSizeColumns? Where?
            //https://sourceforge.net/p/treeviewadv/discussion/568369/thread/b9e687fa/

            var tcTitle = new TreeColumn { Header = "Title", Width = 240 };
            var tbTitle = new NodeTextBox { DataPropertyName = "FileName", ParentColumn = tcTitle };
            tvCollections.Columns.Add(tcTitle);
            tvCollections.NodeControls.Add(tbTitle);

            var tcTheme = new TreeColumn { Header = "Theme", Width = 75 };
            var tbTheme = new NodeTextBox { DataPropertyName = "Theme", ParentColumn = tcTheme };
            tvCollections.Columns.Add(tcTheme);
            tvCollections.NodeControls.Add(tbTheme);

            var tcQuality = new TreeColumn { Header = "Quality", Width = 35 };
            var tbQuality = new NodeTextBox { DataPropertyName = "Quality", ParentColumn = tcQuality };
            tvCollections.Columns.Add(tcQuality);
            tvCollections.NodeControls.Add(tbQuality);

            tvCollections.FullRowSelect = true;
            tvCollections.GridLineStyle = GridLineStyle.HorizontalAndVertical;
            tvCollections.UseColumns = true;

            var treeModel = new SeriesTreeModel(true);
            tvCollections.Model = treeModel;

            if (tvCollections.AllNodes.Any())
                tvCollections.SelectedNode = tvCollections.AllNodes.First();
        }

        private void LoadSelectionDetails(int scrollAt = -1)
        {
            if (tvCollections.SelectedNode == null) return; //why??

            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pCollectionElementDetailsContainer);

                var seShortInfo = (SeriesEpisodesShortInfo)tvCollections.SelectedNode.Tag;
                var isElementSeleted = tvCollections.SelectedNode.Level == 2;

                btnImportElements.Enabled = !isElementSeleted;
                btnLoadPoster.Enabled =
                    (!isElementSeleted && (CollectionsSiteSectionType)seShortInfo.SectionType == CollectionsSiteSectionType.SeriesType) ||
                    (isElementSeleted && (CollectionsSiteSectionType)seShortInfo.SectionType == CollectionsSiteSectionType.MovieType);
                btnRefreshElementData.Enabled = isElementSeleted;
                btnDeleteElement.Enabled = isElementSeleted; //the collection is deleted by action in the main form toolbar


                btnAddMovieToCollection.Enabled = (CollectionsSiteSectionType)seShortInfo.SectionType == CollectionsSiteSectionType.MovieType;

                if (!isElementSeleted) //collection level
                {
                    ////when moving focus from ElementDetail to CollectionDetail, the "ucCollectionElements" must be removed
                    //var prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucCollectionElements", false);
                    //if (prevInstance.Any())
                    //    pCollectionElementDetailsContainer.Controls.Remove(prevInstance[0]);

                    //attempting to reuse the "ucCollectionInfo" user control
                    var prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucCollectionInfo", false);

                    if (seShortInfo.Poster == null /*&& (CollectionsSiteSectionType)seShortInfo.SectionType == CollectionsSiteSectionType.SeriesType*/)
                        seShortInfo.Poster = DAL.GetPoster(seShortInfo.Id);

                    if (prevInstance.Any())
                    {
                        ((ucCollectionInfo)prevInstance[0]).RefreshControls(seShortInfo);
                    }
                    else
                    {
                        if (pCollectionElementDetailsContainer.Controls.Count > 0)
                            pCollectionElementDetailsContainer.Controls.Clear();

                        var baseInfoControl = new ucCollectionInfo() { Dock = DockStyle.Top };
                        baseInfoControl.ParentEl = pCollectionElementDetailsContainer;
                        baseInfoControl.RefreshControls(seShortInfo);

                        pCollectionElementDetailsContainer.Controls.Add(baseInfoControl);
                    }

                    prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucCollectionElements", false);

                    //attempting to reuse the "ucSeriesEpisodes" user control
                    if (prevInstance.Any())
                    {
                        ((ucCollectionElements)prevInstance[0]).LoadControls(seShortInfo);
                        pCollectionElementDetailsContainer.Invalidate();
                    }
                    else
                    {
                        if (!_isFiltered)
                        {
                            var ucCollectionElements = new ucCollectionElements(seShortInfo, this) { Dock = DockStyle.Top };
                            pCollectionElementDetailsContainer.Controls.Add(ucCollectionElements);
                            ucCollectionElements.BringToFront();
                        }
                    }

                    _prevSelectedSeriesId = seShortInfo.Id;
                }
                else
                {
                    var opRes = DAL.LoadMTD(seShortInfo.Id, true);

                    if (!opRes.Success)
                    {
                        MsgBox.Show(string.Format("The following error occurred while loading the file details:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucCollectionElements", false);
                    if (prevInstance.Any())
                        pCollectionElementDetailsContainer.Controls.Clear();

                    switch ((CollectionsSiteSectionType)seShortInfo.SectionType)
                    {
                        case CollectionsSiteSectionType.MovieType:

                            prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucEpisodeDetails", false);
                            if (prevInstance.Any())
                                pCollectionElementDetailsContainer.Controls.Remove(prevInstance[0]);

                            prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucMovieInfo", false);

                            if (prevInstance.Any())
                                ((ucMovieInfo)prevInstance[0]).RefreshControls(DAL.CurrentMTD);
                            else
                            {
                                var ucMovieInfo = new ucMovieInfo(false) { Dock = DockStyle.Top };
                                ucMovieInfo.RefreshControls(DAL.CurrentMTD);

                                pCollectionElementDetailsContainer.Controls.Add(ucMovieInfo);
                                ucMovieInfo.BringToFront();
                            }

                            break;

                        case CollectionsSiteSectionType.SeriesType:
                            prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucMovieInfo", false);
                            if (prevInstance.Any())
                                pCollectionElementDetailsContainer.Controls.Remove(prevInstance[0]);

                            prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucEpisodeDetails", false);

                            if (prevInstance.Any())
                                ((ucEpisodeDetails)prevInstance[0]).LoadControls(seShortInfo.Id, null, "aaaa");
                            else
                            {
                                var seriesNode = tvCollections.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == seShortInfo.SeriesId && ((SeriesEpisodesShortInfo)x.Tag).IsSeries);
                                var seriesName = seriesNode == null
                                    ? "unknown !!!"
                                    : ((SeriesEpisodesShortInfo)seriesNode.Tag).FileName;

                                var ucEpisodeDetails = new ucEpisodeDetails(seShortInfo.Id, seShortInfo.SeriesId, seriesName) { Dock = DockStyle.Top };

                                pCollectionElementDetailsContainer.Controls.Add(ucEpisodeDetails);
                                ucEpisodeDetails.BringToFront();
                            }

                            break;
                    }

                }
                /*
                var prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucCollectionInfo", false);

                if (!isElementSeleted)
                {
                    if (prevInstance.Any())
                    {
                        ((ucCollectionInfo)prevInstance[0]).RefreshControls(seShortInfo);
                    }
                    else
                    {
                        prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucMovieInfo", false);
                        if (prevInstance.Any())
                            pCollectionElementDetailsContainer.Controls.Remove(prevInstance[0]);

                        prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucEpisodeDetails", false);
                        if (prevInstance.Any())
                            pCollectionElementDetailsContainer.Controls.Remove(prevInstance[0]);

                        var ucCol = new ucCollectionInfo() { Dock = DockStyle.Top };
                        ucCol.RefreshControls(seShortInfo);

                        pCollectionElementDetailsContainer.Controls.Add(ucCol);
                    }
                }
                else
                {
                    if (prevInstance.Any()) //the CollectionInfo UserControl
                        pCollectionElementDetailsContainer.Controls.Remove(prevInstance[0]);

                    var opRes = DAL.LoadMTD(seShortInfo.Id, true);

                    if (!opRes.Success)
                    {
                        MsgBox.Show(string.Format("The following error occurred while loading the file details:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucCollectionInfo", false);

                    switch ((CollectionsSiteSectionType)seShortInfo.SectionType)
                    {
                        case CollectionsSiteSectionType.MovieType:

                            prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucEpisodeDetails", false);
                            if (prevInstance.Any())
                                pCollectionElementDetailsContainer.Controls.Remove(prevInstance[0]);

                            prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucMovieInfo", false);

                            if (prevInstance.Any())
                                ((ucMovieInfo)prevInstance[0]).RefreshControls(DAL.CurrentMTD);
                            else
                            {
                                var ucMovieInfo = new ucMovieInfo(false) { Dock = DockStyle.Top };
                                ucMovieInfo.RefreshControls(DAL.CurrentMTD);

                                pCollectionElementDetailsContainer.Controls.Add(ucMovieInfo);
                                ucMovieInfo.BringToFront();
                            }

                            break;

                        case CollectionsSiteSectionType.SeriesType:
                            prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucMovieInfo", false);
                            if (prevInstance.Any())
                                pCollectionElementDetailsContainer.Controls.Remove(prevInstance[0]);

                            prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucEpisodeDetails", false);

                            if (prevInstance.Any())
                                ((ucEpisodeDetails)prevInstance[0]).LoadControls(seShortInfo.Id, null, "aaaa");
                            else
                            {
                                var seriesNode = tvCollections.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == seShortInfo.SeriesId && ((SeriesEpisodesShortInfo)x.Tag).IsSeries);
                                var seriesName = seriesNode == null
                                    ? "unknown !!!"
                                    : ((SeriesEpisodesShortInfo)seriesNode.Tag).FileName;

                                var ucEpisodeDetails = new ucEpisodeDetails(seShortInfo.Id, seShortInfo.SeriesId, seriesName) { Dock = DockStyle.Top };

                                pCollectionElementDetailsContainer.Controls.Add(ucEpisodeDetails);
                                ucEpisodeDetails.BringToFront();
                            }

                            break;
                    }
                }
                */
            }
            finally
            {
                if (scrollAt > -1)
                    pCollectionElementDetailsContainer.VerticalScroll.Value = scrollAt;

                SetSaveButtonState(false);

                DrawingControl.ResumeDrawing(pCollectionElementDetailsContainer);
                pCollectionElementDetailsContainer.PerformLayout(); //to refresh the vertical scrollbar

                Cursor = Cursors.Default;
            }
        }

        #region ******** Treeview code

        #region Basic Events

        private void tvCollections_SelectionChanged(object sender, EventArgs e)
        {
            if (_preventEvent || tvCollections.SelectedNode == null) return;

            if (!Utils.Helpers.ConfirmDiscardChanges())
            {
                _preventEvent = true;
                tvCollections.SelectedNode = _prevSelectedNode;//tvSeries.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == _prevSelectedSeriesId);
                _preventEvent = false;

                return;
            }

            LoadSelectionDetails();

            _prevSelectedNode = tvCollections.SelectedNode;
        }

        #endregion

        #region Helper methods

        private void ReloadTreeView(int? selectedParentd, int? selectedChildId = null)
        {
            //https://sourceforge.net/p/treeviewadv/discussion/568369/thread/9f164a97/
            //^^ not working?

            var treeModel = new SeriesTreeModel(true); //to refresh the tree
            tvCollections.Model = treeModel;

            //tvSeries.FindNodeByTag()
            //no support for finding based on a single property?

            if (selectedParentd != null)
            {
                tvCollections.SelectedNode = tvCollections.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == selectedParentd);

                if (selectedChildId != null)
                {
                    tvCollections.SelectedNode.ExpandAll();
                    tvCollections.SelectedNode = tvCollections.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == selectedChildId);
                }
            }
            else
                tvCollections.SelectedNode = tvCollections.AllNodes.FirstOrDefault();
            //treeViewAdv1.Focus();
        }

        public void TryLocateEpisodeInTree(int episodeId)
        {
            if (tvCollections.SelectedNode != null)
            {
                tvCollections.SelectedNode.ExpandAll();
                tvCollections.SelectedNode = tvCollections.AllNodes.FirstOrDefault(x => ((SeriesEpisodesShortInfo)x.Tag).Id == episodeId);
                tvCollections.Focus();
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

        private void AddCollection(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            var frmAddCollection = new FrmAddCollection { Owner = _parent };

            if (frmAddCollection.ShowDialog() != DialogResult.OK) return;

            ReloadTreeView(frmAddCollection.NewId, null);
        }

        private void DeleteCollection(object sender, EventArgs e)
        {
            if (tvCollections.AllNodes.Count() == 0) return;

            var selectedNodeData = (SeriesEpisodesShortInfo)tvCollections.SelectedNode.Tag;

            if (MsgBox.Show(
                    string.Format("Are you sure you want to remove the Collection{0}{0}{1}{0}{0}with all it's Elements?",
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

            /*
            var prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucEpisodeDetails", false);
            if (prevInstance.Any())
                ((ucEpisodeDetails)prevInstance[0]).LoadThemesInControl(true);
                */
        }

        private OperationResult SaveChanges()
        {
            var selectedNodeData = (SeriesEpisodesShortInfo)tvCollections.SelectedNode.Tag;
            tvCollections.Focus(); //required to push all changes from editors into the DAL.CurrentMTD object

            var isElementSeleted = tvCollections.SelectedNode.Level == 2;
            var opRes = new OperationResult();

            if (!isElementSeleted) //collection level
            {
                var prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucCollectionInfo", false);
                if (!prevInstance.Any() || !(prevInstance[0] is ucCollectionInfo))
                {
                    opRes.Success = false;
                    opRes.CustomErrorMessage = "UI element not found or of wrong type!";
                }
                else
                {
                    var prevAsType = (ucCollectionInfo)prevInstance[0];

                    opRes = DAL.UpdateCollection(selectedNodeData.Id, prevAsType.Title, prevAsType.Notes, prevAsType.SectionType, prevAsType.Poster);

                    if (opRes.Success)
                    {
                        selectedNodeData.FileName = prevAsType.Title;
                        selectedNodeData.Notes = prevAsType.Notes;
                        selectedNodeData.SectionType = prevAsType.SectionType;
                    }
                }
            }
            else
            {
                opRes = DAL.SaveMTD(selectedNodeData.IsEpisode);

                if (opRes.Success)
                {
                    selectedNodeData.FileName = DAL.CurrentMTD.FileName;
                    selectedNodeData.Quality = DAL.CurrentMTD.Quality;
                    selectedNodeData.Theme = DAL.CurrentMTD.Theme;
                }
            }


            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while saving the changes:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return opRes;
        }

        private void btnImportElements_Click(object sender, EventArgs e)
        {
            var sesi = (SeriesEpisodesShortInfo)tvCollections.SelectedNode.Tag;

            var iParams = new FrmElementsInfoFromFiles(sesi.Id) { Owner = _parent };

            if (iParams.ShowDialog() != DialogResult.OK)
                return;


            var files = Directory.GetFiles(iParams.ElementsImportParams.Location, iParams.ElementsImportParams.FilesExtension);


            var opRes = FilesMetaData.GetFilesTechnicalDetails(files, iParams.ElementsImportParams);

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
                iParams.ElementsImportParams, importResult.Value));

            var saveErrors = (List<TechnicalDetailsImportError>)opRes.AdditionalDataReturn;
            if (saveErrors != null && saveErrors.Any())
            {
                var frmIE = new FrmImportErrors(saveErrors, true);
                frmIE.ShowDialog();
            }

            ReloadTreeView(sesi.Id);
            tvCollections.SelectedNode.ExpandAll();

            LoadSelectionDetails();
        }

        private void btnRefreshEpisodeData_Click(object sender, EventArgs e)
        {
            if (Helpers.UnsavedChanges && !SaveChanges().Success)
                return;

            using (var rParam = new FrmMTDFromFile(false, true) { Owner = _parent })
            {
                if (rParam.ShowDialog() != DialogResult.OK)
                    return;

                if (rParam.mtd == null)
                {
                    MsgBox.Show("An error occurred while determining the file (movie) details. No additional data available!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                var opRes = DAL.RemoveMovieOrEpisode(((SeriesEpisodesShortInfo)_prevSelectedNode.Tag).Id);

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
                rParam.mtd.Trailer = DAL.CurrentMTD.Trailer;
                rParam.mtd.StreamLink = DAL.CurrentMTD.StreamLink;
                rParam.mtd.Poster = DAL.CurrentMTD.Poster;
                rParam.mtd.InsertedDate = DAL.CurrentMTD.InsertedDate;
                rParam.mtd.LastChangeDate = DateTime.Now;

                //not using UpdateMTD in order to avoud additional check regarding the number of streams
                opRes = DAL.InsertMTD(
                    rParam.mtd,
                    new FilesImportParams
                    {
                        Year = rParam.mtd.Year,
                        Season = "",
                        ParentId = ((SeriesEpisodesShortInfo)_prevSelectedNode.Tag).SeriesId
                    });

                if (!opRes.Success)
                {
                    MsgBox.Show(
                        string.Format("The following error occurred while inserting the new movie details into the database:{0}{0}{1}{0}{0}", Environment.NewLine, opRes.CustomErrorMessage),
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                rParam.mtd.Id = (int)opRes.AdditionalDataReturn;
                DAL.CurrentMTD = rParam.mtd;

                ReloadTreeView(((SeriesEpisodesShortInfo)_prevSelectedNode.Tag).SeriesId, rParam.mtd.Id);

                LoadSelectionDetails();
            }
        }

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            var prevInstance = pCollectionElementDetailsContainer.Controls.Find("ucMovieInfo", false);
            if (!prevInstance.Any())
            {
                MsgBox.Show("The previous UserControl instance could not be found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var openFileDialog = new OpenFileDialog())
            {
                var selectedNodeData = (SeriesEpisodesShortInfo)tvCollections.SelectedNode.Tag;

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

                    ((ucMovieInfo)prevInstance[0]).SetPoster(bytes); //todo?
                }

                Helpers.UnsavedChanges = true;
            }
        }

        private void btnDeleteElement_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("Are you sure you want to remove the selected Element?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var sesi = (SeriesEpisodesShortInfo)tvCollections.SelectedNode.Tag;

            var opRes = DAL.RemoveMovieOrEpisode(sesi.Id);

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

                tvCollections.SelectedNode.ExpandAll();

                LoadSelectionDetails();
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            LoadSelectionDetails(pCollectionElementDetailsContainer.VerticalScroll.Value);
            Helpers.UnsavedChanges = false;
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

            var treeModel = new SeriesTreeModel(true);
            tvCollections.Model = treeModel;

            tvCollections.SelectedNode = tvCollections.AllNodes.FirstOrDefault();

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
                var treeModel = new FilteredTreeModel(tbFilter.Text, true);
                tvCollections.Model = treeModel;
                tvCollections.ExpandAll();

                tvCollections.SelectedNode = tvCollections.AllNodes.FirstOrDefault();
                _prevSelectedNode = tvCollections.SelectedNode;
            }
            else
            {
                CancelFilter();
            }
        }

        private void btnAddMovieToCollection_Click(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            var selectedNodeData = (SeriesEpisodesShortInfo)tvCollections.SelectedNode.Tag;
            var frmAddMovie = new FrmAddMovie(selectedNodeData.IsEpisode ? selectedNodeData.SeriesId : selectedNodeData.Id) { Owner = _parent };

            if (frmAddMovie.ShowDialog() != DialogResult.OK)
            {
                Helpers.UnsavedChanges = false;
                return;
            }

            var sesi = (SeriesEpisodesShortInfo)tvCollections.SelectedNode.Tag;
            var collectionId =
                tvCollections.SelectedNode.Level == 1
                    ? sesi.Id
                    : sesi.SeriesId;

            ReloadTreeView(collectionId, DAL.CurrentMTD.Id);
            tvCollections.SelectedNode.ExpandAll();

            LoadSelectionDetails();
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