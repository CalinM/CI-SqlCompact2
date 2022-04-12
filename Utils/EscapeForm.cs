using System.Windows.Forms;

namespace Utils
{
    public partial class EscapeForm : Form
    {
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape && Utils.Helpers.ConfirmDiscardChanges())
            {
                this.Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}
