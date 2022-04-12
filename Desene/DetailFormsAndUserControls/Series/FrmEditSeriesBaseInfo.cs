using System;
using System.IO;
using System.Windows.Forms;
using Common;
using DAL;
using Desene.Properties;

using Utils;

namespace Desene
{
    public partial class FrmEditSeriesBaseInfo : EscapeForm
    {
        public int NewId;

        public FrmEditSeriesBaseInfo()
        {
            InitializeComponent();
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Padding = new Padding(5, 0, 1, 0);

        }

        private void FrmEditSeriesBaseInfo_Load(object sender, EventArgs e)
        {

        }

        private void FrmEditSeriesBaseInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;

            Common.Helpers.UnsavedChanges = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ucEditSeriesBaseInfo.ValidateInput())
            {
                MsgBox.Show("Please specify all required details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var opRes = DAL.InsertSeries(ucEditSeriesBaseInfo.Title, ucEditSeriesBaseInfo.DescriptionLink, ucEditSeriesBaseInfo.Recommended,
                ucEditSeriesBaseInfo.RecommendedLink, ucEditSeriesBaseInfo.Notes, DAL.TmpPoster, ucEditSeriesBaseInfo.Trailer);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while inserting the new Series into the database:{0}{0}{1}{0}{0}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NewId = (int)opRes.AdditionalDataReturn;

            if (ucEditSeriesBaseInfo.RecommendedLink.Trim().Length > 0)
            {
                if (!ucEditSeriesBaseInfo.RecommendedLink.ToLower().Contains("commonsensemedia"))
                {
                    Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                        "The 'recommended' data is from a site which doesn't have scraper and parser built!", 5000, this);
                }
                else
                {
                    opRes = WebScraping.GetCommonSenseMediaData(ucEditSeriesBaseInfo.RecommendedLink);

                    if (!opRes.Success)
                    {
                        Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                            opRes.CustomErrorMessage, 5000, this);
                    }
                    else
                    {
                        opRes = DAL.SaveCommonSenseMediaData(NewId, (CSMScrapeResult)opRes.AdditionalDataReturn);

                        if (!opRes.Success)
                        {
                            Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Recommended data",
                                opRes.CustomErrorMessage, 5000, this);
                        }
                    }
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            var iniFile = new IniFile();

            var dialog = new CustomDialogs
            {
                Title = "Choose a poster",
                DialogType = DialogType.OpenFile,
                InitialDirectory = iniFile.ReadString("LastCoverPath", "General"),
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*",
                FileNameLabel = "FileName or URL",
                //ConfirmButtonText = "Confirm"
            };

            if (!dialog.Show(Handle)) return;

            iniFile.Write("LastCoverPath", Path.GetFullPath(dialog.FileName), "General");

            using (var ms = new MemoryStream())
            {
                using (var file = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                }

                ucEditSeriesBaseInfo.SetPoster(ms.ToArray());
            }
        }

        private void btnOpenPages_Click(object sender, EventArgs e)
        {
            Utils.Helpers.OpenBaseWebPages(ucEditSeriesBaseInfo.Title, Sections.Series);
        }
    }
}