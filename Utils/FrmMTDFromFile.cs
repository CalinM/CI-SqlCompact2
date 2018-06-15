using DAL;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils.Properties;

namespace Utils
{
    public partial class FrmMTDFromFile : Form
    {
        private string _filePath = string.Empty;

        public FrmMTDFromFile()
        {
            InitializeComponent();
        }

        public FrmMTDFromFile(bool updatingEpisode)
        {
            InitializeComponent();
            cbGenerateThumbnails.Visible = updatingEpisode;
        }

        private void btnFolderSelector_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Episode file";
                openFileDialog.Filter = "Video files (*.mkv, *.mp4, *.m4v, *.avi)|*.mkv;*.mp4;*.m4v;*.avi|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Settings.Default.LastPath;

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

            var opRes = FilesMetaData.GetFileTechnicalDetails(_filePath);

            if (!opRes.Success)
            {
                MsgBox.Show(opRes.CustomErrorMessage, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newMTD = (MovieTechnicalDetails)opRes.AdditionalDataReturn;
            newMTD.Year = Desene.DAL.CurrentMTD.Year;
            newMTD.Season = Desene.DAL.CurrentMTD.Season;
            newMTD.Quality = Desene.DAL.GetQualityStrFromSize(newMTD);
            newMTD.AudioLanguages = string.Join(", ", newMTD.AudioStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
            newMTD.SubtitleLanguages = string.Join(", ", newMTD.SubtitleStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());

            Desene.DAL.CurrentMTD = newMTD;

            DialogResult = DialogResult.OK;
            Close();
        }
        private void tbFilesLocation_TextChanged(object sender, EventArgs e)
        {
            lbFilename.ForeColor = SystemColors.WindowText;
        }
    }
}
