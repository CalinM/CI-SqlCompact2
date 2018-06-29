using System;
using System.IO;
using System.Windows.Forms;

using Utils;

namespace Desene
{
    public partial class FrmEditSeriesBaseInfo : Form
    {
        public int NewId;

        public FrmEditSeriesBaseInfo()
        {
            InitializeComponent();
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
                MsgBox.Show(@"Please specify all required details!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var opRes = DAL.InsertSeries(ucEditSeriesBaseInfo.Title, ucEditSeriesBaseInfo.DescriptionLink, ucEditSeriesBaseInfo.Recommended,
                ucEditSeriesBaseInfo.RecommendedLink, ucEditSeriesBaseInfo.Notes, ucEditSeriesBaseInfo.Poster);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error had occurred while inserting the new Series into the database:{0}{0}{1}{0}{0}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NewId = (int)opRes.AdditionalDataReturn;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog())
            {
                if (selectFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var file = new FileStream(selectFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            byte[] bytes = new byte[file.Length];
                            file.Read(bytes, 0, (int)file.Length);
                            ms.Write(bytes, 0, (int)file.Length);
                        }

                        ucEditSeriesBaseInfo.Poster = ms.ToArray();
                    }
                }
            }
        }
    }
}