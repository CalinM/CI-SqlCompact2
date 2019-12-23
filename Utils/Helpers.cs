using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Common;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Ookii.Dialogs;
using Utils.Properties;

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
            var f = new FrmToast(startPosition, messageType, title, message, duration);
            f.Show();

            if (parentForm != null)
                parentForm.Focus(); //https://stackoverflow.com/questions/156046/show-a-form-without-stealing-focus
        }

        public static string SelectFolder(string title, string lastPath)
        {
            //using (var dialog = new CommonOpenFileDialog())
            //{
            //    dialog.IsFolderPicker = true;
            //    dialog.Title = title;
            //    dialog.InitialDirectory = lastPath;
            //    dialog.DialogOpening += Dialog_DialogOpening;

            //    return dialog.ShowDialog() == CommonFileDialogResult.Ok ? dialog.FileName : null;
            //}

            /*
            CMA:
            due to HIGH DPI issues:
                - no force DPI awareness => when showing a CommonOpenFileDialog the main form is scaled down
                - with force DPI awareness => column treeview incorrectly rendered and the MoviesStills panel height is incorrectly determined
            (fixed attempted: https://stackoverflow.com/questions/42975285/commonopenfiledialog-cause-windows-form-to-shrink?rq=1)

            For now, the Ookii dialog is used
            */

            ShowNotificationAutoExpandToSelection();

            using (var dialog = new VistaFolderBrowserDialog())
            {
                dialog.Description = title;
                dialog.UseDescriptionForTitle = true;

                return dialog.ShowDialog(Form.ActiveForm) == DialogResult.OK ? dialog.SelectedPath : null;
            }
        }

        //private static void Dialog_DialogOpening(object sender, EventArgs e)
        //{
        //    ShowNotificationAutoExpandToSelection();
        //}

        private static void ShowNotificationAutoExpandToSelection()
        {
            if (Settings.Default.FolderCfgNotShown > 10)
                return;

            var currentConfig = GetCurrentFolderBehavior();

            if (currentConfig != null && currentConfig == 0)
            {
                ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Folders behavior",
                    "Change the 'NavPaneExpandToCurrentFolder' registry property to 1 in order to have the folder tree automatically expand to the current selection", 10000, Form.ActiveForm);

                Settings.Default.FolderCfgNotShown = Settings.Default.FolderCfgNotShown + 1;
                Settings.Default.Save();
            }            
        }

        private static int? GetCurrentFolderBehavior()
        {
            try
            {
                //https://www.softwareok.com/?seite=faq-Windows-10&faq=65
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced"))
                {
                    if (key != null)
                    {
                        var o = key.GetValue("NavPaneExpandToCurrentFolder");
                        if (o != null)
                            return int.Parse(o.ToString());
                    }
                }
            }
            catch
            { }

            return null;
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
