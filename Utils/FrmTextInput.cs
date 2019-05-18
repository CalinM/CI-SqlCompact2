using System;
using System.Windows.Forms;

namespace Utils
{
    public partial class FrmTextInput : Form
    {
        public string InputText
        { 
            get 
            { 
                return tbInputText.Text;
            }
        }
        public FrmTextInput()
        {
            InitializeComponent();
        }

        public FrmTextInput(string caption)
        {
            InitializeComponent();

            Text = caption;
        }

        private void TbInputText_TextChanged(object sender, EventArgs e)
        {
            btnConfirm.Enabled = tbInputText.Text.Trim() != string.Empty;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
