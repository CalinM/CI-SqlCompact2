using Common.ExtensionMethods;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using DAL;

using Utils.Properties;

namespace Utils
{
    public partial class FrmMoviesInfoFromFiles : Form
    {
        public FilesImportParams MoviesImportParams;

        public FrmMoviesInfoFromFiles()
        {
            InitializeComponent();
        }

        private void btnFolderSelector_Click(object sender, EventArgs e)
        {
            var selectedPath = Helpers.SelectFolder("Please select the movies location (folder)", Settings.Default.LastPath);
            if (string.IsNullOrEmpty(selectedPath))
                return;

            Settings.Default.LastPath = selectedPath;
            Settings.Default.Save();

            tbFilesLocation.Text = selectedPath;

            var files = Directory.GetFiles(selectedPath, "*.*");

            if (!files.ToList().DistinctBy(Path.GetExtension).Any())
                cbFileExtensions.Text = @"folder empty!";
            else
            {
                var ext = "*" + Path.GetExtension(files.ToList().DistinctBy(Path.GetExtension).FirstOrDefault());
                if (cbFileExtensions.Items.IndexOf(ext) >= 0)
                {
                    cbFileExtensions.Text = ext;
                }
            }
        }

        private void cbGenerateThumbnails_CheckedChanged(object sender, EventArgs e)
        {
            lbWarning.Visible = cbGenerateThumbnails.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbFilesLocation.Text == string.Empty || cbFileExtensions.SelectedIndex == -1)
            {
                if (tbFilesLocation.Text == string.Empty)
                    lbLocation.ForeColor = Color.Red;
                if (cbFileExtensions.SelectedIndex == -1)
                    lbFilesExtensions.ForeColor = Color.Red;

                MsgBox.Show("Please specify all required import parameters!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            MoviesImportParams = new FilesImportParams
            {
                Location = tbFilesLocation.Text,
                FilesExtension = cbFileExtensions.Text,
                GenerateThumbnail = cbGenerateThumbnails.Checked,
                ForceAddMissingEntries = cbForceAddMissingMovies.Checked,
                PreserveManuallySetData = cbPreserveManuallySetData.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void tbFilesLocation_TextChanged(object sender, EventArgs e)
        {
            lbLocation.ForeColor = SystemColors.WindowText;
        }

        private void cbFileExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbFilesExtensions.ForeColor = SystemColors.WindowText;
        }
    }
}
