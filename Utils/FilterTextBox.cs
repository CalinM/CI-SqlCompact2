using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utils
{
    public class FilterTextBox: TextBox
    {
        private bool _clearButtonVisible;
        private Label _cancelSearchLabel;

        public event EventHandler ButtonClick { add { _cancelSearchLabel.Click += value; } remove { _cancelSearchLabel.Click -= value; } }

        protected override void OnTextChanged(EventArgs args)
        {
            //KeyEventArgs kpe = (KeyEventArgs)args;

            base.OnTextChanged(args);

            _clearButtonVisible = Text != string.Empty;
            _cancelSearchLabel.Visible = _clearButtonVisible;
            SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)(_cancelSearchLabel.Width << 16));

            SetupWatermark();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_clearButtonVisible)
                SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)(_cancelSearchLabel.Width << 16));
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (!_clearButtonVisible)
            {
                Text = string.Empty;
                SendMessage(Handle, 0x1501, 1, "");
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            SetupWatermark();
        }

        public FilterTextBox()
        {
            _cancelSearchLabel = new Label
                            {
                                Cursor = Cursors.Hand,
                                Size = new Size(16, ClientSize.Height),
                                Visible = false,
                                Dock = DockStyle.Right,
                                Text = "🗙",
                                ForeColor = Color.Gray
                            };

            _cancelSearchLabel.MouseEnter += CancelSearchLabelOnMouseEnter;
            _cancelSearchLabel.MouseLeave += CancelSearchLabelOnMouseLeave;

            Controls.Add(_cancelSearchLabel);

            SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)(_cancelSearchLabel.Width << 16));
            SetupWatermark();
        }

        private void SetupWatermark()
        {
            ForeColor = !_clearButtonVisible ? Color.Silver : SystemColors.WindowText;

            SendMessage(Handle, 0x1501, 1, "Type a filter criteria and press the Return key ...");
        }

        private void CancelSearchLabelOnMouseEnter(object sender, EventArgs eventArgs)
        {
            ((Label)sender).BackColor = Color.WhiteSmoke;
            ((Label)sender).ForeColor = Color.Black;
        }

        private void CancelSearchLabelOnMouseLeave(object sender, EventArgs eventArgs)
        {
            ((Label)sender).BackColor = SystemColors.Window;
            ((Label)sender).ForeColor = Color.Gray;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
    }
}
