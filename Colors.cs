using System.Security.RightsManagement;
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
        static Color clr = Color.Transparent;

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

        public static int Brightness(Color color)
        {
            return (color.R + color.G + color.B) / 3;
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

        public static Color Invert(Color toCompute)
        {
            return Color.FromArgb(toCompute.A, 255 - toCompute.R, 255 - toCompute.G, 255 - toCompute.B);
        }

        static int maxValue = 255;

        private static Color ColorFromAhsb(double H, double S, double B)
        {
            H = (H / maxValue) * 360;
            S /= maxValue;
            B /= maxValue;

            double r = 0, g = 0, b = 0;

            if (S == 0)
            {
                r = B;
                g = B;
                b = B;
            }
            else
            {
                int i;
                double f, p, q, t;

                if (H == 360)
                    H = 0;
                else
                    H = H / 60;

                i = floor(H);
                f = H - i;

                p = b * (1.0 - S);
                q = b * (1.0 - (S * f));
                t = b * (1.0 - (S * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        r = b;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = b;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = b;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = B;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = B;
                        break;

                    default:
                        r = b;
                        g = p;
                        b = q;
                        break;
                }

            }

            return Color.FromArgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
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
        /// <summary>   colorMode() changes the way CCreative interprets color data. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="modes">    The modes. </param>

        public static void colorMode(colorModes modes, int MaxValue)
        {
            colormode = modes;
            maxValue = MaxValue;
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

        public static Color lerpColor(Color start, Color stop, double atm)
        {
            atm = constrain(atm, 0, 1);

            return Color.FromArgb
                (
                     (int)(lerp(start.A, stop.A, atm)),
                     (int)(lerp(start.R, stop.R, atm)),
                     (int)(lerp(start.G, stop.G, atm)),
                     (int)(lerp(start.B, stop.B, atm))
                );
        }

        public static Color lerpColor(Color[] colors, double atm)
        {
            if (colors.Length > 1)
            {
                double scaledT = atm * (float)(colors.Length - 1);
                Color prevC = colors[(int)scaledT];
                Color nextC = colors[(int)(scaledT + 1f)];
                double newT = scaledT - (float)((int)scaledT);
                
                return lerpColor(prevC, nextC, newT); 
            }

            return colors[0];
        }

        private static int lerpInt(int start, int stop, double atm)
        {
            return (int)(start + (stop - start) * atm);
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

        public static Color color(double value)
        {
            switch (colorMode())
            {
                case colorModes.RGB:
                    int value1 = (int)constrain(value, 0, 255);
                    clr = Color.FromArgb(255, value1, value1, value1);
                    break;
                case colorModes.HSB:
                    value = (int)constrain(value, 0, maxValue);
                    clr = ColorFromAhsb(value, maxValue, maxValue / 2);
                    break;
                default:
                    break;
            }
            return clr;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates colors for storing in variables of the Color datatype </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="hex">    The hexadecimal color. </param>
        ///
        /// <returns>   A new Color. </returns>

        public static Color color(string hex)
        {
            if (hex[0] != '#')
            {
                return ColorTranslator.FromHtml("#" + hex);
            }

            return ColorTranslator.FromHtml(hex);
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
            switch (colorMode())
            {
                case colorModes.RGB:
                    value = (int)constrain(value, 0, 255);
                    clr = Color.FromArgb(transparancy, value, value, value);
                    break;
                case colorModes.HSB:
                    value = (int)constrain(value, 0, 360);
                    clr = ColorFromAhsb(value, 0.5f, 1);
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
            if (colorMode() == colorModes.HSB)
            {
                clr = ColorFromAhsb(value1, value2, value3);
            }
            else if (colorMode() == colorModes.RGB)
            {
                value1 = constrain(value1, 0, 255);
                value2 = constrain(value2, 0, 255);
                value3 = constrain(value3, 0, 255);
                clr = Color.FromArgb(round(value1), round(value2), round(value3));
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

        public static Color grayScale(Color toCalculate)
        {
            int b = (toCalculate.R + toCalculate.G + toCalculate.B) / 3;
            return Color.FromArgb(toCalculate.A, b, b, b);
        }
        
        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        
        public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            correctionFactor = constrain(correctionFactor, -1, 1);
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

    }
}
