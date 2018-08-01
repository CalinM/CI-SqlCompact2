using System;
using System.Collections.Generic;
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
    public partial class ucMovieInfo : UserControl
    {
        private BindingSource _bsControlsData;

        public ucMovieInfo()
        {
            InitializeComponent();

            InitControls();

            cbTheme.MouseWheel += Utils.Helpers.Combobox_OnMouseWheel;
            cbQuality.MouseWheel += Utils.Helpers.Combobox_OnMouseWheel;
        }

        private void ucMovieInfo_Load(object sender, EventArgs e)
        {
            tbmDuration.ValidatingType = typeof(TimeSpan);
            tbDummyForFocus.Location = new Point( tbNotes.Left + 10, tbNotes.Top + 10 );

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
            LoadThemesInControl();

            cbQuality.DataBindings.Add("Text", _bsControlsData, "Quality");
            tbSizeAsInt.DataBindings.Add("Text", _bsControlsData, "FileSize"); //SizeAsInt on RefreshControl ... see comment!
            tbmDuration.DataBindings.Add("Text", _bsControlsData, "DurationFormatted");
            tbAudioSummary.DataBindings.Add("Text", _bsControlsData, "AudioLanguages");
            tbSubtitleSummary.DataBindings.Add("Text", _bsControlsData, "SubtitleLanguages");
            tbNotes.DataBindings.Add("Text", _bsControlsData, "Notes");
            tbStreamLink.DataBindings.Add("Text", _bsControlsData, "StreamLink");
            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbEncodedWith.DataBindings.Add("Text", _bsControlsData, "Encoded_Application");
            cbTheme.DataBindings.Add("Text", _bsControlsData, "Theme");
            pbCover.DataBindings.Add("Image", _bsControlsData, "Poster", true);
        }

        public void LoadThemesInControl(bool refreshText = false)
        {
            cbTheme.DataSource = null;
            cbTheme.DataSource = DAL.MovieThemes;

            if (refreshText)
                cbTheme.Text = DAL.CurrentMTD.Theme;
            //else
            //    cbTheme.Text = string.Empty;

            //after a Save operation, it refreshed the AudioSummary and SubtitleSummary
            if (tbAudioSummary.DataBindings.Count > 0 && refreshText)
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
                    ttTitleContent.RemoveAll();
                    chbTitle.Cursor = Cursors.Default;
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
                }

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
            }
            finally
            {
                DrawingControl.ResumeDrawing(this);
                Cursor = Cursors.Default;
            }
        }

        private void LoadControls2()
        {
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
        }

        public void SetPoster(byte[] bytes, bool isNew)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                pbCover.Image = Image.FromStream(ms);
            }

            if (!isNew)
                DAL.CurrentMTD.Poster = bytes;
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

        private void SetMaskedTextBoxSelectAll(MaskedTextBox txtbox)
        {
            txtbox.SelectAll();
        }
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
    }
}
