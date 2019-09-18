
namespace Utils
{
    public class CustomPanel : System.Windows.Forms.Panel
    {
        protected override System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)
        {
            // Returning the current location prevents the panel from
            // scrolling to the active control when the panel loses and regains focus

            //CMA: this prevents the Series panel to scroll to another position when the episodes grid get's focus
            return DisplayRectangle.Location;
        }
    }
}
