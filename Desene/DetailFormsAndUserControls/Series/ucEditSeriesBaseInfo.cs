using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using DAL;

namespace Desene.EditUserControls
{
    public partial class ucEditSeriesBaseInfo : UserControl
    {
        private BindingSource _bsControlsData;

        public string Title
        {
            get { return tbTitle.Text; }
        }

        public string DescriptionLink
        {
            get { return tbDescriptionLink.Text; }
        }

        public string Recommended
        {
            get { return tbRecommended.Text; }
        }

        public string RecommendedLink
        {
            get { return tbRecommendedLink.Text; }
        }

        public string Notes
        {
            get { return tbNotes.Text; }
        }
        private byte[] _poster;
        public byte[] Poster
        {
            get { return _poster; }
            set
            {
                _poster = value;

                using (var ms = new MemoryStream(_poster))
                {
                    pbCover.Image = Image.FromStream(ms);
                }
            }
        }

        public ucEditSeriesBaseInfo(bool isNew = true)
        {
            InitializeComponent();

            if (!isNew)
            {
                InitControls();
                RefreshControls();
            }
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            tbTitle.DataBindings.Add("Text", _bsControlsData, "FileName");
            tbDescriptionLink.DataBindings.Add("Text", _bsControlsData, "DescriptionLink");
            tbRecommended.DataBindings.Add("Text", _bsControlsData, "Recommended");
            tbRecommendedLink.DataBindings.Add("Text", _bsControlsData, "RecommendedLink");
            tbNotes.DataBindings.Add("Text", _bsControlsData, "Notes");
            pbCover.DataBindings.Add("Image", _bsControlsData, "Poster", true);
        }

        public void RefreshControls(MovieTechnicalDetails mtd = null)
        {
            _bsControlsData.DataSource = mtd ?? DAL.CurrentMTD;
            _bsControlsData.ResetBindings(false);
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

        public void SetPoster(byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                pbCover.Image = Image.FromStream(ms);
            }

            DAL.CurrentMTD.Poster = bytes;
        }
    }
}
