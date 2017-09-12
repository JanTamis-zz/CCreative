using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CCreative.General;
using static CCreative.Drawing;
using System.Windows;

namespace CCreative
{
    public static class Math
    {
        static Random rand = new Random();

        static Perlin Noise = new Perlin();
        //static OpenSimplexNoise Noise = new OpenSimplexNoise((long)random(long.MaxValue))

        public static readonly float HALF_PI = (float)(System.Math.PI * 0.5);
        public static readonly float PI = (float)System.Math.PI;
        public static readonly float QUARTER_PI = (float)(System.Math.PI * 0.25);
        public static readonly float TAU = (float)(System.Math.PI * 2);
        public static readonly float TWO_PI = (float)(System.Math.PI * 2);
        public static readonly float Infinity = float.PositiveInfinity;

        #region Calculations

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the absolute value (magnitude) of a number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Number to compute. </param>
        ///
        /// <returns>   The absolute value. </returns>

        public static float abs(double number)
        {
            return (float)System.Math.Abs(number);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the closest int value that is greater than or equal to the value of the parameter. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Number to round up. </param>
        ///
        /// <returns>   The ceiling. </returns>

        public static float ceil(double number)
        {
            return (float)System.Math.Ceiling((decimal)number);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constrains a value between a minimum and maximum value </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Number to constrain. </param>
        /// <param name="low">      Minimum limit. </param>
        /// <param name="high">     Maximum limit. </param>
        ///
        /// <returns>   The constrain value. </returns>

        public static float constrain(double number, double low, double high)
        {
            if (number > high) { number = high; }
            else if (number < low) { number = low; }
            return (float)number;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the distance between two points </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="beginPoint">   The begin point. </param>
        /// <param name="endPoint">     The end point. </param>
        ///
        /// <returns>   The distance. </returns>

        public static float dist(PointF beginPoint, PointF endPoint)
        {
            return dist(beginPoint.X, beginPoint.Y, endPoint.X, endPoint.Y);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the distance between two points </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="beginPoint">   The begin point. </param>
        /// <param name="endPoint">     The end point. </param>
        ///
        /// <returns>   The distance. </returns>

        public static float dist(PointF[] points)
        {
            double d = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                d += dist(points[i], points[i + 1]);
            }
            return (float)d;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the distance between two points. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="beginX">   x-coordinate of the first point. </param>
        /// <param name="beginY">   y-coordinate of the first point. </param>
        /// <param name="endX">     x-coordinate of the second point. </param>
        /// <param name="endY">     y-coordinate of the second point. </param>
        ///
        /// <returns>   The distance. </returns>

        public static float dist(double beginX, double beginY, double endX, double endY)
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(endX - beginX, 2) + System.Math.Pow(endY - beginY, 2)));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns Euler's number e (2.71828...) raised to the power of the number parameter. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Exponent to raise. </param>
        ///
        /// <returns>   The exponent value. </returns>

        public static float exp(double number)
        {
            return (float)(System.Math.Pow(System.Math.E, number));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the closest int value that is less than or equal to the value of the parameter. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Number to round down. </param>
        ///
        /// <returns>   The rounded number. </returns>

        public static int floor(double number)
        {
            return (int)(System.Math.Floor(number));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates a number between two numbers at a specific increment. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="start">    first value. </param>
        /// <param name="stop">     second value. </param>
        /// <param name="atm">      number between 0.0 and 1.0. </param>
        ///
        /// <returns>   The value. </returns>

        public static float lerp(double start, double stop, double atm)
        {
            if (atm > 1) { atm = 1; }
            else if (atm < 0) { atm = 0; }

            return (float)((1 - atm) * start + atm * stop);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the natural logarithm (the base-e logarithm) of a number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   number greater than 0. </param>
        ///
        /// <returns>   The value. </returns>

        public static float log(double number)
        {
            return (float)(System.Math.Log(number));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the magnitude (or length) of a vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="numberX">  The x coordinate. </param>
        /// <param name="numberY">  The y coordinate. </param>
        ///
        /// <returns>   The lenght. </returns>

        public static float mag(double numberX, double numberY)
        {
            Vector vec = new Vector(numberX, numberY);
            return (float)vec.Length;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Re-maps a number from one range to another.  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">        The incoming value to be converted. </param>
        /// <param name="least">        Lower bound of the value's current range. </param>
        /// <param name="max">          Upper bound of the value's current range. </param>
        /// <param name="toMinimum">    Lower bound of the value's target range. </param>
        /// <param name="toMaximum">    Upper bound of the value's target range. </param>
        ///
        /// <returns>   The re-maped value. </returns>

        public static float map(double value, double least, double max, double toMinimum, double toMaximum)
        {
            return (float)(toMinimum + (value - least) * (toMaximum - toMinimum) / (max - least));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Determines the largest value in a sequence of numbers, and then returns that value. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="numbers">  The array to check. </param>
        ///
        /// <returns>   The maximum value. </returns>

        public static float max(Double[] numbers)
        {
            float maxNumber = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                float tempNumber = float.Parse(numbers[i].ToString());

                if (maxNumber < tempNumber)
                {
                    maxNumber = float.Parse(numbers[i].ToString());
                }
            }

            return maxNumber;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Determines the smallest value in a sequence of numbers, and then returns that value. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="numbers">  The array to check. </param>
        ///
        /// <returns>   The minimum value. </returns>

        public static float min(double[] numbers)
        {
            float maxNumber = float.MaxValue;
            for (int i = 0; i < numbers.Length; i++)
            {
                float tempNumber = float.Parse(numbers[i].ToString());

                if (tempNumber < maxNumber)
                {
                    maxNumber = float.Parse(numbers[i].ToString());
                }
            }

            return maxNumber;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Normalizes a number from another range into a value between 0 and 1.  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    Incoming value to be normalized. </param>
        /// <param name="start">    Lower bound of the value's current range. </param>
        /// <param name="stop">     Upper bound of the value's current range. </param>
        ///
        /// <returns>   The normalized value. </returns>

        public static float norm(double value, double start, double stop)
        {
            return map(value, start, stop, 0, 1);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Facilitates exponential expressions. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Base of the exponential expression. </param>
        /// <param name="power">    Power by which to raise the base. </param>
        ///
        /// <returns>   The value. </returns>

        public static float pow(double number, double power)
        {
            return (float)System.Math.Pow(number, power);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Rounds the number parameter </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   The number to round. </param>
        ///
        /// <returns>   The rounded value. </returns>

        public static int round(double number)
        {
            return (int)(System.Math.Round(number));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Squares a number (multiplies a number by itself). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Number to square. </param>
        ///
        /// <returns>   The result. </returns>

        public static float sq(double number)
        {
            return (float)(number * number);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the square root of a number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Non-negative number to square root. </param>
        ///
        /// <returns>   The result. </returns>

        public static float sqrt(double number)
        {
            return (float)(System.Math.Sqrt(number));
        }

        #endregion

        #region Noise

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the Perlin noise value at specified coordinates. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    x-coordinate in noise space. </param>
        ///
        /// <returns>   The noise value. </returns>

        public static float noise(double x)
        {
            return constrain(Noise.perlin(x, 0, 0), 0, 1);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the Perlin noise value at specified coordinates. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    x-coordinate in noise space. </param>
        /// <param name="y">    y-coordinate in noise space. </param>
        ///
        /// <returns>   The noise value. </returns>

        public static float noise(double x, double y)
        {
            return (float)Noise.perlin(x, y, 0);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the Perlin noise value at specified coordinates. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    x-coordinate in noise space. </param>
        /// <param name="y">    y-coordinate in noise space. </param>
        /// <param name="z">    z-coordinate in noise space. </param>
        ///
        /// <returns>   The noise value. </returns>

        public static float noise(double x, double y, double z)
        {
            return (float)(Noise.perlin(x, y, z));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the seed value for noise(). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="Seed"> The seed value. </param>

        public static void noiseSeed(int Seed)
        {
            Noise.SetSeed(Seed);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adjusts the character and level of detail produced by the Perlin noise function. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="Detail">   Number of octaves to be used by the noise. </param>

        public static void noiseDetail(int Detail)
        {
            //Noise.SetFractalOctaves(Detail);
        }

        public class Perlin
        {

            public int repeat;

            private double seed = random(double.MaxValue);

            public Perlin(int repeat = -1)
            {
                this.repeat = repeat;
            }

            public double OctavePerlin(double x, double y, double z, int octaves, double persistence)
            {
                double total = 0;
                double frequency = 1;
                double amplitude = 1;
                double maxValue = 0;            // Used for normalizing result to 0.0 - 1.0
                for (int i = 0; i < octaves; i++)
                {
                    total += perlin(x * frequency, y * frequency, z * frequency) * amplitude;

                    maxValue += amplitude;

                    amplitude *= persistence;
                    frequency *= 2;
                }

                return total / maxValue;
            }

            private static readonly int[] permutation = { 151,160,137,91,90,15,					// Hash lookup table as defined by Ken Perlin.  This is a randomly
		        131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,	// arranged array of all numbers from 0-255 inclusive.
		        190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
                88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
                77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
                102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
                135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
                5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
                223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
                129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
                251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
                49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
                138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
            };

            private static readonly int[] p;                                                    // Doubled permutation to avoid overflow

            static Perlin()
            {
                p = new int[512];
                for (int x = 0; x < 512; x++)
                {
                    p[x] = permutation[x % 256];
                }
            }

            public void SetSeed(double seed)
            {
                this.seed = seed;
            }

            public double perlin(double x, double y, double z)
            {
                //x += seed;
                //y += seed;
                //z += seed;

                if (repeat > 0)
                {                                   // If we have any repeat on, change the coordinates to their "local" repetitions
                    x = x % repeat;
                    y = y % repeat;
                    z = z % repeat;
                }

                int xi = (int)x & 255;                              // Calculate the "unit cube" that the point asked will be located in
                int yi = (int)y & 255;                              // The left bound is ( |_x_|,|_y_|,|_z_| ) and the right bound is that
                int zi = (int)z & 255;                              // plus 1.  Next we calculate the location (from 0.0 to 1.0) in that cube.
                double xf = x - (int)x;                             // We also fade the location to smooth the result.
                double yf = y - (int)y;

                double zf = z - (int)z;
                double u = fade(xf);
                double v = fade(yf);
                double w = fade(zf);

                int aaa, aba, aab, abb, baa, bba, bab, bbb;
                aaa = p[p[p[xi] + yi] + zi];
                aba = p[p[p[xi] + inc(yi)] + zi];
                aab = p[p[p[xi] + yi] + inc(zi)];
                abb = p[p[p[xi] + inc(yi)] + inc(zi)];
                baa = p[p[p[inc(xi)] + yi] + zi];
                bba = p[p[p[inc(xi)] + inc(yi)] + zi];
                bab = p[p[p[inc(xi)] + yi] + inc(zi)];
                bbb = p[p[p[inc(xi)] + inc(yi)] + inc(zi)];

                double x1, x2, y1, y2;
                x1 = lerp(grad(aaa, xf, yf, zf),                // The gradient function calculates the dot product between a pseudorandom
                            grad(baa, xf - 1, yf, zf),              // gradient vector and the vector from the input coordinate to the 8
                            u);                                     // surrounding points in its unit cube.
                x2 = lerp(grad(aba, xf, yf - 1, zf),                // This is all then lerped together as a sort of weighted average based on the faded (u,v,w)
                            grad(bba, xf - 1, yf - 1, zf),              // values we made earlier.
                              u);
                y1 = lerp(x1, x2, v);

                x1 = lerp(grad(aab, xf, yf, zf - 1),
                            grad(bab, xf - 1, yf, zf - 1),
                            u);
                x2 = lerp(grad(abb, xf, yf - 1, zf - 1),
                              grad(bbb, xf - 1, yf - 1, zf - 1),
                              u);
                y2 = lerp(x1, x2, v);

                return (lerp(y1, y2, w) + 1) / 2;                       // For convenience we bound it to 0 - 1 (theoretical min/max before is -1 - 1)
            }

            public int inc(int num)
            {
                num++;
                if (repeat > 0) num %= repeat;

                return num;
            }

            public static double grad(int hash, double x, double y, double z)
            {
                int h = hash & 15;                                  // Take the hashed value and take the first 4 bits of it (15 == 0b1111)
                double u = h < 8 /* 0b1000 */ ? x : y;              // If the most significant bit (MSB) of the hash is 0 then set u = x.  Otherwise y.

                double v;                                           // In Ken Perlin's original implementation this was another conditional operator (?:).  I
                                                                    // expanded it for readability.

                if (h < 4 /* 0b0100 */)                             // If the first and second significant bits are 0 set v = y
                    v = y;
                else if (h == 12 /* 0b1100 */ || h == 14 /* 0b1110*/)// If the first and second significant bits are 1 set v = x
                    v = x;
                else                                                // If the first and second significant bits are not equal (0/1, 1/0) set v = z
                    v = z;

                return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v); // Use the last 2 bits to decide if u and v are positive or negative.  Then return their addition.
            }

            public static double fade(double t)
            {
                // Fade function as defined by Ken Perlin.  This eases coordinate values
                // so that they will "ease" towards integral values.  This ends up smoothing
                // the final output.
                return t * t * t * (t * (t * 6 - 15) + 10);         // 6t^5 - 15t^4 + 10t^3
            }

            public static double lerp(double a, double b, double x)
            {
                return a + x * (b - a);
            }
        }

        #endregion

        #region Trigonometry

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The inverse of cos(), returns the arc cosine of a value. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The value whose arc cosine is to be returned. </param>
        ///
        /// <returns>   The result. </returns>

        public static float acos(double value)
        {
            return (float)System.Math.Acos(value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The inverse of sin(), returns the arc sine of a value. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The value whose arc sine is to be returned. </param>
        ///
        /// <returns>   The result. </returns>

        public static float asin(double value)
        {
            return (float)System.Math.Asin(value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The inverse of tan(), returns the arc tangent of a value. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The value whose arc tangent is to be returned. </param>
        ///
        /// <returns>   The result. </returns>

        public static float atan(double value)
        {
            return (float)System.Math.Atan(value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the angle (in radians) from a specified point to the coordinate origin as measured from the positive x-axis. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="y">    y-coordinate of the point. </param>
        /// <param name="x">    x-coordinate of the point. </param>
        ///
        /// <returns>   The result. </returns>

        public static float atan2(double y, double x)
        {
            return (float)System.Math.Atan2(y, x);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the cosine of an angle. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The angle. </param>
        ///
        /// <returns>   The result. </returns>

        public static float cos(double value)
        {
            return (float)System.Math.Cos(value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the sine of an angle. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The angle. </param>
        ///
        /// <returns>   A value between -1 and 1. </returns>

        public static float sin(double value)
        {
            return (float)System.Math.Sin(value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the tangent of an angle. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The angle. </param>
        ///
        /// <returns>   The result. </returns>

        public static float tan(double value)
        {
            return (float)System.Math.Tan(value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts degrees to radiands. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="radians">  The radians to convert. </param>
        ///
        /// <returns>   The result. </returns>

        public static float degrees(double radians)
        {
            return (float)(radians * 180 / System.Math.PI);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts radiands to degrees. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="degrees">  The radiands. </param>
        ///
        /// <returns>   The result. </returns>

        public static float radians(double degrees)
        {
            return (float)(degrees * System.Math.PI / 180);
        }

        #endregion

        #region Algorithms

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts the angle and radius to a x-y point. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="StartPosition">    The start position. </param>
        /// <param name="angle">            The angle. </param>
        /// <param name="radius">           The radius. </param>
        ///
        /// <returns>   The location. </returns>

        public static PointF PolarToCartesian(double angle, double radius)
        {
            float x = 0, y = 0;

            if (angleMode() == angleModes.Degrees)
            {
                x += (float)(radius * System.Math.Cos(angle * 2 * System.Math.PI / 360));
                y += (float)(radius * System.Math.Sin(angle * 2 * System.Math.PI / 360));
            }
            else
            {
                x += (float)(radius * System.Math.Cos(angle));
                y += (float)(radius * System.Math.Sin(angle));
            }
            

            return new System.Drawing.PointF(x, y);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A result. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public struct Result
        {
            public float Angle;
            public float Radius;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   converts the locations to a radius and angle. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="Start">    The start. </param>
        /// <param name="location"> The location. </param>
        ///
        /// <returns>   The result. </returns>

        public static Result CartesianToPolar(System.Drawing.Point Start, System.Drawing.Point location)
        {

            double dX = Start.X - location.X;
            double dY = Start.Y - location.Y;
            double multi = dX * dX + dY * dY;

            double radius = System.Math.Round(System.Math.Sqrt(multi), 3);

            double angle = 360 + (System.Math.Atan2((Start.Y - location.Y), Start.X - location.X) * 180 / System.Math.PI - 180);
            if (angle > 360)
                angle = angle - 360;

            var info = new Result
            {
                Angle = (float)angle,
                Radius = (float)radius
            };

            return info;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   returns the contrast color from a color. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="color">    The color to check. </param>
        ///
        /// <returns>   The contrast color (White or Black). </returns>

        public static Color contrastColor(Color color)
        {
            int d = 0;

            // Counting the perceptive luminance - human eye favors green color... 
            double a = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            if (a < 0.5)
                d = 0; // bright colors - black font
            else
                d = 255; // dark colors - white font

            return Color.FromArgb(d, d, d);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets dominant color of a bitmap. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="bmp">  The bitmap to check. </param>
        ///
        /// <returns>   The dominant color. </returns>

        public static Color getDominantColor(Bitmap bmp)
        {
            Rectangle bounds = new Rectangle(System.Drawing.Point.Empty, bmp.Size);

            //Lock Image
            Bitmap processor = bmp;

            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            for (int x = bmp.Width / 100 * 10; x < bmp.Width / 100 * 90; x++)
            {
                for (int y = bmp.Height / 100 * 10; y < bmp.Height / 100 * 90; y++)
                {
                    Color clr = processor.GetPixel(x, y);

                    r += clr.R - 10;
                    g += clr.G - 10;
                    b += clr.B - 10;

                    total++;
                }
            }

            //Calculate average
            r /= total;
            g /= total;
            b /= total;
            processor.Dispose();

            return Color.FromArgb(r, g, b);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random Point </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A random point. </returns>

        public static System.Drawing.PointF randomPoint()
        {
            Random rand = new Random();
            return new System.Drawing.PointF(map((float)rand.NextDouble(), 0, 1, 0, width), map((float)rand.NextDouble(), 0, 1, 0, height));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random string with a given lenght. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="length">   The length. </param>
        ///
        /// <returns>   A random string. </returns>

        public static string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[floor(random(s.Length))]).ToArray());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random normalized vector </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A random vector. </returns>

        public static Vector randomVector()
        {
            Vector vec = new Vector(random(-100, 100), random(-100, 100));
            vec.Normalize();
            return vec;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current milliseconds. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The milliseconds. </returns>

        public static int millis()
        {
            return DateTime.Now.Millisecond;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current seconds. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   the current seconds. </returns>

        public static int second()
        {
            return DateTime.Now.Second;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current minute. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The current minute. </returns>

        public static int minute()
        {
            return DateTime.Now.Minute;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current hour. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The current hour. </returns>

        public static int hour()
        {
            return DateTime.Now.Hour;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current day. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The current day. </returns>

        public static int day()
        {
            return DateTime.Now.Day;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current month. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The current month. </returns>

        public static int month()
        {
            return DateTime.Now.Month;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current year. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The current year. </returns>

        public static int year()
        {
            return DateTime.Now.Year;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Return a random floating-point number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="min">  The lower bound (inclusive). </param>
        /// <param name="max">  the upper bound (exclusive). </param>
        ///
        /// <returns>   System.Single. </returns>

        public static float random(double min, double max)
        {
            return (float)(min + (rand.NextDouble() * (max - 1 - min)));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Return a random floating-point number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="max">  the upper bound (exclusive). </param>
        ///
        /// <returns>   System.Single. </returns>

        public static float random(double value)
        {
            return random(0, value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Return a random floating-point number between 0 and 1. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A random floating-point number between 0 and 1. </returns>

        public static float random()
        {
            return random(0, 1);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random number fitting a Gaussian, or normal, distribution. There is theoretically no minimum or maximum value that randomGaussian() might return.  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="mean">       The mean. </param>
        /// <param name="sd">    The standard deviation. </param>
        ///
        /// <returns>   A float. </returns>

        public static float nextGaussian(double mean, double sd)
        {
            var u1 = random();
            var u2 = random();

            var rand_std_normal = sqrt(-2.0 * log(u1)) *
                                sin(2.0 * PI * u2);

            var rand_normal = mean + sd * rand_std_normal;

            return (float)rand_normal;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random number fitting a Gaussian, or normal, distribution. There is theoretically no minimum or maximum value that randomGaussian() might return.  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A Gaussian of mean 0 and deviation of 1. </returns>

        public static float nextGaussian()
        {
            return nextGaussian(0, 1);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a true or false the chance is 50-50. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The result. </returns>

        public static bool nextBoolean()
        {
            return floor(random(2)) > 0;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Generates values from a triangular distribution. </summary>
        ///
        /// <remarks>
        ///     See http://en.wikipedia.org/wiki/Triangular_distribution for a description of the
        ///     triangular probability distribution and the algorithm for generating one.
        /// </remarks>
        ///
        /// <param name="min">    Minimum. </param>
        /// <param name="max">    Maximum. </param>
        /// <param name="mode">    Mode (most frequent value) </param>
        ///
        /// <returns>   A float. </returns>

        public static float nextTriangular(double min, double max, double mode)
        {
            var u = random();

            return (float)(u < (mode - min) / (max - min)
                       ? min + sqrt(u * (max - min) * (mode - min))
                       : max - sqrt((1 - u) * (max - min) * (max - mode)));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   returns the sign of a number, indicating whether the number is positive, negative or zero </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   The number to check. </param>
        ///
        /// <returns>   -1 if lower than 0, 0 if equal to 0 and 1 if higher that 0. </returns>

        public static int sign(double number)
        {
            if (number > 0)
                return 1;
            else if (number < 0)
                return -1;

            return 0;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the fibonacci of the givn number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">    The number. </param>
        ///
        /// <returns>   A float. </returns>

        public static float fibonacci(double number)
        {
            double a = 0;
            double b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < number; i++)
            {
                double temp = a;
                a = b;
                b = temp + b;
            }
            return (float)a;
        }

        #endregion
    }
}