using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Common;

using DAL;
using Desene.DetailFormsAndUserControls.Movies;
using Desene.DetailFormsAndUserControls.Shared;

using Utils;

using Helpers = Common.Helpers;

namespace Desene.DetailFormsAndUserControls
{
    public partial class ucMovieInfo : UserControl
    {
        private BindingSource _bsControlsData;
        private bool _isNew;
        public string MovieTitle
        {
            get { return tbTitle.Text; }
        }

        public bool GetCSMData
        {
            get { return cbGrabCSMData.Checked; }
        }

        public ucMovieInfo()
        {
            InitializeComponent();

            _isNew = true;
            pbDbDates.Visible = false;
            bRefreshCSMData.Visible = false;
            PostConstructor();
        }

        public ucMovieInfo(bool? isNew)
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic, null,
                pMovieDetail, new object[] { true });

            _isNew = isNew.GetValueOrDefault(true);
            pbDbDates.Visible = !_isNew;
            bRefreshCSMData.Visible = !_isNew;

            PostConstructor();
        }

        private void PostConstructor()
        {
            InitControls();

            DAL.TmpPoster = null; //used when a poster is dragged before initializing the DAL.NewMTD object

            cbTheme.MouseWheel += Utils.Helpers.Combobox_OnMouseWheel;
            cbQuality.MouseWheel += Utils.Helpers.Combobox_OnMouseWheel;

            //button1.TabStop = false;
            //button1.CanSelect = false;

            ttTitleContent.SetToolTip(chbTitle, "The file doesn't have a 'Title' tag");
            ttTitleContent.SetToolTip(cbGrabCSMData, "Attemp to grab meaningful data from CommonSenseMedia site on save");
        }

        private void ucMovieInfo_Load(object sender, EventArgs e)
        {
            tbmDuration.ValidatingType = typeof(TimeSpan);
            tbDummyForFocus.Location = new Point(tbNotes.Left + 10, tbNotes.Top + 10);
            tbSizeAsInt.Location = new Point(tbSize.Left + 10, tbSize.Top);

            var tt2 = new ToolTip(); //ttTitleContent not working?!!
            tt2.SetToolTip(bGotoDescription, "Navigate using the default system browser to the current Description link");
            tt2.SetToolTip(bGotoTrailer, "Navigate using the default system browser to the current Trailer link");
            tt2.SetToolTip(bRefreshCSMData, "Reload data from CommonSenseMedia");
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();
            _bsControlsData.DataSource = new MovieTechnicalDetails();

            tbTitle.DataBindings.Add("Text", _bsControlsData, "FileName");
            tbDescriptionLink.DataBindings.Add("Text", _bsControlsData, "DescriptionLink");
            tbRecommended.DataBindings.Add("Text", _bsControlsData, "Recommended");
            tbRecommendedLink.DataBindings.Add("Text", _bsControlsData, "RecommendedLink");
            tbYear.DataBindings.Add("Text", _bsControlsData, "Year");

            cbQuality.DataSource = Enum.GetValues(typeof(Quality));
            cbTheme.DataSource = DAL.MovieThemes;

            cbQuality.DataBindings.Add("Text", _bsControlsData, "Quality");
            tbSizeAsInt.DataBindings.Add("Text", _bsControlsData, "FileSize"); //SizeAsInt on RefreshControl ... see comment!
            tbmDuration.DataBindings.Add("Text", _bsControlsData, "DurationFormatted");
            tbAudioSummary.DataBindings.Add("Text", _bsControlsData, "AudioLanguages");
            tbSubtitleSummary.DataBindings.Add("Text", _bsControlsData, "SubtitleLanguages");
            tbNotes.DataBindings.Add("Text", _bsControlsData, "Notes");
            tbTrailer.DataBindings.Add("Text", _bsControlsData, "Trailer");
            tbStreamLink.DataBindings.Add("Text", _bsControlsData, "StreamLink");
            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbEncodedWith.DataBindings.Add("Text", _bsControlsData, "Encoded_Application");
            cbTheme.DataBindings.Add("Text", _bsControlsData, "Theme");
            //pbCover.DataBindings.Add("Image", _bsControlsData, "Poster", true);

            chbTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");
            tbSynopsis.DataBindings.Add("Text", _bsControlsData, "Synopsis");
        }

        public void RefreshAfterSave()
        {
            //if a new Theme has been added, it must be placed in list
            cbTheme.DataSource = null;
            cbTheme.DataSource = DAL.MovieThemes;
            cbTheme.Text = DAL.CurrentMTD.Theme;

            //after a Save operation made after a FileDetails refresh, AudioSummary and SubtitleSummary must be manually refreshed
            if (tbAudioSummary.DataBindings.Count > 0)
            {
                tbAudioSummary.DataBindings[0].ReadValue();
                tbSubtitleSummary.DataBindings[0].ReadValue();
            }
        }

        public void RefreshControls(MovieTechnicalDetails mtd)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(this);

                _bsControlsData.DataSource = mtd;
                _bsControlsData.ResetBindings(false);

                ttTitleContent.RemoveAll();
                if (mtd.HasTitle && !string.IsNullOrEmpty(mtd.Title))
                {
                    ttTitleContent.SetToolTip(chbTitle, mtd.Title);
                    chbTitle.Cursor = Cursors.Help;
                }
                else
                {
                    ttTitleContent.SetToolTip(chbTitle, "The file doesn't have a 'Title' tag");
                    chbTitle.Cursor = Cursors.Default;
                }

                //having it on "InitControls" will cause a crash, the column being NOT recognized!
                //only for this custom TextBox
                //todo: why?
                if (tbSize.DataBindings.Count == 0)
                {
                    tbSize.DataBindings.Add("Text", _bsControlsData, "FileSize2");
                }

                //CheckSizeMismatch();

                if (mtd.Id > 0)
                {
                    var cachedMovieStills = DAL.CachedMoviesStills.FirstOrDefault(ms => ms.FileDetailId == mtd.Id);
                    if (cachedMovieStills != null)
                    {
                        SetMovieStills(cachedMovieStills.MovieStills);
                    }
                    else
                    {
                        cachedMovieStills = DAL.LoadMovieStills(mtd.Id);
                        DAL.CachedMoviesStills.Add(cachedMovieStills);
                        SetMovieStills(cachedMovieStills.MovieStills);
                    }

                    LoadControls2();
                }
                else
                {
                    SetMovieStills(mtd.MovieStills);
                }


                var tt3 = new ToolTip(); //ttTitleContent not working?!!
                tt3.SetToolTip(bGotoRecommendedSite, mtd.HasRecommendedDataSaved
                    ? "Displays a window showing the last scraped/passed data from CommmonSenseMedia site"
                    : "Navigate using the default system browser to the current CommonSenseMedia link");

                _hasRecommendedDataSaved = mtd.HasRecommendedDataSaved;
            }
            finally
            {
                DrawingControl.ResumeDrawing(this);
                Cursor = Cursors.Default;
            }
        }

        private bool _hasRecommendedDataSaved;

        private void LoadControls2()
        {
            SetPoster(DAL.CurrentMTD.Poster);

            ttMovieDbDates.SetToolTip(pbDbDates,
                string.Format("Inserted: {0}{1}Last modified: {2}{1}Id: {3}",
                    DAL.CurrentMTD.InsertedDate == null
                        ? "?"
                        : ((DateTime)DAL.CurrentMTD.InsertedDate).ToString("dd.MM.yyyy"),
                    Environment.NewLine,
                    DAL.CurrentMTD.LastChangeDate == null
                        ? "-"
                        : ((DateTime)DAL.CurrentMTD.LastChangeDate).ToString("dd.MM.yyyy"),
                    DAL.CurrentMTD.Id
                    ));

            var vsUC = Controls.OfType<UserControl>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == "videoStreams");
            if (vsUC == null)
            {
                Utils.Helpers.AddSectionHeader(this, "Video stream(s)", "V");

                var ucVideoStreams = new ucGenericStreamsWrapper(DAL.CurrentMTD.VideoStreams)
                {
                    Dock = DockStyle.Top,
                    Tag = "videoStreams"
                };

                Controls.Add(ucVideoStreams);
                ucVideoStreams.BringToFront();
            }
            else
            {
                ((ucGenericStreamsWrapper)vsUC).LoadControlsForVideoStreams(DAL.CurrentMTD.VideoStreams);
            }


            var asUC = Controls.OfType<UserControl>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == "audioStreams");
            if (asUC == null)
            {
                Utils.Helpers.AddSectionHeader(this, "Audio stream(s)", "A");

                var ucAudioStreams = new ucGenericStreamsWrapper(DAL.CurrentMTD.AudioStreams)
                {
                    Dock = DockStyle.Top,
                    Tag = "audioStreams"
                };

                Controls.Add(ucAudioStreams);
                ucAudioStreams.BringToFront();
            }
            else
            {
                ((ucGenericStreamsWrapper)asUC).LoadControlsForAudioStreams(DAL.CurrentMTD.AudioStreams);
            }

            var ssUC = Controls.OfType<UserControl>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == "subtitleStreams");
            if (DAL.CurrentMTD.SubtitleStreams.Any())
            {
                if (ssUC == null)
                {
                    Utils.Helpers.AddSectionHeader(this, "Subtitle stream(s)", "S");

                    var ucSubtitleStreams = new ucGenericStreamsWrapper(DAL.CurrentMTD.SubtitleStreams)
                    {
                        Dock = DockStyle.Top,
                        Tag = "subtitleStreams"
                    };

                    Controls.Add(ucSubtitleStreams);
                    ucSubtitleStreams.BringToFront();
                }
                else
                {
                    ((ucGenericStreamsWrapper)ssUC).LoadControlsForSubtitleStreams(DAL.CurrentMTD.SubtitleStreams);
                }
            }
            else
            {
                if (ssUC != null)
                {
                    Controls.Remove(ssUC);

                    var sHeader = Controls.OfType<Panel>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == "SectionHeader_S");
                    if (sHeader != null)
                        Controls.Remove(sHeader);
                }
            }
        }

        public void SetMovieStills(List<byte[]> movieStills)
        {
            if (movieStills.Count > 0)
            {
                using (var ms = new MemoryStream(movieStills[0]))
                {
                    pbMovieStill1.Image = Image.FromStream(ms);
                }
            }

            if (movieStills.Count > 1)
            {
                using (var ms = new MemoryStream(movieStills[1]))
                {
                    pbMovieStill2.Image = Image.FromStream(ms);
                }
            }

            if (movieStills.Count > 2)
            {
                using (var ms = new MemoryStream(movieStills[2]))
                {
                    pbMovieStill3.Image = Image.FromStream(ms);
                }
            }

            var anyVisible = movieStills.Count > 0;
            tlpMovieStillsWrapper.Visible = anyVisible;
            pMovieDetail.Size = new Size(pMovieDetail.Width, anyVisible ? 565 : 365);
            //pMovieDetail.Size = new Size(pMovieDetail.Width, anyVisible ? 865 : 555);
        }

        public void SetPoster(byte[] bytes)
        {
            if (bytes == null)
            {
                pbCover.Image = null;
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    ms.Write(bytes, 0, bytes.Length);
                    pbCover.Image = Image.FromStream(ms);
                }
            }

            if (_isNew)
            {
                if (DAL.NewMTD == null)
                    DAL.TmpPoster = bytes;
                else
                    DAL.NewMTD.Poster = bytes;
            }
            else
            {
                //2020.12 ->it must go in DAL.CurrentMTD (and from there to the cached list) only when saved!
                //DAL.CurrentMTD.Poster = bytes;
                DAL.TmpPoster = bytes;
            }
        }

        private void cbTheme_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }

        private void chbTitle_MouseClick(object sender, MouseEventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }

        public bool ValidateInput()
        {
            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                lbSeriesTitle.ForeColor = Color.Red;
                return false;
            }

            return true;
        }

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            lbSeriesTitle.ForeColor = SystemColors.WindowText;
        }

        private void tbmDuration_Enter(object sender, EventArgs e)
        {
            //BeginInvoke((Action) delegate { SetMaskedTextBoxSelectAll((MaskedTextBox) sender); });
        }

        //private void SetMaskedTextBoxSelectAll(MaskedTextBox txtbox)
        //{
        //    txtbox.SelectAll();
        //}

        private void tbmDuration_Leave(object sender, EventArgs e)
        {
            //tbmDuration.Select(0, 0);
        }

        private void tbmDuration_KeyUp(object sender, KeyEventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }

        private void tbmDuration_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            //ValidatingType is set on UserControl Load event!
            if (!e.IsValidInput)
            {
                MsgBox.Show("The duration is not a valid Time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //tbmDuration.SelectAll();
                tbmDuration.Text = "00:00:00";
                e.Cancel = true;
            }
        }

        private void tbSizeAsInt_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSize_TextChanged(object sender, EventArgs e)
        {

        }

        public void SetNewPoster(string imgPath)
        {
            try
            {
                using (Image img = Image.FromFile(imgPath))
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Close();
                    var poster = ms.ToArray();
                    SetPoster(poster);
                }

                Helpers.UnsavedChanges = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PMovieDetail_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void PMovieDetail_DragDrop(object sender, DragEventArgs e)
        {
            var droppedObj = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            try
            {
                if (File.GetAttributes(droppedObj).HasFlag(FileAttributes.Directory))
                {
                    var opRes = Utils.Helpers.GetFilesDetails(droppedObj, ParentForm);

                    if (!opRes.Success)
                        MsgBox.Show(opRes.CustomErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                var picturesExt = new string[] { ".jpg", ".jpeg", ".png", ".bmp" };
                var textFilesExt = new string[] { ".txt", ".srt" };
                var moviesExt = new string[] { ".mkv", ".mp4", ".avi" };

                if (Array.IndexOf(picturesExt, Path.GetExtension(droppedObj).ToLower()) >= 0)
                {
                    //SetNewPoster(droppedObj);
                    using (var file = new FileStream(droppedObj, FileMode.Open, FileAccess.Read))
                    {
                        var bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);

                        SetPoster(bytes);
                        Helpers.UnsavedChanges = true;
                    }
                }
                else
                if (Array.IndexOf(textFilesExt, Path.GetExtension(droppedObj).ToLower()) >= 0)
                {
                    Utils.Helpers.FixDiacriticsInTextFile(droppedObj);
                }
                else
                if (Array.IndexOf(moviesExt, Path.GetExtension(droppedObj).ToLower()) >= 0 && _isNew)
                {
                    //todo: move importfrom file in Utils and call the code here as well
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bGotoRecommendedSite_Click(object sender, EventArgs e)
        {
            if (_hasRecommendedDataSaved)
            {
                var frmRecommendedData = new FrmRecommendedData(DAL.CurrentMTD.Id);
                frmRecommendedData.ShowDialog();
            }
            else
            {
                if (!string.IsNullOrEmpty(tbRecommendedLink.Text))
                    System.Diagnostics.Process.Start(tbRecommendedLink.Text);
            }
        }

        private void bGotoTrailer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTrailer.Text))
                System.Diagnostics.Process.Start(tbTrailer.Text);
        }

        private void bGotoDescription_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDescriptionLink.Text))
                System.Diagnostics.Process.Start(tbDescriptionLink.Text);
        }

        private void tbDescriptionLink_Leave(object sender, EventArgs e)
        {
            if (tbDescriptionLink.Text.ToLower().Contains("imdb") && tbDescriptionLink.Text.ToLower().Contains("?ref"))
                tbDescriptionLink.Text = tbDescriptionLink.Text.Substring(0, tbDescriptionLink.Text.ToLower().IndexOf("?ref"));

            TryGetSynopsis();
        }

        private void TryGetSynopsis()
        {
            if (!string.IsNullOrEmpty(tbSynopsis.Text))
                return;

            tbDescriptionLink.DataBindings[0].WriteValue();

            if (string.IsNullOrEmpty(tbDescriptionLink.Text))
            {
                tbSynopsis.Text = string.Empty;
                tbSynopsis.DataBindings[0].WriteValue();
            }
            else
            {
                try
                {
                    pbLoader.Visible = true;

                    var opRes = WebScraping.GetSynopsis(tbDescriptionLink.Text);

                    if (!opRes.Success)
                    {
                        Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Synopsis retrieval",
                            opRes.CustomErrorMessage, 5000, (this.Parent as Form));
                    }
                    else
                    {
                        tbSynopsis.Text = (string)opRes.AdditionalDataReturn;
                        tbSynopsis.DataBindings[0].WriteValue();

                        if (Helpers.GenericSetButtonsState2 != null)
                            Helpers.GenericSetButtonsState2(true);
                    }
                }
                finally
                {
                    pbLoader.Visible = false;
                }
            }
        }

        private void tbDescriptionLink_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                TryGetSynopsis();
        }

        private void bRefreshCSMData_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(DAL.CurrentMTD.RecommendedLink))
                {
                    if (!DAL.CurrentMTD.RecommendedLink.ToLower().Contains("commonsensemedia"))
                    {
                        Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                            "The 'recommended' data is from a site which doesn't have scraper and parser built!", 5000, ParentForm);
                    }
                    else
                    {
                        var opRes = WebScraping.GetCommonSenseMediaData(DAL.CurrentMTD.RecommendedLink);

                        if (!opRes.Success)
                        {
                            Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                                opRes.CustomErrorMessage, 5000, ParentForm);
                        }
                        else
                        {
                            opRes = DAL.SaveCommonSenseMediaData(DAL.CurrentMTD.Id, (CSMScrapeResult)opRes.AdditionalDataReturn);

                            if (!opRes.Success)
                            {
                                Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                                    opRes.CustomErrorMessage, 5000, ParentForm);
                            }
                            else
                            {
                                _hasRecommendedDataSaved = true;
                                Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Information, "Recommended data",
                                    "CommonSenseMedia data was scraped, parsed and saved succesfully!", 5000, ParentForm);
                            }
                        }
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        //private void CheckSizeMismatch()
        //{
        //    var b = (string.IsNullOrEmpty(tbSizeAsInt.Text) || tbSizeAsInt.Text == "0") && tbSize.Text != string.Empty && tbSize.Text != "0";
        //    decimal sizeCalc = 0m;

        //    if (!b)
        //    {
        //        long sizeAsLong = 0;

        //        if (!long.TryParse(tbSizeAsInt.Text, out sizeAsLong))
        //            b = true;
        //        else
        //        {
        //            sizeCalc =
        //                tbSize.Text.ToLower().IndexOf("gb") > 0
        //                    ? Math.Truncate(((decimal)Math.Abs(sizeAsLong) / 1024 / 1024 / 1024) * 100) / 100
        //                    : Math.Truncate(((decimal)Math.Abs(sizeAsLong) / 1024 / 1024) * 100) / 100;

        //            decimal valFromNiceStr = 0m;
        //            if (!decimal.TryParse(tbSize.Text.Replace(" ", "").Replace("Gb", "").Replace("Mb", ""), out valFromNiceStr))
        //                b = true;
        //            else
        //            {
        //                b = Math.Abs(valFromNiceStr - sizeCalc) > (decimal)0.5;
        //            }
        //        }
        //    }

        //    tbSize.ButtonVisible = b;

        //    if (b)
        //    {
        //        tbSize.ButtonToolTip =
        //            "Size mismatch!" + Environment.NewLine +
        //            string.Format("Current values: {0} (database display value) and {1} (database raw value)",
        //                tbSize.Text.Replace(" ", "").Replace("Gb", "").Replace("Mb", ""),
        //                sizeCalc) + Environment.NewLine +
        //            "Possible cause: a previously edited value was provided in a format that couldn't be transformed into a number representation." + Environment.NewLine +
        //            "Please revise the FileSize value!";
        //    }
        //}
    }
}
