using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CCreative.Data;
using static CCreative.General;
using static CCreative.Math;
using static CCreative.Drawing;
using System.Runtime.InteropServices;

namespace CCreative
{
    public static class Colors
    {
        [DllImport("shlwapi.dll")]
        public static extern int ColorHLSToRGB(int H, int L, int S);
        ///<summary>
        ///Returns the alpha component of a Color.
        ///</summary>
        public static byte alpha(Color color)
        {
            return color.A;
        }

        ///<summary>
        ///Returns the red component of a Color.
        ///</summary>
        public static byte red(Color color)
        {
            return color.R;
        }

        ///<summary>
        ///Returns the green component of a Color.
        ///</summary>
        public static byte green(Color color)
        {
            return color.G;
        }

        ///<summary>
        ///Returns the blue component of a Color.
        ///</summary>
        public static byte blue(Color color)
        {
            return color.B;
        }

        ///<summary>
        ///Returns the hue of a Color.
        ///</summary>
        public static float hue(Color color)
        {
            return color.GetHue();
        }

        ///<summary>
        ///Returns the saturation of a Color.
        ///</summary>
        public static float saturation(Color color)
        {
            return color.GetSaturation();
        }

        ///<summary>
        ///Returns the brightness/lightness of a Color.
        ///</summary>
        public static float brightness(Color color)
        {
            return color.GetBrightness();
        }

        ///<summary>
        ///Returns a random Color between a range.
        ///</summary>
        public static Color randomColor(int min = 0, int max = 255)
        {
            max++;

            min = (int)constrain(min, 0, 256);
            max = (int)constrain(max, 0, 255);

            if (max < min)
                max = min + 1;
            if (max > 256)
                max = 0;
            else if (max > 256)
                max = 256;

            Random rand = new Random();
            return Color.FromArgb(255, rand.Next(min, max), rand.Next(min, max), rand.Next(min, max));
        }

        public static Color ColorFromHSV(double h, double l, double s)
        {
            h = h % 360;
            double p2;
            if (l <= 0.5) p2 = l * (1 + s);
            else p2 = l + s - l * s;

            double p1 = 2 * l - p2;
            double double_r, double_g, double_b;
            if (s == 0)
            {
                double_r = l;
                double_g = l;
                double_b = l;
            }
            else
            {
                double_r = QqhToRgb(p1, p2, h + 120);
                double_g = QqhToRgb(p1, p2, h);
                double_b = QqhToRgb(p1, p2, h - 120);
            }

            // Convert RGB to the 0 to 255 range.
            int r = (int)(double_r * 255.0);
            int g = (int)(double_g * 255.0);
            int b = (int)(double_b * 255.0);
            return Color.FromArgb(r, g, b);
        }

        private static double QqhToRgb(double q1, double q2, double hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }

        ///<summary>
        ///The possible Color modes
        ///</summary>
        public enum colorModes
        {
            RGB,
            HSB
        }

        ///<summary>
        ///Sets the Color mode
        ///</summary>
        public static void colorMode(colorModes modes)
        {
            Drawing.colormode = modes;
        }

        public static Color invertColor(Color color)
        {
            return Color.FromArgb(color.ToArgb() ^ 0xffffff);
        }

        public static Color invertColor(int value)
        {
            Color clr = Color.Transparent;
            if (colormode == colorModes.RGB)
            {
                value = (int)constrain(value, 0, 255);
                clr = Color.FromArgb(255 - value, 255 - value, 255 - value);
            }
            else if (colormode == colorModes.HSB)
            {
                clr = ColorTranslator.FromWin32(ColorHLSToRGB(value, 120, 240));
                clr = invertColor(clr);
            }
            return clr;
        }

        public static Color lerpColor(Color color1, Color color2, double atm)
        {
            double R, G, B;
            R = lerp(color1.R, color2.R, atm);
            G = lerp(color1.G, color2.G, atm);
            B = lerp(color1.B, color2.B, atm);

            return Color.FromArgb((int)R, (int)G, (int)B);
        }
    }
}
