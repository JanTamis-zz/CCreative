using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CCreative.Colors;
using static CCreative.Data;
using static CCreative.General;
using static CCreative.Math;
using static CCreative.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using CCreative.FastBitmapLib;
using System.Drawing.Drawing2D;

namespace CCreative
{
    public static class Drawing
    {
        [DllImport("shlwapi.dll")]
        static extern int ColorHLSToRGB(int H, int L, int S);
        
        static PointF translated = new Point(0, 0);
        static Color backgroundColor;

        static List<PointF> points = new List<PointF>();

        static Bitmap image;
        static FastBitmap bitmap;
        static float Angle = 0;

        static TextLocation location = TextLocation.None;
        static drawModes CircleMode = drawModes.Center;
        static drawModes RectangleMode = drawModes.Corner;
        static Pen pen;
        static Brush brush;
        static bool Smooth = true;
        static bool center = true;
        static float scaleFactor = 1;

        public static BufferedGraphicsContext currentContext;
        public static BufferedGraphics myBuffer;
        // Gets a reference to the current BufferedGraphicsContext

        private static Color BrushColor;
        private static Color PenColor;
        private static float PenWidht;
        private static float size = 15;

        static Color tempBrushColor;
        static Color tempPenColor;
        static float tempPenWidht;
        static float tempSize;
        static float tempAngle;
        static bool tempCenter;
        static drawModes tempCircleMode;
        static drawModes tempRectangleMode;
        static PointF temptTranslated;
        static colorModes tempColormode;
        

        ///<summary>
        ///the possible modes to draw the shapes
        ///</summary>
        public enum drawModes
        {
            Center,
            Radius,
            Corner,
            Corners
        }

        ///<summary>
        ///The possible places om the screen to draw the text.
        ///</summary>
        public enum TextLocation
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            None
        }

        ///<summary>
        ///A constant that is equal to TextLocation.TopLeft.
        ///</summary>
        public static TextLocation TOPLEFT
        {
            get { return TextLocation.TopLeft; }
        }

        ///<summary>
        ///A constant that is equal to TextLocation.TopRight.
        ///</summary>
        public static TextLocation TOPRIGHT
        {
            get { return TextLocation.TopRight; }
        }

        ///<summary>
        ///A constant that is equal to TextLocation.BottomLeft.
        ///</summary>
        public static TextLocation BOTTOMLEFT
        {
            get { return TextLocation.BottomLeft; }
        }

        ///<summary>
        ///A constant that is equal to TextLocation.BotomRight.
        ///</summary>
        public static TextLocation BOTTOMRIGHT
        {
            get { return TextLocation.BottomRight; }
        }

        ///<summary>
        ///A constant that is equal to TextLocation.None.
        ///</summary>
        public static TextLocation NONE
        {
            get { return TextLocation.None; }
        }

        ///<summary>
        ///A constant that is equal to drawModes.Center.
        ///</summary>
        public static drawModes CENTER
        {
            get { return drawModes.Center; }
        }

        ///<summary>
        ///A constant that is equal to drawModes.Radius.
        ///</summary>
        public static drawModes RADIUS
        {
            get { return drawModes.Radius; }
        }

        ///<summary>
        ///A constant that is equal to drawModes.Corner.
        ///</summary>
        public static drawModes CORNER
        {
            get { return drawModes.Corner; }
        }

        ///<summary>
        ///A constant that is equal to drawModes.Corners.
        ///</summary>
        public static drawModes CORNERS
        {
            get { return drawModes.Corners; }
        }

        ///<summary>
        ///A constant that is equal to drawModes.Center.
        ///</summary>
        private static PointF translatePoint(PointF point)
        {
            PointF newPoint = point;
            newPoint.X = translated.X;
            newPoint.Y = translated.Y;
            return newPoint;
        }

        ///<summary>
        ///Chances the origin of the canvas to the given X and Y values.
        ///</summary>
        public static PointF translate(double x, double y)
        {
            return translate(new PointF((float)x, (float)y));
        }

        ///<summary>
        ///Chances the origin of the canvas to the given point.
        ///</summary>
        public static PointF translate(PointF point)
        {
            translated = new PointF(point.X, point.Y);
            return translated;
        }

        ///<summary>
        ///Returns the current origin.
        ///</summary>
        public static PointF translate()
        {
            return translated;
        }

        ///<summary>
        ///Scales the next shapes with the given scalefactor.
        ///</summary>
        public static void scale(double ScaleFactor)
        {
            scaleFactor = (float)ScaleFactor;
        }

        ///<summary>
        ///Returns the current scalefactor.
        ///</summary>
        public static float scale()
        {
            return scaleFactor;
        }

        ///<summary>
        ///Saves the current configuration.
        ///</summary>
        public static void pushMatrix()
        {
            tempBrushColor = BrushColor;
            tempPenColor = PenColor;
            tempPenWidht = PenWidht;
            tempSize = size;
            tempAngle = Angle;
            tempCenter = center;
            tempCircleMode = CircleMode;
            tempRectangleMode = RectangleMode;
            temptTranslated = translated;
            tempColormode = colorMode();
        }

        ///<summary>
        ///Restores the saved configuration from the pushMatrix method.
        ///</summary>
        public static void popMatrix()
        {
            BrushColor = tempBrushColor;
            PenColor = tempBrushColor;
            size = tempSize;
            Angle = tempAngle;
            center = tempCenter;
            CircleMode = tempCircleMode;
            RectangleMode = tempRectangleMode;
            translated = temptTranslated;
            colorMode(tempColormode);
        }

        ///<summary>
        ///Sets the textlocation of the text.
        ///</summary>
        public static TextLocation textLocation(TextLocation Location)
        {
            location = Location;
            return location;
        }

        ///<summary>
        ///Returns the current textlocation.
        ///</summary>
        public static TextLocation textLocation()
        {
            return location;
        }

        ///<summary>
        ///Fills the next shape with the given color.
        ///</summary>
        public static Color fill(Color DrawColor)
        {
            BrushColor = DrawColor;
            return DrawColor;
        }

        private static Color calculateColor(int value)
        {
            Color clr = Color.Transparent;
            if (colorMode() == colorModes.HSB)
            {
                value = floor(constrain(value, 0, 360));
                clr = hueToColor(value);
            }
            else if (colorMode() == colorModes.RGB)
            {
                value = floor(constrain(value, 0, 255));
                clr = Color.FromArgb(255, value, value, value);
            }
            return clr;
        }

        ///<summary>
        ///Clears all the drawings and fills the background with the given color.
        ///</summary>
        public static Color background(Color backcolor)
        {
            Drawing.myBuffer.Graphics.Clear(backcolor);
            backgroundColor = backcolor;
            return backcolor;
        }

        ///<summary>
        ///Clears all the drawings and fills the background with the given color, the color depents on the colormode.
        ///<para>RGB: a color from the given grayscale value.</para>
        ///<para>HSB: a color from the given hue value.</para> 
        ///</summary>
        public static Color background(int value)
        {
            return background(calculateColor(value));
        }

        ///<summary>
        ///Returns the background color.
        ///</summary>
        public static Color background()
        {
            return backgroundColor;
        }

        ///<summary>
        ///Fills the next shapes with the given color, the color depents on the colormode
        ///<para>RGB: a color from the given grayscale value.</para>
        ///<para>HSB: a color from the given hue value.</para> 
        ///</summary>
        public static Color fill(int value)
        {
            BrushColor = calculateColor(value);
            return BrushColor;
        }

        ///<summary>
        ///Fills the next shapes with the given color and transparancy, the color depents on the colormode
        ///<para>RGB: a color from the given grayscale value.</para>
        ///<para>HSB: a color from the given hue value.</para> 
        ///</summary>
        public static Color fill(int value, int transparancy)
        {
            BrushColor = Color.FromArgb((int)constrain(value, 0, 255), calculateColor(value));
            return BrushColor;
        }

        ///<summary>
        ///Returns the fill color of the shapes.
        ///</summary>
        public static Color fill()
        {
            return BrushColor;
        }

        ///<summary>
        ///Returns the stroke color of the shapes.
        ///</summary>
        public static Color stroke()
        {
            return PenColor;
        }

        private static Color hueToColor(int hue)
        {
            hue = (int)map(hue, 0, 360, 0, 240);
            return ColorTranslator.FromWin32(ColorHLSToRGB(hue, 120, 240));
        }

        ///<summary>
        ///Don't fill the next shapes.
        ///</summary>
        public static void noFill()
        {
            BrushColor = Color.Transparent;
        }

        ///<summary>
        ///Draw a stroke around the next shapes with the given color.
        ///</summary>
        public static Color stroke(Color DrawColor)
        {
            PenColor = DrawColor;
            return DrawColor;
        }

        ///<summary>
        ///Draw a stroke around the next shapes, the color depents on the colormode.
        ///<para>RGB: a color from the given grayscale value.</para>
        ///<para>HSB: a color from the given hue value.</para> 
        ///</summary>
        public static Color stroke(int value)
        {
            PenColor = calculateColor(value);
            return PenColor;
        }

        ///<summary>
        ///Gives the stroke a thickness/weight that depents on the given weight.
        ///</summary>
        public static float strokeWeight(double weight)
        {
            PenWidht = (float)weight;
            return PenWidht;
        }

        ///<summary>
        ///Don't draw a stroke around the next shapes.
        ///</summary>
        public static void noStroke()
        {
            PenColor = Color.Transparent;
        }

        ///<summary>
        ///Use AntiAliasing for the next shapes.
        ///</summary>
        public static void smooth()
        {
            myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            myBuffer.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            myBuffer.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            Smooth = true;
        }

        ///<summary>
        ///Returns if the shapes are drawn with AntiAliasing.
        ///</summary>
        public static bool smoothing
        {
            get { return Smooth; }
        }

        ///<summary>
        ///Don't use AntiAliasing for the next shapes.
        ///</summary>
        public static void noSmooth()
        {
            myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            myBuffer.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            myBuffer.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

            Smooth = false;
        }

        ///<summary>
        ///Set the way how ellipses are drawn.
        ///</summary>
        public static void ellipseMode(drawModes modes)
        {
            CircleMode = modes;
        }

        ///<summary>
        ///Set the way how rectangles are drawn.
        ///</summary>
        public static void rectMode(drawModes modes)
        {
            RectangleMode = modes;
        }

        private static void transform()
        {
            using (Matrix m = new Matrix())
            {
                if (translated.X != 0 || translated.Y != 0)
                    m.Translate(translated.X, translated.Y);
                if (scaleFactor != 1)
                    m.Scale(scaleFactor, scaleFactor);
                if (Angle != 0)
                    m.RotateAt(Angle, new PointF(translated.X, translated.Y));

                myBuffer.Graphics.Transform = m;
            }
        }

        ///<summary>
        ///Draw a ellipse with the given x, y location and a width and height.</summary>
        ///<remarks>Use the fill() method to change the ellipses fill color.</remarks>
        ///<para>Use the stroke() method to change the ouline color.</para> 
        ///<para>Use the strokeWeight() method to change the thickness/weight of the ouline.</para> 
        ///<param name="x"> Parameter description for s goes here.</param>

        public static void ellipse(double x, double y, double w, double h)
        {
            if (renderer() == GDI)
            {
                RectangleF Rect = new RectangleF();
                PointF point = new PointF((float)x, (float)y);

                switch (CircleMode)
                {
                    case drawModes.Corner:
                        Rect = new RectangleF(point.X, point.Y, (float)w, (float)h);
                        break;
                    case drawModes.Center:
                        Rect = new RectangleF(point.X - (float)w / 2, point.Y - (float)h / 2, (float)w, (float)h);
                        break;
                    case drawModes.Radius:
                        Rect = new RectangleF(point.X - (float)w, point.Y - (float)h, (float)w * 2, (float)h * 2);
                        break;
                    default:
                        break;
                }

                transform();

                if (BrushColor.A > 0)
                {
                    brush = new SolidBrush(BrushColor);
                    myBuffer.Graphics.FillEllipse(brush, Rect);
                }
                if (PenColor.A > 0)
                {
                    pen = new Pen(PenColor, PenWidht);
                    myBuffer.Graphics.DrawEllipse(pen, Rect);
                }

                myBuffer.Graphics.ResetTransform(); 
            }
        }

        ///<summary>
        ///Draw a ellipse with the given location and a width and height.
        ///<para>Use the fill() method to change the ellipses fill color.</para>
        ///<para>Use the stroke() method to change the ouline color.</para> 
        ///<para>Use the strokeWeight() method to change the thickness/weight of the ouline.</para> 
        ///</summary>
        public static void ellipse(PointF center, double w, double h)
        {
            ellipse(center.X, center.Y, w, h);
        }

        ///<summary>
        ///Draw a ellipse with the given location and size
        ///</summary>
        public static void ellipse(PointF center, double s)
        {
            ellipse(center.X, center.Y, s, s);
        }

        ///<summary>
        ///Draw a ellipse with the given x, y location and a size
        ///</summary>
        public static void ellipse(double x, double y, double s)
        {
            ellipse(x, y, s, s);
        }

        ///<summary>
        ///Draw a ellipse with the given locations.
        ///</summary>
        public static void ellipse(PointF point1, PointF point2)
        {
            if (renderer() == GDI)
            {
                if (point2.X < point1.X || point2.Y < point1.Y)
                {
                    float x = point1.X;
                    float y = point1.Y;

                    point1 = point2;
                    point2 = new PointF(x, y);
                }

                RectangleF Rect = new RectangleF(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);

                transform();

                if (BrushColor.A != 0)
                {
                    brush = new SolidBrush(BrushColor);
                    myBuffer.Graphics.FillEllipse(brush, Rect);
                }
                if (PenColor.A != 0)
                {
                    pen = new Pen(PenColor, PenWidht);
                    myBuffer.Graphics.DrawEllipse(pen, Rect);
                }
                myBuffer.Graphics.ResetTransform(); 
            }
        }

        ///<summary>
        ///Draw a line beween two points with the given stroke values;
        ///</summary>
        public static void line(Point point1, Point point2)
        {
            line(point1.X, point1.Y, point2.X, point2.Y);
        }

        public static PointF point(double x, double y)
        {
            return point(new PointF((float)x, (float)y));
        }

        public static PointF point(PointF point)
        {
            ellipse(point.X, point.Y, PenWidht);
            return point;
        }

        public static void line(double x1, double y1, double x2, double y2)
        {
            if (renderer() == GDI)
            {
                if (PenColor.A != 0)
                {
                    transform();

                    pen = new Pen(PenColor, PenWidht);
                    pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
                    myBuffer.Graphics.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);

                    myBuffer.Graphics.ResetTransform();
                } 
            }
        }

        public static void rect(double x, double y, double w, double h)
        {
            if (renderer() == GDI)
            {
                Rectangle Rect = new Rectangle();

                switch (RectangleMode)
                {
                    case drawModes.Corner:
                        Rect = new Rectangle((int)x, (int)y, (int)w, (int)h);
                        break;
                    case drawModes.Center:
                        Rect = new Rectangle((int)x - (int)w / 2, (int)y - (int)h / 2, (int)w, (int)h);
                        break;
                    case drawModes.Radius:
                        Rect = new Rectangle((int)x - (int)w, (int)y - (int)h, (int)w * 2, (int)h * 2);
                        break;
                    default:
                        break;
                }

                transform();

                if (BrushColor.A > 0)
                {
                    myBuffer.Graphics.FillRectangle(new SolidBrush(BrushColor), Rect);
                }
                if (PenColor.A > 0)
                {
                    myBuffer.Graphics.DrawRectangle(new Pen(PenColor, PenWidht), Rect);
                }

                myBuffer.Graphics.ResetTransform(); 
            }
        }

        public static void rect(PointF center, double w, double h)
        {
            rect(center.X, center.Y, w, h);
        }

        public static void rect(RectangleF Rect)
        {
            rect(Rect.X, Rect.Y, Rect.Width, Rect.Height);
        }

        public static void rect(double x, double y, double s)
        {
            rect(x, y, s, s);
        }

        public static void rect(PointF center, double s)
        {
            rect(center.X, center.Y, s, s);
        }

        public static void rect(Point point1, Point point2)
        {
            Rectangle Rect = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);

            if (point1.X > 0 && point1.Y > 0 && point2.X > 0 && point2.Y > 0)
            {
                rect(Rect);
            }
        }

        public static void quad(PointF point1, PointF point2, PointF point3, PointF point4)
        {
            if (renderer() == GDI)
            {
                PointF[] pointarray =
                        {
                        point1,
                        point2,
                        point3,
                        point4
                };

                transform();

                if (BrushColor.A != 0)
                {
                    myBuffer.Graphics.FillPolygon(new SolidBrush(BrushColor), pointarray);
                }
                if (PenColor.A != 0)
                {
                    myBuffer.Graphics.DrawPolygon(new Pen(PenColor, PenWidht), pointarray);
                }

                myBuffer.Graphics.ResetTransform(); 
            }
        }

        public static void quad(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            quad(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), new PointF((float)x3, (float)y3), new PointF((float)x4, (float)y4));
        }

        public static void Text(string text, Point point)
        {
            if (renderer() == GDI)
            {
                if (size > 0 && BrushColor.A != 0)
                {
                    Font font = new Font(FontFamily.GenericSansSerif, size, System.Drawing.FontStyle.Regular);

                    brush = new SolidBrush(BrushColor);
                    StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                    Size size2 = TextRenderer.MeasureText(text, font);

                    transform();

                    switch (location)
                    {
                        case TextLocation.TopLeft:
                            point = new Point(0, 0);
                            break;
                        case TextLocation.TopRight:
                            point = new Point(width - size2.Width - 2, 0);
                            break;
                        case TextLocation.BottomLeft:
                            point = new Point(0, height - size2.Height);
                            break;
                        case TextLocation.BottomRight:
                            point = new Point(width - size2.Width - 2, height - size2.Height);
                            break;
                        default:
                            break;
                    }

                    if (center)
                    {
                        format.LineAlignment = StringAlignment.Center;
                        myBuffer.Graphics.DrawString(text, font, brush, point);
                    }
                    else
                    {
                        myBuffer.Graphics.DrawString(text, font, brush, point, format);
                    }

                    myBuffer.Graphics.ResetTransform();
                } 
            }
        }

        public static void triangle(PointF point1, PointF point2, PointF point3)
        {
            transform();

            if (BrushColor.A > 0)
            {
                myBuffer.Graphics.FillPolygon(new SolidBrush(BrushColor), new PointF[] { point1, point2, point3 });
            }
            if (PenColor.A > 0)
            {
                myBuffer.Graphics.DrawPolygon(new Pen(PenColor, PenWidht), new PointF[] { point1, point2, point3 });
            }

            myBuffer.Graphics.ResetTransform();
        }

        public static void triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            triangle(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), new PointF((float)x3, (float)y3));
        }

        public static void drawFramerate()
        {
            pushMatrix();
            rotate(0);
            translate(0, 0);
            scale(1);

            CenterText(false);
            textLocation(TOPLEFT);

            Text("FPS: " + frameRate(), new Point(0, 0));
            popMatrix();
        }

        public static void drawTotalTime()
        {
            pushMatrix();

            rotate(0);
            translate(0, 0);
            scale(1);

            CenterText(false);
            textLocation(TOPRIGHT);

            if (totalTime.Hours != 0)
            {
                Text(string.Format("TotalTime: {0}:{1}:{2}", totalTime.ToString("hh"), totalTime.ToString("mm"), totalTime.ToString("ss")), new Point(0, 0));
            }
            else if (totalTime.Hours == 0)
            {
                Text(string.Format("TotalTime: {0}:{1}", totalTime.ToString("mm"), totalTime.ToString("ss")), new Point(0, 0));
            }

            popMatrix();
        }

        public static float textSize(double fontsize)
        {
            size = (float)fontsize;
            return size;
        }

        public static float rotate(double angle)
        {
            Angle = (float)angle;
            return Angle;
        }
        public static float rotate()
        {
            return Angle;
        }

        public static bool CenterText(bool Center)
        {
            center = Center;
            return center;
        }

        public static bool CenterText()
        {
            return center;
        }

        public static void beginShape()
        {
            points = new List<PointF>();
        }

        public static void vertex(double x, double y)
        {
            vertex(new PointF((float)x, (float)y));
        }

        public static void vertex(PointF point)
        {
            if (renderer() == GDI)
            {
                points.Add(point); 
            }
        }

        public static void endShape()
        {
            endShape(false);
        }

        public static void endShape(bool Connect)
        {
            if (points.Count > 0 && renderer() == GDI)
            {
                transform();
                if (Connect)
                {
                    if (PenColor.A != 0)
                    {
                        myBuffer.Graphics.DrawPolygon(new Pen(PenColor, PenWidht), points.ToArray());
                    }
                    if (BrushColor != Color.Transparent)
                    {
                        myBuffer.Graphics.FillPolygon(new SolidBrush(BrushColor), points.ToArray());
                    }
                }
                else
                {
                    if (PenColor.A != 0)
                    {
                        myBuffer.Graphics.DrawPolygon(new Pen(PenColor, PenWidht), points.ToArray());
                    }
                    if (BrushColor.A != 0)
                    {
                        myBuffer.Graphics.FillPolygon(new SolidBrush(BrushColor), points.ToArray());
                    }
                }

                myBuffer.Graphics.ResetTransform();
            }
        }

        public static void resetOrigin()
        {
            translated.X = 0;
            translated.Y = 0;
        }
        
        public static void loadPixels()
        {
            image = new Bitmap(width, height);
            bitmap = new FastBitmap(image);
            bitmap.Lock();
        }

        public static void set(int x, int y, Color color)
        {
            x = (int)constrain(x, 0, width);
            y = (int)constrain(y, 0, height);

            bitmap.SetPixel(x, y, color);
        }

        public static void updatePixels()
        {
            bitmap.Unlock();
            myBuffer.Graphics.DrawImage(image, 0, 0);
        }

        public static PointF centerPolygon(PointF[] vertices)
        {
            PointF centroid = new PointF();
            double signedArea = 0.0;
            double x0 = 0.0; // Current vertex X
            double y0 = 0.0; // Current vertex Y
            double x1 = 0.0; // Next vertex X
            double y1 = 0.0; // Next vertex Y
            double a = 0.0;  // Partial signed area

            // For all vertices except last
            int i = 0;
            for (i = 0; i < vertices.Length - 1; ++i)
            {
                x0 = vertices[i].X;
                y0 = vertices[i].Y;
                x1 = vertices[i + 1].X;
                y1 = vertices[i + 1].Y;
                a = x0 * y1 - x1 * y0;
                signedArea += a;
                centroid.X += (float)((x0 + x1) * a);
                centroid.Y += (float)((y0 + y1) * a);
            }

            // Do last vertex
            x0 = vertices[i].X;
            y0 = vertices[i].Y;
            x1 = vertices[0].X;
            y1 = vertices[0].Y;
            a = x0 * y1 - x1 * y0;
            signedArea += a;
            centroid.X += (float)((x0 + x1) * a);
            centroid.Y += (float)((y0 + y1) * a);

            signedArea *= 0.5;
            centroid.X /= (float)(6 * signedArea);
            centroid.Y /= (float)(6 * signedArea);

            return centroid;
        }
    }
}