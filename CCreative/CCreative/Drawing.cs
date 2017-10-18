using System.Collections.Generic;
using System.Linq;
using static CCreative.Colors;
using static CCreative.General;
using static CCreative.Math;
using System.Drawing;
using System.Runtime.InteropServices;
using CCreative.FastBitmapLib;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CCreative
{
    public static class Drawing
    {
        struct Style
        {
            public float strokeWidth { get; set; }
            public Color strokeColor { get; set; }
            public Color fillColor { get; set; }
            public Color tintColor { get; set; }

            public drawModes rectangleMode { get; set; }
            public drawModes cirlceMode { get; set; }
            public angleModes angleMode { get; set; }

            public Style(float strokeweight, Color strokecolor, Color fillcolor, Color tintcolor, drawModes rectmode, drawModes ellipsemode, angleModes anglemode) : this()
            {
                strokeWidth = strokeweight;
                strokeColor = strokecolor;
                fillColor = fillcolor;
                tintColor = tintcolor;
                RectangleMode = rectmode;
                CircleMode = ellipsemode;
                angleMode = anglemode;
            }
        }

        [DllImport("shlwapi.dll")]
        static extern int ColorHLSToRGB(int H, int L, int S);
        
        static PointF translated = new Point(0, 0);
        static Color backgroundColor;

        static List<Vector2> points = new List<Vector2>();

        static List<Vector2> vertbuffer = new List<Vector2>();

        static VertexBuffer buffer = new VertexBuffer(VertexFormat.XY_COLOR);

        //static Bitmap image;
        static FastBitmap bitmap;

        public static int VBO = GL.GenBuffer();
        public static int textureBuffer = GL.GenTexture();

        static Color tintColor = Color.White;

        static drawModes CircleMode = drawModes.Center;
        static drawModes RectangleMode = drawModes.Corner;
        static bool Smooth = true;
        static bool center = true;

        static float size = 12;

        static PrimitiveType drawtype;

        public static FastBitmap img;

        static Stack<Style> styleStack = new Stack<Style>();

        static Style currentStyle = new Style(1, Color.Transparent, Color.White, Color.White, drawModes.Corner, drawModes.Center, angleModes.Radians);


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Values that represent the drawing modes. </summary>
        ///
        public enum drawModes
        {
            Center,
            Radius,
            Corner
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Values that represent the angle modes. </summary>
        ///

        public enum angleModes
        {
            Radians,
            Degrees
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Specifies which angle mode to use with the rotate function. </summary>
        ///
        ///
        /// <param name="modes">    The angle mode to set it to. </param>
        /// 
        public static void angleMode(angleModes modes)
        {
            currentStyle.angleMode = modes;
        }

        public static void pushStyle()
        {
            styleStack.Push(currentStyle);
        }

        public static void popstyle()
        {
            currentStyle = styleStack.Pop();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current angle mode. </summary>
        ///
        public static angleModes angleMode()
        {
            return currentStyle.angleMode;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Specifies an amount to displace objects within the display window. </summary>
        ///
        ///
        /// <param name="x">    Left/right translation. </param>
        /// <param name="y">    Up/down translation. </param>

        public static void translate(double x, double y)
        {
            GL.Translate(x, y, 0);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Specifies an amount to displace objects within the display window. </summary>
        ///
        ///
        /// <param name="point">    The point to displace the objects with. </param>

        public static void translate(PointF point)
        {
            GL.Translate(point.X, point.Y, 0);
        }
        
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the color used to fill shapes. </summary>
        ///
        ///
        /// <param name="DrawColor">    The fill color. </param>
        ///
        /// <returns>   The fill color. </returns>

        public static Color fill(Color DrawColor)
        {
            currentStyle.fillColor = DrawColor;
            return DrawColor;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The background() function sets the color used for the background and removes every drawing. </summary>
        ///
        ///
        /// <param name="backcolor">    The backcolor. </param>
        ///
        /// <returns>  The background color. </returns>

        public static Color background(Color backcolor)
        {
            GL.ClearColor(backcolor);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            backgroundColor = backcolor;
            return backcolor;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The background() function sets the color used for the background and removes every drawing. </summary>
        ///
        ///
        /// <param name="value">    The grayscale or hue value, depending on the colormode. </param>
        ///
        /// <returns>   The background color. </returns>

        public static Color background(int value)
        {
            Color clr = Color.Transparent;
            if (colorMode() == colorModes.HSB)
            {
                value = (int)constrain(value, 0, 360);
                clr = ColorTranslator.FromWin32(ColorHLSToRGB((int)map(value, 0, 360, 0, 240), 120, 240));
            }
            else if (colorMode() == colorModes.RGB)
            {
                value = (int)constrain(value, 0, 255);
                clr = Color.FromArgb(255, value, value, value);
            }
            
            return background(clr);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the background color. </summary>
        ///
        ///
        /// <returns>   The background color. </returns>

        public static Color background()
        {
            return backgroundColor;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the color used to fill shapes. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="value">    The grayscale or hue, depending on the colormode. </param>
        ///
        /// <returns>   The fill color. </returns>

        public static Color fill(int value)
        {
            if (colorMode() == colorModes.HSB)
            {
                currentStyle.fillColor = hueToColor(value);
            }
            else if (colorMode() == colorModes.RGB)
            {
                currentStyle.fillColor = Color.FromArgb(255, value, value, value);
            }
            return currentStyle.fillColor;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the color used to fill shapes. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="value">        The grayscale or hue, depending on the colormode. </param>
        /// <param name="transparancy"> The transparancy. </param>
        ///
        /// <returns>   The given color. </returns>

        public static Color fill(int value, int transparancy)
        {
            if (colorMode() == colorModes.HSB)
            {
                currentStyle.fillColor = hueToColor(value);
            }
            else if (colorMode() == colorModes.RGB)
            {
                currentStyle.fillColor = Color.FromArgb(transparancy, value, value, value);
            }
            return currentStyle.fillColor;
        }

        private static Color hueToColor(int hue)
        {
            hue = (int)map(hue, 0, 360, 0, 240);
            return ColorTranslator.FromWin32(ColorHLSToRGB(hue, 120, 240));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Disables filling geometry. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        public static void noFill()
        {
            currentStyle.fillColor = Color.Transparent;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the color used to draw lines and borders around shapes. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="DrawColor">    The stroke color. </param>
        ///
        /// <returns>   The stroke color. </returns>

        public static Color stroke(Color DrawColor)
        {
            currentStyle.strokeColor = DrawColor;
            return DrawColor;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the color used to draw lines and borders around shapes. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="value">    The grayscale or hue, depending on the colormode. </param>
        ///
        /// <returns>   The stroke color. </returns>

        public static Color stroke(int value)
        {
            
            if (colorMode() == colorModes.HSB)
            {
                value = (int)constrain(value, 0, 360);
                int Hue = (int)map(value, 0, 360, 0, 240);
                currentStyle.strokeColor = ColorTranslator.FromWin32(ColorHLSToRGB(Hue, 120, 240));
            }
            else if (colorMode() == colorModes.RGB)
            {
                currentStyle.strokeColor = Color.FromArgb(value, value, value);
            }
            return currentStyle.strokeColor;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the width of the stroke used for lines, points, and the border around shapes. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="weight">   The weight (in pixels) of the stroke. </param>
        ///
        /// <returns>   A float. </returns>

        public static float strokeWeight(double weight)
        {
            currentStyle.strokeWidth = (float)weight;
            return currentStyle.strokeWidth;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Disables drawing the stroke (outline). </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        public static void noStroke()
        {
            currentStyle.strokeColor = Color.Transparent;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws all geometry with smooth (anti-aliased) edges. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        public static void smooth()
        {
            Smooth = true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws all geometry with jagged (aliased) edges. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        public static void noSmooth()
        {
            Smooth = false;
            GL.Disable(EnableCap.LineSmooth);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Modifies the location from which ellipses are drawn by changing the way in which parameters given to ellipse() are interpreted. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="modes">    The modes. </param>

        public static void ellipseMode(drawModes modes)
        {
            CircleMode = modes;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Modifies the location from which rectangles are drawn by changing the way in which parameters given to rect() are interpreted. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="modes">    The modes. </param>

        public static void rectMode(drawModes modes)
        {
            RectangleMode = modes;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws an ellipse (oval) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="w">    The width. </param>
        /// <param name="h">    The height. </param>

        public static void ellipse(double x, double y, double w, double h)
        {
            // http://slabode.exofire.net/circle_draw.shtml

            int num_segments = 0;

            double r = ((w / h) * 2);
            num_segments = ceil(1.275 * sqrt((w * h) / 2));

            Vector2[] vertbuffer = new Vector2[num_segments]; 

            Vector2 vector = new Vector2(0, 0);
            if (CircleMode == drawModes.Center)
            {
                w /= 2;
                h /= 2;
            }

            if (currentStyle.fillColor.A > 0 || currentStyle.strokeColor.A > 0)
            {
                float theta = TWO_PI / (num_segments);

                float tangetial_factor = tan(theta);
                float radial_factor = cos(theta);

                double X = w * cos(0);//we now start at the start angle
                double Y = h * sin(0);


                for (int i = 0; i < num_segments; i++)
                {
                    vector.X = (float)(X + x);
                    vector.Y = (float)(Y + y);

                    double tx = -Y;
                    double ty = X;

                    X += tx * tangetial_factor;
                    Y += ty * tangetial_factor;

                    X *= radial_factor;
                    Y *= radial_factor;
                    
                    vertbuffer[i] = vector;
                }
                
                GL.BufferData(BufferTarget.ArrayBuffer, Vector2.SizeInBytes * vertbuffer.Length, vertbuffer.ToArray(), BufferUsageHint.DynamicDraw);
            }

            if (currentStyle.fillColor.A > 0)
            {
                GL.Color4(currentStyle.fillColor);
                GL.DrawArrays(PrimitiveType.Polygon, 0, vertbuffer.Length);
            }

            if (currentStyle.strokeColor.A > 0)
            {
                GL.LineWidth(currentStyle.strokeWidth);
                GL.Color4(currentStyle.strokeColor);
                GL.DrawArrays(PrimitiveType.LineLoop, 0, vertbuffer.Length);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws an ellipse (oval) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="location"> The location. </param>
        /// <param name="w">        The width. </param>
        /// <param name="h">        The height. </param>

        public static void ellipse(PointF location, double w, double h)
        {
            ellipse(location.X, location.Y, w, h);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws an ellipse (oval) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="size"> The diameter. </param>

        public static void ellipse(double x, double y, double size)
        {
            ellipse(x, y, size, size);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws an ellipse (oval) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="location"> The location. </param>
        /// <param name="size">     The diameter. </param>

        public static void ellipse(PointF location, double size)
        {
            ellipse(location.X, location.Y, size, size);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws an ellipse (oval) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="location"> The location. </param>
        /// <param name="size">     The width and height. </param>

        public static void ellipse(PointF location, SizeF size)
        {
            ellipse(location.X, location.Y, size.Width, size.Height);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws an ellipse (oval) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="size"> The width and height. </param>

        public static void ellipse(double x, double y, SizeF size)
        {
            ellipse(x, y, size.Width, size.Width);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws an ellipse (oval) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="point1">   The first point. </param>
        /// <param name="point2">   The second point. </param>

        public static void ellipse(Point point1, Point point2)
        {
            Rectangle Rect = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);

            //if (Smooth) { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
            //else { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a line (a direct path between two points) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="point1">   The first point. </param>
        /// <param name="point2">   The second point. </param>

        public static void line(PointF point1, PointF point2)
        {
            line(point1.X, point1.Y, point2.X, point2.Y);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a line (a direct path between two points) to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="x1">   The first x value. </param>
        /// <param name="y1">   The first y value. </param>
        /// <param name="x2">   The second x value. </param>
        /// <param name="y2">   The second y value. </param>

        public static void line(double x1, double y1, double x2, double y2)
        {
            if (currentStyle.strokeColor.A > 0)
            {
                GL.Begin(PrimitiveType.Lines);

                GL.Color3(currentStyle.strokeColor);
                GL.LineWidth(currentStyle.strokeWidth);

                GL.Vertex2(x1, y1);
                GL.Vertex2(x2, y2);

                GL.End();
            }
            //noStroke();
            //stroke(Color.Transparent);
            //ellipse(x1, y1, strokeWidth / 3);
            //ellipse(x2, y2, strokeWidth / 3);
            //point(x1, y1);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a point, a coordinate in space at the dimension of one pixel </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        ///
        /// <returns>   The given point. </returns>

        public static void point(double x, double y)
        {
            if (currentStyle.strokeColor.A > 0)
            {
                if (currentStyle.strokeWidth > 3)
                {
                    int num_segments = 0;

                    num_segments = ceil(5 * sqrt(currentStyle.strokeWidth / 2));

                    Vector2[] vertbuffer = new Vector2[num_segments];

                    Vector2 vector = new Vector2(0, 0);

                    float theta = TWO_PI / (num_segments);

                    float tangetial_factor = tan(theta);
                    float radial_factor = cos(theta);

                    double X = (currentStyle.strokeWidth / 2) * cos(0);//we now start at the start angle
                    double Y = (currentStyle.strokeWidth / 2) * sin(0);


                    for (int i = 0; i < num_segments; i++)
                    {
                        vector.X = (float)(X + x);
                        vector.Y = (float)(Y + y);

                        double tx = -Y;
                        double ty = X;

                        X += tx * tangetial_factor;
                        Y += ty * tangetial_factor;

                        X *= radial_factor;
                        Y *= radial_factor;

                        vertbuffer[i] = vector;
                    }

                    GL.BufferData(BufferTarget.ArrayBuffer, Vector2.SizeInBytes * vertbuffer.Length, vertbuffer.ToArray(), BufferUsageHint.DynamicDraw);
                    GL.Color4(currentStyle.strokeColor);
                    GL.DrawArrays(PrimitiveType.Polygon, 0, vertbuffer.Length);

                }
                else
                {
                    GL.PointSize(currentStyle.strokeWidth);

                    GL.Begin(PrimitiveType.Points);
                    
                    GL.Color4(currentStyle.strokeColor);
                    GL.Vertex2(x, y);

                    GL.End();
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a rectangle to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="w">    The width. </param>
        /// <param name="h">    The height. </param>
        ///
        /// <returns>   The rectangle. </returns>

        public static void rect(double x, double y, double w, double h)
        {
            switch (RectangleMode)
            {
                case drawModes.Corner:
                    quad(x, y, x, y + h, x + w, y + h, x + w, y);
                    break;
                case drawModes.Center:
                    quad(x - w / 2, y - h / 2, x + w / 2, y - h / 2, x + w / 2, y + h / 2, x - w / 2, y + h / 2);
                    break;
                case drawModes.Radius:
                    quad(x - w, y - h, x + w, y - h, x + w, y + h, x - w, y + h);
                    break;
                default:
                    break;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a rectangle to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="location"> The location. </param>
        /// <param name="w">        The width. </param>
        /// <param name="h">        The height. </param>

        public static void rect(PointF location, double w, double h)
        {
            rect(location.X, location.Y, w, h);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a rectangle to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="location"> The location. </param>
        /// <param name="size">     The width and height. </param>

        public static void rect(PointF location, SizeF size)
        {
            rect(location.X, location.Y, size.Width, size.Height);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a rectangle to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="size"> The width and height. </param>

        public static void rect(double x, double y, SizeF size)
        {
            rect(x, y, size.Width, size.Height);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a rectangle to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">        The x coordinate. </param>
        /// <param name="y">        The y coordinate. </param>
        /// <param name="diameter"> The diameter. </param>

        public static void rect(double x, double y, double diameter)
        {
            rect(x, y, diameter, diameter);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a rectangle to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="location"> The location. </param>
        /// <param name="diameter"> The diameter. </param>

        public static void rect(PointF location, double diameter)
        {
            rect(location.X, location.Y, diameter, diameter);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a rectangle to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="point1">   The first point. </param>
        /// <param name="point2">   The second point. </param>
        ///
        /// <returns>   The rectangle. </returns>

        public static void rect(Point point1, Point point2)
        {
            Rectangle Rect = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);
            
            rect(Rect.X, Rect.Y, Rect.Width, Rect.Height);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a rectangle to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="rectangle">    The rectangle to draw. </param>

        public static void rect(RectangleF rectangle)
        {
            rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a quad </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="point1">   The first point. </param>
        /// <param name="point2">   The second point. </param>
        /// <param name="point3">   The third point. </param>
        /// <param name="point4">   The fourth point. </param>

        public static void quad(PointF point1, PointF point2, PointF point3, PointF point4)
        {
            quad(point1.X, point1.Y, point2.X, point2.Y, point3.X, point3.Y, point4.X, point4.Y);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws a quad. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="x1">   The first x value. </param>
        /// <param name="y1">   The first y value. </param>
        /// <param name="x2">   The second x value. </param>
        /// <param name="y2">   The second y value. </param>
        /// <param name="x3">   The third x value. </param>
        /// <param name="y3">   The third y value. </param>
        /// <param name="x4">   The fourth x value. </param>
        /// <param name="y4">   The fourth y value. </param>

        public static void quad(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            if (currentStyle.fillColor.A > 0)
            {
                //GL.Color4(currentStyle.fillColor);
                //GL.DrawArrays(PrimitiveType.Quads, 0, vertbuffer.Length);

                GL.Begin(PrimitiveType.Quads);

                GL.Color4(currentStyle.fillColor);
                GL.Vertex2(x1, y1);
                GL.Vertex2(x2, y2);
                GL.Vertex2(x3, y3);
                GL.Vertex2(x4, y4);

                GL.End();
            }

            if (currentStyle.strokeColor.A > 0)
            {
                //GL.LineWidth(strokeWidth);
                //GL.Color4(currentStyle.strokeColor);
                //GL.DrawArrays(PrimitiveType.LineLoop, 0, vertbuffer.Length);

                GL.Begin(PrimitiveType.LineLoop);

                GL.Color4(currentStyle.strokeColor);
                GL.LineWidth(currentStyle.strokeWidth);

                GL.Vertex2(x1, y1);
                GL.Vertex2(x2, y2);
                GL.Vertex2(x3, y3);
                GL.Vertex2(x4, y4);

                GL.End();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draws text to the screen. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="text">     the alphanumeric symbols to be displayed. </param>
        /// <param name="point">    The location. </param>

        public static void Text(string text, Point point)
        {
            //if (size > 0 && BrushColor != Color.Transparent)
            //{
            //    myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //    myBuffer.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //    myBuffer.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            //    Font font = new Font(FontFamily.GenericSansSerif, size, System.Drawing.FontStyle.Regular);
            //    brush = new SolidBrush(BrushColor);

            //    //SizeF sizef = myBuffer.Graphics.MeasureString(text, font);
            //    StringFormat format = new StringFormat(StringFormatFlags.NoClip);

            //    if (center)
            //    {
            //        format.LineAlignment = StringAlignment.Center;
            //        format.Alignment = StringAlignment.Center; myBuffer.Graphics.DrawString(text, font, brush, point.X, point.Y, format);
            //    }

            //    else
            //        myBuffer.Graphics.DrawString(text, font, brush, point, format);

            //}
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Show the FrameRate from the window into the statusbar. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        //public static void drawFramerate()
        //{
        //    //if (BrushColor != Color.Transparent)
        //    //{
        //    //    bool center = CenterText();

        //    //    CenterText(false);

        //    //    Text("FPS: " + frameRate(), new Point(0, 0));
        //    //    CenterText(center); 
        //    //}

        //    window.Title = "CCreative | " + window.RenderFrequency.ToString("n") + " FPS";
        //}

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Shows the total time that the program is running into the status bar </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        //public static void drawTotalTime()
        //{
        //    window.Title = "CCreative | TotalTime: " + totalTime.Seconds;
        //}

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Shows the average framerate into the status bar </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        //public static void drawAverageFramerate()
        //{
        //    window.Title = "CCreative | Average: " + averageFramerate().ToString("n") + " FPS";
        //}

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the fontsize for the Text() method. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="fontsize"> The fontsize. </param>
        ///
        /// <returns>   The fontsize. </returns>

        public static float textSize(double fontsize)
        {
            size = (float)fontsize;
            return size;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Rotates a shape the amount specified by the angle parameter. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="angle">    The angle of rotation, specified in radians or degrees, depending on current angleMode. </param>
        ///
        /// <returns>   The angle. </returns>

        public static float rotate(double angle)
        {
            if (currentStyle.angleMode == angleModes.Radians)
                GL.Rotate((angle * PI / 180), 0, 0, 1);
            else
                GL.Rotate(angle, 0, 0, 1);

            return (float)angle;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Centers the text around the point </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="Center">   If the text needs to be centerd around the point. </param>
        ///
        /// <returns>   if the text is being centerd around the point. </returns>

        public static bool CenterText(bool Center)
        {
            center = Center;
            return center;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns if text text is being centerd around the point. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <returns>   if the text is being centerd around the point. </returns>

        public static bool CenterText()
        {
            return center;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Using the beginShape() and endShape() functions allow creating more complex forms. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///

        public static void beginShape()
        {
            points.Clear();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   All shapes are constructed by connecting a series of vertices. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="x">    x-coordinate of the vertex. </param>
        /// <param name="y">    y-coordinate of the vertex. </param>

        public static void vertex(double x, double y)
        {
            points.Add(new Vector2((float)x, (float)y));
            //GL.Vertex2()
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets vertecies for the shape with the given points. </summary>
        ///
        /// <remarks>   Jan Tamis, 30-8-2017. </remarks>
        ///
        /// <param name="vertecies">    The vertecies. </param>

        public static void vertex(PointF[] vertecies)
        {
            foreach (PointF item in vertecies)
            {
                vertex(item);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets a vertex for the shape with the given point. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="point">    The point. </param>

        public static void vertex(PointF point)
        {
            points.Add(new Vector2(point.X + translated.X, point.Y + translated.Y));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The endShape() function is the companion to beginShape() and may only be called after beginShape(). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public static void endShape()
        {
            endShape(false);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The endShape() function is the companion to beginShape() and may only be called after beginShape(). </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
        ///
        /// <param name="Connect">  Connect the ending and beginning of the shape. </param>

        public static void endShape(bool Connect)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            if (currentStyle.fillColor.A > 0 || currentStyle.strokeColor.A > 0)
            {
                GL.BufferData(BufferTarget.ArrayBuffer, Vector2.SizeInBytes * points.Count, points.ToArray(), BufferUsageHint.DynamicDraw);
            }

            if (currentStyle.fillColor.A > 0)
            {
                GL.Color3(currentStyle.fillColor);
                GL.DrawArrays(PrimitiveType.Polygon, 0, points.Count);
            }

            if (currentStyle.strokeColor.A > 0)
            {
                if (Connect)
                {
                    GL.LineWidth(currentStyle.strokeWidth);
                    GL.Color3(currentStyle.strokeColor);

                    GL.DrawArrays(PrimitiveType.LineLoop, 0, points.Count);
                }
                else
                {
                    GL.LineWidth(currentStyle.strokeWidth);
                    GL.Color3(currentStyle.strokeColor);

                    GL.DrawArrays(PrimitiveType.LineStrip, 0, points.Count);
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads the pixel data for the display window into the pixels[] array. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        public static void loadPixels()
        {
            //    if (!buffer.IsLoaded)
            //    {
            //        buffer.Load();
            //    }
            //    buffer.Clear();
            //}
            img.Clear(Color.Transparent);

            img.Lock();
        }

            ///-------------------------------------------------------------------------------------------------
            /// <summary>   Changes the color of any pixel, or writes an image directly to the display window. </summary>
            ///
            /// <remarks>   Jan Tamis, 27-8-2017. </remarks>
            ///
            /// <param name="x">        x-coordinate of the pixel. </param>
            /// <param name="y">        y-coordinate of the pixel. </param>
            /// <param name="color">    The color to fill the pixel. </param>

        public static void set(int x, int y, Color color)
        {
            //if (!img.Locked)
            //{
            //    img.Lock();
            //}

            img.SetPixel(x, y, color);

            //pixels[y * width + x] = color;
            //buffer.AddVertex(x, y, 0xffffff);//ColorToUInt(color));
        }

        private static uint ColorToUInt(Color c)
        {
            return (uint)(((c.A << 24) | (c.R << 16) | (c.G << 8) | c.B) & 0xffffffffL);

        }


        public static Color[] pixels { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Updates the display window with the data in the pixels[] array. </summary>
        ///
        /// <remarks>   Jan Tamis, 27-8-2017. </remarks>

        public static void updatePixels()
        {
            //GL.Begin(PrimitiveType.Points);
            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < height; j++)
            //    {
            //        if (pixels[j * width + i].A > 0)
            //        {
            //            GL.Color4(pixels[j * width + i]);
            //            GL.Vertex2(i, j);
            //        }
            //    }
            //}
            //GL.End();
            //GL.DrawPixels<byte>(width, height, OpenTK.Graphics.OpenGL.PixelFormat., PixelType.Byte, pixels);
            //buffer.Bind();

            GL.BindTexture(TextureTarget.Texture2D, textureBuffer);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, img.Scan0);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex2(width, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex2(width, height);

            GL.TexCoord2(0, 1);
            GL.Vertex2(0, height);

            GL.End();
            img.Unlock();
        }

        /// <summary>
        /// Images the specified image.
        /// </summary>
        /// <param name="Image">The image.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>

        public static void image(PImage Image, double x, double y, double width, double height)
        {
            GL.BindTexture(TextureTarget.Texture2D, (int)Image.textureID);
            
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(tintColor);

            switch (RectangleMode)
            {
                case drawModes.Center:

                    GL.TexCoord2(0, 0);
                    GL.Vertex2(x - width / 2, y - height / 2);

                    GL.TexCoord2(1, 0);
                    GL.Vertex2(x + width / 2, y - height / 2);

                    GL.TexCoord2(1, 1);
                    GL.Vertex2(x + width / 2, y + height / 2);

                    GL.TexCoord2(0, 1);
                    GL.Vertex2(x - width / 2, y + height / 2);

                    break;
                case drawModes.Radius:

                    GL.TexCoord2(0, 0);
                    GL.Vertex2(x - width, y - width);

                    GL.TexCoord2(1, 0);
                    GL.Vertex2(x + width, y - width);

                    GL.TexCoord2(1, 1);
                    GL.Vertex2(x + width, y + height);

                    GL.TexCoord2(0, 1);
                    GL.Vertex2(x - width, y + height);

                    break;
                case drawModes.Corner:

                    GL.TexCoord2(0, 0);
                    GL.Vertex2(x, y);

                    GL.TexCoord2(1, 0);
                    GL.Vertex2(x + Image.width, y);

                    GL.TexCoord2(1, 1);
                    GL.Vertex2(x + Image.width, y + Image.height);

                    GL.TexCoord2(0, 1);
                    GL.Vertex2(x, y + Image.height);

                    break;
                default:
                    break;
            }
            
            GL.End();
        }

        /// <summary>
        /// Draws a given image at the given point and the size of the image.
        /// </summary>

        /// <param name="Image">The image to draw.</param>
        /// <param name="x">The x axis.</param>
        /// <param name="y">The y axis.</param>

        public static void image(PImage Image, double x, double y)
        {
            image(Image, x, y, Image.width, Image.height);
        }

        /// <summary>
        /// Draws a triangle form the given points.
        /// </summary>

        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>

        public static void triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            if (currentStyle.fillColor.A > 0)
            {
                GL.Begin(PrimitiveType.Triangles);

                GL.Color4(currentStyle.fillColor);
                GL.Vertex2(x1, y1);
                GL.Vertex2(x2, y2);
                GL.Vertex2(x3, y3);

                GL.End();
            }

            if (currentStyle.strokeColor.A > 0)
            {
                GL.Begin(PrimitiveType.LineLoop);

                GL.Color4(currentStyle.strokeColor);
                GL.LineWidth(currentStyle.strokeWidth);

                GL.Vertex2(x1, y1);
                GL.Vertex2(x2, y2);
                GL.Vertex2(x3, y3);

                GL.End();
            }
        }

        /// <summary>
        /// Draws a triangle form the given points.
        /// </summary>

        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <param name="point3">The point3.</param>

        public static void triangle(PointF point1, PointF point2, PointF point3)
        {
            triangle(point1.X, point1.Y, point2.X, point2.Y, point3.X, point3.Y);
        }

        /// <summary>
        /// Sets the tint color for the images.
        /// </summary>

        /// <param name="tintcolor">The tintcolor.</param>

        public static void Tint(Color tintcolor)
        {
            tintColor = tintcolor;
        }

        /// <summary>
        /// Returns the current tint color.
        /// </summary>

        /// <returns> the current tint color.</returns>

        public static Color Tint()
        {
            return tintColor;
        }

        /// <summary>
        /// Removes the tint
        /// </summary>

        public static void noTint()
        {
            tintColor = Color.White;
        }
    }
}