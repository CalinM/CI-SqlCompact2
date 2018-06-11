using DAL;
using System.Windows.Forms;

namespace Desene.DetailFormsAndUserControls.Episodes
{
    public partial class ucVideoStreamDetail : UserControl
    {
        private BindingSource _bsControlsData;

        public ucVideoStreamDetail()
        {
            InitializeComponent();
        }

        public ucVideoStreamDetail(VideoStreamInfo videoStreamInfo)
        {
            InitializeComponent();

            InitControls();
            RefreshControls(videoStreamInfo);
        }

        public void LoadControls(VideoStreamInfo videoStreamInfo)
        {
            /*
            lbIndexResolution.Text = @"Position " + videoStreamInfo.Index;
            tbWidth.Text = videoStreamInfo.Width;
            tbHeight.Text = videoStreamInfo.Height;
            tbFormat.Text = videoStreamInfo.Format;
            tbFormatProfile.Text = videoStreamInfo.Format_Profile;
            tbBitRate.Text = videoStreamInfo.BitRate;
            tbBitRateMode.Text = videoStreamInfo.BitRateMode;
            tbFrameRate.Text = videoStreamInfo.FrameRate;
            tbFrameRateMode.Text = videoStreamInfo.FrameRate_Mode;
            tbLanguage.Text = videoStreamInfo.Language;
            tbDelay.Text = videoStreamInfo.Delay;
            tbStreamSize.Text = videoStreamInfo.StreamSize;

            cbTitle.Checked = !string.IsNullOrEmpty(videoStreamInfo.Title);
            */

            lbIndexResolution.Text = @"Position " + videoStreamInfo.Index;

            if (tbWidth.DataBindings.Count > 0)
            {
                //tbBitRate.DataBindings.Clear();
                //tbBitRate.DataBindings.Add("Text", videoStreamInfo, "BitRate");
                return;
            }




            /*
            tbWidth.DataBindings.Add("Text", videoStreamInfo, "Width");
            tbHeight.DataBindings.Add("Text", videoStreamInfo, "Height");
            tbFormat.DataBindings.Add("Text", videoStreamInfo, "Format");
            tbFormatProfile.DataBindings.Add("Text", videoStreamInfo, "Format_Profile");
            tbBitRate.DataBindings.Add("Text", videoStreamInfo, "BitRate");
            tbBitRateMode.DataBindings.Add("Text", videoStreamInfo, "BitRateMode");
            tbFrameRate.DataBindings.Add("Text", videoStreamInfo, "FrameRate");
            tbFrameRateMode.DataBindings.Add("Text", videoStreamInfo, "FrameRate_Mode");
            tbLanguage.DataBindings.Add("Text", videoStreamInfo, "Language");
            tbDelay.DataBindings.Add("Text", videoStreamInfo, "Delay");
            tbStreamSize.DataBindings.Add("Text", videoStreamInfo, "StreamSize");

            cbTitle.DataBindings.Add("Checked", videoStreamInfo, "HasTitle");
            */
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            tbWidth.DataBindings.Add("Text", _bsControlsData, "Width");
            tbHeight.DataBindings.Add("Text", _bsControlsData, "Height");
            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbFormatProfile.DataBindings.Add("Text", _bsControlsData, "Format_Profile");
            tbBitRate.DataBindings.Add("Text", _bsControlsData, "BitRate");
            tbBitRateMode.DataBindings.Add("Text", _bsControlsData, "BitRateMode");
            tbFrameRate.DataBindings.Add("Text", _bsControlsData, "FrameRate");
            tbFrameRateMode.DataBindings.Add("Text", _bsControlsData, "FrameRate_Mode");
            tbLanguage.DataBindings.Add("Text", _bsControlsData, "Language");
            tbDelay.DataBindings.Add("Text", _bsControlsData, "Delay");
            tbStreamSize.DataBindings.Add("Text", _bsControlsData, "StreamSize");

            cbTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");
        }

        public void RefreshControls(VideoStreamInfo videoStreamInfo)
        {
            _bsControlsData.DataSource = videoStreamInfo;
            _bsControlsData.ResetBindings(false);
        }
    }
}
