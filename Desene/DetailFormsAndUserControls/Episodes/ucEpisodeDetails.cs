using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Common;
using DAL;
using Desene.DetailFormsAndUserControls.Shared;
using Utils;

using Helpers = Common.Helpers;

namespace Desene.DetailFormsAndUserControls
{
    public partial class ucEpisodeDetails : UserControl
    {
        private BindingSource _bsControlsData;

        public ucEpisodeDetails()
        {
            InitializeComponent();
        }

        public ucEpisodeDetails(int episodeId, int seriesId, string seriesName)
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic, null,
                pEpisodeDetails, new object[] { true });

            InitControls();
            LoadControls(episodeId, seriesId, seriesName);

            cbTheme.MouseWheel += Utils.Helpers.Combobox_OnMouseWheel;
            cbQuality.MouseWheel += Utils.Helpers.Combobox_OnMouseWheel;
        }

        private void ucEpisodeDetails_Load(object sender, EventArgs e)
        {
            tbmDuration.ValidatingType = typeof(TimeSpan);
        }

        //"seriesId" can be null when the Episode is changed (in the same Series)
        public void LoadControls(int episodeId, int? seriesId, string seriesName)
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
                    DAL.CachedMoviesStills.Add(cachedMovieStills);
                    SetMovieStills(cachedMovieStills);
                }

                if (seriesId != null)
                {
                    lbSeriesTitle_Value.Text = seriesName;
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
            switch (DAL.EpisodeParentType)
            {
                case EpisodeParentType.Series:
                    lbSeriesTitle.Text = "Series title:";
                    tbSeason.Enabled = true;
                    break;

                case EpisodeParentType.Collection:
                    lbSeriesTitle.Text = "Collection title:";
                    tbSeason.Enabled = false;
                    break;
            }


            _bsControlsData = new BindingSource();

            cbQuality.DataSource = Enum.GetValues(typeof(Quality));
            cbTheme.DataSource = DAL.MovieThemes;

            tbEpisodeName.DataBindings.Add("Text", _bsControlsData, "FileName");
            cbQuality.DataBindings.Add("Text", _bsControlsData, "Quality");
            tbSeason.DataBindings.Add("Text", _bsControlsData, "Season");
            tbYear.DataBindings.Add("Text", _bsControlsData, "Year");
            tbmDuration.DataBindings.Add("Text", _bsControlsData, "DurationFormatted");
            tbSizeAsInt.DataBindings.Add("Text", _bsControlsData, "FileSize");
            tbAudioSummary.DataBindings.Add("Text", _bsControlsData, "AudioLanguages");
            tbSubtitleSummary.DataBindings.Add("Text", _bsControlsData, "SubtitleLanguages");
            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbEncodedWith.DataBindings.Add("Text", _bsControlsData, "Encoded_Application");
            cbTheme.DataBindings.Add("Text", _bsControlsData, "Theme");
            tbStreamLink.DataBindings.Add("Text", _bsControlsData, "StreamLink");

            chbHasEmbeddedTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");
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
            _bsControlsData.DataSource = mtd;
            _bsControlsData.ResetBindings(false);

            ttTitleContent.RemoveAll();
            if (mtd.HasTitle && !string.IsNullOrEmpty(mtd.Title))
            {
                ttTitleContent.SetToolTip(chbHasEmbeddedTitle, mtd.Title);
                chbHasEmbeddedTitle.Cursor = Cursors.Help;
            }
            else
            {
                ttTitleContent.RemoveAll();
                chbHasEmbeddedTitle.Cursor = Cursors.Default;
            }

            //having it on "InitControls" will cause a crash, the column being NOT recognized!
            //only for this custom TextBox
            //todo: why?
            if (tbSize.DataBindings.Count == 0)
            {
                tbSize.DataBindings.Add("Text", _bsControlsData, "FileSize2");
                tbSize.ButtonToolTip =
                    "Size mismatch!" + Environment.NewLine +
                    "Possible cause: a previously edited value was provided in a format that couldn't be transformed into a number representation." + Environment.NewLine +
                    "Please revise the FileSize value!";

                //todo: copy implementation model from "Movies"
            }
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
                    if (ms != null && ms.Length > 0) //Rocket and Groot?!?!
                        pbMovieStill3.Image = Image.FromStream(ms);
                }
            }

            var anyVisible = cachedMovieStills.MovieStills.Count > 0;
            tlpMovieStillsWrapper.Visible = anyVisible;
            pEpisodeDetails.Size = new Size(pEpisodeDetails.Width, anyVisible ? 420 : 220);
        }

        private void tbSizeAsInt_TextChanged(object sender, EventArgs e)
        {
            tbSize.ButtonVisible = tbSizeAsInt.Text == "0" && tbSize.Text != string.Empty;
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

        private void cbTheme_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }

        private void chbHasEmbeddedTitle_MouseClick(object sender, MouseEventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }
    }
}