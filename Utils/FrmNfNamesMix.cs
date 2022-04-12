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
        private NamesMix_Ext _namesMix_Ext;
        private NamesMix_NameType _namesMix_NameType;
        private NamesMix_ProcessFN _namesMix_ProcessFN;
        private IniFile _iniFile = new IniFile();

        public FrmNfNamesMix()
        {
            InitializeComponent();

            rtbLanguage1.Text = string.Empty;
            rtbLanguage2.Text = string.Empty;
        }

        private void FrmNfNamesMix_Load(object sender, EventArgs e)
        {
            rtbLanguage1.Focus();

            if (_iniFile.KeyExists("Top", "MixNamesWindow") && _iniFile.KeyExists("Left", "MixNamesWindow"))
            {
                Location = new Point(_iniFile.ReadInt("Left", "MixNamesWindow"), _iniFile.ReadInt("Top", "MixNamesWindow"));
            }

            if (_iniFile.KeyExists("Width", "MixNamesWindow") && _iniFile.KeyExists("Height", "MixNamesWindow"))
            {
                Size = new Size(_iniFile.ReadInt("Width", "MixNamesWindow"), _iniFile.ReadInt("Height", "MixNamesWindow"));
            }

            SetCurrentOpt();
        }

        private void FrmNfNamesMix_FormClosed(object sender, FormClosedEventArgs e)
        {
            _iniFile.Write("Top", Location.Y.ToString(), "MixNamesWindow");
            _iniFile.Write("Left", Location.X.ToString(), "MixNamesWindow");

            var xSize = WindowState == FormWindowState.Normal ? Size : RestoreBounds.Size;
            _iniFile.Write("Width", xSize.Width.ToString(), "MixNamesWindow");
            _iniFile.Write("Height", xSize.Height.ToString(), "MixNamesWindow");
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
                        ReadOnly = true,
                        BackColor = Color.WhiteSmoke,
                    };

                    previewRE.KeyPress += PreviewRE_KeyPress;

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

        private void PreviewRE_KeyPress(object sender, KeyPressEventArgs e)
        {
            //to get rid of the Windows sound
            e.Handled = true;
        }

        private void RtbLanguage1_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = !string.IsNullOrWhiteSpace(rtbLanguage1.Text);
            btnPreview.Enabled = btnSave.Enabled;

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
                var ext = EnumHelpers.GetEnumDescription(_namesMix_Ext);
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
                                    EnumHelpers.GetEnumDescription(_namesMix_NameType))
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

                            switch (_namesMix_ProcessFN)
                            {
                                case NamesMix_ProcessFN.none:
                                    break;

                                case NamesMix_ProcessFN.ToSentenceCase:
                                    title1 = title1.ToSentenceCase(true);
                                    title2 = title2.ToSentenceCase(true);
                                    break;

                                case NamesMix_ProcessFN.ToTitleCase:
                                    title1 = title1.ToTitleCase(true);
                                    title2 = title2.ToTitleCase(true);
                                    break;
                            }

                            mixLines.Add(
                                title1 == title2
                                    ? string.Format("E{0}{3}{1}{2}",
                                        epNo,
                                        title1,
                                        ext,
                                        EnumHelpers.GetEnumDescription(_namesMix_NameType))
                                    : string.Format("E{0}{4}{1} ({2}){3}",
                                        epNo,
                                        title1,
                                        title2,
                                        ext,
                                        EnumHelpers.GetEnumDescription(_namesMix_NameType))
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
                mixReplaceDef.Add(new MixFileNameReplaceDef("/", _replaceSlashWithAnd.GetValueOrDefault(false) ? " & " : string.Empty));
                mixReplaceDef.Add(new MixFileNameReplaceDef("+", _replaceSlashWithAnd.GetValueOrDefault(false) ? " & " : string.Empty));

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

        private void UncheckAllMenuItems(ToolStripMenuItem parent)
        {
            foreach (ToolStripItem item in parent.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    ((ToolStripMenuItem)item).Checked = false;
                }
            }
        }

        private void miOpt_FileExt_SubItems_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems(miOpt_FileExt);
            ((ToolStripMenuItem)sender).Checked = true;
            _namesMix_Ext = (NamesMix_Ext)int.Parse(((ToolStripMenuItem)sender).Tag.ToString());

            SetCurrentOpt();
        }

        private void miOpt_NameType_SubItems_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems(miOpt_NameType);
            ((ToolStripMenuItem)sender).Checked = true;
            _namesMix_NameType = (NamesMix_NameType)int.Parse(((ToolStripMenuItem)sender).Tag.ToString());

            SetCurrentOpt();
        }

        private void miOpt_FNproc_SubItems_Click(object sender, EventArgs e)
        {
            var currentMenuItem = (ToolStripMenuItem)sender;
            var originallyChecked = currentMenuItem.Checked;

            UncheckAllMenuItems(miOpt_FNproc);

            currentMenuItem.Checked = !originallyChecked;

            _namesMix_ProcessFN =
                    currentMenuItem.Checked
                        ? (NamesMix_ProcessFN)int.Parse(((ToolStripMenuItem)sender).Tag.ToString())
                        : NamesMix_ProcessFN.none;

            SetCurrentOpt();
        }

        private void SetCurrentOpt()
        {
            _mustRebuild = true;

            tsslCurrentOpt.Text =
                string.Format("{0}, {1}, {2}",
                    _namesMix_Ext.ToString(),
                    EnumHelpers.GetEnumDescription(_namesMix_NameType),
                    EnumHelpers.GetEnumDescription(_namesMix_ProcessFN));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tcNfNamesMix.SelectedIndex = 0;
            rtbLanguage1.Clear();
            rtbLanguage2.Clear();

            if (tcNfNamesMix.TabPages.ContainsKey("tpPreview"))
            {
                var rtpPreview = (RichTextBox)(tcNfNamesMix.TabPages["tpPreview"].Controls.Find("rtbPreview", true)[0]);
                rtpPreview.Clear();
            }

            _namesMix_Ext = NamesMix_Ext.mkv;
            _namesMix_NameType = NamesMix_NameType.dash;
            _namesMix_ProcessFN = NamesMix_ProcessFN.none;

            SetCurrentOpt();
        }

        private void miSampleData_Click(object sender, EventArgs e)
        {
            rtbLanguage1.Text = @"1. Tak’s Torenhoge Tuimeltaart / Bamboeboe
2. Gipsbaksels / Okra-bal
3. Kijk op en Speel/ Naar De Maan
4. Lente-Surprise-Ei
5. De Jacht op het Blaadje/ Ridders van de Duikeltafel
6. Esdoorn’s mobiele Blubsietaartenkraam/ Papieren-Vliegtuig-Briefjes
7. Met de stroom mee/ Doorgeef-Fantasie
8. Op jacht naar de Kist/ Houvast
9. Niets in de Schatkist/ Zoekspelletje
10. Kukeldroedeldag/ Slaapfeestje aan dek
11. Tak’s stille knalfeest / Tappa-Tappa-Tapschoenen
12. De Wieldinges / Teletouw
13. Natuurvrienden/ De Schip-shop";

            rtbLanguage2.Text = @"1.Stick's Towering, Toppling Cake/Bambooboo
2.Rutabagels / Okra - Ball
3.Look Up And Play / To The Moon
4.Spring - a - ling Surprise
5.Chase The Leaf/ Knights of the Tumble Tale
6.Maple's Mobile Mudpie Stand/Paper Plane Messages
7.Go With The Flow / Passing Fancy
8.Quest for The Chest/ Get A Grip
9.Nothing In The Finding Place / Finding Play
10.Cock - A - Doodle Day / Glow In The Dark Sleepover
11.Stick's Quiet Riot/Tappa Tappa Tap Shoes
12.The Wheel Thing / Twine Line
13.The Nature Of Friendship / The Ship Shop";

            //_mustRebuild = true;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            BuildPreview();
        }

        private void btnSave_Click(object sender, EventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
