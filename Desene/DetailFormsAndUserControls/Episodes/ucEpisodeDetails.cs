using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using DAL;

using Desene.DetailFormsAndUserControls.Episodes;

using Utils;

namespace Desene.DetailFormsAndUserControls
{
    public partial class ucEpisodeDetails : UserControl
    {
        public ucEpisodeDetails()
        {
            InitializeComponent();
        }

        public ucEpisodeDetails(int episodeId, int seriesId)
        {
            InitializeComponent();
            LoadControls(episodeId, seriesId);
        }

        //"seriesId" can be null when the Episode is changed (in the same Series)
        public void LoadControls(int episodeId, int? seriesId)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SuspendLayout();

                var cachedMovieStills = DAL.CachedMoviesStills.FirstOrDefault(ms => ms.FileDetailId == episodeId);
                if (cachedMovieStills != null)
                {
                    SetMovieStills(cachedMovieStills);
                }
                else
                {
                    cachedMovieStills = DAL.LoadMovieStills(episodeId);

                    if (cachedMovieStills != null)
                    {
                        DAL.CachedMoviesStills.Add(cachedMovieStills);
                        SetMovieStills(cachedMovieStills);
                    }
                }

                if (seriesId != null)
                {
                    lbSeriesTitle_Value.Text = DAL.Series.Select("Id = " + seriesId)[0]["FileName"].ToString();
                }

                var opRes = DAL.LoadMTD(episodeId);

                if (!opRes.Success)
                {
                    MsgBox.Show(string.Format("The following error occurred while loading the files details:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                        @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                /*
                var fileInfoRow = DAL.Series.Select("Id = " + episodeId)[0];
                tbEpisodeName.Text = fileInfoRow["FileName"].ToString();
                cbQuality.Text = fileInfoRow["Quality"].ToString();
                tbSeason.Text = fileInfoRow["Season"].ToString();
                tbYear.Text = fileInfoRow["Year"].ToString();
                tbSize.Text = fileInfoRow["FileSize2"].ToString();
                tbmDuration.Text = ((DateTime)fileInfoRow["Duration"]).ToString("HH:mm:ss");
                tbAudioSummary.Text = fileInfoRow["AudioLanguages"].ToString();
                tbSubtitleSummary.Text = fileInfoRow["SubtitleLanguages"].ToString();
                tbFormat.Text = fileInfoRow["Format"].ToString();
                tbEncodedWith.Text = fileInfoRow["Encoded_Application"].ToString();

                chbHasEmbeddedTitle.Checked = fileInfoRow["TitleEmbedded"].ToString().ToUpper() == "TRUE";
                cbHasEmbeddedCover.Checked = fileInfoRow["CoverEmbedded"].ToString().ToUpper() == "TRUE";
                */

                if (tbEpisodeName.DataBindings.Count == 0)
                {
                tbEpisodeName.DataBindings.Add("Text", DAL.CurrentMTD, "FileName");
                cbQuality.DataBindings.Add("Text", DAL.CurrentMTD, "Quality");
                tbSeason.DataBindings.Add("Text", DAL.CurrentMTD, "Season");
                tbYear.DataBindings.Add("Text", DAL.CurrentMTD, "Year");
                tbSize.DataBindings.Add("Text", DAL.CurrentMTD, "FileSize2");
                tbmDuration.DataBindings.Add("Text", DAL.CurrentMTD, "DurationFormatted");
                tbAudioSummary.DataBindings.Add("Text", DAL.CurrentMTD, "AudioLanguages");
                tbSubtitleSummary.DataBindings.Add("Text", DAL.CurrentMTD, "SubtitleLanguages");
                tbFormat.DataBindings.Add("Text", DAL.CurrentMTD, "Format");
                tbEncodedWith.DataBindings.Add("Text", DAL.CurrentMTD, "Encoded_Application");

                chbHasEmbeddedTitle.DataBindings.Add("Checked", DAL.CurrentMTD, "HasTitle");
                }


                //foreach (var vsCtrl in Controls.OfType<ucGenericStreamsWrapper>())
                //{
                //    Controls.Remove(vsCtrl);
                //}

                //foreach (var vsCtrl in Controls.Cast<Control>().Where(c => c.Tag != null)) //.ToString() == "SectionHeader"
                //{
                //    Controls.Remove(vsCtrl);
                //}

                //var mtd = (MovieTechnicalDetails)opRes.AdditionalDataReturn;

                var vsUC = Controls.OfType<UserControl>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == "videoStreams");
                if (vsUC == null)
                {
                    AddSectionHeader("Video stream(s)");

                    //var ucVideoStreams = new ucGenericStreamsWrapper(mtd.VideoStreams) { Dock = DockStyle.Top };
                    var ucVideoStreams = new ucGenericStreamsWrapper(DAL.CurrentMTD.VideoStreams) { Dock = DockStyle.Top };
                    ucVideoStreams.Tag = "videoStreams";
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
                    AddSectionHeader("Audio stream(s)");

                    //var ucAudioStreams = new ucGenericStreamsWrapper(mtd.AudioStreams) { Dock = DockStyle.Top };
                    var ucAudioStreams = new ucGenericStreamsWrapper(DAL.CurrentMTD.AudioStreams) { Dock = DockStyle.Top };
                    ucAudioStreams.Tag = "audioStreams";
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
                        AddSectionHeader("Subtitle stream(s)");

                        //var ucSubtitleStreams = new ucGenericStreamsWrapper(mtd.SubtitleStreams) { Dock = DockStyle.Top };
                        var ucSubtitleStreams = new ucGenericStreamsWrapper(DAL.CurrentMTD.SubtitleStreams) { Dock = DockStyle.Top };
                        ucSubtitleStreams.Tag = "subtitleStreams";
                        Controls.Add(ucSubtitleStreams);
                        ucSubtitleStreams.BringToFront();
                    }
                    else
                    {
                        ((ucGenericStreamsWrapper)asUC).LoadControlsForSubtitleStreams(DAL.CurrentMTD.SubtitleStreams);
                    }
                }
                else
                {
                    if (ssUC != null)
                    {
                        Controls.Remove(ssUC);
                    }
                }
            }
            finally
            {
                ResumeLayout();
                Cursor = Cursors.Default;
            }
        }

        private void AddSectionHeader(string caption)
        {
            var pHeader = new Panel();
            pHeader.Dock = DockStyle.Top;
            pHeader.BackColor = Color.DimGray;
            pHeader.Size = new Size(250, 25);
            pHeader.Tag = "SectionHeader";

            var lbHeaderText = new Label();
            lbHeaderText.ForeColor = Color.White;
            lbHeaderText.Font = new Font(lbHeaderText.Font, FontStyle.Bold);
            lbHeaderText.Location = new Point(9,6);
            lbHeaderText.Text = caption;
            pHeader.Controls.Add(lbHeaderText);

            Controls.Add(pHeader);
            pHeader.BringToFront();
        }

        private void SetMovieStills(CachedMovieStills cachedMovieStills)
        {
            if (cachedMovieStills.MovieStills.Count > 0)
            {
                using (var ms = new MemoryStream(cachedMovieStills.MovieStills[0]))
                {
                    pbMovieStill1.Image = Image.FromStream(ms);
                }
            }

            if (cachedMovieStills.MovieStills.Count > 1)
            {
                using (var ms = new MemoryStream(cachedMovieStills.MovieStills[1]))
                {
                    pbMovieStill2.Image = Image.FromStream(ms);
                }
            }

            if (cachedMovieStills.MovieStills.Count > 2)
            {
                using (var ms = new MemoryStream(cachedMovieStills.MovieStills[2]))
                {
                    pbMovieStill3.Image = Image.FromStream(ms);
                }
            }
        }

        private void tbEpisodeName_TextChanged(object sender, EventArgs e)
        {
            Common.Helpers.UnsavedChanges = true;
        }
    }
}
