using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using DAL;
using Utils;

namespace Desene.EditUserControls
{
    public partial class ucEditSeriesBaseInfo : UserControl
    {
        private bool _isNew;
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

        public string Trailer
        {
            get { return tbTrailer.Text; }
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

        //public ucEditSeriesBaseInfo()
        //{
        //    InitializeComponent();
        //}

        public ucEditSeriesBaseInfo(bool isNew = true)
        {
            InitializeComponent();
            _isNew = isNew;

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
            tbTrailer.DataBindings.Add("Text", _bsControlsData, "Trailer");
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

            if (_isNew)
                Poster = bytes;
            else
                DAL.CurrentMTD.Poster = bytes;
        }

        private void UcEditSeriesBaseInfo_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void UcEditSeriesBaseInfo_DragDrop(object sender, DragEventArgs e)
        {
            var droppedObj = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            try
            {
                if (File.GetAttributes(droppedObj).HasFlag(FileAttributes.Directory))
                    return;

                var picturesExt = new string[] { ".jpg", ".jpeg", ".png", ".bmp" };

                if (Array.IndexOf(picturesExt, Path.GetExtension(droppedObj).ToLower()) >= 0)
                {
                    //SetNewPoster(droppedObj);
                    using (var file = new FileStream(droppedObj, FileMode.Open, FileAccess.Read))
                    {
                        var bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);

                        SetPoster(bytes);
                        Common.Helpers.UnsavedChanges = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
