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

        //https://stackoverflow.com/questions/28807363/how-to-make-multiline-textbox-pass-mousewheel-events-so-that-container-is-scroll
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x020A: // WM_MOUSEWHEEL
                case 0x020E: // WM_MOUSEHWHEEL
                    if (ScrollBars == ScrollBars.None && Parent != null)
                        m.HWnd = Parent.Handle; // forward this to your parent
                    base.WndProc(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}