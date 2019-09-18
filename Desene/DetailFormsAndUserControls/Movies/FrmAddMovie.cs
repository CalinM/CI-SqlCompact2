using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using DAL;

using Desene.Properties;

using Utils;

namespace Desene
{
    public partial class FrmAddMovie : Form
    {
        private MovieTechnicalDetails _newMtd;
        private byte[] _poster;

        public int NewId;
        //public event EventHandler OnImportMovie;

        public FrmAddMovie()
        {
            InitializeComponent();
        }

        private void btnImportMovieData_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/1210026/return-a-value-from-an-event-is-there-a-good-practice-for-this
            //if (OnImportMovie is null) return;

            //OnImportMovie(sender, e);

            if (_newMtd != null)
            {
                if (MsgBox.Show(
                        "The previous movie details and all changes made by hand (except the poster) will be lost. Are you sure you want to continue?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            using (var rParam = new FrmMTDFromFile(true, false) { Owner = this })
            {
                if (rParam.ShowDialog() != DialogResult.OK)
                    return;

                if (rParam.mtd == null)
                {
                    MsgBox.Show("An error occurred while determining the file (movie) details. No additional data available!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                _newMtd = rParam.mtd;
            }

            _newMtd.Poster = _poster;
            ucMovieInfo1.RefreshControls(_newMtd);

            if (_newMtd.MovieStills.Count > 0)
            {
                ucMovieInfo1.SetMovieStills(_newMtd.MovieStills);
                Size = new Size(900, 625);
            }
            else
            {
                Size = new Size(900, 430);
            }

            btnSave.Enabled = true;
            //btnLoadPoster.Enabled = true;
        }

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Choose a poster";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Settings.Default.LastCoverPath;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                Settings.Default.LastCoverPath = Path.GetFullPath(openFileDialog.FileName);
                Settings.Default.Save();

                SetNewPoster(openFileDialog.FileName);
            }
        }

        private void SetNewPoster(string imgPath)
        {
            try
            {
                using (Image img = Image.FromFile(imgPath))
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Close();
                    _poster = ms.ToArray();
                    ucMovieInfo1.SetPoster(_poster, true);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //making sure the latest values go into the BindingSource (that happen only when the control loses focus)
            ucMovieInfo1.tbEncodedWith.Focus();

            if (!ucMovieInfo1.ValidateInput())
            {
                MsgBox.Show("Please specify all required details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _newMtd.Poster = _poster;
            var opRes = DAL.InsertMTD(_newMtd, null);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while inserting the new Movie into the database:{0}{0}{1}{0}{0}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ucMovieInfo1.tbTitle.Focus();
                return;
            }

            _newMtd.Id = (int)opRes.AdditionalDataReturn;
            DAL.CurrentMTD = _newMtd;
            Common.Helpers.UnsavedChanges = false;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
