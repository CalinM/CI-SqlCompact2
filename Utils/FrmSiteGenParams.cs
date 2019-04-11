using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Utils
{
    public partial class FrmSiteGenParams : Form
    {
        private string _lastPath = string.Empty;

        public SiteGenParams SiteGenParams;

        public FrmSiteGenParams(string lastPath)
        {
            _lastPath = lastPath;
            
            InitializeComponent();
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
                SaveEpisodesThumbnals = cbSaveEpisodesThumbnals.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnFolderSelector_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder to save the generated files:";
                folderBrowserDialog.ShowNewFolderButton = true;
                folderBrowserDialog.SelectedPath = _lastPath;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    tbFilesLocation.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
