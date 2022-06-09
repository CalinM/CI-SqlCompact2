using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAL;

namespace Desene.DetailFormsAndUserControls.Series
{
    public partial class ucBulkEditFieldValue : UserControl
    {
        private FrmBulkEpisodeEdit _parent;

        public BulkEditField NewValue
        {
            get
            {
                if (cbFields.SelectedItem == null || string.IsNullOrEmpty(tbNewValue.Text))
                    return null;

                var selectedFieldObj = (BulkEditField)cbFields.SelectedItem;

                return 
                    new BulkEditField
                    {
                        FieldName = selectedFieldObj.FieldName,
                        Value = tbNewValue.Text,
                        RequireRefresh = selectedFieldObj.RequireRefresh
                    };
            }
        }
        public ucBulkEditFieldValue()
        {
            InitializeComponent();
        }

        public ucBulkEditFieldValue(FrmBulkEpisodeEdit parent, List<BulkEditField> availableFields)
        {
            InitializeComponent();

            _parent = parent;

            cbFields.DataSource = availableFields;
            cbFields.DisplayMember = "Caption";
            cbFields.ValueMember = "FieldName";
        }

        private void ucBulkEditFieldValue_Load(object sender, System.EventArgs e)
        {
            tbNewValue.Select();
        }

        private void tbNewValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                _parent.DoClose();
        }
    }
}
