using System;
using System.Reflection;
using System.Windows.Forms;

namespace Utils
{
    /// <summary>
    /// Present the Windows Vista-style open file dialog to select a folder. Fall back for older Windows Versions
    /// https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-ifiledialog
    /// https://stackoverflow.com/questions/4136477/trying-to-open-a-file-dialog-using-the-new-ifiledialog-and-ifileopendialog-inter
    /// see examples at the end!
    /// </summary>
    public class CustomDialogs
    {
        private string _initialDirectory;
        private string _title;

        public DialogType DialogType { get; set; }

        public string InitialDirectory
        {
            get { return string.IsNullOrEmpty(_initialDirectory) ? Environment.CurrentDirectory : _initialDirectory; }
            set { _initialDirectory = value; }
        }

        public string Filter { get; set; } = string.Empty;

        public string Title
        {
            get
            {
                return
                    !string.IsNullOrEmpty(_title)
                        ? _title
                        : DialogType == DialogType.SelectFolder
                            ? "Select a folder"
                            : DialogType == DialogType.OpenFile
                                ? "Open"        // |
                                : "Save As";    // |-> default values
            }
            set { _title = value; }
        }

        public string FileName { get; private set; } = string.Empty;

        public string FileNameLabel { get; set; } = string.Empty;

        public string ConfirmButtonText { get; set; } = string.Empty;

        public bool Show() { return Show(IntPtr.Zero); }

        /// <param name="hWndOwner">Handle of the control or window to be the parent of the file dialog</param>
        /// <returns>true if the user clicks OK</returns>
        public bool Show(IntPtr hWndOwner)
        {
            var result = Environment.OSVersion.Version.Major >= 6
                ? VistaDialog.Show(hWndOwner, DialogType, InitialDirectory, Title, Filter, FileNameLabel, ConfirmButtonText)
                : ShowXpDialog(hWndOwner, InitialDirectory, Title);

            FileName = result.FileName;
            return result.Result;
        }

        private struct ShowDialogResult
        {
            public bool Result { get; set; }
            public string FileName { get; set; }
        }

        private static ShowDialogResult ShowXpDialog(IntPtr ownerHandle, string initialDirectory, string title)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                Description = title,
                SelectedPath = initialDirectory,
                ShowNewFolderButton = false
            };
            var dialogResult = new ShowDialogResult();
            if (folderBrowserDialog.ShowDialog(new WindowWrapper(ownerHandle)) == DialogResult.OK)
            {
                dialogResult.Result = true;
                dialogResult.FileName = folderBrowserDialog.SelectedPath;
            }
            return dialogResult;
        }

        private static class VistaDialog
        {
            private const BindingFlags c_flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            private readonly static Assembly s_windowsFormsAssembly = typeof(FileDialog).Assembly;
            private readonly static Type s_iFileDialogType = s_windowsFormsAssembly.GetType("System.Windows.Forms.FileDialogNative+IFileDialog");
            private readonly static MethodInfo s_createVistaDialogMethodInfo = typeof(OpenFileDialog).GetMethod("CreateVistaDialog", c_flags);
            private readonly static MethodInfo s_onBeforeVistaDialogMethodInfo = typeof(OpenFileDialog).GetMethod("OnBeforeVistaDialog", c_flags);
            private readonly static MethodInfo s_getOptionsMethodInfo = typeof(FileDialog).GetMethod("GetOptions", c_flags);
            private readonly static MethodInfo s_setOptionsMethodInfo = s_iFileDialogType.GetMethod("SetOptions", c_flags);

            private readonly static uint s_fosPickFoldersBitFlag = (uint)s_windowsFormsAssembly
                .GetType("System.Windows.Forms.FileDialogNative+FOS")
                .GetField("FOS_PICKFOLDERS")
                .GetValue(null);

            private readonly static ConstructorInfo s_vistaDialogEventsConstructorInfo = s_windowsFormsAssembly
                .GetType("System.Windows.Forms.FileDialog+VistaDialogEvents")
                .GetConstructor(c_flags, null, new[] { typeof(FileDialog) }, null);

            private readonly static MethodInfo s_adviseMethodInfo = s_iFileDialogType.GetMethod("Advise");
            private readonly static MethodInfo s_unAdviseMethodInfo = s_iFileDialogType.GetMethod("Unadvise");
            private readonly static MethodInfo s_showMethodInfo = s_iFileDialogType.GetMethod("Show");

            private readonly static MethodInfo s_setOkButtonLabelMethodInfo = s_iFileDialogType.GetMethod("SetOkButtonLabel", c_flags);
            private readonly static MethodInfo s_setFileNameLabelMethodInfo = s_iFileDialogType.GetMethod("SetFileNameLabel", c_flags);

            public static ShowDialogResult Show(IntPtr ownerHandle, DialogType dialogType, string initialDirectory, string title, string filter,
                string fileNameLabel, string confirmButtonText)
            {
                var openFileDialog = new OpenFileDialog
                {
                    AddExtension = false,
                    CheckFileExists = false,
                    DereferenceLinks = true,
                    Filter = dialogType == DialogType.SelectFolder ? "Folders|\n" : filter,
                    InitialDirectory = initialDirectory,
                    Multiselect = false,
                    Title = title
                };

                var iFileDialog = s_createVistaDialogMethodInfo.Invoke(openFileDialog, new object[] { });
                s_onBeforeVistaDialogMethodInfo.Invoke(openFileDialog, new[] { iFileDialog });

                if (dialogType == DialogType.SelectFolder)
                    s_setOptionsMethodInfo.Invoke(iFileDialog, new object[] { (uint)s_getOptionsMethodInfo.Invoke(openFileDialog, new object[] { }) | s_fosPickFoldersBitFlag });

                var adviseParametersWithOutputConnectionToken = new[] { s_vistaDialogEventsConstructorInfo.Invoke(new object[] { openFileDialog }), 0U };
                s_adviseMethodInfo.Invoke(iFileDialog, adviseParametersWithOutputConnectionToken);

                if (!string.IsNullOrEmpty(fileNameLabel))
                    s_setFileNameLabelMethodInfo.Invoke(iFileDialog, new object[] { fileNameLabel });

                if (!string.IsNullOrEmpty(confirmButtonText))
                    s_setOkButtonLabelMethodInfo.Invoke(iFileDialog, new object[] { confirmButtonText });

                try
                {
                    int retVal = (int)s_showMethodInfo.Invoke(iFileDialog, new object[] { ownerHandle });
                    return new ShowDialogResult
                    {
                        Result = retVal == 0,
                        FileName = openFileDialog.FileName
                    };
                }
                finally
                {
                    s_unAdviseMethodInfo.Invoke(iFileDialog, new[] { adviseParametersWithOutputConnectionToken[1] });
                }
            }
        }

        // Wrap an IWin32Window around an IntPtr
        private class WindowWrapper : IWin32Window
        {
            private readonly IntPtr _handle;
            public WindowWrapper(IntPtr handle) { _handle = handle; }
            public IntPtr Handle { get { return _handle; } }
        }
    }

    //OpenFile and Save file are both a "FileSelector", only difference is in the Title fall-back
    public enum DialogType
    {
        OpenFile = 0,
        SaveFile = 1,
        SelectFolder = 2
    }
}


/*
var dialog = new CustomDialog
    {
        DialogType = DialogType.SelectFolder,
        InitialDirectory = "c:\\",
        Title = "Select a folder to import files from ..."
    };

if (dialog.Show(Handle))
{
    //something to do with the dialog.FileName
}



var dialog = new CustomDialog
    {
        DialogType = DialogType.OpenFile,
        InitialDirectory = "c:\\",
        Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*",
        FileNameLabel = "FileName or URL",
        ConfirmButtonText = "Yey!"
    };

if (dialog.Show(Handle))
{
    //something to do with the dialog.FileName
}
 */