using DAL;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Utils.Properties;

namespace Utils
{
    public partial class FrmMTDFromFile : Form
    {
        private string _filePath = string.Empty;
        private bool _isNew;

        public MovieTechnicalDetails mtd;

        public FrmMTDFromFile()
        {
            InitializeComponent();
        }

        public FrmMTDFromFile(bool isNew, bool isEpisode)
        {
            InitializeComponent();

            _isNew = isNew;

            Text = isEpisode
                ? "Refresh episode data from file"
                : "Refresh movie data from file";
        }

        private void btnFolderSelector_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Episode file";
                openFileDialog.Filter = "Video files (*.mkv, *.mp4, *.m4v, *.avi)|*.mkv;*.mp4;*.m4v;*.avi|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = !string.IsNullOrEmpty(Settings.Default.LastPath) ? Path.GetDirectoryName(Settings.Default.LastPath) : "";

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                Settings.Default.LastPath = Path.GetFullPath(openFileDialog.FileName);
                Settings.Default.Save();

                _filePath = Path.GetFullPath(openFileDialog.FileName);
                toolTip.SetToolTip(tbFileName, _filePath);
                tbFileName.Text = Path.GetFileName(_filePath);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbFileName.Text == string.Empty)
            {
                lbFilename.ForeColor = Color.Red;

                MsgBox.Show("Please specify all required import parameters!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            btnOk.Text = "Working ...";
            btnOk.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            var opRes = FilesMetaData.GetFileTechnicalDetails(_filePath);

            if (!opRes.Success)
            {
                MsgBox.Show(opRes.CustomErrorMessage, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOk.Text = "Ok";
                btnOk.Enabled = true;
                Cursor.Current = Cursors.Default;
                return;
            }

            mtd = (MovieTechnicalDetails)opRes.AdditionalDataReturn;
            mtd.Quality = Desene.DAL.GetQualityStrFromSize(mtd);
            mtd.AudioLanguages = string.Join(", ", mtd.AudioStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
            mtd.SubtitleLanguages = string.Join(", ", mtd.SubtitleStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());

            if (cbGenerateThumbnails.Checked)
            {
                FilesMetaData.GetMovieStills(mtd, null);
            }

            if (!_isNew)
            {
                mtd.Year = Desene.DAL.CurrentMTD.Year;
                mtd.Season = Desene.DAL.CurrentMTD.Season;

                mtd.DescriptionLink = Desene.DAL.CurrentMTD.DescriptionLink;
                mtd.Recommended = Desene.DAL.CurrentMTD.Recommended;
                mtd.RecommendedLink = Desene.DAL.CurrentMTD.RecommendedLink;
                mtd.Year = Desene.DAL.CurrentMTD.Year;
                mtd.Theme = Desene.DAL.CurrentMTD.Theme;
                mtd.Notes = Desene.DAL.CurrentMTD.Notes;
                mtd.Trailer = Desene.DAL.CurrentMTD.Trailer;
                mtd.StreamLink = Desene.DAL.CurrentMTD.StreamLink;
                mtd.Poster = Desene.DAL.CurrentMTD.Poster;
                mtd.InsertedDate = Desene.DAL.CurrentMTD.InsertedDate;
                mtd.LastChangeDate = DateTime.Now;

                //CMA: the CurrentMTD will be reset after save
                //Desene.DAL.CurrentMTD = mtd;
            }

            Cursor.Current = Cursors.Default;
            DialogResult = DialogResult.OK;
            Close();
        }
        private void tbFilesLocation_TextChanged(object sender, EventArgs e)
        {
            lbFilename.ForeColor = SystemColors.WindowText;
        }
    }
}
