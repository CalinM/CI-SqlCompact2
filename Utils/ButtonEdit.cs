using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utils
{
    [Bindable(true)]
    public class ButtonEdit: TextBox
    {
        private Image _buttonImage;
        private bool _buttonVisible;
        private PictureBox _buttonEmulation;
        private PictureBoxSizeMode _buttonImageSizeMode = PictureBoxSizeMode.CenterImage;
        private int _buttonImageForceWidth = 16;
        private Cursor _buttonCursor = Cursors.Default;

        private string _buttonToolTip = string.Empty;
        private ToolTip _tooltip;

        //private Color ButtonBackgroundColorNormal =

        public event EventHandler ButtonClick
        {
            add { _buttonEmulation.Click += value; }
            remove { _buttonEmulation.Click -= value; }
        }

        public Image ButtonImage
        {
            get
            {
                return _buttonImage;
            }
            set
            {
                _buttonImage = value;
                _buttonEmulation.Image = _buttonImage;
            }
        }

        public bool ButtonVisible
        {
            get
            {
                return _buttonVisible;
            }
            set
            {
                _buttonVisible = value;
                _buttonEmulation.Visible = _buttonVisible;

                if (_buttonVisible)
                {
                    SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)(_buttonImageForceWidth + 4 << 16)); //4 = "-2" from the right padding and 2 more for the left margin
                    SelectionStart = Text.Length;
                    SelectionLength = 0;
                }
                else
                {
                    SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)0);
                    SelectionStart = 0;
                    SelectionLength = 0;
                }


                if (_buttonVisible && !string.IsNullOrEmpty(_buttonToolTip))
                {
                    if (_tooltip == null)
                        _tooltip = new ToolTip();

                    _tooltip.SetToolTip(_buttonEmulation, _buttonToolTip);
                    _tooltip.ShowAlways = true;
                }
            }
        }

        public PictureBoxSizeMode ButtonImageSizeMode
        {
            get
            {
                return _buttonImageSizeMode;
            }
            set
            {
                _buttonImageSizeMode = value;
                _buttonEmulation.SizeMode = _buttonImageSizeMode;
            }
        }

        public int ButtonImageForceWidth
        {
            get
            {
                return _buttonImageForceWidth;
            }
            set
            {
                _buttonImageForceWidth = value;
                _buttonEmulation.Size = new Size(_buttonImageForceWidth, ClientSize.Height);
                //_buttonEmulation.Location = new Point(ClientSize.Width-_buttonImageForceWidth - 2, 0);
            }
        }

        public Cursor ButtonCursor
        {
            get
            {
                return _buttonCursor;
            }
            set
            {
                _buttonCursor = value;
                _buttonEmulation.Cursor = _buttonCursor;
            }
        }

        public string ButtonToolTip
        {
            get
            {
                return _buttonToolTip;
            }
            set
            {
                _buttonToolTip = value;

                if (_buttonVisible && !string.IsNullOrEmpty(_buttonToolTip))
                {
                    if (_tooltip == null)
                        _tooltip = new ToolTip();

                    _tooltip.SetToolTip(_buttonEmulation, _buttonToolTip);
                    _tooltip.ShowAlways = true;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_buttonVisible)
                SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)(_buttonImageForceWidth + 4 << 16)); //4 = "-2" from the right padding and 2 more for the left margin
        }

        public ButtonEdit()
        {
            _buttonEmulation = new PictureBox
                                   {
                                       Size = new Size(_buttonImageForceWidth, ClientSize.Height),
                                       //Location = new Point(ClientSize.Width-_buttonImageForceWidth, 0),
                                       //Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                                       BackColor = Color.FromArgb(252, 252, 252),
                                       Dock = DockStyle.Right
                                    };

            _buttonEmulation.MouseHover += (s, e) =>
                {
                    ((PictureBox)s).BackColor = Color.WhiteSmoke;//SystemColors.InactiveCaption;
    };

            _buttonEmulation.MouseLeave += (s, e) =>
                {
                    ((PictureBox)s).BackColor = Color.FromArgb(252, 252, 252);
                };

            Controls.Add(_buttonEmulation);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
}
