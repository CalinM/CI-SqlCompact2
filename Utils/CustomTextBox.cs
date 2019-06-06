using System.Windows.Forms;

namespace Utils
{
    public class CustomTextBox: TextBox
    {
        //protected override void OnKeyPress(KeyPressEventArgs e)
        //{
        //    base.OnKeyPress(e);
        //    Common.Helpers.UnsavedChanges = true;
        //}

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (ModifierKeys != Keys.Control)
                Common.Helpers.UnsavedChanges = true;
        }
    }
}