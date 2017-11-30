using OpenTK.Platform.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CCreative.General;
using static CCreative.Drawing;
using System.Windows;
using System.Collections;

namespace CCreative
{
    public static class Math
    {
        static Random rand = new Random();
        
        static OpenSimplexNoise Noise = new OpenSimplexNoise();
        
        public static float HALF_PI { get { return (float)(System.Math.PI * 0.5); } }
        public static float PI { get { return (float)System.Math.PI; } }
        public static float QUARTER_PI { get { return (float)(System.Math.PI * 0.25); } }
        public static float TAU { get { return (float)System.Math.PI * 2; } }
        public static float TWO_PI { get { return (float)System.Math.PI * 2; } }
        public static float Infinity { get { return float.PositiveInfinity; } }
        

        static int octaves = 1;
        static double persistence = 0.5;
        static double lacunarity = 2;

        static bool noiseChanged = false;

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
            return (number >= 0) ? (float)number : (float)-number;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the closest int value that is greater than or equal to the value of the parameter. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Number to round up. </param>
        ///
        /// <returns>   The ceiling. </returns>

        public static int ceil(double number)
        {
            return floor(number + 1);
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
            return (number < low) ? (float)low : (number > high) ? (float)high : (float)number;
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
        /// <param name="points">   The points tot est the distance with. </param>
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
            return (number >= 0 ? (int)number : (int)number - 1);
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
            return (float)(start + (stop - start) * atm);
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
            return sqrt(numberX * numberX + numberY * numberY);
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

        public static float max(params Double[] numbers)
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

        public static float min(params double[] numbers)
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
            return (float)((value - start) / (stop - start));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Facilitates exponential expressions. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">   Base of the exponential expression. </param>
        /// <param name="exponent">    Power by which to raise the base. </param>
        ///
        /// <returns>   The value. </returns>

        public static float pow(double number, int exponent)
        {
            double result = 1.0;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                    result *= number;
                exponent >>= 1;
                number *= number;
            }
            return (float)result;

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
            return floor(number + 0.5);
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
            return noise(x, 0);
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
            if (noiseChanged)
            {
                double total = 0;
                double frequency = 1;
                double amplitude = 1;
                double maxValue = 0;            // Used for normalizing result to 0.0 - 1.0

                for (int i = 0; i < octaves; i++)
                {
                    total += (Noise.Evaluate(x * frequency, y * frequency)) * amplitude;

                    maxValue += amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                return (float)(total / maxValue); 
            }
            return (float)Noise.Evaluate(x, y);
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
            if (noiseChanged)
            {
                double total = 0;
                double frequency = 1;
                double amplitude = 1;
                double maxValue = 0;            // Used for normalizing result to 0.0 - 1.0

                for (int i = 0; i < octaves; i++)
                {
                    total += (Noise.Evaluate(x * frequency, y * frequency, z * frequency)) * amplitude;

                    maxValue += amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                return (float)(total / maxValue); 
            }

            return (float)(Noise.Evaluate(x, y, z));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the Perlin noise value at specified coordinates. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    x-coordinate in noise space. </param>
        /// <param name="y">    y-coordinate in noise space. </param>
        /// <param name="z">    z-coordinate in noise space. </param>
        /// <param name="w">    w-coordinate in noise space. </param>
        ///
        /// <returns>   The noise value. </returns>

        public static float noise(double x, double y, double z, double w)
        {
            if (noiseChanged)
            {
                double total = 0;
                double frequency = 1;
                double amplitude = 1;
                double maxValue = 0;            // Used for normalizing result to 0.0 - 1.0

                for (int i = 0; i < octaves; i++)
                {
                    total += (Noise.Evaluate(x * frequency, y * frequency, z * frequency, w * frequency)) * amplitude;

                    maxValue += amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                return (float)(total / maxValue);
            }

            return (float)(Noise.Evaluate(x, y, z));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the seed value for noise(). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="Seed"> The seed value. </param>

        public static void noiseSeed(double Seed)
        {
            Noise.setSeed(Seed);
            noiseChanged = true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adjusts the character and level of detail produced by the Perlin noise function. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="Detail">   The detail of the noise function (a value between 1 and 30). </param>

        public static void noiseDetail(int Detail)
        {
            Detail = round(constrain(Detail, 1, 29));
            octaves = Detail;
            noiseChanged = true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adjusts the character and level of detail produced by the Perlin noise function. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="Detail">   Number of octaves to be used by the noise. </param>
        /// <param name="Persistence">   The persistence to use. </param>

        public static void noiseDetail(int Detail, double Persistence, double Lacunarity)
        {
            octaves = Detail;
            persistence = Persistence;
            lacunarity = Lacunarity;

            noiseChanged = true;
        }

        public static Color whiteNoise()
        {
            int randomVal = randomInt(0, 255);
            return Color.FromArgb(randomVal, randomVal, randomVal);
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
        /// <param name="angle">            The angle. </param>
        /// <param name="radius">           The radius. </param>
        /// <param name="startlocation">           The startlocation. </param>
        ///
        /// <returns>   The location. </returns>

        public static PointF PolarToCartesian(PointF startlocation, double angle, double radius)
        {
            float x = startlocation.X, y = startlocation.Y;

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
        /// <summary>   Converts the angle and radius to a x-y point. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="angle">            The angle. </param>
        /// <param name="radius">           The radius. </param>
        ///
        /// <returns>   The location. </returns>

        public static PointF PolarToCartesian(double angle, double radius)
        {
            return (PolarToCartesian(new PointF(0, 0), angle, radius));
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

        public static Result CartesianToPolar(PointF location)
        {
            //double dX = Start.X - location.X;
            //double dY = Start.Y - location.Y;
            //double multi = dX * dX + dY * dY;

            //double radius = System.Math.Round(System.Math.Sqrt(multi), 3);

            //double angle = 360 + (System.Math.Atan2((Start.Y - location.Y), Start.X - location.X) * 180 / System.Math.PI - 180);
            //if (angle > 360)
            //    angle = angle - 360;

            float radius = sqrt(location.X * location.X + location.Y * location.Y);
            float angle = atan2(location.Y, location.X);

            var info = new Result
            {
                Angle = angle,
                Radius = radius
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
        /// <summary>   Returns a random Point </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A random point. </returns>

        public static System.Drawing.PointF randomPointF()
        {
            return new PointF(random(width + 1), random(height + 1));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a random Point </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A random point. </returns>

        public static System.Drawing.Point randomPoint()
        {
            return new System.Drawing.Point(randomInt(width + 1), randomInt(height + 1));
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
            Vector vec = new Vector(random(double.MinValue, double.MaxValue), random(double.MinValue, double.MaxValue));
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
        /// <param name="value">  the upper bound (exclusive). </param>
        ///
        /// <returns>   System.Single. </returns>

        public static float random(double value)
        {
            return (float)(rand.NextDouble() * (value - 0.000000000000001));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Return a random floating-point number between 0 and 1. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A random floating-point number between 0 and 1. </returns>

        public static float random()
        {
            return (float)rand.NextDouble();
        }

        /// <summary>
        /// Returns a random item form the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        /// 
        public static T random<T>(T[] array)
        {
            return array[randomInt(array.Length)];
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Return a random int number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="min">  The lower bound (inclusive). </param>
        /// <param name="max">  the upper bound (exclusive). </param>
        ///
        /// <returns>   System.Single. </returns>

        public static int randomInt(int min, int max)
        {
            return rand.Next(min, max);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Return a random int number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">  the upper bound (exclusive). </param>
        ///
        /// <returns>   System.Single. </returns>

        public static int randomInt(int value)
        {
            return rand.Next(value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Return a random byte number. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   System.Single. </returns>

        public static byte randomByte()
        {
            return (byte)rand.Next(byte.MaxValue + 1);
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
            return floor(randomInt(2)) > 0;
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

        public static T[] shuffle<T>(T[] array)
        {
            T[] temp = new T[array.Length];
            int n = array.Length;
            while (n > 1)
            {
                int k = round(random(n--));
                T tempT = temp[n];
                temp[n] = temp[k];
                temp[k] = tempT;
            }
            return temp;
        }

        public static void shuffle<T>(T[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int n = array.Length;
                while (n > 1)
                {
                    int k = round(random(n--));
                    T temp = array[i][n];
                    array[i][n] = array[i][k];
                    array[i][k] = temp;
                } 
            }
        }

        #endregion

        public static bool collideRectCircle(double rx, double ry, double rw, double rh, double cx, double cy, double diameter)
        {
            double testX = cx;
            double testY = cy;

            if (cx < rx)
                testX = rx;       // left edge
            else if (cx > rx + rw)
                testX = rx + rw;   // right edge

            if (cy < ry)
                testY = ry;       // top edge
            else if (cy > ry + rh)
                testY = ry + rh;   // bottom edge

            double distance = dist(cx, cy, testX, testY);

            return distance <= diameter / 2;
        }

        public static bool collideRectRect(double x, double y, double w, double h, double x2, double y2, double w2, double h2)
        {
            return (x + w >= x2 &&    // r1 right edge past r2 left
                x <= x2 + w2 &&    // r1 left edge past r2 right
                y + h >= y2 &&    // r1 top edge past r2 bottom
                y <= y2 + h2);
        }

        public static bool collideCircleCircle(double x, double y, double d, double x2, double y2, double d2)
        {
            return dist(x, y, x2, y2) <= (d / 2) + (d2 / 2);
        }

        public static bool collidePointCircle(double x, double y, double cx, double cy, double d)
        {
            return dist(x, y, cx, cy) <= d / 2;
        }

        public static bool collidePointRect(double pointX, double pointY, double x, double y, double xW, double yW)
        {
            return pointX >= x &&         // right of the left edge AND
                pointX <= x + xW &&    // left of the right edge AND
                pointY >= y &&         // below the top AND
                pointY <= y + yW;    // above the bottom
        }

        public static bool collideLineCircle(double x1, double y1, double x2, double y2, double cx, double cy, double diameter)
        {
            bool inside1 = collidePointCircle(x1, y1, cx, cy, diameter);
            bool inside2 = collidePointCircle(x2, y2, cx, cy, diameter);
            if (inside1 || inside2) return true;

            // get length of the line
            double distX = x1 - x2;
            double distY = y1 - y2;
            double len = sqrt((distX * distX) + (distY * distY));

            // get dot product of the line and circle
            double dot = (((cx - x1) * (x2 - x1)) + ((cy - y1) * (y2 - y1))) / pow(len, 2);

            // find the closest point on the line
            double closestX = x1 + (dot * (x2 - x1));
            double closestY = y1 + (dot * (y2 - y1));

            // is this point actually on the line segment?
            // if so keep going, but if not, return false
            bool onSegment = collidePointLine(closestX, closestY, x1, y1, x2, y2, 0.1);
            if (!onSegment) return false;


            // get distance to closest point
            distX = closestX - cx;
            distY = closestY - cy;
            double distance = sqrt((distX * distX) + (distY * distY));

            return distance <= diameter / 2;
        }

        public static bool collidePointLine(double px, double py, double x1, double y1, double x2, double y2, double buffer)
        {
            double d1 = dist(px, py, x1, y1);
            double d2 = dist(px, py, x2, y2);

            // get the length of the line
            double lineLen = dist(x1, y1, x2, y2);

            // if the two distances are equal to the line's length, the point is on the line!
            // note we use the buffer here to give a range, rather than one #
            return d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer;
        }

        public static bool collideLineLine(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            double uA = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            double uB = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));

            return (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1);
        }



        /// <summary>
        /// Determines whether the value is a power of two.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <c>true</c> if value is of power two; otherwise, <c>false</c>.
        /// </returns>
        public static bool isPowerOfTwo(int value)
        {
            return (value != 0) && ((value & (value - 1)) == 0);
        }

        /// <summary>
        /// Encryps the string with the given XOR cipher algoritm
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="key">The encryption key.</param>
        /// <returns>The encrypted data.</returns>
        public static string XORCipher(string data, string key)
        {
            int dataLen = data.Length;
            int keyLen = key.Length;
            char[] output = new char[dataLen];

            for (int i = 0; i < dataLen; ++i)
            {
                output[i] = (char)(data[i] ^ key[i % keyLen]);
            }

            return new string(output);
        }

        /// <summary>
        /// This algorithm find the greatest common divisor of two integers.
        /// </summary>
        /// <param name="a"> The first number to check.</param>
        /// <param name="b"> The second number to check.</param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            if (a == 0)
                return b;

            while (b != 0)
            {
                if (a > b)
                    a -= b;
                else
                    b -= a;
            }

            return a;
        }

        /// <summary>
        /// converts a word to a number.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns> The result of the number.</returns>
        public static ulong WordsToNumbers(string words)
        {
            if (string.IsNullOrEmpty(words)) return 0;

            words = words.Trim();
            words += ' ';

            ulong number = 0;
            string[] singles = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] teens = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tens = new string[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninty" };
            string[] powers = new string[] { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion" };

            for (int i = powers.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(powers[i]))
                {
                    int index = words.IndexOf(powers[i]);

                    if (index >= 0 && words[index + powers[i].Length] == ' ')
                    {
                        ulong count = WordsToNumbers(words.Substring(0, index));
                        number += count * (ulong)Math.pow(1000, i);
                        words = words.Remove(0, index);
                    }
                }
            }

            {
                int index = words.IndexOf("hundred");

                if (index >= 0 && words[index + "hundred".Length] == ' ')
                {
                    ulong count = WordsToNumbers(words.Substring(0, index));
                    number += count * 100;
                    words = words.Remove(0, index);
                }
            }

            for (int i = tens.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(tens[i]))
                {
                    int index = words.IndexOf(tens[i]);

                    if (index >= 0 && words[index + tens[i].Length] == ' ')
                    {
                        number += (uint)(i * 10);
                        words = words.Remove(0, index);
                    }
                }
            }

            for (int i = teens.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(teens[i]))
                {
                    int index = words.IndexOf(teens[i]);

                    if (index >= 0 && words[index + teens[i].Length] == ' ')
                    {
                        number += (uint)(i + 10);
                        words = words.Remove(0, index);
                    }
                }
            }

            for (int i = singles.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(singles[i]))
                {
                    int index = words.IndexOf(singles[i] + ' ');

                    if (index >= 0 && words[index + singles[i].Length] == ' ')
                    {
                        number += (uint)(i);
                        words = words.Remove(0, index);
                    }
                }
            }

            return number;
        }

        private enum RomanDigit
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000
        }

        /// <summary>
        /// Converts roman number to normal numbers.
        /// </summary>
        /// <param name="roman">The roman numbers.</param>
        /// <returns></returns>
        public static int RomanToNumbers(string roman)
        {
            roman = roman.ToUpper().Trim();
            if (roman == "N") return 0;

            int ptr = 0;
            ArrayList values = new ArrayList();
            int maxDigit = 1000;
            while (ptr < roman.Length)
            {
                char numeral = roman[ptr];
                int digit = (int)Enum.Parse(typeof(RomanDigit), numeral.ToString());

                int nextDigit = 0;
                if (ptr < roman.Length - 1)
                {
                    char nextNumeral = roman[ptr + 1];
                    nextDigit = (int)Enum.Parse(typeof(RomanDigit), nextNumeral.ToString());

                    if (nextDigit > digit)
                    {
                        maxDigit = digit - 1;
                        digit = nextDigit - digit;
                        ptr++;
                    }
                }

                values.Add(digit);
                ptr++;
            }

            int total = 0;
            foreach (int digit in values)
                total += digit;

            return total;
        }
    }
}

namespace ExtensionMethods
{
    using static CCreative.Math;
    using System.Linq;

    public static class Extensions
    {
        public static int constrain(this int number, double low, double high)
        {
            if (number > high) { number = (int)high; }
            else if (number < low) { number = (int)low; }
            return number;
        }

        public static float constrain(this float number, double low, double high)
        {
            if (number > high) { number = (float)high; }
            else if (number < low) { number = (float)low; }
            return number;
        }

        public static double constrain(this double number, double low, double high)
        {
            if (number > high) { number = high; }
            else if (number < low) { number = low; }
            return number;
        }

        public static double map(this double value, double least, double max, double toMinimum, double toMaximum)
        {
            return (toMinimum + value - least) * (toMaximum - toMinimum) / (max - least);
        }

        public static int floor(this float value)
        {
            return (int)value;
        }

        public static int floor(this double value)
        {
            return (int)value;
        }

        public static int round(this float value)
        {
            return (int)(value + 0.5);
        }

        public static int round(this double value)
        {
            return (int)(value + 0.5);
        }

        public static int ceil(this float value)
        {
            return (int)(value + 1);
        }

        public static int ceil(this double value)
        {
            return (int)(value + 1);
        }
        
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }

        public static T[] push<T>(this T[] source, T toAdd)
        {
            source.ToList().Add(toAdd);
            return source.ToArray();
        }

        public static bool includes<T>(this T[] source, T toCheck)
        {
            return Array.IndexOf(source, toCheck) != -1;
        }
    }
}