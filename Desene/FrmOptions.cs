using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

using Common;
using Utils;

namespace Desene
{
    public partial class FrmOptions : EscapeForm
    {
        private IniFile _iniFile = new IniFile();

        public FrmOptions()
        {
            InitializeComponent();

            ConfigurationToControls();
        }

        private void ConfigurationToControls()
        {
            var checkMkvDuringFileDetails = _iniFile.ReadBool("CheckMkvDuringFileDetails", "Configurations");
            cbCheckMkvDuringFileDetails.Checked = checkMkvDuringFileDetails;
            SetCheckMkvDuringFileDetailsDependentControlsState(checkMkvDuringFileDetails);

            tbMkValidatorPath.Text = _iniFile.ReadString("MkvValidatorPath", "Configurations");
        }

        private void SetCheckMkvDuringFileDetailsDependentControlsState(bool b)
        {
            lbMkValidatorPath.ForeColor = b ? SystemColors.ControlText : Color.Gray;
            tbMkValidatorPath.Enabled = b;
            btnFolderSelectorMkvValidator.Enabled = b;
        }

        private void cbCheckMkvDuringFileDetails_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckMkvDuringFileDetailsDependentControlsState(cbCheckMkvDuringFileDetails.Checked);
        }

        private void tbMkValidatorPath_TextChanged(object sender, EventArgs e)
        {
            lbMkValidatorPath.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void btnFolderSelectorMkvValidator_Click(object sender, EventArgs e)
        {
            var selectedPath = Utils.Helpers.SelectFolder("Please select the MkvValidator.exe location (folder)", "");
            if (string.IsNullOrEmpty(selectedPath))
                return;

            if (!File.Exists(Path.Combine(selectedPath, "mkvalidator.exe")))
            {
                MsgBox.Show(
                    string.Format("The file 'mkvalidator.exe' could not be found in the selected folder.{0}{0}Warning! This program is not part of the mkvToolnix suite and needs to be downloaded separately!",
                        Environment.NewLine), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tbMkValidatorPath.Text = selectedPath;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateControlsState()) return;

            ControlsToConfiguration();

            //DialogResult = DialogResult.OK;
            Close();
        }

        private void ControlsToConfiguration()
        {
            _iniFile.Write("CheckMkvDuringFileDetails", cbCheckMkvDuringFileDetails.Checked.ToString(), "Configurations");
            _iniFile.Write("MkvValidatorPath", tbMkValidatorPath.Text, "Configurations");
        }

        private bool ValidateControlsState()
        {
            if (cbCheckMkvDuringFileDetails.Checked && tbMkValidatorPath.Text == string.Empty)
            {
                lbMkValidatorPath.ForeColor = System.Drawing.Color.Red;
                MsgBox.Show("The path to MkvValidator.exe needs to be specified!", "Warning", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
