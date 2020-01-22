using System.Drawing;
using System.Windows.Forms;

namespace Utils
{
    public class UnselectableButton : Button
    {
        public UnselectableButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            BackgroundImageLayout = ImageLayout.Center;

            TabStop = false;
            SetStyle(ControlStyles.Selectable, false);

            MouseEnter += UnselectableButton_MouseEnter;
            MouseLeave += UnselectableButton_MouseLeave;
            MouseDown += UnselectableButton_MouseDown;
            MouseUp += UnselectableButton_MouseUp;
        }

        private void UnselectableButton_MouseUp(object sender, MouseEventArgs e)
        {
            FlatAppearance.BorderColor = Color.Gray;
        }

        private void UnselectableButton_MouseDown(object sender, MouseEventArgs e)
        {
            FlatAppearance.BorderColor = Color.Blue;
        }

        private void UnselectableButton_MouseLeave(object sender, System.EventArgs e)
        {
            FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
        }

        private void UnselectableButton_MouseEnter(object sender, System.EventArgs e)
        {
            FlatAppearance.BorderColor = Color.Gray;
        }
    }
}
