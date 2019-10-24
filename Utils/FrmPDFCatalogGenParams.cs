using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Utils
{
    public partial class FrmPDFCatalogGenParams : Form
    {
        public PdfGenParams PdfGenParams;

        public FrmPDFCatalogGenParams()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbFilesLocation.Text == string.Empty)
            {
                if (tbFilesLocation.Text == string.Empty)
                    lbLocation.ForeColor = Color.Red;

                MsgBox.Show("Please specify all required site generation parameters!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            PdfGenParams = new PdfGenParams
            {
                FileName = tbFilesLocation.Text,
                PDFGenType =
                    rbAll.Checked
                        ? PDFGenType.All
                        : rbChristmas.Checked
                            ? PDFGenType.Christmas
                            : PDFGenType.Helloween,
                ForMovies = rbMovies.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnFolderSelector_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save catalog as ...";
                saveDialog.Filter = "Adobe Acrobat files (*.pdf)|*.pdf";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                tbFilesLocation.Text = saveDialog.FileName;
            }
        }

        private void rbSeries_CheckedChanged(object sender, EventArgs e)
        {
            SetControlStates(false);
        }

        private void rbMovies_CheckedChanged(object sender, EventArgs e)
        {
            SetControlStates(true);
        }

        private void SetControlStates(bool b)
        {
            rbAll.Enabled = b;
            rbChristmas.Enabled = b;
            rbHelloween.Enabled = b;

            if (!b)
            {
                rbAll.Checked = false;
                rbChristmas.Checked = false;
                rbHelloween.Checked = false;
            }
            else
            {
                rbAll.Checked = true;
            }
        }
    }
}
