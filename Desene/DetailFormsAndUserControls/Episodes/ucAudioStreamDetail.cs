using DAL;
using System.Windows.Forms;

namespace Desene.DetailFormsAndUserControls.Episodes
{
    public partial class ucAudioStreamDetail : UserControl
    {
        private BindingSource _bsControlsData;

        public ucAudioStreamDetail()
        {
            InitializeComponent();
        }

        public ucAudioStreamDetail(AudioStreamInfo audioStreamInfo)
        {
            InitializeComponent();

            InitControls();
            RefreshControls(audioStreamInfo);
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            lbIndex.DataBindings.Add("Text", _bsControlsData, "Index");
            cbLanguage.DataBindings.Add("Text", _bsControlsData, "Language");
            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbBitRate.DataBindings.Add("Text", _bsControlsData, "BitRate");
            tbChannels.DataBindings.Add("Text", _bsControlsData, "Channel");
            tbChannelsPosition.DataBindings.Add("Text", _bsControlsData, "ChannelPosition");
            tbSamplingRate.DataBindings.Add("Text", _bsControlsData, "SamplingRate");
            tbResolution.DataBindings.Add("Text", _bsControlsData, "Resolution");
            tbDelay.DataBindings.Add("Text", _bsControlsData, "Delay");
            tbVideoDelay.DataBindings.Add("Text", _bsControlsData, "Video_Delay");
            tbStreamSize.DataBindings.Add("Text", _bsControlsData, "StreamSize");

            cbTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");
        }

        public void RefreshControls(AudioStreamInfo audioStreamInfo)
        {
            _bsControlsData.DataSource = audioStreamInfo;
            _bsControlsData.ResetBindings(false);
        }
    }
}
