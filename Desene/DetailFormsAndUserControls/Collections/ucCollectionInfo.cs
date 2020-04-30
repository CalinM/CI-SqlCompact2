using Common;
using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desene.DetailFormsAndUserControls.Collections
{
    public partial class ucCollectionInfo : UserControl
    {
        public string Title { get { return tbTitle.Text; } }

        public string Notes { get { return tbNotes.Text; } }

        public int SectionType { get { return cbSectionType.SelectedIndex; } }

        public ucCollectionInfo()
        {
            InitializeComponent();
        }

        public void RefreshControls(SeriesEpisodesShortInfo sesi)
        {
            tbTitle.Text = sesi.FileName;
            tbNotes.Text = sesi.Notes;
            cbSectionType.SelectedIndex = sesi.SectionType;

            Helpers.UnsavedChanges = false;

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
