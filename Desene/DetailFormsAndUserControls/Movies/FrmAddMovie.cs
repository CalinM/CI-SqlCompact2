using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Common;
using DAL;
using Desene.Properties;

using Utils;

namespace Desene
{
    public partial class FrmAddMovie : EscapeForm
    {
        private int? _collectionId;

        public int NewId;

        public FrmAddMovie(int? collectionId = null)
        {
            InitializeComponent();

            _collectionId = collectionId;
            DAL.NewMTD = null;
        }

        private void btnImportMovieData_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/1210026/return-a-value-from-an-event-is-there-a-good-practice-for-this
            //if (OnImportMovie is null) return;

            //OnImportMovie(sender, e);
            var currentPoster = DAL.TmpPoster;

            if (DAL.NewMTD != null)
            {
                if (MsgBox.Show(
                        "The previous movie details and all changes made by hand (except the poster) will be lost. Are you sure you want to continue?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                currentPoster = DAL.NewMTD.Poster;
            }

            using (var rParam = new FrmMTDFromFile(true, false) { Owner = this })
            {
                if (rParam.ShowDialog() != DialogResult.OK)
                    return;

                if (rParam.mtd == null)
                {
                    MsgBox.Show("An error occurred while determining the file (movie) details. No additional data available!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DAL.NewMTD = rParam.mtd;
            }

            DAL.NewMTD.Poster = currentPoster;
            DAL.TmpPoster = null;
            ucMovieInfo1.RefreshControls(DAL.NewMTD);

            if (DAL.NewMTD.MovieStills.Count > 0)
            {
                ucMovieInfo1.SetMovieStills(DAL.NewMTD.MovieStills);
                Size = new Size(900, 625);
            }
            else
            {
                Size = new Size(900, 430);
            }

            btnSave.Enabled = true;
            //btnLoadPoster.Enabled = true;
        }

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            var dialog = new CustomDialogs
            {
                Title = "Choose a poster",
                DialogType = DialogType.OpenFile,
                InitialDirectory = Settings.Default.LastCoverPath,
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*",
                FileNameLabel = "FileName or URL",
                //ConfirmButtonText = "Confirm"
            };

            if (!dialog.Show(Handle)) return;

            Settings.Default.LastCoverPath = Path.GetFullPath(dialog.FileName);
            Settings.Default.Save();

            ucMovieInfo1.SetNewPoster(dialog.FileName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //making sure the latest values go into the BindingSource (that happen only when the control loses focus)
            ucMovieInfo1.tbEncodedWith.Focus();

            if (!ucMovieInfo1.ValidateInput())
            {
                MsgBox.Show("Please specify all required details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DAL.NewMTD.ParentId = _collectionId;
            var opRes = DAL.InsertMTD(DAL.NewMTD, null);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while inserting the new Movie into the database:{0}{0}{1}{0}{0}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ucMovieInfo1.tbTitle.Focus();
                return;
            }

            DAL.NewMTD.Id = (int)opRes.AdditionalDataReturn;
            DAL.CurrentMTD = DAL.NewMTD;
            Common.Helpers.UnsavedChanges = false;

            if (ucMovieInfo1.GetCSMData && !string.IsNullOrEmpty(DAL.CurrentMTD.RecommendedLink))
            {
                if (!DAL.CurrentMTD.RecommendedLink.ToLower().Contains("commonsensemedia"))
                {
                    Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                        "The 'recommended' data is from a site which doesn't have scraper and parser built!", 5000, this);
                }
                else
                {
                    opRes = WebScraping.GetCommonSenseMediaData(DAL.CurrentMTD.RecommendedLink);

                    if (!opRes.Success)
                    {
                        Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                            opRes.CustomErrorMessage, 5000, this);
                    }
                    else
                    {
                        opRes = Desene.DAL.SaveCommonSenseMediaData(DAL.CurrentMTD.Id, (CSMScrapeResult)opRes.AdditionalDataReturn);

                        if (!opRes.Success)
                        {
                            Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                                opRes.CustomErrorMessage, 5000, this);
                        }
                    }
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOpenPages_Click(object sender, EventArgs e)
        {
            var movieName = ucMovieInfo1.MovieTitle;

            if (string.IsNullOrEmpty(movieName))
            {
                MsgBox.Show("Movie title is mandatory!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Process.Start("http://www.google.com/search?q=" + Uri.EscapeDataString(movieName + "+cinemagia"), "_blank");
            Process.Start("http://www.google.com/search?q=" + Uri.EscapeDataString(movieName + "+imdb"), "_blank");
            Process.Start("https://www.commonsensemedia.org/search/" + Uri.EscapeDataString(movieName), "_blank");
            Process.Start("https://www.youtube.com/results?search_query=" + movieName.Replace(" ", "+") + "+trailer", "_blank");
        }
    }
}
