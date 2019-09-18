using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils.Properties;

namespace Utils
{
    public partial class FrmNfNamesMix : Form
    {
        private List<string> _fileNamesMix;
        private bool _mustRebuild;

        public FrmNfNamesMix()
        {
            InitializeComponent();
        }

        private void FrmNfNamesMix_Load(object sender, EventArgs e)
        {
            if (Settings.Default.FrmNfNamesMix_WL.X > 0 && Settings.Default.FrmNfNamesMix_WL.Y > 0) //to auto-correct bad configuration
            {
                Location = Settings.Default.FrmNfNamesMix_WL;
            }

            // Set window size
            if (Settings.Default.FrmNfNamesMix_WS != null)
            {
                Size = Settings.Default.FrmNfNamesMix_WS;
            }

            cbNamingType.SelectedIndex = 1;
        }

        private void FrmNfNamesMix_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Copy window location to app settings
            Settings.Default.FrmNfNamesMix_WL = Location;

            // Copy window size to app settings
            Settings.Default.FrmNfNamesMix_WS = WindowState == FormWindowState.Normal ? Size : RestoreBounds.Size;

            // Save settings
            Settings.Default.Save();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (_mustRebuild) MixFileNames();
            else SaveMixedNames();

            Close();
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            MixFileNames(true);
        }

        private void RtbLanguage1_TextChanged(object sender, EventArgs e)
        {
            btnConfirm.Enabled = !string.IsNullOrWhiteSpace(rtbLanguage1.Text) && !string.IsNullOrWhiteSpace(rtbLanguage2.Text);
            btnPreview.Enabled = btnConfirm.Enabled;

            _mustRebuild = true;
        }

        private void MixFileNames(bool isPreview = false)
        {
            _mustRebuild = false;
            _fileNamesMix = new List<string>();

            var translatedFNLine = string.Empty;
            var originalFNLine = string.Empty;
            RichTextBox previewRE = null;

            if (isPreview)
            {
                if (!tcNfNamesMix.TabPages.ContainsKey("tpPreview"))
                {
                    var tpPreview = new TabPage
                    {
                        Name = "tpPreview",
                        Text = "Preview"
                    };

                    previewRE = new RichTextBox
                    {
                        Name = "rtbPreview",
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.None,
                        ScrollBars = RichTextBoxScrollBars.Vertical
                    };

                    tpPreview.Controls.Add(previewRE);

                    tcNfNamesMix.TabPages.Add(tpPreview);
                }
                else
                {
                    previewRE = (RichTextBox)(tcNfNamesMix.TabPages["tpPreview"].Controls.Find("rtbPreview", true)[0]);
                }
            }

            try
            {
                foreach (var myString1 in rtbLanguage1.Lines)
                {
                    translatedFNLine = myString1;

                    var charLocation1 = myString1.IndexOf(".", StringComparison.Ordinal);

                    if (charLocation1 == -1)
                        continue;

                    var episodeNo1 = myString1.Substring(0, charLocation1);

                    if (!int.TryParse(episodeNo1, out int episodeNoVal1))
                        continue;

                    foreach (var myString2 in rtbLanguage2.Lines)
                    {
                        originalFNLine = myString2;

                        var charLocation2 = myString2.IndexOf(".", StringComparison.Ordinal);

                        if (charLocation2 == -1)
                            continue;

                        var episodeNo2 = myString2.Substring(0, charLocation2);

                        if (episodeNo1 != episodeNo2)
                            continue;

                        if (!int.TryParse(episodeNo2, out int episodeNoVal2))
                            continue;

                        var epNo = episodeNoVal2 < 10 ? string.Format("0{0}", episodeNoVal2) : episodeNoVal2.ToString();
                        var title1 = myString1.Substring((charLocation1 + 1), myString1.Length - (charLocation1 + 1)).Trim();
                        var title2 = myString2.Substring((charLocation2 + 1), myString2.Length - (charLocation2 + 1)).Trim();
                        var ext =
                            string.IsNullOrEmpty(cbFilesExt.Text)
                                ? string.Empty
                                : string.Format("{0}{1}",
                                    cbFilesExt.Text.Contains(".")
                                        ? string.Empty
                                        : ".",
                                    cbFilesExt.Text);

                        _fileNamesMix.Add(
                            title1 == title2
                                ? string.Format("E{0}{3}{1}{2}",
                                    epNo,
                                    title1,
                                    ext,
                                    cbNamingType.Text)
                                : string.Format("E{0}{4}{1} ({2}){3}",
                                    epNo,
                                    title1,
                                    title2,
                                    ext,
                                    cbNamingType.Text)
                        );

                        break;
                    }
                }

                var replaceSlashWithAnd = false;
                if (_fileNamesMix.Any(s => s.Contains("/")))
                {
                    replaceSlashWithAnd =
                        MsgBox.Show("Replace '/' with '&' ?", "Confirmation",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
                }

                if (replaceSlashWithAnd)
                {
                    _fileNamesMix =
                        _fileNamesMix
                            .Select(s =>
                                s.Replace(": ", " - ")
                                 .Replace(" : ", " - ")
                                 .Replace(" :", " - ")
                                 .Replace(":", " - ")
                                 .Replace("?", string.Empty)
                                 .Replace("\"", "'")
                                 .Replace("*", string.Empty)
                                 .Replace("\\", string.Empty)
                                 .Replace("/", replaceSlashWithAnd ? "&" : string.Empty)
                                 .Replace("|", string.Empty)
                                 .Replace("<", "[")
                                 .Replace(">", "]"))
                            .ToList();
                }

                if (isPreview)
                {
                    previewRE.Clear();
                    previewRE.Lines = _fileNamesMix.ToArray();

                    tcNfNamesMix.SelectedIndex = 2;
                }
                else
                {
                    SaveMixedNames();
                }
            }
            catch (Exception ex)
            {
                var msg =
                    string.Format("{0}{1}{1}Translated file names line:{1}{2}{1}{1}Original file names line:{3}",
                        OperationResult.GetErrorMessage(ex),
                        Environment.NewLine,
                        translatedFNLine,
                        originalFNLine);

                if (isPreview)
                    previewRE.Text = msg;
                else
                    MsgBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveMixedNames()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save combined names as ...";
                saveDialog.Filter = "Text files (*.txt)|*.txt";
                saveDialog.FileName = "_#_combined-filenames";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                using (var sw = new StreamWriter(saveDialog.FileName, false, Encoding.Unicode))
                {
                    foreach (var s in _fileNamesMix)
                        sw.WriteLine(s);
                }
            }
        }

        private void CbFilesExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnConfirm.Enabled)
                MixFileNames(true);
        }
    }
}
