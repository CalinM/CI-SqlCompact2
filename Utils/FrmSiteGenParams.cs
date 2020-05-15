using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Utils
{
    public partial class FrmSiteGenParams : Form
    {
        public SiteGenParams SiteGenParams;

        public FrmSiteGenParams(string lastPath)
        {
            InitializeComponent();

            tbFilesLocation.Text = lastPath;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (tbFilesLocation.Text == string.Empty)
            {
                if (tbFilesLocation.Text == string.Empty)
                    lbLocation.ForeColor = Color.Red;

                MsgBox.Show("Please specify all required site generation parameters!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            SiteGenParams = new SiteGenParams
            {
                Location = tbFilesLocation.Text,
                SavePosters = cbSavePosters.Checked,
                SaveMoviesThumbnals = cbSaveMoviesThumbnals.Checked,
                SaveEpisodesThumbnals = cbSaveEpisodesThumbnals.Checked,
                PreserveMarkesForExistingThumbnails =
                    cbSaveEpisodesThumbnals.Checked
                        ? false
                        : cbPreserveMarkesForExistingThumbnails.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnFolderSelector_Click(object sender, EventArgs e)
        {
            var selectedPath = Helpers.SelectFolder("Select a folder to save the generated files:", tbFilesLocation.Text);
            if (string.IsNullOrEmpty(selectedPath))
                return;

            tbFilesLocation.Text = selectedPath;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CbSaveEpisodesThumbnals_CheckedChanged(object sender, EventArgs e)
        {
            cbPreserveMarkesForExistingThumbnails.Enabled = !cbSaveEpisodesThumbnals.Checked;
        }
    }
}
