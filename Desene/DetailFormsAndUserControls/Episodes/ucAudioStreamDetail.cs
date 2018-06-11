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

        public void LoadControls(AudioStreamInfo audioStreamInfo)
        {
            /*
            lbIndexResolution.Text = @"Position " + audioStreamInfo.Index;
            cbLanguage.Text = audioStreamInfo.Language;
            tbFormat.Text = audioStreamInfo.Format;
            tbBitRate.Text = audioStreamInfo.BitRate;
            tbChannels.Text = audioStreamInfo.Channel;
            tbChannelsPosition.Text = audioStreamInfo.ChannelPosition;
            tbSamplingRate.Text = audioStreamInfo.SamplingRate;
            tbResolution.Text = audioStreamInfo.Resolution;
            tbDelay.Text = audioStreamInfo.Delay;
            tbVideoDelay.Text = audioStreamInfo.Video_Delay;
            tbStreamSize.Text = audioStreamInfo.StreamSize;
            cbTitle.Checked = !string.IsNullOrEmpty(audioStreamInfo.Title);
            */
            /*
            lbIndexResolution.Text = @"Position " + audioStreamInfo.Index;

            if (cbLanguage.DataBindings.Count > 0) return;

            cbLanguage.DataBindings.Add("Text", audioStreamInfo, "Language");
            tbFormat.DataBindings.Add("Text", audioStreamInfo, "Format");
            tbBitRate.DataBindings.Add("Text", audioStreamInfo, "BitRate");
            tbChannels.DataBindings.Add("Text", audioStreamInfo, "Channel");
            tbChannelsPosition.DataBindings.Add("Text", audioStreamInfo, "ChannelPosition");
            tbSamplingRate.DataBindings.Add("Text", audioStreamInfo, "SamplingRate");
            tbResolution.DataBindings.Add("Text", audioStreamInfo, "Resolution");
            tbDelay.DataBindings.Add("Text", audioStreamInfo, "Delay");
            tbVideoDelay.DataBindings.Add("Text", audioStreamInfo, "Video_Delay");
            tbStreamSize.DataBindings.Add("Text", audioStreamInfo, "StreamSize");

            cbTitle.DataBindings.Add("Checked", audioStreamInfo, "HasTitle");*/
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

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
