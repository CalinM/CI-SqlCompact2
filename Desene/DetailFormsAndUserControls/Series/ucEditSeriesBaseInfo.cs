using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Common;
using DAL;
using Desene.DetailFormsAndUserControls.Shared;
using Utils;

namespace Desene.EditUserControls
{
    public partial class ucEditSeriesBaseInfo : UserControl
    {
        private bool _isNew;
        private BindingSource _bsControlsData;
        private bool _hasRecommendedDataSaved;

        public string Title
        {
            get { return tbTitle.Text; }
        }

        public string DescriptionLink
        {
            get { return tbDescriptionLink.Text; }
        }

        public string Recommended
        {
            get { return tbRecommended.Text; }
        }

        public string RecommendedLink
        {
            get { return tbRecommendedLink.Text; }
        }

        public string Notes
        {
            get { return tbNotes.Text; }
        }

        public string Trailer
        {
            get { return tbTrailer.Text; }
        }

        /*
        private byte[] _poster;
        public byte[] Poster
        {
            get { return _poster; }
            set
            {
                _poster = value;

                using (var ms = new MemoryStream(_poster))
                {
                    pbCover.Image = Image.FromStream(ms);
                }
            }
        }*/

        //public ucEditSeriesBaseInfo()
        //{
        //    InitializeComponent();
        //}

        public ucEditSeriesBaseInfo(bool isNew = true)
        {
            InitializeComponent();
            _isNew = isNew;

            if (!isNew)
            {
                InitControls();
                RefreshControls();
            }
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            tbTitle.DataBindings.Add("Text", _bsControlsData, "FileName");
            tbDescriptionLink.DataBindings.Add("Text", _bsControlsData, "DescriptionLink");
            tbRecommended.DataBindings.Add("Text", _bsControlsData, "Recommended");
            tbRecommendedLink.DataBindings.Add("Text", _bsControlsData, "RecommendedLink");
            tbNotes.DataBindings.Add("Text", _bsControlsData, "Notes");
            tbTrailer.DataBindings.Add("Text", _bsControlsData, "Trailer");
            pbCover.DataBindings.Add("Image", _bsControlsData, "Poster", true);

            var tt2 = new ToolTip(); //ttTitleContent not working?!!
            tt2.SetToolTip(bGotoDescription, "Navigate using the default system browser to the current Description link");
            tt2.SetToolTip(bGotoTrailer, "Navigate using the default system browser to the current Trailer link");
            tt2.SetToolTip(bRefreshCSMData, "Reload data from CommonSenseMedia");
        }

        public void RefreshControls(MovieTechnicalDetails mtd = null)
        {
            _bsControlsData.DataSource = mtd ?? DAL.CurrentMTD;
            _bsControlsData.ResetBindings(false);

            _hasRecommendedDataSaved = DAL.CurrentMTD.HasRecommendedDataSaved;

            var tt3 = new ToolTip(); //ttTitleContent not working?!!
            tt3.SetToolTip(bGotoRecommendedSite, _hasRecommendedDataSaved
                ? "Displays a window showing the last scraped/passed data from CommmonSenseMedia site"
                : "Navigate using the default system browser to the current CommonSenseMedia link");
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

        public void SetPoster(byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                pbCover.Image = Image.FromStream(ms);
            }

            //2020.12 -> it must go in DAL.CurrentMTD (and from there to the cached list) only when saved!
            DAL.TmpPoster = bytes;
        }

        private void UcEditSeriesBaseInfo_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void UcEditSeriesBaseInfo_DragDrop(object sender, DragEventArgs e)
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

                if (Array.IndexOf(picturesExt, Path.GetExtension(droppedObj).ToLower()) >= 0)
                {
                    //SetNewPoster(droppedObj);
                    using (var file = new FileStream(droppedObj, FileMode.Open, FileAccess.Read))
                    {
                        var bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);

                        SetPoster(bytes);
                        Common.Helpers.UnsavedChanges = true;
                    }
                }
                else
                if (Array.IndexOf(textFilesExt, Path.GetExtension(droppedObj).ToLower()) >= 0)
                {
                    Utils.Helpers.FixDiacriticsInTextFile(droppedObj);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bGotoDescription_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDescriptionLink.Text))
                System.Diagnostics.Process.Start(tbDescriptionLink.Text);
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

        private void tbDescriptionLink_Leave(object sender, EventArgs e)
        {
            if (tbDescriptionLink.Text.ToLower().Contains("imdb") && tbDescriptionLink.Text.ToLower().Contains("ref"))
                tbDescriptionLink.Text = tbDescriptionLink.Text.Substring(0, tbDescriptionLink.Text.ToLower().IndexOf("ref")-1);

            /*
            ref-1 because it can be like:
            https://www.imdb.com/title/tt00000000/?ref_=fn_al_tt_1
            https://www.imdb.com/title/tt00000000/episodes?season=1&ref_=tt_eps_sn_1
            */
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
    }
}
