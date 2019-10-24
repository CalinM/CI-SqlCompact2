using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace Utils
{
    public class GraphicsHelpers
    {
        public static Image CreatePosterThumbnail(int width, int height, Image source)
        {
            var newImage = new Bitmap(width, height);

            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(source, new Rectangle(0, 0, width, height));
            }

            return newImage;
        }

        public static byte[] CreatePosterThumbnail(int width, int height, byte[] source)
        {
            Image resizedImage = null;

            using (var ms = new MemoryStream(source))
            {
                resizedImage = CreatePosterThumbnail(width, height, Image.FromStream(ms));

                //imgOgj.Save(fileName, ImageFormat.Jpeg);
            }

            using (var ms = new MemoryStream())
            {
                resizedImage.Save(ms, ImageFormat.Jpeg);
                //resizedImage.Save(ms, resizedImage.RawFormat);
                return ms.ToArray();
            }
        }
    }
}