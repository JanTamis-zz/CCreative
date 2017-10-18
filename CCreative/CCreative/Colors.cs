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
        static extern int ColorHLSToRGB(int H, int L, int S);
        static colorModes colormode = colorModes.RGB;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Extracts the alpha value from a color. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to check. </param>
        ///
        /// <returns>   The alpha component. </returns>

        public static byte alpha(Color color)
        {
            return color.A;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Extracts the red value from a color. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to check. </param>
        ///
        /// <returns>   The red component. </returns>

        public static byte red(Color color)
        {
            return color.R;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Extracts the green value from a color or pixel array. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to check. </param>
        ///
        /// <returns>   The green component. </returns>

        public static byte green(Color color)
        {
            return color.G;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Extracts the blue value from a color or pixel array. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to check. </param>
        ///
        /// <returns>   The blue component. </returns>

        public static byte blue(Color color)
        {
            return color.B;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Extracts the hue value from a color. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to check. </param>
        ///
        /// <returns>   The hue component. </returns>

        public static float hue(Color color)
        {
            return color.GetHue();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Extracts the saturation value from a color. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to check. </param>
        ///
        /// <returns>   The saturation component. </returns>

        public static float saturation(Color color)
        {
            return color.GetSaturation();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Extracts the HSB brightness value from a color. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to check. </param>
        ///
        /// <returns>   The brightness component. </returns>

        public static float brightness(Color color)
        {
            return color.GetBrightness();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random Color between a range. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="min">          The minimum value. </param>
        /// <param name="max">          The maximum value. </param>
        /// <param name="transparancy"> The transparancy of the color. </param>
        ///
        /// <returns>   The random color. </returns>

        public static Color randomColor(byte min, byte max, byte transparancy)
        {
            return Color.FromArgb(transparancy, randomInt(min, max + 1), randomInt(min, max + 1), randomInt(min, max + 1));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random Color between a range. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="min">  The minimum value. </param>
        /// <param name="max">  The maximum value. </param>
        ///
        /// <returns>   The random color. </returns>

        public static Color randomColor(byte min, byte max)
        {
            return randomColor(min, max, 255);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random Color between a range. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The random color. </returns>

        public static Color randomColor()
        {
            return randomColor(0, 255, 255);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random Color between a range. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="transparancy"> The transparancy of the color. </param>
        ///
        /// <returns>   The random color. </returns>

        public static Color randomColor(byte transparancy)
        {
            return randomColor(0, 255, transparancy);
        }

        private static Color ColorFromHSV(double h, double l, double s)
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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   colorMode() changes the way CCreative interprets color data. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="modes">    The modes. </param>

        public static void colorMode(colorModes modes)
        {
            colormode = modes;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   returns the mode CCreative interprets color date. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   the mode CCreative interprets color date. </returns>

        public static colorModes colorMode()
        {
            return colormode;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Inverts a color. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to invert. </param>
        ///
        /// <returns>   the inverted color. </returns>

        public static Color invertColor(Color color)
        {
            return Color.FromArgb(color.ToArgb() ^ 0xffffff);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Inverts a color. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The grayscale or hue component to invert. </param>
        ///
        /// <returns>   the inverted color. </returns>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Blends two colors to find a third color somewhere between them. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color1">   Interpolate from this color. </param>
        /// <param name="color2">   Interpolate to this color. </param>
        /// <param name="atm">      number between 0 and 1. </param>
        ///
        /// <returns>   A Color. </returns>

        public static Color lerpColor(Color color1, Color color2, double atm)
        {
            double R, G, B;
            R = lerp(color1.R, color2.R, atm);
            G = lerp(color1.G, color2.G, atm);
            B = lerp(color1.B, color2.B, atm);

            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        private static Color hueToColor(int hue)
        {
            hue = (int)map(hue, 0, 360, 0, 240);
            return ColorTranslator.FromWin32(ColorHLSToRGB(hue, 120, 240));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates colors for storing in variables of the Color datatype </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The grayscale or hue component. </param>
        ///
        /// <returns>   A new Color. </returns>

        public static Color color(int value)
        {
            Color clr = Color.Transparent;

            switch (colorMode())
            {
                case colorModes.RGB:
                    value = (int)constrain(value, 0, 255);
                    clr = Color.FromArgb(255, value, value, value);
                    break;
                case colorModes.HSB:
                    value = (int)constrain(value, 0, 360);
                    clr = ColorTranslator.FromWin32(ColorHLSToRGB((int)map(value, 0, 360, 0, 240), 120, 240));
                    break;
                default:
                    break;
            }
            return clr;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates colors for storing in variables of the Color datatype. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">        The grayscale or hue component. </param>
        /// <param name="transparancy"> The transparancy of the color. </param>
        ///
        /// <returns>   A new Color. </returns>

        public static Color color(int value, int transparancy)
        {
            Color clr = Color.Transparent;

            switch (colorMode())
            {
                case colorModes.RGB:
                    value = (int)constrain(value, 0, 255);
                    clr = Color.FromArgb(transparancy, value, value, value);
                    break;
                case colorModes.HSB:
                    value = (int)constrain(value, 0, 360);
                    clr = ColorTranslator.FromWin32(ColorHLSToRGB((int)map(value, 0, 360, 0, 240), 120, 240));
                    clr = Color.FromArgb(transparancy, clr);
                    break;
                default:
                    break;
            }
            clr = Color.FromArgb(transparancy, clr);
            return clr;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates colors for storing in variables of the Color datatype. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value1">   Red or hue value relative to the current color range. </param>
        /// <param name="value2">   Green or saturation value relative to the current color range. </param>
        /// <param name="value3">   Blue or brightness value relative to the current color range. </param>
        ///
        /// <returns>   A new Color. </returns>

        public static Color color(double value1, double value2, double value3)
        {
            Color clr = Color.Transparent;

            if (colorMode() == colorModes.HSB)
            {
                clr = ColorFromHSV(value1, value2, value3);
            }
            else if (colorMode() == colorModes.RGB)
            {
                value1 = value1 % 255;
                value2 = value2 % 255;
                value3 = value3 % 255;
                clr = Color.FromArgb((int)value1, (int)value2, (int)value3);
            }
            return clr;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates colors for storing in variables of the Color datatype. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value1">       Red or hue value relative to the current color range. </param>
        /// <param name="value2">       Green or saturation value relative to the current color range. </param>
        /// <param name="value3">       Blue or brightness value relative to the current color range. </param>
        /// <param name="transparancy"> The transparancy of the color. </param>
        ///
        /// <returns>   A new Color. </returns>

        public static Color color(int value1, int value2, int value3, int transparancy)
        {
            return Color.FromArgb(transparancy, color(value1, value2, value3));
        }
    }
}
