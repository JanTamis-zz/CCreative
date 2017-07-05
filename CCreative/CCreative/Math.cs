using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CCreative.General;
using System.Windows;

namespace CCreative
{
    public static class Math
    {
        static Random rand = new Random();

        public static readonly float HALF_PI = (float)(System.Math.PI * 0.5);
        public static readonly float PI = (float)System.Math.PI;
        public static readonly float QUARTER_PI = (float)(System.Math.PI * 0.25);
        public static readonly float TAU = (float)(System.Math.PI * 2);
        public static readonly float TWO_PI = (float)(System.Math.PI * 2);
        public static readonly float Infinity = float.PositiveInfinity;

        #region Calculations
        public static float abs(double number)
        {
            return (float)System.Math.Abs(number);
        }

        public static float ceil(double number)
        {
            return (float)System.Math.Ceiling((decimal)number);
        }

        public static float constrain(double number, double low, double high)
        {
            if (number > high) { number = high; }
            else if (number < low) { number = low; }
            return (float)number;
        }

        public static float dist(System.Drawing.Point beginPoint, System.Drawing.Point endPoint)
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(endPoint.X - beginPoint.X, 2) + System.Math.Pow(endPoint.Y - beginPoint.Y, 2)));
        }

        public static float exp(double number)
        {
            return (float)(System.Math.Pow(System.Math.E, number));
        }

        public static int floor(double number)
        {
            return (int)(System.Math.Floor(number));
        }

        public static float lerp(double start, double stop, double atm)
        {
            if (atm > 1) { atm = 1; }
            else if (atm < 0) { atm = 0; }

            return (float)((1 - atm) * start + atm * stop);
        }

        public static float log(double number)
        {
            return (float)(System.Math.Log(number));
        }

        public static float mag(double numberX, double numberY)
        {
            Vector vec = new Vector(numberX, numberY);
            return (float)vec.Length;
        }

        public static float map(double value, double least, double max, double toMinimum, double toMaximum)
        {
            return (float)(toMinimum + (value - least) * (toMaximum - toMinimum) / (max - least));
        }

        public static float max<T>(T[] numbers)
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

        public static float min<T>(T[] numbers)
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

        public static float norm(double value, double start, double stop)
        {
            return map(value, start, stop, 0, 1);
        }

        public static float pow(double number, double power)
        {
            return (float)System.Math.Pow(number, power);
        }

        public static float round(double number)
        {
            return (float)(System.Math.Round(number));
        }

        public static float sq(double number)
        {
            return (float)(number * number);
        }

        public static float sqrt(double number)
        {
            return (float)(System.Math.Sqrt(number));
        }

        #endregion

        #region Noise

        public static double noise(double x, double y = 0, double z = 0)
        {
            return Perlin.perlin(x, y, z);
        }

        public class Perlin
        {

            public static double OctavePerlin(double x, double y, double z, int octaves, double persistence)
            {
                double total = 0;
                double frequency = 1;
                double amplitude = 1;
                for (int i = 0; i < octaves; i++)
                {
                    total += perlin(x * frequency, y * frequency, z * frequency) * amplitude;

                    amplitude *= persistence;
                    frequency *= 2;
                }

                return total;
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

            public static double perlin(double x, double y, double z)
            {
                int xi = (int)x & 255;                              // Calculate the "unit cube" that the point asked will be located in
                int yi = (int)y & 255;                              // The left bound is ( |_x_|,|_y_|,|_z_| ) and the right bound is that
                int zi = (int)z & 255;                              // plus 1.  Next we calculate the location (from 0.0 to 1.0) in that cube.
                double xf = x - (int)x;                             // We also fade the location to smooth the result.
                double yf = y - (int)y;
                double zf = z - (int)z;
                double u = fade(xf);
                double v = fade(yf);
                double w = fade(zf);

                int a = p[xi] + yi;                             // This here is Perlin's hash function.  We take our x value (remember,
                int aa = p[a] + zi;                             // between 0 and 255) and get a random value (from our p[] array above) between
                int ab = p[a + 1] + zi;                             // 0 and 255.  We then add y to it and plug that into p[], and add z to that.
                int b = p[xi + 1] + yi;                             // Then, we get another random value by adding 1 to that and putting it into p[]
                int ba = p[b] + zi;                             // and add z to it.  We do the whole thing over again starting with x+1.  Later
                int bb = p[b + 1] + zi;                             // we plug aa, ab, ba, and bb back into p[] along with their +1's to get another set.
                                                                    // in the end we have 8 values between 0 and 255 - one for each vertex on the unit cube.
                                                                    // These are all interpolated together using u, v, and w below.

                double x1, x2, y1, y2;
                x1 = lerp(grad(p[aa], xf, yf, zf),          // This is where the "magic" happens.  We calculate a new set of p[] values and use that to get
                            grad(p[ba], xf - 1, yf, zf),            // our final gradient values.  Then, we interpolate between those gradients with the u value to get
                            u);                                     // 4 x-values.  Next, we interpolate between the 4 x-values with v to get 2 y-values.  Finally,
                x2 = lerp(grad(p[ab], xf, yf - 1, zf),          // we interpolate between the y-values to get a z-value.
                            grad(p[bb], xf - 1, yf - 1, zf),
                            u);                                     // When calculating the p[] values, remember that above, p[a+1] expands to p[xi]+yi+1 -- so you are
                y1 = lerp(x1, x2, v);                               // essentially adding 1 to yi.  Likewise, p[ab+1] expands to p[p[xi]+yi+1]+zi+1] -- so you are adding
                                                                    // to zi.  The other 3 parameters are your possible return values (see grad()), which are actually
                x1 = lerp(grad(p[aa + 1], xf, yf, zf - 1),      // the vectors from the edges of the unit cube to the point in the unit cube itself.
                            grad(p[ba + 1], xf - 1, yf, zf - 1),
                            u);
                x2 = lerp(grad(p[ab + 1], xf, yf - 1, zf - 1),
                              grad(p[bb + 1], xf - 1, yf - 1, zf - 1),
                              u);
                y2 = lerp(x1, x2, v);

                return (lerp(y1, y2, w) + 1) / 2;                       // For convenience we bound it to 0 - 1 (theoretical min/max before is -1 - 1)
            }

            public static double grad(int hash, double x, double y, double z)
            {
                int h = hash & 15;                                  // Take the hashed value and take the first 4 bits of it (15 == 0b1111)
                double u = h < 8 /* 0b1000 */ ? x : y;              // If the most signifigant bit (MSB) of the hash is 0 then set u = x.  Otherwise y.

                double v;                                           // In Ken Perlin's original implementation this was another conditional operator (?:).  I
                                                                    // expanded it for readability.

                if (h < 4 /* 0b0100 */)                             // If the first and second signifigant bits are 0 set v = y
                    v = y;
                else if (h == 12 /* 0b1100 */ || h == 14 /* 0b1110*/)// If the first and second signifigant bits are 1 set v = x
                    v = x;
                else                                                // If the first and second signifigant bits are not equal (0/1, 1/0) set v = z
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

        public static float acos(double value)
        {
            return (float)System.Math.Acos(value);
        }

        public static float asin(double value)
        {
            return (float)System.Math.Asin(value);
        }

        public static float atan(double value)
        {
            return (float)System.Math.Atan(value);
        }

        public static float atan2(double y, double x)
        {
            return (float)System.Math.Atan2(y, x);
        }

        public static float cos(double value)
        {
            return (float)System.Math.Cos(value);
        }

        public static float sin(double value)
        {
            return (float)System.Math.Sin(value);
        }

        public static float tan(double value)
        {
            return (float)System.Math.Tan(value);
        }

        public static float degrees(double radians)
        {
            return (float)(radians * 180 / System.Math.PI);
        }

        public static float radians(double degrees)
        {
            return (float)(degrees * System.Math.PI / 180);
        }

        #endregion

        #region Algorithms

        public static PointF PolarToCartesian(System.Drawing.Point StartPosition, double angle, double radius)
        {
            float x, y;

            angle -= 90;

            x = StartPosition.X;
            y = StartPosition.Y;

            x += (float)(radius * System.Math.Cos(angle * 2 * System.Math.PI / 360));
            y += (float)(radius * System.Math.Sin(angle * 2 * System.Math.PI / 360));

            return new System.Drawing.PointF(x, y);
        }

        public struct Result
        {
            public float Angle;
            public float Radius;
        }

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

        public static System.Drawing.PointF randomPoint()
        {
            Random rand = new Random();
            return new System.Drawing.PointF(map((float)rand.NextDouble(), 0, 1, 0, width), map((float)rand.NextDouble(), 0, 1, 0, height));
        }

        public static Vector randomVector()
        {
            Vector vec = new Vector(random(-100, 100), random(-100, 100));
            vec.Normalize();
            return vec;
        }

        public static int millis()
        {
            return DateTime.Now.Millisecond;
        }

        public static int second()
        {
            return DateTime.Now.Second;
        }

        public static int minute()
        {
            return DateTime.Now.Minute;
        }

        public static int hour()
        {
            return DateTime.Now.Hour;
        }

        public static int day()
        {
            return DateTime.Now.Day;
        }

        public static int month()
        {
            return DateTime.Now.Month;
        }

        public static int year()
        {
            return DateTime.Now.Year;
        }

        public static float random(double min, double max)
        {
            return (float)(min + (rand.NextDouble() * (max - min)));
        }

        public static float random(double value)
        {
            return random(0, value);
        }

        public static float random()
        {
            return random(0, 1);
        }

        public static float fibonacci(double n)
        {
            double a = 0;
            double b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < n; i++)
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