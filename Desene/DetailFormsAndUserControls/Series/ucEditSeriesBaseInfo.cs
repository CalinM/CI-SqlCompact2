using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Desene.EditUserControls
{
    public partial class ucEditSeriesBaseInfo : UserControl
    {
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

        public ucEditSeriesBaseInfo()
        {
            InitializeComponent();
        }
        public ucEditSeriesBaseInfo(DataRow row)
        {
            InitializeComponent();
            LoadControls(row);
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

        public void LoadControls(DataRow row)
        {
            tbTitle.Text = row["FileName"].ToString();
            tbDescriptionLink.Text = row["DescriptionLink"].ToString();
            tbRecommended.Text = row["Recommended"].ToString();
            tbRecommendedLink.Text = row["RecommendedLink"].ToString();
            tbNotes.Text = row["Notes"].ToString();

            if (row["Poster"] != DBNull.Value)
            {
                using (var ms = new MemoryStream((byte[])row["Poster"]))
                {
                    pbCover.Image = Image.FromStream(ms);
                }
            }
        }

        public void SetPoster(MemoryStream poster)
        {
            pbCover.Image = Image.FromStream(poster);
        }
    }
}
