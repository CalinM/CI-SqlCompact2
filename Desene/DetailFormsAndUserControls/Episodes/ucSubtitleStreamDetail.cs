using DAL;
using System.Windows.Forms;

namespace Desene.DetailFormsAndUserControls.Episodes
{
    public partial class ucSubtitleStreamDetail : UserControl
    {
        private BindingSource _bsControlsData;

        public ucSubtitleStreamDetail()
        {
            InitializeComponent();
        }

        public ucSubtitleStreamDetail(SubtitleStreamInfo subtitleStreamInfo)
        {
            InitializeComponent();

            InitControls();
            RefreshControls(subtitleStreamInfo);
        }

        private void LoadControls(SubtitleStreamInfo subtitleStreamInfo)
        {
            /*
            lbIndexResolution.Text = @"Position " + subtitleStreamInfo.Index;
            cbLanguage.Text = subtitleStreamInfo.Language;
            tbFormat.Text = subtitleStreamInfo.Format;
            tbStreamSize.Text = subtitleStreamInfo.StreamSize;
            cbTitle.Checked = !string.IsNullOrEmpty(subtitleStreamInfo.Title);
            */
            /*
            lbIndexResolution.Text = @"Position " + subtitleStreamInfo.Index;

            if (cbLanguage.DataBindings.Count > 0) return;

            cbLanguage.DataBindings.Add("Text", subtitleStreamInfo, "Language");
            tbFormat.DataBindings.Add("Text", subtitleStreamInfo, "Format");
            tbStreamSize.DataBindings.Add("Text", subtitleStreamInfo, "StreamSize");

            cbTitle.DataBindings.Add("Checked", subtitleStreamInfo, "HasTitle");*/
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            cbLanguage.DataBindings.Add("Text", _bsControlsData, "Language");
            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbStreamSize.DataBindings.Add("Text", _bsControlsData, "StreamSize");

            cbTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");
        }

        public void RefreshControls(SubtitleStreamInfo subtitleStreamInfo)
        {
            _bsControlsData.DataSource = subtitleStreamInfo;
            _bsControlsData.ResetBindings(false);
        }
    }
}
