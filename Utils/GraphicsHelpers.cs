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

        public static byte[] CreatePosterThumbnailForPDF(int width, int height, byte[] source, string rec, string audioSummary, Image themeImage)
        {
            Image resizedImage = null;
            Image audioSymbol = null;

            if (!string.IsNullOrEmpty(audioSummary))
            {
                if (audioSummary.ToLower().Contains("ro") && audioSummary.ToLower().Contains("nl"))
                    audioSymbol = new Bitmap(Properties.Resources.Ro_Nl);
                else
                if (audioSummary.ToLower().Contains("ro"))
                    audioSymbol = new Bitmap(Properties.Resources.Ro);
                else
                if (audioSummary.ToLower().Contains("nl"))
                    audioSymbol = new Bitmap(Properties.Resources.Nl);
            }


            if (source != null)
            {
                using (var ms = new MemoryStream(source))
                {
                    resizedImage = CreatePosterThumbnail(width, height, Image.FromStream(ms));
                }
            }
            else
            {
                resizedImage = new Bitmap(Properties.Resources.poster_not_found);
            }

            using (var graphics = Graphics.FromImage(resizedImage))
            {
                using (var brush = new SolidBrush(Color.FromArgb(200, 255, 250, 250)))
                {
                    //var pen = new Pen(brush);

                    using (GraphicsPath path = RoundedRect(new Rectangle(width - 30, -10, 40, audioSymbol != null ? 65 : 40), 5))
                    {
                        //graphics.DrawPath(pen, path);
                        graphics.FillPath(brush, path);
                    }

                    if (themeImage != null)
                    {
                        using (GraphicsPath path = RoundedRect(new Rectangle(-34, -34, 68, 68), 5))
                        {
                            //graphics.DrawPath(pen, path);
                            graphics.FillPath(brush, path);
                        }
                    }
                }


                if (themeImage != null)
                {
                    graphics.DrawImage(themeImage, new Point(0, 0));
                }


                //graphics.DrawEllipse(new Pen(Color.Red, 2), new Rectangle(0, 0, 20, 20));
                graphics.FillEllipse(new SolidBrush(Color.Red), new Rectangle(width - 25, 5, 21, 20));

                var recToPaint = rec.Replace("+", "").Replace("?", "");
                if (string.IsNullOrEmpty(recToPaint))
                    recToPaint = "?";

                var recPosition = new PointF(width - (recToPaint.Length == 1 ? 19 : 23), 7);
                var fontSize = recToPaint.Length == 1 ? 10 : 9;

                using (var arialFont = new Font("Arial", fontSize, FontStyle.Bold))
                {
                    graphics.DrawString(recToPaint, arialFont, Brushes.White, recPosition);
                }



                if (audioSymbol != null)
                {
                    graphics.DrawImage(audioSymbol, new Point(width - 25, 30));
                }


                ////using (var brush = new SolidBrush(Color.FromArgb(200, 255, 250, 250)))
                ////{
                ////    //graphics.FillRectangle(brush, width - 30, audioSymbol != null ? 50 : 25, 10, 15);
                ////    graphics.FillRectangle(brush, width - 30, 0, 30, 50);
                ////}

                //themeImage
            }

            using (var ms = new MemoryStream())
            {
                resizedImage.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }


        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc
            path.AddArc(arc, 180, 90);

            // top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}