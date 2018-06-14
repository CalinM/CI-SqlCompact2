using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Common;
using DAL;
using Desene.DetailFormsAndUserControls;
using Desene.EditUserControls;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
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
        private TreeNodeAdv _prevSelectedNode = null;
        private bool _preventEvent = false;

        public ucSeries(FrmMain parent)
        {
            InitializeComponent();

            _parent = parent;
            _parent.OnAddButtonPress += AddSeries;

            Helpers.GenericSetButtonsState2 = SetButtonsState;
            SetButtonsState(false);
        }

        private void ucSeries_Load(object sender, EventArgs e)
        {
            DAL.LoadSeriesData();

            //AutoSizeColumns? Where?
            //https://sourceforge.net/p/treeviewadv/discussion/568369/thread/b9e687fa/

            var tcTitle = new TreeColumn { Header = @"Title", Width = 240 };
            var tbTitle = new NodeTextBox { DataPropertyName = "FileName", ParentColumn = tcTitle };
            tvSeries.Columns.Add(tcTitle);
            tvSeries.NodeControls.Add(tbTitle);

            var tcTheme = new TreeColumn { Header = @"Theme", Width = 75 };
            var tbTheme = new NodeTextBox { DataPropertyName = "Theme", ParentColumn = tcTheme };
            tvSeries.Columns.Add(tcTheme);
            tvSeries.NodeControls.Add(tbTheme);

            var tcQuality = new TreeColumn { Header = @"Quality", Width = 35 };
            var tbQuality = new NodeTextBox { DataPropertyName = "Quality", ParentColumn = tcQuality };
            tvSeries.Columns.Add(tcQuality);
            tvSeries.NodeControls.Add(tbQuality);

            tvSeries.FullRowSelect = true;
            tvSeries.GridLineStyle = GridLineStyle.HorizontalAndVertical;
            tvSeries.UseColumns = true;

            var treeModel = new DataTableTreeModel();
            tvSeries.Model = treeModel;

            if (tvSeries.AllNodes.Any())
                tvSeries.SelectedNode = tvSeries.AllNodes.First();
        }

        private void AddSeries(object sender, EventArgs e)
        {
            var frmEditSeriesBaseInfo = new FrmEditSeriesBaseInfo { Owner = _parent };

            if (frmEditSeriesBaseInfo.ShowDialog() != DialogResult.OK) return;

            DAL.LoadSeriesData();

            //https://sourceforge.net/p/treeviewadv/discussion/568369/thread/9f164a97/
            //^^ not working?

            var treeModel = new DataTableTreeModel();
            tvSeries.Model = treeModel;

            //treeViewAdv1.FindNodeByTag()
            //no support for finding based on a single property?

            tvSeries.SelectedNode = tvSeries.AllNodes.FirstOrDefault(x => ((FileInfoNode)x.Tag).Id == frmEditSeriesBaseInfo.NewId);
            //treeViewAdv1.Focus();
        }

        private void tvSeries_SelectionChanged(object sender, EventArgs e)
        {
            if (_preventEvent) return;

            if (Helpers.UnsavedChanges)
            {
                if (MsgBox.Show("There are unsaved changes. You you want to continue and discard those changes?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    _preventEvent = true;
                    tvSeries.SelectedNode = _prevSelectedNode;//tvSeries.AllNodes.FirstOrDefault(x => ((FileInfoNode)x.Tag).Id == _prevSelectedSeriesId);
                    _preventEvent = false;

                    return;
                }
            }

            ShowSeriesDetails();

            _prevSelectedNode = tvSeries.SelectedNode;

            var fin = (FileInfoNode)_prevSelectedNode.Tag;
            btnImportEpisodes.Enabled = !fin.IsEpisode;
            btnLoadPoster.Enabled = !fin.IsEpisode;
            btnRefreshEpisodeData.Enabled = fin.IsEpisode;
        }

        private void ShowSeriesDetails()
        {
            if (tvSeries.SelectedNode == null) return; //why??

            try
            {
                Cursor = Cursors.WaitCursor;
                pSeriesDetailsContainer.SuspendLayout();

                var fileInfoNode = (FileInfoNode)tvSeries.SelectedNode.Tag;
                var seriesFI = fileInfoNode.Row;

                if (fileInfoNode.IsSeason || fileInfoNode.IsSeries)
                {
                    //when moving focus from EpisodeDetail to SeasonDetail, the "ucEpisodeDetails" must be removed
                    var prevInstance = pSeriesDetailsContainer.Controls.Find("ucEpisodeDetails", false);
                    if (prevInstance.Any())
                        pSeriesDetailsContainer.Controls.Remove(prevInstance[0]);

                    if (_prevSelectedSeriesId != fileInfoNode.Id)
                    {
                        //attempting to reuse the "ucEditSeriesBaseInfo" user control
                        prevInstance = pSeriesDetailsContainer.Controls.Find("ucEditSeriesBaseInfo", false);

                        if (prevInstance.Any())
                        {
                            if (_prevSelectedSeriesId != fileInfoNode.Id)
                                ((ucEditSeriesBaseInfo)prevInstance[0]).LoadControls(seriesFI);
                        }
                        else
                        {
                            pSeriesDetailsContainer.Controls.Add(new ucEditSeriesBaseInfo(seriesFI) { Dock = DockStyle.Top });
                        }
                    }
                    else
                    {
                        prevInstance = pSeriesDetailsContainer.Controls.Find("ucEditSeriesBaseInfo", false);
                        if (!prevInstance.Any())
                        {
                            pSeriesDetailsContainer.Controls.Add(new ucEditSeriesBaseInfo(seriesFI) { Dock = DockStyle.Top });
                        }
                    }


                    prevInstance = pSeriesDetailsContainer.Controls.Find("ucSeriesEpisodes", false);

                    //refresh the episode list ... but only if needed
                    if (_prevSelectedSeriesId != fileInfoNode.Id || !prevInstance.Any()) //when the selection moved from Episode to Series/Season, the "ucSeriesEpisodes" is not there, but the SeriesId remains the same!
                    {
                        //attempting to reuse the "ucSeriesEpisodes" user control
                        if (prevInstance.Any())
                            ((ucSeriesEpisodes)prevInstance[0]).LoadControls(fileInfoNode.Id);
                        else
                        {
                            var ucSeriesEpisodes = new ucSeriesEpisodes(fileInfoNode.Id) { Dock = DockStyle.Top };
                            pSeriesDetailsContainer.Controls.Add(ucSeriesEpisodes);
                            ucSeriesEpisodes.BringToFront();
                        }
                    }

                    _prevSelectedSeriesId = fileInfoNode.Id;
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


                    prevInstance = pSeriesDetailsContainer.Controls.Find("ucEpisodeDetails", false);
                    if (prevInstance.Any())
                        ((ucEpisodeDetails)prevInstance[0]).LoadControls(fileInfoNode.Id, null);
                    else
                    {
                        var ucEpisodeDetails = new ucEpisodeDetails(fileInfoNode.Id, _prevSelectedSeriesId) { Dock = DockStyle.Top };
                        pSeriesDetailsContainer.Controls.Add(ucEpisodeDetails);
                        ucEpisodeDetails.BringToFront();
                    }
                }
            }
            finally
            {
                pSeriesDetailsContainer.ResumeLayout();
                Cursor = Cursors.Default;
            }
        }

        private void SetButtonsState(bool b)
        {
            btnSaveChanges.Enabled = b;
            btnLoadPoster.Enabled = b && !((FileInfoNode)tvSeries.SelectedNode.Tag).IsEpisode;
        }

        //public IEnumerable<Control> GetAll(Control control,Type type)
        //{
        //    var controls = control.Controls.Cast<Control>();

        //    return controls.SelectMany(ctrl => GetAll(ctrl,type))
        //                              .Concat(controls)
        //                              .Where(c => c.GetType() == type);
        //}

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            var opRes = SaveChanges();

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error had occurred while saving the changes:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Helpers.UnsavedChanges = false;
            }
        }

        private OperationResult SaveChanges()
        {
            tvSeries.Focus(); //required to push all changes from editors into the DAL.CurrentMTD object
            return DAL.SaveMTD();
        }

        private void btnImportEpisodes_Click(object sender, EventArgs e)
        {
            var iParams = new FrmEpisodeInfoFromFiles(((FileInfoNode)tvSeries.SelectedNode.Tag).Id) { Owner = _parent };

            if (iParams.ShowDialog() != DialogResult.OK)
                return;


            var files = Directory.GetFiles(iParams.EpisodesImportParams.Location, iParams.EpisodesImportParams.FilesExtension);

            if (files.Length == 0)
            {
                MsgBox.Show("There are no files with the specified extension in the selected folder!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MsgBox.Show(string.Format("Are you sure you want to import {0} episodes in the selected Series?", files.Length), @"Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var opRes = FilesMetaData.GetFilesTechnicalDetails(files, iParams.EpisodesImportParams);

            if (!opRes.Success)
            {
                MsgBox.Show(opRes.CustomErrorMessage, @"Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (saveErrors.Any())
            {
                var frmIE = new FrmImportErrors(saveErrors, true);
                frmIE.ShowDialog();
            }
        }

        private void btnRefreshEpisodeData_Click(object sender, EventArgs e)
        {

        }




        //public void ImportSeriesFromDisk()
        //{
        //    var seriesPath = string.Empty;

        //    using (var folderBrowserDialog = new FolderBrowserDialog())
        //    {
        //        if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
        //            return;

        //        seriesPath = folderBrowserDialog.SelectedPath;
        //    }

        //    using (var conn = new SqlCeConnection(Constants.ConnectionString))
        //    {
        //        conn.Open();
        //        SqlCeCommand cmd;

        //        cmd = new SqlCeCommand("SELECT COUNT(*) FROM FileDetail where ParentId = -1 AND FileName = " + Path.GetFileName(seriesPath));
        //        var count = (Int32) cmd.ExecuteScalar();
        //    }
        //}

        //public void RefreshEpisodesFromDisk()
        //{


        //    //var seriesPath = string.Empty;

        //    //using (var folderBrowserDialog = new FolderBrowserDialog())
        //    //{
        //    //    folderBrowserDialog.Description = "Please select "
        //    //    if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
        //    //        return;

        //    //    seriesPath = folderBrowserDialog.SelectedPath;
        //    //}

        //    //using (var conn = new SqlCeConnection(Constants.ConnectionString))
        //    //{
        //    //    conn.Open();
        //    //    SqlCeCommand cmd;

        //    //    cmd = new SqlCeCommand("SELECT COUNT(*) FROM FileDetail where ParentId = -1 AND FileName = " + Path.GetFileName(seriesPath));
        //    //    var count = (Int32) cmd.ExecuteScalar();
        //    //}
        //}

        //public void
    }
}