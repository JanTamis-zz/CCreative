using System.Windows.Forms;
using System.IO;
using NAudio.CoreAudioApi;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCreative.FastBitmapLib;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.General;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CCreative
{
    //public class PImage : IDisposable
    //{
    //    FastBitmap fast;

    //    public int width { get { return fast.Width; } }
    //    public int height { get { return fast.Height; } }
    //    public bool isLoaded { get; private set; }

    //    int textureid;
    //    public int textureID { get { return textureid; } }

    //    public BitmapData Data { get; private set; }

    //    public Color[] pixels { get; private set; }

    //    private FastBitmap restoreBMP;

    //    public PImage(string pathName, bool highQuality = true)
    //    {
    //        fast = new FastBitmap(new Bitmap(pathName));
    //        textureid = ContentPipe.loadTexture(fast, highQuality);

    //        isLoaded = true;
    //    }

    //    public PImage(Bitmap image, bool highQuality = true)
    //    {
    //        fast = new FastBitmap(new Bitmap(image));
    //        textureid = ContentPipe.loadTexture(fast, highQuality);

    //        isLoaded = true;
    //    }

    //    public PImage(int width, int height, bool highQuality = true)
    //    {
    //        fast = new FastBitmap(new Bitmap(width, height));
    //        textureid = ContentPipe.loadTexture(fast, highQuality);
    //        isLoaded = true;
    //    }

    //    /// <summary>
    //    /// Copies this image.
    //    /// </summary>
    //    /// <returns>this image.</returns>
    //    public PImage copy()
    //    {
    //        return this;
    //    }

    //    /// <summary>
    //    /// Applies a filter to this image. 
    //    /// </summary>
    //    /// <param name="type">The filter type.</param>
    //    public void filter(filterType type, double value)
    //    {
    //        fast.Lock();
    //        //loadPixels();
    //        switch (type)
    //        {
    //            case filterType.Gray:
    //                for (int i = 0; i < pixels.Length; i++)
    //                {
    //                    pixels[i] = grayScale(pixels[i]);
    //                }
    //                break;
    //            case filterType.Invert:
    //                for (int x = 0; x < width; x++)
    //                {
    //                    for (int y = 0; y < height; y++)
    //                    {
    //                        fast.SetPixel(x, y, invertColor(fast.GetPixel(x, y)));
    //                    }
    //                }
    //                break;
    //            case filterType.Threshold:

    //                for (int x = 0; x < width; x++)
    //                {
    //                    for (int y = 0; y < height; y++)
    //                    {
    //                        Color clr = fast.GetPixel(x, y);

    //                        if (clr.GetBrightness() < value)
    //                            fast.SetPixel(x, y, Color.FromArgb(clr.A, Color.Black));
    //                        else
    //                            fast.SetPixel(x, y, Color.FromArgb(clr.A, Color.White));
    //                    }
    //                }
    //                break;
    //            case filterType.Blur:
    //                Convolve(GetBoxFilter(floor(value)));
    //                break;
    //            case filterType.Pixelate:
    //                pixelate(floor(value));
    //                break;

    //            case filterType.Jitter:
    //                ApplyRandomJitter((short)value);
    //                break;

    //            case filterType.Brightness:
    //                value = map(constrain(value, 0, 1), 0, 1, -255, 255);
    //                for (int i = 0; i < pixels.Length; i++)
    //                {
    //                    Color temp = pixels[i];
    //                    double valR, valG, valB;

    //                    valR = temp.R + value;
    //                    if (valR < 0) valR = 0;
    //                    else if (valR > 255) valR = 255;

    //                    valG = temp.G + value;
    //                    if (valG < 0) valG = 0;
    //                    else if (valG > 255) valG = 255;

    //                    valB = temp.G + value;
    //                    if (valB < 0) valB = 0;
    //                    else if (valB > 255) valB = 255;

    //                    pixels[i] = Color.FromArgb(temp.A, (byte)valR, (byte)valG, (byte)valB);
    //                }

    //                break;
    //            case filterType.Posterize:
    //                posterize(floor(value));
    //                break;

    //            case filterType.Hue:
    //                for (int i = 0; i < pixels.Length; i++)
    //                {
    //                    Color clr = pixels[i];

    //                    if (clr.A > 0)
    //                    {
    //                        pixels[i] = ColorFromAhsb(clr.A, value % 360, clr.GetSaturation(), clr.GetBrightness());
    //                    }
    //                }
    //                break;

    //            case filterType.Satuation:
    //                for (int i = 0; i < pixels.Length; i++)
    //                {
    //                    Color clr = pixels[i];

    //                    if (clr.A > 0)
    //                    {
    //                        pixels[i] = ColorFromAhsb(clr.A, clr.GetHue(), value, clr.GetBrightness());
    //                    }
    //                }
    //                break;
    //        }
    //        fast.Unlock();
    //        //updatePixels();

    //        update();
    //    }

    //    public Color averageColor()
    //    {
    //        int a = 0, r = 0, g = 0, b = 0;

    //        int[] array = fast.DataArray;

    //        for (int i = 0; i < array.Length; i++)
    //        {
    //            int color = array[i];

    //            a += color >> 24 & 255;
    //            r += color >> 16 & 255;
    //            g += color >> 8 & 255;
    //            b += color >> 0 & 255;
    //        }

    //        return Color.FromArgb(a / array.Length, r / array.Length, g / array.Length, b / array.Length);
    //    }

    //    /// <summary>
    //    /// Applies a filter to this image. 
    //    /// </summary>
    //    /// <param name="type">The filter type.</param>
    //    public void filter(filterType type)
    //    {
    //        fast.Lock();
    //        switch (type)
    //        {
    //            case filterType.Gray:
    //                for (int x = 0; x < width; x++)
    //                {
    //                    for (int y = 0; y < height; y++)
    //                    {
    //                        fast.SetPixel(x, y, grayScale(fast.GetPixel(x, y)));
    //                    }
    //                }
    //                break;
    //            case filterType.Invert:
    //                for (int x = 0; x < width; x++)
    //                {
    //                    for (int y = 0; y < height; y++)
    //                    {
    //                        fast.SetPixel(x, y, invertColor(fast.GetPixel(x, y)));
    //                    }
    //                }
    //                break;
    //            case filterType.Threshold:
    //                for (int i = 0; i < pixels.Length; i++)
    //                {
    //                    Color clr = pixels[i];

    //                    if (clr.GetBrightness() < 0.5)
    //                        pixels[i] = Color.FromArgb(clr.A, Color.Black);
    //                    else
    //                        pixels[i] = Color.FromArgb(clr.A, Color.White);
    //                }
    //                break;
    //            case filterType.Blur:
    //                Convolve(GetBoxFilter(3));
    //                break;
    //            case filterType.Pixelate:
    //                pixelate(5);
    //                break;
    //            case filterType.Sepia:
    //                Sepia();
    //                break;
    //            case filterType.Opaque:
    //                for (int i = 0; i < pixels.Length; i++)
    //                {
    //                    pixels[i] = Color.FromArgb(255, pixels[i]);
    //                }
    //                break;
    //            case filterType.Posterize:
    //                posterize(4);
    //                break;
    //            case filterType.Red:
    //                for (int i = 0; i < pixels.Length; i++)
    //                {
    //                    Color clr = pixels[i];

    //                    pixels[i] = Color.FromArgb(clr.A, clr.R, 0, 0);
    //                }
    //                break;
    //            case filterType.Green:
    //                for (int x = 0; x < width; x++)
    //                {
    //                    for (int y = 0; y < height; y++)
    //                    {
    //                        Color clr = fast.GetPixel(x, y);

    //                        fast.SetPixel(x, y, Color.FromArgb(clr.A, 0, 255, 0));
    //                    }
    //                }
    //                break;
    //            case filterType.Blue:
    //                for (int i = 0; i < pixels.Length; i++)
    //                {
    //                    Color clr = pixels[i];

    //                    pixels[i] = Color.FromArgb(clr.A, 0, 0, clr.B);
    //                }
    //                break;
    //        }
    //        fast.Unlock();

    //        update();
    //    }

    //    private float[,] GetBoxFilter(int size)
    //    {
    //        float[,] filter = new float[size, size];
    //        float constant = size * size;

    //        for (int i = 0; i < filter.GetLength(0); i++)
    //        {
    //            for (int j = 0; j < filter.GetLength(1); j++)
    //            {
    //                filter[i, j] = 1.0f / constant;
    //            }
    //        }

    //        return filter;
    //    }

    //    private void Convolve(float[,] filter)
    //    {
    //        //Find center of filter
    //        int xMiddle = floor(filter.GetLength(0) / 2.0);
    //        int yMiddle = floor(filter.GetLength(1) / 2.0);

    //        //Create new image

    //        for (int x = 0; x < width; x++)
    //        {
    //            for (int y = 0; y < height; y++)
    //            {
    //                float a = 0;
    //                float r = 0;
    //                float g = 0;
    //                float b = 0;

    //                //Apply filter
    //                for (int xFilter = 0; xFilter < filter.GetLength(0); xFilter++)
    //                {
    //                    for (int yFilter = 0; yFilter < filter.GetLength(1); yFilter++)
    //                    {
    //                        int x0 = x - xMiddle + xFilter;
    //                        int y0 = y - yMiddle + yFilter;

    //                        //Only if in bounds
    //                        if (x0 >= 0 && x0 < width &&
    //                            y0 >= 0 && y0 < height)
    //                        {
    //                            Color clr = fast.GetPixel(x0, y0);

    //                            a += clr.A * filter[xFilter, yFilter];
    //                            r += clr.R * filter[xFilter, yFilter];
    //                            g += clr.G * filter[xFilter, yFilter];
    //                            b += clr.B * filter[xFilter, yFilter];
    //                        }
    //                    }
    //                }

    //                //Normalize (basic)
    //                if (r > 255)
    //                    r = 255;
    //                if (g > 255)
    //                    g = 255;
    //                if (b > 255)
    //                    b = 255;

    //                if (r < 0)
    //                    r = 0;
    //                if (g < 0)
    //                    g = 0;
    //                if (b < 0)
    //                    b = 0;

    //                //Set the pixel
    //                fast.SetPixel(x, y, Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b));
    //            }
    //        }
    //    }

    //    private void pixelate(int pixelateSize)
    //    {
    //        for (Int32 xx = 0; xx < 0 + width && xx < width; xx += pixelateSize)
    //        {
    //            for (Int32 yy = 0; yy < 0 + height && yy < height; yy += pixelateSize)
    //            {
    //                Int32 offsetX = pixelateSize / 2;
    //                Int32 offsetY = pixelateSize / 2;

    //                // make sure that the offset is within the boundry of the image
    //                while (xx + offsetX >= width) offsetX--;
    //                while (yy + offsetY >= height) offsetY--;

    //                // get the pixel color in the center of the soon to be pixelated area
    //                Color pixel = fast.GetPixel((xx + offsetX), (yy + offsetY));

    //                // for each pixel in the pixelate size, set it to the center color
    //                for (int x = xx; x < xx + pixelateSize && x < width; x++)
    //                    for (int y = yy; y < yy + pixelateSize && y < height; y++)
    //                        fast.SetPixel(x, y, pixel);
    //            }
    //        }
    //    }

    //    private void posterize(int level)
    //    {
    //        //www.qtcentre.org/threads/36385-Posterizes-an-image-with-results-identical-to-Gimp-s-Posterize-command

    //        Color val;
    //        double levels = level; // posterize value 5.0
    //        levels--; // by following gimp code
    //        //int newRed, newGreen, newBlue;

    //        double sr, sg, sb;
    //        int dr, dg, db;

    //        for (int x = 0; x < width; x++)
    //        {
    //            for (int y = 0; y < height; y++)
    //            {
    //                val = fast.GetPixel(x, y);
    //                sr = val.R / 255.0;
    //                sg = val.G / 255.0;
    //                sb = val.B / 255.0; // to make sr, sg, sb between 0 and 1
    //                dr = floor(255 * round(sr * levels) / levels);
    //                dg = floor(255 * round(sg * levels) / levels);
    //                db = floor(255 * round(sb * levels) / levels); // qRound does the same as RINT (rounding and NOT TRUNCATING its argument).
    //                fast.SetPixel(x, y, Color.FromArgb(val.A, dr, dg, db));
    //            }
    //        }
    //    }

    //    private void Sepia()
    //    {
    //        int Pixel = 0;

    //        for (int x = 0; x < width; x++)
    //        {
    //            for (int y = 0; y < height; y++)
    //            {
    //                Color p = fast.GetPixel(x, y);

    //                int a = p.A;
    //                int r = p.R;
    //                int g = p.G;
    //                int b = p.B;

    //                //calculate temp value
    //                int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
    //                int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
    //                int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

    //                //set new RGB value
    //                if (tr > 255)
    //                    r = 255;
    //                else
    //                    r = tr;

    //                if (tg > 255)
    //                    g = 255;
    //                else
    //                    g = tg;

    //                if (tb > 255)
    //                    b = 255;
    //                else
    //                    b = tb;

    //                fast.SetPixel(x, y, Color.FromArgb(a, r, g, b));
    //            }
    //        }
    //    }

    //    private void ApplyRandomJitter(short degree)
    //    {
    //        loadPixels();

    //        Color[] temp = pixels;

    //        int BmpWidth = width;
    //        int BmpHeight = height;
    //        int i = 0, X = 0, Y = 0;
    //        int Val = 0, XVal = 0, YVal = 0;
    //        short Half = (short)(degree / 2);

    //        for (int j = 0; j < pixels.Length; j++)
    //        {
    //            X = j % BmpWidth;
    //            Y = j / BmpWidth;

    //            XVal = X + (randomInt(degree) - Half);
    //            YVal = Y + (randomInt(degree) - Half);

    //            if (XVal > 0 && XVal < BmpWidth && YVal > 0 && YVal < BmpHeight)
    //            {
    //                Val = (YVal * width) + (XVal);

    //                pixels[Y * width + X] = temp[Val];
    //            }
    //        }

    //        updatePixels();
    //    }


    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary>   Loads the from the image to the pixels array. </summary>
    //    ///
    //    /// <remarks>   Jan Tamis, 7-11-2017. </remarks>
    //    /// 
    //    public void loadPixels()
    //    {
    //        pixels = Array.ConvertAll(fast.DataArray, (p => Color.FromArgb(p)));
    //    }

    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary>   Updates the pixels. </summary>
    //    ///
    //    /// <remarks>   Jan Tamis, 7-11-2017. </remarks>

    //    public void updatePixels()
    //    {
    //        fast.CopyFromArray(Array.ConvertAll(pixels, (p => p.ToArgb())));

    //        update();
    //    }

    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary>   Clears the bitmap. </summary>
    //    /// 
    //    ///  <param name="color">The clear color.</param>
    //    ///
    //    /// <remarks>   Jan Tamis, 7-11-2017. </remarks>

    //    public void Clear(Color color)
    //    {
    //        fast.Clear(color);

    //        update();
    //    }

    //    private void update()
    //    {
    //        GL.BindTexture(TextureTarget.Texture2D, textureID);
    //        GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, fast.Scan0);
    //    }

    //    public void Restore()
    //    {
    //        fast = restoreBMP;

    //        update();
    //    }

    //    private static Color ColorFromAhsb(byte a, double h, double s, double b)
    //    {
    //        if (0 == s)
    //        {
    //            return Color.FromArgb(255, Convert.ToInt32(b * 255),
    //              Convert.ToInt32(b * 255), Convert.ToInt32(b * 255));
    //        }

    //        double fMax, fMid, fMin;
    //        int iSextant, iMax, iMid, iMin;

    //        if (0.5 < b)
    //        {
    //            fMax = b - (b * s) + s;
    //            fMin = b + (b * s) - s;
    //        }
    //        else
    //        {
    //            fMax = b + (b * s);
    //            fMin = b - (b * s);
    //        }

    //        iSextant = floor(h / 60f);
    //        if (300f <= h)
    //        {
    //            h -= 360f;
    //        }
    //        h /= 60f;
    //        h -= 2f * floor(((iSextant + 1f) % 6f) / 2f);
    //        if (0 == iSextant % 2)
    //        {
    //            fMid = h * (fMax - fMin) + fMin;
    //        }
    //        else
    //        {
    //            fMid = fMin - h * (fMax - fMin);
    //        }

    //        iMax = Convert.ToInt32(fMax * 255);
    //        iMid = Convert.ToInt32(fMid * 255);
    //        iMin = Convert.ToInt32(fMin * 255);

    //        switch (iSextant)
    //        {
    //            case 1:
    //                return Color.FromArgb(a, iMid, iMax, iMin);
    //            case 2:
    //                return Color.FromArgb(a, iMin, iMax, iMid);
    //            case 3:
    //                return Color.FromArgb(a, iMin, iMid, iMax);
    //            case 4:
    //                return Color.FromArgb(a, iMid, iMin, iMax);
    //            case 5:
    //                return Color.FromArgb(a, iMax, iMin, iMid);
    //            default:
    //                return Color.FromArgb(a, iMax, iMid, iMin);
    //        }
    //    }

    //    void BoxBlurVertical(int range)
    //    {
    //        int[] pixels = fast.DataArray;
    //        int w = width;
    //        int h = height;
    //        int halfRange = range / 2;

    //        int[] newColors = new int[h];
    //        int oldPixelOffset = -(halfRange + 1) * w;
    //        int newPixelOffset = (halfRange) * w;

    //        for (int x = 0; x < w; x++)
    //        {
    //            int hits = 0;
    //            int a = 0;
    //            int r = 0;
    //            int g = 0;
    //            int b = 0;
    //            int index = -halfRange * w + x;
    //            for (int y = -halfRange; y < h; y++)
    //            {
    //                int oldPixel = y - halfRange - 1;
    //                if (oldPixel >= 0)
    //                {
    //                    int col = pixels[index + oldPixelOffset];
    //                    if (col != 0)
    //                    {
    //                        a -= ((byte)(col >> 24));
    //                        r -= ((byte)(col >> 16));
    //                        g -= ((byte)(col >> 8));
    //                        b -= ((byte)col);
    //                    }
    //                    hits--;
    //                }

    //                int newPixel = y + halfRange;
    //                if (newPixel < h)
    //                {
    //                    int col = pixels[index + newPixelOffset];
    //                    if (col != 0)
    //                    {
    //                        a += ((byte)(col >> 24));
    //                        r += ((byte)(col >> 16));
    //                        g += ((byte)(col >> 8));
    //                        b += ((byte)col);
    //                    }
    //                    hits++;
    //                }

    //                if (y >= 0)
    //                {
    //                    int color =
    //                        ((byte)(a / hits) << 24)
    //                        | ((byte)(r / hits) << 16)
    //                        | ((byte)(g / hits) << 8)
    //                        | ((byte)(b / hits));

    //                    newColors[y] = color;
    //                }

    //                index += w;
    //            }

    //            for (int y = 0; y < h; y++)
    //            {
    //                pixels[y * w + x] = newColors[y];
    //            }

    //            fast.CopyFromArray(pixels);
    //        }
    //    }

    //    void BoxBlurHorizontal(int range)
    //    {
    //        int[] pixels = fast.DataArray;
    //        int w = width;
    //        int h = height;
    //        int halfRange = range / 2;
    //        int index = 0;
    //        int[] newColors = new int[w];

    //        for (int y = 0; y < h; y++)
    //        {
    //            int hits = 0;
    //            int a = 0;
    //            int r = 0;
    //            int g = 0;
    //            int b = 0;
    //            for (int x = -halfRange; x < w; x++)
    //            {
    //                int oldPixel = x - halfRange - 1;
    //                if (oldPixel >= 0)
    //                {
    //                    int col = pixels[index + oldPixel];
    //                    if (col != 0)
    //                    {
    //                        a -= ((byte)(col >> 24));
    //                        r -= ((byte)(col >> 16));
    //                        g -= ((byte)(col >> 8));
    //                        b -= ((byte)col);
    //                    }
    //                    hits--;
    //                }

    //                int newPixel = x + halfRange;
    //                if (newPixel < w)
    //                {
    //                    int col = pixels[index + newPixel];
    //                    if (col != 0)
    //                    {
    //                        a += ((byte)(col >> 24));
    //                        r += ((byte)(col >> 16));
    //                        g += ((byte)(col >> 8));
    //                        b += ((byte)col);
    //                    }
    //                    hits++;
    //                }

    //                if (x >= 0)
    //                {
    //                    int color =
    //                         ((byte)(a / hits) << 24)
    //                        | ((byte)(r / hits) << 16)
    //                        | ((byte)(g / hits) << 8)
    //                        | ((byte)(b / hits));

    //                    newColors[x] = color;
    //                }
    //            }

    //            for (int x = 0; x < w; x++)
    //            {
    //                pixels[index + x] = newColors[x];
    //            }

    //            index += w;
    //        }
    //        fast.CopyFromArray(pixels);
    //    }

    //    void BoxBlur(int range)
    //    {
    //        //if ((range & 1) == 0)
    //        //{
    //        //    range++;
    //        //}

    //        BoxBlurHorizontal(range);
    //        BoxBlurVertical(range);
    //    }

    //    private bool _disposed;

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!_disposed)
    //        {
    //            if (disposing)
    //            {
    //                GL.DeleteBuffers(1, ref textureid);
    //            }

    //            _disposed = true;
    //        }
    //    }
    //}

    public class PImage
    {
        Bitmap fast;

        public int width { get { return fast.Width; } }
        public int height { get { return fast.Height; } }
        public bool isLoaded { get; private set; }

        int textureid;
        public int textureID { get { return textureid; } }
        public int bytesPerPixel { get; private set; }

        public BitmapData Data { get; private set; }

        public byte[] pixels { get; private set; }

        private FastBitmap restoreBMP;
        private bool _disposed;

        public PImage(string pathName, bool highQuality = true)
        {
            fast = new Bitmap(pathName);
            textureid = ContentPipe.loadTexture(fast, highQuality);

            Data = fast.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, fast.PixelFormat);
            fast.UnlockBits(Data);

            bytesPerPixel = Image.GetPixelFormatSize(Data.PixelFormat) / 8;
            pixels = new byte[width * height * bytesPerPixel];

            isLoaded = true;
        }

        public PImage(Bitmap image, bool highQuality = true)
        {
            fast = new Bitmap(image);
            textureid = ContentPipe.loadTexture(fast, highQuality);

            Data = fast.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, fast.PixelFormat);
            fast.UnlockBits(Data);

            bytesPerPixel = Image.GetPixelFormatSize(Data.PixelFormat) / 8;
            pixels = new byte[width * height * bytesPerPixel];

            isLoaded = true;
        }

        public PImage(int width, int height, bool highQuality = true)
        {
            fast = new Bitmap(width, height);
            textureid = ContentPipe.loadTexture(fast, highQuality);

            Data = fast.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, fast.PixelFormat);
            fast.UnlockBits(Data);

            bytesPerPixel = Image.GetPixelFormatSize(Data.PixelFormat) / 8;
            pixels = new byte[width * height * bytesPerPixel];

            isLoaded = true;
        }

        public void loadPixels()
        {
            Marshal.Copy(Data.Scan0, pixels, 0, width * height * bytesPerPixel);
        }

        public void updatePixels()
        {
            Marshal.Copy(pixels, 0, Data.Scan0, pixels.Length);

            update();
        }

        private void update()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, Data.Scan0);
        }

        public Color averageColor()
        {
            int r = 0, g = 0, b = 0;
            int pixelcount = width * height;

            unsafe
            {
                byte* ptr = (byte*)Data.Scan0.ToPointer();
                int stopAddress = (int)ptr + Data.Stride * height;

                while ((int)ptr != stopAddress)
                {

                    r += ptr[2];
                    g += ptr[1];
                    b += ptr[0];

                    ptr += bytesPerPixel;
                }
            }


            return Color.FromArgb(r / pixelcount, g / pixelcount, b / pixelcount);
        }

        public void grayscale()
        {
            unsafe
            {
                byte* ptr = (byte*)Data.Scan0.ToPointer();
                int stopAddress = (int)ptr + Data.Stride * height;

                while ((int)ptr != stopAddress)
                {
                    byte average = (byte)((ptr[2] + ptr[1] + ptr[0]) / 3);

                    ptr[2] = average;
                    ptr[1] = average;
                    ptr[0] = average;

                    ptr += bytesPerPixel;
                }
            }
            update();
        }

        public void Set(int x, int y, byte a, byte r, byte g, byte b)
        {
            int idx = x * bytesPerPixel + y * Data.Stride;

            pixels[idx + 3] = a;
            pixels[idx + 2] = r;
            pixels[idx + 1] = g;
            pixels[idx + 0] = b;
        }

        public void invert()
        {
            unsafe
            {
                byte* ptr = (byte*)Data.Scan0.ToPointer();
                int stopAddress = (int)ptr + Data.Stride * height;

                while ((int)ptr != stopAddress)
                {

                    ptr[2] = (byte)(ptr[2] ^ 255);
                    ptr[1] = (byte)(ptr[1] ^ 255);
                    ptr[0] = (byte)(ptr[0] ^ 255);

                    ptr += bytesPerPixel;
                }
            }
            update();
        }

        public float[,] GetBoxFilter(int size)
        {
            float[,] filter = new float[size, size];
            float constant = size * size;

            for (int i = 0; i < filter.GetLength(0); i++)
            {
                for (int j = 0; j < filter.GetLength(1); j++)
                {
                    filter[i, j] = 1.0f / constant;
                }
            }

            return filter;
        }

        public void Convolve(float[,] filter)
        {
            loadPixels();

            int heightLimit = height * Data.Stride;
            int widthLimit = width * bytesPerPixel;

            //Find center of filter
            int xMiddle = floor(filter.GetLength(0) / 2.0);
            int yMiddle = floor(filter.GetLength(1) / 2.0);

            //Create new image

            for (int x = 0; x < widthLimit; x += bytesPerPixel)
            {
                for (int y = 0; y < heightLimit; y += Data.Stride)
                {
                    float a = 0;
                    float r = 0;
                    float g = 0;
                    float b = 0;

                    //Apply filter
                    for (int xFilter = 0; xFilter < filter.GetLength(0); xFilter++)
                    {
                        for (int yFilter = 0; yFilter < filter.GetLength(1); yFilter++)
                        {
                            int x0 = x - xMiddle + xFilter;
                            int y0 = y - yMiddle + yFilter;

                            //Only if in bounds
                            if (x0 >= 0 && x0 < widthLimit &&
                                y0 >= 0 && y0 < heightLimit)
                            {
                                a += pixels[y + x + 3] * filter[xFilter, yFilter];
                                r += pixels[y + x + 2] * filter[xFilter, yFilter];
                                g += pixels[y + x + 1] * filter[xFilter, yFilter];
                                b += pixels[y + x] * filter[xFilter, yFilter];
                            }
                        }
                    }

                    //Normalize (basic)
                    r = constrain(r, 0, 255);
                    g = constrain(g, 0, 255);
                    b = constrain(b, 0, 255);

                    pixels[y + x + 2] = (byte)r;
                    pixels[y + x + 1] = (byte)g;
                    pixels[y + x + 0] = (byte)b;
                    pixels[y + x + 3] = (byte)a;
                }
            }
            updatePixels();
        }

        public void Blur(int blurSize)
        {
            ApplyGaussianBlur(ref fast, blurSize);
            update();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    GL.DeleteBuffers(1, ref textureid);
                }

                _disposed = true;
            }
        }

        class ConvolutionMatrix
        {
            public ConvolutionMatrix()
            {
                Pixel = 1;
                Factor = 1;
            }

            public void Apply(int Val)
            {
                TopLeft = TopMid = TopRight = MidLeft = MidRight = BottomLeft = BottomMid = BottomRight = Pixel = Val;
            }

            public int TopLeft { get; set; }

            public int TopMid { get; set; }

            public int TopRight { get; set; }

            public int MidLeft { get; set; }

            public int MidRight { get; set; }

            public int BottomLeft { get; set; }

            public int BottomMid { get; set; }

            public int BottomRight { get; set; }

            public int Pixel { get; set; }

            public int Factor { get; set; }

            public int Offset { get; set; }
        }

        class Convolution
        {
            public void Convolution3x3(ref Bitmap bmp, int bytes)
            {
                int Factor = Matrix.Factor;

                if (Factor == 0) return;

                int TopLeft = Matrix.TopLeft;
                int TopMid = Matrix.TopMid;
                int TopRight = Matrix.TopRight;
                int MidLeft = Matrix.MidLeft;
                int MidRight = Matrix.MidRight;
                int BottomLeft = Matrix.BottomLeft;
                int BottomMid = Matrix.BottomMid;
                int BottomRight = Matrix.BottomRight;
                int Pixel = Matrix.Pixel;
                int Offset = Matrix.Offset;

                Bitmap TempBmp = (Bitmap)bmp.Clone();

                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                BitmapData TempBmpData = TempBmp.LockBits(new Rectangle(0, 0, TempBmp.Width, TempBmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

                unsafe
                {
                    byte* ptr = (byte*)bmpData.Scan0.ToPointer();
                    byte* TempPtr = (byte*)TempBmpData.Scan0.ToPointer();

                    int Pix = 0;
                    int Stride = bmpData.Stride;
                    int DoubleStride = Stride * 2;
                    int Width = bmp.Width - 2;
                    int Height = bmp.Height - 2;
                    int stopAddress = (int)ptr + bmpData.Stride * bmpData.Height;

                    for (int y = 0; y < Height; ++y)
                        for (int x = 0; x < Width; ++x)
                        {
                            Pix = (((((TempPtr[2] * TopLeft) + (TempPtr[5] * TopMid) + (TempPtr[8] * TopRight)) +
                              ((TempPtr[2 + Stride] * MidLeft) + (TempPtr[5 + Stride] * Pixel) + (TempPtr[8 + Stride] * MidRight)) +
                              ((TempPtr[2 + DoubleStride] * BottomLeft) + (TempPtr[5 + DoubleStride] * BottomMid) + (TempPtr[8 + DoubleStride] * BottomRight))) / Factor) + Offset);

                            if (Pix < 0) Pix = 0;
                            else if (Pix > 255) Pix = 255;

                            ptr[5 + Stride] = (byte)Pix;

                            Pix = (((((TempPtr[1] * TopLeft) + (TempPtr[4] * TopMid) + (TempPtr[7] * TopRight)) +
                                  ((TempPtr[1 + Stride] * MidLeft) + (TempPtr[4 + Stride] * Pixel) + (TempPtr[7 + Stride] * MidRight)) +
                                  ((TempPtr[1 + DoubleStride] * BottomLeft) + (TempPtr[4 + DoubleStride] * BottomMid) + (TempPtr[7 + DoubleStride] * BottomRight))) / Factor) + Offset);

                            if (Pix < 0) Pix = 0;
                            else if (Pix > 255) Pix = 255;

                            ptr[4 + Stride] = (byte)Pix;

                            Pix = (((((TempPtr[0] * TopLeft) + (TempPtr[3] * TopMid) + (TempPtr[6] * TopRight)) +
                                  ((TempPtr[0 + Stride] * MidLeft) + (TempPtr[3 + Stride] * Pixel) + (TempPtr[6 + Stride] * MidRight)) +
                                  ((TempPtr[0 + DoubleStride] * BottomLeft) + (TempPtr[3 + DoubleStride] * BottomMid) + (TempPtr[6 + DoubleStride] * BottomRight))) / Factor) + Offset);

                            if (Pix < 0) Pix = 0;
                            else if (Pix > 255) Pix = 255;

                            ptr[bytes + Stride] = (byte)Pix;

                            ptr += bytes;
                            TempPtr += bytes;
                        }
                }

                bmp.UnlockBits(bmpData);
                TempBmp.UnlockBits(TempBmpData);
            }

            public ConvolutionMatrix Matrix { get; set; }
        }

        Convolution C = new Convolution();
        ConvolutionMatrix m = new ConvolutionMatrix();

        public void ApplyGaussianBlur(ref Bitmap bmp, int Weight)
        {
            m.Apply(1);
            m.Pixel = Weight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = Weight + 12;

            
            C.Matrix = m;
            C.Convolution3x3(ref bmp, bytesPerPixel);
        }
    }
}