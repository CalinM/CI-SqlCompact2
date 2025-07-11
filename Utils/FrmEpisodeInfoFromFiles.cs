﻿using Common;
using Common.ExtensionMethods;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Utils
{
    public partial class FrmEpisodeInfoFromFiles : EscapeForm
    {
        private int? _parentId;
        private List<SelectableElement> _seasons = new List<SelectableElement>();
        private IniFile _iniFile = new IniFile();

        public FilesImportParams EpisodesImportParams;


        public FrmEpisodeInfoFromFiles()
        {
            InitializeComponent();

            //InitSeasonsCombobox();
        }

        public FrmEpisodeInfoFromFiles(int parentId, string seasonId)
        {
            InitializeComponent();

            _parentId = parentId;

            //InitSeasonsCombobox();

            if (!string.IsNullOrEmpty(seasonId))
            {
                //cbSeason.SelectedItem = _seasons.FirstOrDefault(e => (int)e.Value == seasonId);
                //cbSe
                Text = string.Format("Refresh episodes data in Season {0}", seasonId);
            }

            if (Desene.DAL.SeriesType == SeriesType.Recordings)
            {
                pRercordingSpecifics.Visible = true;

                cbLanguages.DataSource = Languages.Iso639;
                cbLanguages.ValueMember = "Code";
                cbLanguages.DisplayMember = "Name";
                cbLanguages.SetSeparator(3);

                cbSkipMultiVersion.Checked = true;
            }
            else
            {
                pRercordingSpecifics.Visible = false;
                cbSkipMultiVersion.Checked = false;
            }
        }

        //private void InitSeasonsCombobox()
        //{
        //    _seasons.Add(new SelectableElement(-2, "Specials"));
        //    for (var i = 1; i < 30; i++)
        //    {
        //        _seasons.Add(new SelectableElement(i, i.ToString()));
        //    }

        //    cbSeason.DropDownHeight = 100;
        //    cbSeason.DataSource = _seasons;
        //    cbSeason.DisplayMember = "Description";
        //    cbSeason.ValueMember = "Value";

        //    cbSeason.SelectedIndex = -1;
        //}

        private void btnFolderSelector_Click(object sender, EventArgs e)
        {
            var selectedPath = Helpers.SelectFolder("Please select the episodes location (folder)", _iniFile.ReadString("LastPath", "General"));
            if (string.IsNullOrEmpty(selectedPath))
                return;

            _iniFile.Write("LastPath", Path.GetFullPath(selectedPath), "General");

            tbFilesLocation.Text = selectedPath;

            var files = Directory.GetFiles(selectedPath, "*.*");

            if (!files.ToList().DistinctBy(Path.GetExtension).Any())
                cbFileExtensions.Text = "folder empty!";
            else
            {
                var ext = "*" + Path.GetExtension(files.ToList().DistinctBy(Path.GetExtension).FirstOrDefault());
                if (cbFileExtensions.Items.IndexOf(ext) >= 0)
                {
                    cbFileExtensions.Text = ext;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveParamsAndClose();
        }

        private void SaveParamsAndClose()
        {
            if (tbFilesLocation.Text == string.Empty || /*tbYear.Text == string.Empty ||*/
                cbFileExtensions.SelectedIndex == -1 || string.IsNullOrEmpty(tbSeason.Text))
            {
                if (tbFilesLocation.Text == string.Empty)
                    lbLocation.ForeColor = Color.Red;
                if (cbFileExtensions.SelectedIndex == -1)
                    lbFilesExtensions.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(tbSeason.Text))
                    lbSeason.ForeColor = Color.Red;

                MsgBox.Show("Please specify all required import parameters!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var files = Directory.GetFiles(tbFilesLocation.Text, cbFileExtensions.Text);

            if (files.Length == 0)
            {
                MsgBox.Show("There are no files with the specified extension in the selected folder!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MsgBox.Show(string.Format("Are you sure you want to import {0} Episodes in the selected Series?", files.Length), "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            EpisodesImportParams = new FilesImportParams
            {
                ParentId = _parentId,
                Location = tbFilesLocation.Text,
                FilesExtension = cbFileExtensions.Text,
                Season = tbSeason.Text,
                Year = tbYear.Text,
                GenerateThumbnail = cbGenerateThumbnails.Checked,
                RecordingAudio = (string)cbLanguages.SelectedValue,
                SkipMultiVersion = cbSkipMultiVersion.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void tbFilesLocation_TextChanged(object sender, EventArgs e)
        {
            lbLocation.ForeColor = SystemColors.WindowText;
        }

        private void cbFileExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbFilesExtensions.ForeColor = SystemColors.WindowText;
        }

        private void cbGenerateThumbnails_CheckedChanged(object sender, EventArgs e)
        {
            lbWarning.Visible = cbGenerateThumbnails.Checked;
        }

        private void cbSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbSeason.ForeColor = SystemColors.WindowText;
        }

        private void tbYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SaveParamsAndClose();
        }
    }
}