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
        private BindingSource _bsControlsData;

        public ucEpisodeDetails()
        {
            InitializeComponent();
        }

        public ucEpisodeDetails(int episodeId, int seriesId)
        {
            InitializeComponent();

            InitControls();
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
                    lbSeriesTitle_Value.Text = "comented!!!!"; //DAL.Series.Select("Id = " + seriesId)[0]["FileName"].ToString();
                }

                LoadControls2(false);
            }
            finally
            {
                ResumeLayout();
                Cursor = Cursors.Default;
            }
        }

        public void LoadControls2(bool suspendLayout)
        {
            try
            {
                if (suspendLayout)
                {
                    Cursor = Cursors.WaitCursor;
                    SuspendLayout();
                }

                RefreshControls(DAL.CurrentMTD);


                var vsUC = Controls.OfType<UserControl>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == "videoStreams");
                if (vsUC == null)
                {
                    AddSectionHeader("Video stream(s)", "V");

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
                    AddSectionHeader("Audio stream(s)", "A");

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
                        AddSectionHeader("Subtitle stream(s)", "S");

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
            finally
            {
                if (suspendLayout)
                {
                    ResumeLayout();
                    Cursor = Cursors.Default;
                }
            }
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            tbEpisodeName.DataBindings.Add("Text", _bsControlsData, "FileName");
            cbQuality.DataBindings.Add("Text", _bsControlsData, "Quality");
            tbSeason.DataBindings.Add("Text", _bsControlsData, "Season");
            tbYear.DataBindings.Add("Text", _bsControlsData, "Year");
            tbSize.DataBindings.Add("Text", _bsControlsData, "FileSize2");
            tbmDuration.DataBindings.Add("Text", _bsControlsData, "DurationFormatted");
            tbAudioSummary.DataBindings.Add("Text", _bsControlsData, "AudioLanguages");
            tbSubtitleSummary.DataBindings.Add("Text", _bsControlsData, "SubtitleLanguages");
            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbEncodedWith.DataBindings.Add("Text", _bsControlsData, "Encoded_Application");

            chbHasEmbeddedTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");
        }

        public void RefreshControls(MovieTechnicalDetails mtd)
        {
            _bsControlsData.DataSource = mtd;
            _bsControlsData.ResetBindings(false);
        }

        private void AddSectionHeader(string caption, string identifier)
        {
            var pHeader = new Panel();
            pHeader.Dock = DockStyle.Top;
            pHeader.BackColor = Color.DimGray;
            pHeader.Size = new Size(350, 25);
            pHeader.Tag = "SectionHeader_" + identifier;

            var lbHeaderText = new Label();
            lbHeaderText.ForeColor = Color.White;
            lbHeaderText.Font = new Font(lbHeaderText.Font, FontStyle.Bold);
            lbHeaderText.Location = new Point(9,6);
            lbHeaderText.AutoSize = false;
            lbHeaderText.Size = new Size(350, 15);
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
    }
}
