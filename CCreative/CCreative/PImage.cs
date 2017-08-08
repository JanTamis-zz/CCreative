using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCreative.FastBitmapLib;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.Drawing;
using static CCreative.General;
using static CCreative.Data;
using CCreative;

namespace CCreative
{
    public class PImage
    {
        FastBitmap fastImage;
        Bitmap image;
        List<Color> Pixels = new List<Color>();

        public PImage(int width, int height)
        {
            image = new Bitmap(width, height);
        }

        public PImage(Bitmap img)
        {
            image = img;
        }

        public PImage(string location)
        {
            image = new Bitmap(location);
        }

        public void loadPixels()
        {
            fastImage = new FastBitmap(image);

            Pixels = new List<Color>();

            for (int x = 0; x < fastImage.Width; x++)
            {
                for (int y = 0; y < fastImage.Height; y++)
                {
                    Pixels.Add(fastImage.GetPixel(x, y));
                }
            }
        }

        public void set(int x, int y, Color color)
        {
            Pixels[y * image.Width + x] = color;
        }

        public Color get(int x, int y)
        {
            return Pixels[y * image.Width + x];
        }

        public Color[] pixels
        {
            get
            {
                return Pixels.ToArray();
            }
        }
        
        public void resize(int newWidth, int newHeight)
        {
            var destRect = new Rectangle(0, 0, image.Width, image.Height);
            var destImage = new Bitmap(image.Width, image.Height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            image = destImage;
        }

        public void filter(double tolerance)
        {
            constrain(tolerance, 0, 1);
            tolerance = map(tolerance, 0, 1, 1, 255);

            image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            for (int i = 0; i < fastImage.Width; i++)
            {
                for (int j = 0; j < fastImage.Height; j++)
                {
                    Color c = fastImage.GetPixel(i, j);

                    int average = ((c.R + c.B + c.G) / 3);

                    if (average < tolerance)
                        fastImage.SetPixel(i, j, Color.Black);

                    else
                        fastImage.SetPixel(i, j, Color.White);

                }
            }
        }
    }
}