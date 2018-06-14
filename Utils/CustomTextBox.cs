using System.Windows.Forms;

namespace Utils
{
    public class CustomTextBox: TextBox
    {
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            Common.Helpers.UnsavedChanges = true;
        }

        //public void ResetChangesMarker()
        //{
        //    BorderStyle
        //}
    }
}
