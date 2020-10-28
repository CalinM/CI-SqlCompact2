using Common;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Utils
{
    public partial class FrmImportErrors : EscapeForm
    {
        public FrmImportErrors()
        {
            InitializeComponent();
        }

        public FrmImportErrors(List<TechnicalDetailsImportError> importErrors, bool anyDetailsDetermined)
        {
            InitializeComponent();

            foreach (var ie in importErrors)
            {
                dgvErrors.Rows.Add(ie.ToString());
            }

            lbWarningMessage2.Visible = !anyDetailsDetermined;
            btnConfirm.Enabled = anyDetailsDetermined;
        }
    }
}
