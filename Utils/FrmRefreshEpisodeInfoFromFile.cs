using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;
using Utils.Properties;

namespace Utils
{
    public partial class FrmRefreshEpisodeInfoFromFile : Form
    {
        private int? _parentId;
        public FilesImportParams EpisodesImportParams;
        public FrmRefreshEpisodeInfoFromFile()
        {
            InitializeComponent();
        }

        public FrmRefreshEpisodeInfoFromFile(int parentId)
        {
            InitializeComponent();

            _parentId = parentId;
        }

        private void btnFolderSelector_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Open Text File";
                openFileDialog.Filter = "Video files (*.mkv, *.mp4, *.m4v, *.avi)|*.mkv;*.mp4;*.m4v;*.avi|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Settings.Default.LastPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (tbFilesLocation.Text == string.Empty || /*tbYear.Text == string.Empty ||*/
            //    cbFileExtensions.SelectedIndex == -1 || tbSeason.Text == string.Empty)
            //{
            //    if (tbFilesLocation.Text == string.Empty)
            //        lbLocation.ForeColor = Color.Red;
            //    if (cbFileExtensions.SelectedIndex == -1)
            //        lbFilesExtensions.ForeColor = Color.Red;
            //    if (tbSeason.Text == string.Empty)
            //        lbSeason.ForeColor = Color.Red;

            //    MsgBox.Show(@"Please specify all required import parameters!", @"Error", MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);

            //    return;
            //}

            //EpisodesImportParams = new FilesImportParams
            //                           {
            //                               ParentId = _parentId,
            //                               Location = tbFilesLocation.Text,
            //                               FilesExtension = cbFileExtensions.Text,
            //                               Season = tbSeason.Text,
            //                               Year = tbYear.Text,
            //                               GenerateThumbnail = cbGenerateThumbnails.Checked
            //                           };

            DialogResult = DialogResult.OK;
            Close();
        }
        private void tbFilesLocation_TextChanged(object sender, EventArgs e)
        {
            lbLocation.ForeColor = SystemColors.WindowText;
        }
    }
}
