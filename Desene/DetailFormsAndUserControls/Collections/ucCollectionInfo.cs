﻿using Common;
using DAL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utils;

namespace Desene.DetailFormsAndUserControls.Collections
{
    public partial class ucCollectionInfo : UserControl
    {
        public object ParentEl { get; set; }

        public string Title { get { return tbTitle.Text; } }

        public string Notes { get { return tbNotes.Text; } }

        public int SectionType { get { return cbSectionType.SelectedIndex; } }

        private byte[] _poster;
        public byte[] Poster
        {
            get { return _poster; }
            set
            {
                _poster = value;

                if (value != null)
                {
                    using (var ms = new MemoryStream(_poster))
                    {
                        pbCover.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        public ucCollectionInfo()
        {
            InitializeComponent();

            cbSectionType.SelectedIndex = 0;
            Size = new Size(200, 275);
        }


        public void RefreshControls(SeriesEpisodesShortInfo sesi)
        {
            tbTitle.Text = sesi.FileName;
            tbNotes.Text = sesi.Notes;
            cbSectionType.SelectedIndex = sesi.SectionType;

            Poster = sesi.Poster;
            Common.Helpers.UnsavedChanges = false;
        }

        public bool ValidateInput()
        {
            var result = true;

            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                lbTitle.ForeColor = Color.Red;
                result = false;
            }

            if (cbSectionType.SelectedIndex == -1)
            {
                lbSiteSectionType.ForeColor = Color.Red;
                result = false;
            }

            return result;
        }

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            lbTitle.ForeColor = SystemColors.WindowText;
        }

        private void cbSectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.Helpers.UnsavedChanges = true;
            lbSiteSectionType.ForeColor = SystemColors.WindowText;

            pPosterWrapper.Visible = cbSectionType.SelectedIndex == 1;

            if (ParentEl == null) return; //dev stage, when the parameter is not loaded on the view part of th eUC usage

            if (ParentEl is FrmAddCollection)
            {
                ((FrmAddCollection)ParentEl).btnLoadPoster.Visible = pPosterWrapper.Visible;

                ((FrmAddCollection)ParentEl).Size =
                    pPosterWrapper.Visible
                        ? new Size(700, 450)
                        : new Size(550, 300);
            }

            if (ParentEl is Panel)
            {
                Size =
                    pPosterWrapper.Visible
                        ? new Size(200, 360)
                        : new Size(200, 275);
            }
        }

        private void ucCollectionInfo_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void ucCollectionInfo_DragDrop(object sender, DragEventArgs e)
        {
            var droppedObj = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            try
            {
                if (File.GetAttributes(droppedObj).HasFlag(FileAttributes.Directory))
                    return;

                var picturesExt = new string[] { ".jpg", ".jpeg", ".png", ".bmp" };

                if (Array.IndexOf(picturesExt, Path.GetExtension(droppedObj).ToLower()) >= 0)
                {
                    //SetNewPoster(droppedObj);
                    using (var file = new FileStream(droppedObj, FileMode.Open, FileAccess.Read))
                    {
                        var bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);

                        //SetPoster(bytes);
                        Common.Helpers.UnsavedChanges = true;

                        Poster = bytes;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public void SetPoster(byte[] bytes)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        ms.Write(bytes, 0, bytes.Length);
        //        pbCover.Image = Image.FromStream(ms);
        //    }

        //    if (_isNew)
        //        Poster = bytes;
        //    else
        //        DAL.CurrentMTD.Poster = bytes;
        //}
    }
}