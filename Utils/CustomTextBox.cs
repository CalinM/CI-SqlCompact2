using System.Diagnostics;
using System.Windows.Forms;

namespace Utils
{
    public class CustomTextBox: TextBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (ModifierKeys == Keys.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.ControlKey))
            {
                //
            }
            else
            {
                Common.Helpers.UnsavedChanges = true;
            }
        }
    }
}