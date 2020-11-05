using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace Utils
{
    public partial class FrmToast : Form
    {
        private int _hideAfterXmSec = 5000;
        public FrmToast()
        {
            InitializeComponent();
        }

        public FrmToast(StartPosition2 startPosition, MessageType messageType, string title, string text, int hideAfterXmSec)
        {
            InitializeComponent();
            _hideAfterXmSec = hideAfterXmSec;

            switch (messageType)
            {
                case MessageType.Information:
                    toastImage.Image = Properties.Resources.information;
                    break;
                case MessageType.Warning:
                    toastImage.Image = Properties.Resources.warning;
                    break;
                case MessageType.Error:
                    toastImage.Image = Properties.Resources.error;
                    break;
                case MessageType.Confirmation:
                    toastImage.Image = Properties.Resources.confirmation;
                    break;
            };

            lbTitle.Text = title;
            lbText.Text = text;

            var sz = new Size(lbText.Width, 500);
            sz = TextRenderer.MeasureText(text, lbText.Font, sz, TextFormatFlags.WordBreak);
            lbText.Height = sz.Height;

            lbAppName.Text = Application.ProductName;
            lbAppName.Top = lbText.Height + lbText.Top + 5;

            Height = lbAppName.Top + lbAppName.Height + 22;

            //todo: taskbar position and size ... https://stackoverflow.com/questions/1264406/how-do-i-get-the-taskbars-position-and-size
            switch (startPosition)
            {
                case StartPosition2.TopLeft:
                    Location = new Point(10, 100);
                    break;
                case StartPosition2.TopRight:
                    Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 10 - this.Width, 100);
                    break;
                case StartPosition2.BottomLeft:
                    Location = new Point(10, Screen.PrimaryScreen.WorkingArea.Height - this.Height - 5);
                    break;
                case StartPosition2.BottomRight:
                    Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 10 - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height - 20);
                    break;
            }
        }


        private async void FadeIn(Form o, int interval = 80)
        {
            //Object is not fully invisible. Fade it in
            while (o.Opacity < 1.0)
            {
                await Task.Delay(interval);
                o.Opacity += 0.05;
            }

            o.Opacity = 1; //make fully visible

            if (_hideAfterXmSec > 0) //-1 = noly close by click
            {
                await Task.Delay(_hideAfterXmSec);
                FadeOut(this, 5);
            }
        }

        private async void FadeOut(Form o, int interval = 80)
        {
            while (o.Opacity > 0.0)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }

            o.Opacity = 0; //make fully invisible
            Close();
        }

        private void FrmToast_Shown(object sender, EventArgs e)
        {
            FadeIn(this, 10);
        }

        private void FrmToast_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            //the rectangle, the same size as our Form
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);

            //define gradient's properties
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(45, 120, 165), Color.FromArgb(23, 99, 144), 90f);

            //apply gradient
            graphics.FillRectangle(b, gradient_rectangle);
        }

        private void FrmToast_MouseDown(object sender, MouseEventArgs e)
        {
            FadeOut(this, 5);
        }
    }
}
