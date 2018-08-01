using DAL;
using System.Windows.Forms;

using Common;

namespace Desene.DetailFormsAndUserControls.Shared
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

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            lbIndex.DataBindings.Add("Text", _bsControlsData, "Index");
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

            chbTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");
        }

        public void RefreshControls(VideoStreamInfo videoStreamInfo)
        {
            _bsControlsData.DataSource = videoStreamInfo;
            _bsControlsData.ResetBindings(false);

            ttTitleContent.RemoveAll();
            if (videoStreamInfo.HasTitle && !string.IsNullOrEmpty(videoStreamInfo.Title))
            {
                ttTitleContent.SetToolTip(chbTitle, videoStreamInfo.Title);
                chbTitle.Cursor = Cursors.Help;
            }
            else
            {
                ttTitleContent.RemoveAll();
                chbTitle.Cursor = Cursors.Default;
            }
        }

        private void chbTitle_MouseClick(object sender, MouseEventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }
    }
}
