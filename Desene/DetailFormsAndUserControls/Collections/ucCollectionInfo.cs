using Common;
using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desene.DetailFormsAndUserControls.Collections
{
    public partial class ucCollectionInfo : UserControl
    {
        private BindingSource _bsControlsData;

        public string Title { get { return tbTitle.Text; } }

        public string Notes { get { return tbNotes.Text; } }

        public int SectionType { get { return cbSectionType.SelectedIndex; } }

        public ucCollectionInfo(bool isNew = true)
        {
            InitializeComponent();

            if (!isNew)
            {
                InitControls();
                RefreshControls();
            }
        }

        private void InitControls()
        {
            _bsControlsData = new BindingSource();

            tbTitle.DataBindings.Add("Text", _bsControlsData, "Title");
            tbNotes.DataBindings.Add("Text", _bsControlsData, "Notes");

            cbSectionType.DataSource = Enum.GetValues(typeof(CollectionsSiteSecionType));
            cbSectionType.DataBindings.Add("Value", _bsControlsData, "SiteSectionType");
        }

        public void RefreshControls(CollectionInfo cci = null)
        {
            _bsControlsData.DataSource = cci ?? DAL.CurrentCollection;
            _bsControlsData.ResetBindings(false);
        }

        public bool ValidateInput()
        {
            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                lbTitle.ForeColor = Color.Red;
                return false;
            }

            return true;
        }

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            lbTitle.ForeColor = SystemColors.WindowText;
        }

        private void cbSectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Helpers.UnsavedChanges = true;
        }
    }
}
