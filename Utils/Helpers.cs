using System.Windows.Forms;

namespace Utils
{
    public class Helpers
    {
        public static bool ConfirmDiscardChanges()
        {
            if (!Common.Helpers.UnsavedChanges)
                return true;

            if (MsgBox.Show("There are unsaved changes. You you want to continue and discard those changes?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Common.Helpers.UnsavedChanges = false;
                return true;
            }

            return false;
        }


        public static void Combobox_OnMouseWheel(object sender, MouseEventArgs mouseEventArgs)
        {
            ((HandledMouseEventArgs)mouseEventArgs).Handled = true;
        }
    }
}
