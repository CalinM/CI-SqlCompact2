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
                return cbFields.SelectedItem == null || string.IsNullOrEmpty(tbNewValue.Text)
                    ? null
                    : new BulkEditField
                    {
                        FieldName = ((BulkEditField)cbFields.SelectedItem).FieldName,
                        Value = tbNewValue.Text
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
