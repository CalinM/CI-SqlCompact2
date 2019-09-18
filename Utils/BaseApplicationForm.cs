using System;
using System.Windows.Forms;

namespace Utils
{
    public class BaseApplicationForm : Form
    {
        const int WM_ACTIVATEAPP = 0x1C;

        public event EventHandler ApplicationActivated;

        public event EventHandler ApplicationDeactivated;

        protected virtual void OnApplicationActivated(EventArgs e)
        {
        EventHandler handler;

        handler = ApplicationActivated;

        if (handler != null)
            handler(this, e);
        }

        protected virtual void OnApplicationDeactivated(EventArgs e)
        {
        EventHandler handler;

        handler = ApplicationDeactivated;

        if (handler != null)
            handler(this, e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ACTIVATEAPP)
            {
                if (m.WParam != IntPtr.Zero)
                    OnApplicationActivated(EventArgs.Empty);
                else
                    OnApplicationDeactivated(EventArgs.Empty);
            }

            base.WndProc(ref m);
        }
    }
}
