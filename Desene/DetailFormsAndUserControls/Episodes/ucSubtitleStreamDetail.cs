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

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            lbIndex.DataBindings.Add("Text", _bsControlsData, "Index");
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
