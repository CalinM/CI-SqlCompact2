using System.Collections.Generic;
using System.Windows.Forms;
using DAL;

namespace Desene.DetailFormsAndUserControls.Series
{
    public partial class ucBulkEditFieldValue : UserControl
    {
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

        public ucBulkEditFieldValue(List<BulkEditField> availableFields)
        {
            InitializeComponent();

            cbFields.DataSource = availableFields;
            cbFields.DisplayMember = "Caption";
            cbFields.ValueMember = "FieldName";
        }
    }
}
