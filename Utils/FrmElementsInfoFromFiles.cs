using Utils.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Common.ExtensionMethods;

using DAL;

namespace Utils
{
    public partial class FrmElementsInfoFromFiles : Form
    {
        private int? _parentId;
        private List<SelectableElement> _seasons = new List<SelectableElement>();

        public FilesImportParams ElementsImportParams;


        public FrmElementsInfoFromFiles()
        {
            InitializeComponent();
        }

        public FrmElementsInfoFromFiles(int parentId)
        {
            InitializeComponent();

            _parentId = parentId;
        }

        private void InitSeasonsCombobox()
        {
            _seasons.Add(new SelectableElement(-2, "Specials"));
            for (var i = 1; i < 30; i++)
            {
                _seasons.Add(new SelectableElement(i, i.ToString()));
            }
        }

        private void btnFolderSelector_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Please select the episodes location (folder)";
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.SelectedPath = Settings.Default.LastPath;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.LastPath = folderBrowserDialog.SelectedPath;
                    Settings.Default.Save();

                    tbFilesLocation.Text = folderBrowserDialog.SelectedPath;

                    var files = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.*");

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
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbFilesLocation.Text == string.Empty)
            {
                if (tbFilesLocation.Text == string.Empty)
                    lbLocation.ForeColor = Color.Red;
                if (cbFileExtensions.SelectedIndex == -1)
                    lbFilesExtensions.ForeColor = Color.Red;

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

            if (MsgBox.Show(string.Format("Are you sure you want to import {0} Elements in the selected Collection?", files.Length), "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            ElementsImportParams = new FilesImportParams
                                       {
                                           ParentId = _parentId,
                                           Location = tbFilesLocation.Text,
                                           FilesExtension = cbFileExtensions.Text,
                                           Season = "",
                                           Year = tbYear.Text,
                                           GenerateThumbnail = cbGenerateThumbnails.Checked
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
    }
}