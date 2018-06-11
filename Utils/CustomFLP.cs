using System.Windows.Forms;

namespace Utils
{
    public class CustomFLP: FlowLayoutPanel
    {
        public CustomFLP()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            UpdateStyles();
        }
    }
}
