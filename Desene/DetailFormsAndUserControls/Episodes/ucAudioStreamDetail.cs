using System;
using System.Drawing;

using Common;
using DAL;
using System.Windows.Forms;
using Helpers = Common.Helpers;

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

            cbLanguage.MouseWheel += Utils.Helpers.Combobox_OnMouseWheel;
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            lbIndex.DataBindings.Add("Text", _bsControlsData, "Index");

            cbLanguage.DataSource = Languages.Iso639;
            cbLanguage.ValueMember = "Code";
            cbLanguage.DisplayMember = "Name";
            cbLanguage.SetSeparator(3);
            cbLanguage.DataBindings.Add("SelectedValue", _bsControlsData, "Language");

            chbTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");

            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbBitRate.DataBindings.Add("Text", _bsControlsData, "BitRate");
            tbChannels.DataBindings.Add("Text", _bsControlsData, "Channel");
            tbChannelsPosition.DataBindings.Add("Text", _bsControlsData, "ChannelPosition");
            tbSamplingRate.DataBindings.Add("Text", _bsControlsData, "SamplingRate");
            tbResolution.DataBindings.Add("Text", _bsControlsData, "Resolution");
            tbDelay.DataBindings.Add("Text", _bsControlsData, "Delay");
            tbVideoDelay.DataBindings.Add("Text", _bsControlsData, "Video_Delay");
            tbStreamSize.DataBindings.Add("Text", _bsControlsData, "StreamSize");
        }

        public void RefreshControls(AudioStreamInfo audioStreamInfo)
        {
            _bsControlsData.DataSource = audioStreamInfo;
            _bsControlsData.ResetBindings(false);

            ttTitleContent.RemoveAll();
            if (audioStreamInfo.HasTitle && !string.IsNullOrEmpty(audioStreamInfo.Title))
            {
                ttTitleContent.SetToolTip(chbTitle, audioStreamInfo.Title);
                chbTitle.Cursor = Cursors.Help;
            }
            else
            {
                ttTitleContent.RemoveAll();
                chbTitle.Cursor = Cursors.Default;
            }

            cbLanguage.SelectedItem = Languages.GetLanguageFromIdentifier(audioStreamInfo.Language);
        }

        private void cbLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }

        private void chbTitle_MouseClick(object sender, MouseEventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }
    }
}
