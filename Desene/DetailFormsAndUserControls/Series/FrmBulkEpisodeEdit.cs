using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utils;

namespace Desene.DetailFormsAndUserControls.Series
{
    public partial class FrmBulkEpisodeEdit : EscapeForm
    {
        private List<BulkEditField> _allBulkEditFields = new List<BulkEditField>();
        private List<ucBulkEditFieldValue> _fieldValuesControls = new List<ucBulkEditFieldValue>();

        public List<BulkEditField> SelectedBulkValues
        {
            get
            {
                return
                    _fieldValuesControls.Where(x => !string.IsNullOrEmpty(x.NewValue.Value))
                                        .Select(x => x.NewValue)
                                        .ToList();
            }
        }

        public FrmBulkEpisodeEdit(int selectedEpisodes)
        {
            InitializeComponent();

            Text = string.Format("Select new field values for {0} episodes", selectedEpisodes);

            _allBulkEditFields.Add(new BulkEditField { Caption = "Year", FieldName = "Year", RequireRefresh = false });
            _allBulkEditFields.Add(new BulkEditField { Caption = "Season", FieldName = "Season", RequireRefresh = true });
            _allBulkEditFields.Add(new BulkEditField { Caption = "Theme", FieldName = "Theme", RequireRefresh = true });
            _allBulkEditFields.Add(new BulkEditField { Caption = "First audio language", FieldName = "Language", RequireRefresh = true });
        }

        private void FrmBulkEpisodeEdit_Load(object sender, EventArgs e)
        {
            AddNewFieldElement();
        }

        private void BtnAddChange_Click(object sender, EventArgs e)
        {
            if (_fieldValuesControls.Any(x => x.NewValue is null))
            {
                MsgBox.Show(
                    "Please fill the new field value before attempting to add a new field definition!",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddNewFieldElement();
        }

        private void AddNewFieldElement()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                pBulkChangeControlsContainer.SuspendLayout();

                var availableFields = new List<BulkEditField>();
                foreach (var abef in _allBulkEditFields)
                {
                    if (_fieldValuesControls.FirstOrDefault(x => x.NewValue.FieldName == abef.FieldName) != null)
                        continue;

                    availableFields.Add(abef);
                }

                var befv = new ucBulkEditFieldValue(this, availableFields) { Dock = DockStyle.Top };
                _fieldValuesControls.Add(befv);
                pBulkChangeControlsContainer.Controls.Add(befv);
                befv.BringToFront();
            }
            finally
            {
                
                pBulkChangeControlsContainer.ResumeLayout();
                Cursor = Cursors.Default;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            pBulkChangeControlsContainer.Controls.Clear();
            _fieldValuesControls = new List<ucBulkEditFieldValue>();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DoClose();
        }

        public void DoClose()
        {
            var updates = _fieldValuesControls.Where(x => x.NewValue != null && !string.IsNullOrEmpty(x.NewValue.FieldName)).ToList();

            if (updates.Count == 0)
            {
                MsgBox.Show(
                    "Nothing to save, please review your input!",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MsgBox.Show(
                    string.Format("Are you sure you want update the specified fields ({0}) on all selected episodes?", updates.Count),
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
