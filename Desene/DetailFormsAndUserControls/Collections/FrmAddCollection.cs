using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Desene
{
    public partial class FrmAddCollection : Form
    {
        public int NewId;

        public FrmAddCollection()
        {
            InitializeComponent();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                lbTitle.ForeColor = Color.Red;
                return false;
            }

            return true;
        }

        private void TbTitle_TextChanged(object sender, System.EventArgs e)
        {
            lbTitle.ForeColor = SystemColors.WindowText;
        }

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            if (!ValidateInput())
            {
                MsgBox.Show("Please specify all required details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var opRes =
                DAL.InsertCollection(
                    new MovieTechnicalDetails
                    {
                        FileName = tbTitle.Text, //FileName!, the 'Title' property is used for something else
                        Notes = tbNotes.Text,
                        ParentId = -10
                    }) ;

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while inserting the new Collection into the database:{0}{0}{1}{0}{0}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NewId = (int)opRes.AdditionalDataReturn;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void FrmAddCollection_FormClosed(object sender, FormClosedEventArgs e)
        {
            Common.Helpers.UnsavedChanges = false;
        }
    }
}
