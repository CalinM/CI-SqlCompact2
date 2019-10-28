using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Common;

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

        public static void AddSectionHeader(Control sender, string caption, string identifier)
        {
            var pHeader = new Panel();
            pHeader.Dock = DockStyle.Top;
            pHeader.BackColor = Color.DimGray;
            pHeader.Size = new Size(350, 25);
            pHeader.Tag = "SectionHeader_" + identifier;

            var lbHeaderText = new Label();
            lbHeaderText.ForeColor = Color.White;
            lbHeaderText.Font = new Font(lbHeaderText.Font, FontStyle.Bold);
            lbHeaderText.Location = new Point(9, 6);
            lbHeaderText.AutoSize = false;
            lbHeaderText.Size = new Size(350, 15);
            lbHeaderText.Text = caption;

            pHeader.Controls.Add(lbHeaderText);

            sender.Controls.Add(pHeader);
            pHeader.BringToFront();
        }

        public static void ShowToastForm(StartPosition2 startPosition, MessageType messageType, string title, string message, int duration,
            Form parentForm)
        {
            var t = new FrmToast(startPosition, messageType, title, message, duration);
            t.Show();
            parentForm.Focus(); //https://stackoverflow.com/questions/156046/show-a-form-without-stealing-focus
        }
    }

    public class DrawingControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
    }
}
