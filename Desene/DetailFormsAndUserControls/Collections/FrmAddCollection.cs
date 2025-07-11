﻿using Common;
using Desene.DetailFormsAndUserControls.Collections;
using Desene.Properties;
using System;
using System.IO;
using System.Windows.Forms;
using Utils;

namespace Desene
{
    public partial class FrmAddCollection : EscapeForm
    {
        //private ucCollectionInfo _ucCollectionInfo;

        public int NewId;

        public FrmAddCollection()
        {
            InitializeComponent();

            //_ucCollectionInfo = new ucCollectionInfo() { ParentEl = this };
            //_ucCollectionInfo.Dock = DockStyle.Fill;

            //Controls.Add(_ucCollectionInfo);
            //_ucCollectionInfo.BringToFront();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ucCollectionInfo.ValidateInput())
            {
                MsgBox.Show("Please specify all required details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var opRes = DAL.InsertCollection(ucCollectionInfo.Title, ucCollectionInfo.Notes, ucCollectionInfo.SectionType, DAL.TmpPoster);

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
            if (Utils.Helpers.ConfirmDiscardChanges())
            {
                this.Close();
            }
        }

        private void FrmAddCollection_FormClosed(object sender, FormClosedEventArgs e)
        {
            Common.Helpers.UnsavedChanges = false;
        }

        private void btnLoadPoster_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title =
                    string.IsNullOrEmpty(ucCollectionInfo.Title)
                        ? "Choose a poster for untitled collection"
                        : string.Format("Choose a poster for collection '{0}'",  ucCollectionInfo.Title);

                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";

                var iniFile = new IniFile();
                openFileDialog.InitialDirectory = iniFile.ReadString("LastCoverPath", "General");

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                iniFile.Write("LastCoverPath", Path.GetDirectoryName(openFileDialog.FileName), "General");

                using (var file = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);

                    ucCollectionInfo.SetPoster(bytes);
                    //ucCollectionInfo.Poster = bytes;
                }

                Common.Helpers.UnsavedChanges = true;
            }
        }
    }
}
