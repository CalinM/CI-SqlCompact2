using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Desene
{
    public partial class FrmAddCollection : Form
    {
        //public int NewId;
        //public CollectionInfo CollectionObj;

        public FrmAddCollection()
        {
            InitializeComponent();
        }

        //public FrmAddCollection(CollectionInfo collectionObj)
        //{
        //    InitializeComponent();

        //    CollectionObj = collectionObj;
        //}

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ucCollectionInfo1.ValidateInput())
            {
                MsgBox.Show("Please specify all required details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var opRes = DAL.InsertCollection(ucCollectionInfo1.Title, ucCollectionInfo1.Notes, ucCollectionInfo1.SectionType);

            if (!opRes.Success)
            {
                MsgBox.Show(
                    string.Format("The following error occurred while inserting the new Collection into the database:{0}{0}{1}{0}{0}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //NewId = (int)opRes.AdditionalDataReturn;
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
