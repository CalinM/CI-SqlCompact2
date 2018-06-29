using DAL;
using System.Windows.Forms;

using Common;

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

            tbFormat.DataBindings.Add("Text", _bsControlsData, "Format");
            tbStreamSize.DataBindings.Add("Text", _bsControlsData, "StreamSize");

            chbTitle.DataBindings.Add("Checked", _bsControlsData, "HasTitle");
        }

        public void RefreshControls(SubtitleStreamInfo subtitleStreamInfo)
        {
            _bsControlsData.DataSource = subtitleStreamInfo;
            _bsControlsData.ResetBindings(false);

            ttTitleContent.RemoveAll();
            if (subtitleStreamInfo.HasTitle && !string.IsNullOrEmpty(subtitleStreamInfo.Title))
            {
                ttTitleContent.SetToolTip(chbTitle, subtitleStreamInfo.Title);
                chbTitle.Cursor = Cursors.Help;
            }
            else
            {
                ttTitleContent.RemoveAll();
                chbTitle.Cursor = Cursors.Default;
            }

            cbLanguage.SelectedItem = Languages.GetLanguageFromIdentifier(subtitleStreamInfo.Language);
        }

        private void cbLanguage_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }

        private void chbTitle_MouseClick(object sender, MouseEventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }
    }
}
