using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Common;
using Common.ExtensionMethods;
using DAL;
using Microsoft.Win32;
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

            //ShowNotificationAutoExpandToSelection();

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

        public static bool IsOnScreen(Point RecLocation, Size RecSize, double MinPercentOnScreen = 0.2)
        {
            var PixelsVisible = 0;
            var Rec = new Rectangle(RecLocation, RecSize);

            foreach (var Scrn in Screen.AllScreens)
            {
                var r = Rectangle.Intersect(Rec, Scrn.WorkingArea);

                // intersect rectangle with screen
                if (r.Width != 0 & r.Height != 0)
                {
                    PixelsVisible += (r.Width * r.Height);
                    // tally visible pixels
                }
            }

            return PixelsVisible >= (Rec.Width * Rec.Height) * MinPercentOnScreen;
        }

        public static List<MixFileNameReplaceDef> GetDefaultMixFileNameReplaceValues()
        {
            return
                new List<MixFileNameReplaceDef>()
                {
                    new MixFileNameReplaceDef(":", " - "),
                    new MixFileNameReplaceDef("? (", " ("),
                    new MixFileNameReplaceDef("?", string.Empty),
                    new MixFileNameReplaceDef("\"", "'"),
                    new MixFileNameReplaceDef("*", string.Empty),
                    new MixFileNameReplaceDef("\\", string.Empty),
                    //new MixFileNameReplaceDef("/", " & "), //added based on user response
                    new MixFileNameReplaceDef("|", string.Empty),
                    new MixFileNameReplaceDef("<", "["),
                    new MixFileNameReplaceDef(">", "]"),
                    new MixFileNameReplaceDef("’", "'")
                };
         }

        public static DialogResult InputBox(Form owner, string title, string promptText, ref string value)
        {
            var form = new Form
            {
                Owner = owner,
                Text = title,
                ClientSize = new Size(396, 107),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var label = new Label
            {
                Text = promptText,
                AutoSize = true
            };
            label.SetBounds(9, 20, 372, 13);

            var textBox = new TextBox
            {
                Text = value,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            textBox.SetBounds(12, 36, 372, 20);

            var buttonOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            buttonOk.SetBounds(228, 72, 75, 23);

            var buttonCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            buttonCancel.SetBounds(309, 72, 75, 23);

            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            var dialogResult = form.ShowDialog();
            value = textBox.Text;

            return dialogResult;
        }

        public static OperationResult FixDiacriticsInTextFile(string fileName)
        {
            var result = new OperationResult();

            try
            {
                var ext = Path.GetExtension(fileName).ToLower();

                if (ext != ".srt" && ext != ".txt")
                    result.FailWithMessage(string.Format("Unsupported file format ({0})", ext));

                var subtitleContent = string.Empty;

                subtitleContent = File.ReadAllText(fileName, Encoding.UTF8);

                if (subtitleContent.Contains("�"))
                    subtitleContent = File.ReadAllText(fileName, Encoding.Default);

                var newFileName =
                    Path.Combine(
                        Path.GetDirectoryName(fileName),
                        string.Format("{0}_new{1}",
                            Path.GetFileNameWithoutExtension(fileName),
                            ext)
                        );

                using (var sw = new StreamWriter(newFileName, false, Encoding.UTF8))
                {
                    sw.WriteLine(
                        subtitleContent
                            .Replace("º", "ș")
                            .Replace("þ", "ț")
                            .Replace("ª", "Ș")
                            .Replace("Þ", "Ț")
                            .Replace("ã", "ă")
                            .Replace("”", "\"")
                            .Replace("Ã", "Ă")
                        );
                }
            }
            catch (Exception ex)
            {
                result.FailWithMessage(OperationResult.GetErrorMessage(ex));
            }

            return result;
        }

        public static OperationResult GetFilesDetails(string path, Form parentForm)
        {
            var result = new OperationResult();

            var files =
                 Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                     .Where(s => s.EndsWith(".mkv") || s.EndsWith(".mp4")).ToArray();

            Directory.GetFiles(path, "*.*");

            if (!files.ToList().DistinctBy(Path.GetExtension).Any())
                result.FailWithMessage(string.Format("The specified folder '{0}' is empty!", path));
            else
            {
                var param = new FilesImportParams
                {
                    Location = path,
                    FilesExtension = "*.*",
                    DisplayInfoOnly = true
                };

                var opRes = FilesMetaData.GetFilesTechnicalDetails(files, param);

                if (opRes.AdditionalDataReturn == null)
                    result.FailWithMessage(string.Format("No details retrieved. {0}", opRes.CustomErrorMessage));

                var filesInfoDeterminationResult = (KeyValuePair<List<MovieTechnicalDetails>, List<TechnicalDetailsImportError>>)opRes.AdditionalDataReturn;
                var displayResult = new List<dynamic>();

                foreach (var file in files)
                {
                    var fileInfoObj = filesInfoDeterminationResult.Key.FirstOrDefault(f => f.InitialPath == file);
                    var dynamicObj = new ExpandoObject() as IDictionary<string, object>;
                    dynamicObj.Add("Filename", Path.GetFileName(file));
                    dynamicObj.Add("Resolution", string.Format("{0}x{1}", fileInfoObj.VideoStreams[0].Width, fileInfoObj.VideoStreams[0].Height));
                    dynamicObj.Add("FileVideo Title", fileInfoObj.HasTitle || fileInfoObj.VideoStreams.Any(vs => vs.HasTitle));

                    if (fileInfoObj != null)
                    {
                        foreach (var audioObj in fileInfoObj.AudioStreams)
                        {
                            dynamicObj.Add(string.Format("Audio {0}", audioObj.Index), audioObj.Language);
                            dynamicObj.Add(string.Format("Channels {0}", audioObj.Index), audioObj.Channel);
                            dynamicObj.Add(string.Format("Default {0}", audioObj.Index), audioObj.Default);
                            dynamicObj.Add(string.Format("Forced {0}", audioObj.Index), audioObj.Forced);
                            dynamicObj.Add(string.Format("Title {0}", audioObj.Index), audioObj.HasTitle);
                        }

                        dynamicObj.Add("Error", "");
                    }
                    else
                    {
                        var fileErrorObj = filesInfoDeterminationResult.Value.FirstOrDefault(f => f.FilePath == file);
                        dynamicObj.Add("Error", fileInfoObj != null ? fileErrorObj.ErrorMesage : "Unknown error!");
                    }

                    dynamicObj.Add("Cover", !string.IsNullOrEmpty(fileInfoObj.Cover));

                    displayResult.Add(dynamicObj);
                }

                if (!displayResult.Any())
                    result.FailWithMessage("Something went wrong while processing the files details!");

                var frmFileDetails = new FrmFileDetails(displayResult) { Owner = parentForm };
                frmFileDetails.Show();
            }

            return result;
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

    public class MixFileNameReplaceDef
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public MixFileNameReplaceDef(string oldValue, string newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
