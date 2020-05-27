using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private bool? _replaceSlashWithAnd = null;

        public FrmNfNamesMix()
        {
            InitializeComponent();

            rtbLanguage1.Text = string.Empty;
            rtbLanguage2.Text = string.Empty;
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
            if (_mustRebuild)
            {
                var opRes = MixFileNames();

                if (!opRes.Success)
                {
                    MsgBox.Show(opRes.CustomErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    _fileNamesMix = (List<string>)opRes.AdditionalDataReturn;
                }
            }

            if (SaveMixedNames())
                Close();
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            BuildPreview();
        }

        private void BuildPreview()
        {
            RichTextBox previewRE = null;

            if (_mustRebuild)
            {
                var opRes = MixFileNames();

                if (!opRes.Success)
                {
                    MsgBox.Show(opRes.CustomErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    _fileNamesMix = (List<string>)opRes.AdditionalDataReturn;
                }

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
                        ScrollBars = RichTextBoxScrollBars.Vertical,
                        Font = new Font("Consolas", 10),
                        ReadOnly = true
                    };

                    tpPreview.Controls.Add(previewRE);

                    tcNfNamesMix.TabPages.Add(tpPreview);
                }
                else
                    previewRE = (RichTextBox)(tcNfNamesMix.TabPages["tpPreview"].Controls.Find("rtbPreview", true)[0]);
            }
            else
            {
                previewRE = (RichTextBox)(tcNfNamesMix.TabPages["tpPreview"].Controls.Find("rtbPreview", true)[0]);
            }

            previewRE.Clear();
            previewRE.Lines = _fileNamesMix.ToArray();

            tcNfNamesMix.SelectedIndex = 2;
        }

        private void RtbLanguage1_TextChanged(object sender, EventArgs e)
        {
            btnConfirm.Enabled = !string.IsNullOrWhiteSpace(rtbLanguage1.Text);
            btnPreview.Enabled = btnConfirm.Enabled;

            _mustRebuild = true;
        }

        private OperationResult MixFileNames()
        {
            var result = new OperationResult();

            var translatedFNLine = string.Empty;
            var originalFNLine = string.Empty;
            var mixLines = new List<string>();

            try
            {
                var ext =
                    string.IsNullOrEmpty(cbFilesExt.Text)
                        ? string.Empty
                        : string.Format("{0}{1}",
                            cbFilesExt.Text.Contains(".")
                                ? string.Empty
                                : ".",
                            cbFilesExt.Text);

                var epProcessedList1 = 0;
                var epProcessedList2 = 0;

                foreach (var myString1 in rtbLanguage1.Lines)
                {
                    translatedFNLine = myString1;

                    var charLocation1 = myString1.IndexOf(".", StringComparison.Ordinal);

                    if (charLocation1 == -1)
                        continue;

                    var episodeNo1 = myString1.Substring(0, charLocation1);

                    if (!int.TryParse(episodeNo1, out int episodeNoVal1))
                        continue;

                    epProcessedList1++;

                    if (rtbLanguage2.Lines.Count() == 0)
                    {
                        var epNo = episodeNoVal1 < 10 ? string.Format("0{0}", episodeNoVal1) : episodeNoVal1.ToString();
                        var title1 = myString1.Substring((charLocation1 + 1), myString1.Length - (charLocation1 + 1)).Trim();

                        mixLines.Add(
                            string.Format("E{0}{3}{1}{2}",
                                    epNo,
                                    title1,
                                    ext,
                                    cbNamingType.Text)
                            );

                        epProcessedList2++;
                    }
                    else
                    {
                        var episodeIn2 = false;

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

                            mixLines.Add(
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

                            epProcessedList2++;
                            episodeIn2 = true;
                            break;
                        }

                        if (!episodeIn2) return result.FailWithMessage(string.Format("The episode {0} could not be found in the 'Original' list!", episodeNo1));
                    }
                }

                if (epProcessedList1 != epProcessedList2)
                    return result.FailWithMessage("The episodes lists do not contain the same amount of episodes!");

                if (mixLines.Any(s => s.Contains("/") || s.Contains("+")) && _replaceSlashWithAnd == null)
                {
                    _replaceSlashWithAnd =
                        MsgBox.Show("Replace '/' and '+' with '&' ?", "Confirmation",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
                }

                var mixReplaceDef = Helpers.GetDefaultMixFileNameReplaceValues();
                mixReplaceDef.Add(new MixFileNameReplaceDef("/", (bool)_replaceSlashWithAnd ? " & " : string.Empty));
                mixReplaceDef.Add(new MixFileNameReplaceDef("+", (bool)_replaceSlashWithAnd ? " & " : string.Empty));

                foreach (var replaceDef in mixReplaceDef)
                {
                    var tmplist = new List<string>();

                    foreach (var mixFileName in mixLines)
                    {
                        if (!mixFileName.Contains(replaceDef.OldValue))
                        {
                            tmplist.Add(mixFileName);
                            continue;
                        }

                        var lineElements = mixFileName.Split(new string[] { replaceDef.OldValue }, StringSplitOptions.None).Select(s => s.Trim()).ToList();
                        tmplist.Add(string.Join(replaceDef.NewValue, lineElements));
                    }

                    mixLines = new List<string>(tmplist);
                }

                result.AdditionalDataReturn = mixLines;
                _mustRebuild = false;
            }
            catch (Exception ex)
            {
                var msg =
                    string.Format("{0}{1}{1}Translated file names line:{1}{2}{1}{1}Original file names line:{3}",
                        OperationResult.GetErrorMessage(ex),
                        Environment.NewLine,
                        translatedFNLine,
                        originalFNLine);

                return result.FailWithMessage(msg);
            }

            return result;
        }

        private bool SaveMixedNames()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save combined names as ...";
                saveDialog.Filter = "Text files (*.txt)|*.txt";
                saveDialog.FileName = "_#_combined-filenames";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return false;

                try
                {
                    using (var sw = new StreamWriter(saveDialog.FileName, false, Encoding.Unicode))
                    {
                        foreach (var s in _fileNamesMix)
                            sw.WriteLine(s);
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show(OperationResult.GetErrorMessage(ex), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void CbFilesExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnPreview.Enabled)
            {
                _mustRebuild = true;
                BuildPreview();
            }
        }

        private bool _preventEvent = false;

        private void cbProcessNamesOpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_preventEvent) return;

            RichTextBox currentRichEdit = null;

            switch (tcNfNamesMix.SelectedTab.TabIndex)
            {
                case 0:
                    currentRichEdit = rtbLanguage1;
                    break;
                case 1:
                    currentRichEdit = rtbLanguage2;
                    break;
            }

            if (currentRichEdit == null)
            {
                ResetProcessNamesOptCb();
                return;
            };

            var originalText = currentRichEdit.Lines;

            if (originalText.Length == 0)
            {
                ResetProcessNamesOptCb();
                return;
            };

            currentRichEdit.Text = string.Empty;

            for (var i = 0; i < originalText.Length; i++)
            {
                var textLine = originalText[i];

                currentRichEdit.AppendText(
                    (cbProcessNamesOpt.SelectedIndex == 0
                        ? textLine.ToTitleCase(true)
                        : textLine.ToSentenceCase(true))
                    +
                    (i < originalText.Length-1
                        ? Environment.NewLine
                        : string.Empty
                    )
                );
            }

            if (MsgBox.Show("Do you want to persist the changes?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                currentRichEdit.Text = string.Empty;

                for (var i = 0; i < originalText.Length; i++)
                {
                    var textLine = originalText[i];

                    currentRichEdit.AppendText(textLine +
                        (i < originalText.Length-1
                        ? Environment.NewLine
                        : string.Empty
                    ));
                }
            }

            ResetProcessNamesOptCb();
        }

        private void ResetProcessNamesOptCb()
        {
            _preventEvent = true;
            cbProcessNamesOpt.SelectedIndex = -1;
            _preventEvent = false;
        }
    }
}
