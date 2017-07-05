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

namespace CCreative
{
    public static class Drawing
    {

        public static colorModes colormode = colorModes.RGB;
        private static PointF translated = new Point(0, 0);

        public enum circleModes
        {
            Center,
            Radius,
            Corner,
            Corners
        }

        public enum rectagleModes
        {
            Center,
            Radius,
            Corner,
            Corners
        }

        private static PointF translatePoint(PointF point)
        {
            PointF newPoint = point;
            newPoint.X += translated.X;
            newPoint.Y += translated.Y;
            return newPoint;
        }

        private static Point translatePoint(Point point)
        {
            Point newPoint = point;
            newPoint.X += (int)translated.X;
            newPoint.Y += (int)translated.Y;
            return newPoint;
        }

        public static PointF translate(float x, float y)
        {
            translated = new PointF(x, y);
            return translated;
        }

        public static PointF translate()
        {
            return translated;
        }

        public static PointF translate(PointF point)
        {
            translated = point;
            return translated;
        }

        public static PointF translate(Point point)
        {
            translated = point;
            return translated;
        }

        private static circleModes CircleMode = circleModes.Center;
        private static rectagleModes RectangleMode = rectagleModes.Corner;
        public static Panel canvas;
        public static Pen pen;
        public static Brush brush;// = new Brush(Color.Transparent);
        private static bool Smooth = true;
        private static bool center = true;

        public static BufferedGraphicsContext currentContext;
        public static BufferedGraphics myBuffer;
        // Gets a reference to the current BufferedGraphicsContext

        public static Color BrushColor;
        public static Color PenColor;
        public static float PenWidht;
        private static float size = 12;

        public static Color fill(Color DrawColor)
        {
            BrushColor = DrawColor;
            return DrawColor;
        }

        public static Color fill(int value, int transparancy = 255)
        {
            if (colormode == colorModes.HSB)
            {
                BrushColor = hueToColor(value);
            }
            else if (colormode == colorModes.RGB)
            {
                BrushColor = Color.FromArgb(transparancy, value, value, value);
            }
            return BrushColor;
        }

        private static Color hueToColor(int hue)
        {
            hue = (int)map(hue, 0, 360, 0, 240);
            return ColorTranslator.FromWin32(ColorHLSToRGB(hue, 120, 240));
        }

        public static void noFill()
        {
            BrushColor = Color.Transparent;
        }

        public static Color stroke(Color DrawColor)
        {
            PenColor = DrawColor;
            return DrawColor;
        }

        public static Color stroke(int value)
        {
            
            if (colormode == colorModes.HSB)
            {
                value = (int)constrain(value, 0, 360);
                int Hue = (int)map(value, 0, 360, 0, 240);
                PenColor = ColorTranslator.FromWin32(ColorHLSToRGB(Hue, 120, 240));
            }
            else if (colormode == colorModes.RGB)
            {
                PenColor = Color.FromArgb(value, value, value);
            }


            return PenColor;
        }

        public static float strokeWeight(double weight)
        {
            PenWidht = (float)weight;
            return PenWidht;
        }

        public static void noStroke()
        {
            PenColor = Color.Transparent;
        }

        public static void smooth()
        {
            Smooth = true;
        }

        public static void noSmooth()
        {
            Smooth = false;
        }

        public static void ellipseMode(circleModes modes)
        {
            CircleMode = modes;
        }

        public static void rectMode(rectagleModes modes)
        {
            RectangleMode = modes;
        }

        public static RectangleF ellipse(double x, double y, double w, double h)
        {
            RectangleF Rect = new RectangleF();
            PointF point = translatePoint(new PointF((float)x, (float)y));

            switch (CircleMode)
            {
                case circleModes.Corner:
                    Rect = new RectangleF(point.X, point.Y, (float)w, (float)h);
                    break;
                case circleModes.Center:
                    Rect = new RectangleF(point.X - (float)w / 2, point.Y - (float)h / 2, (float)w, (float)h);
                    break;
                case circleModes.Radius:
                    Rect = new RectangleF(point.X - (float)w, point.Y - (float)h, (float)w * 2, (float)h * 2);
                    break;
                default:
                    break;
            }

            if (Smooth) { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
            else { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }

            brush = new SolidBrush(BrushColor);
            pen = new Pen(PenColor, PenWidht);

            myBuffer.Graphics.FillEllipse(brush, Rect);
            myBuffer.Graphics.DrawEllipse(pen, Rect);
            pen = null;
            brush = null;

            return Rect;
        }

        public static Rectangle ellipse(Point point1, Point point2)
        {
            point1 = translatePoint(point1);
            point2 = translatePoint(point2);

            Rectangle Rect = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);

            using (Graphics grapics = canvas.CreateGraphics())
            {
                if (Smooth) { grapics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
                else { grapics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }

                brush = new SolidBrush(BrushColor);
                pen = new Pen(PenColor, PenWidht);

                grapics.FillEllipse(brush, Rect);
                grapics.DrawEllipse(pen, Rect);
                pen = null;
                brush = null;
            }
            return Rect;
        }

        public static void line(Point point1, Point point2)
        {
            point1 = translatePoint(point1);
            point2 = translatePoint(point2);

            if (Smooth) { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
            else { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }

            pen = new Pen(PenColor, PenWidht);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);

            myBuffer.Graphics.DrawLine(pen, point1, point2);
            pen = null;
            brush = null;
        }

        public static void line(double x1, double y1, double x2, double y2)
        {
            x1 += translated.X;
            y1 += translated.Y;

            x2 += translated.X;
            y2 += translated.Y;

            if (Smooth) { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
            else { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }

            brush = new SolidBrush(BrushColor);
            pen = new Pen(PenColor, PenWidht);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);

            myBuffer.Graphics.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
            pen = null;
            brush = null;
        }

        public static Rectangle rect(int x, int y, int w, int h)
        {
            x += (int)translated.X;
            y += (int)translated.Y;
            Rectangle Rect = new Rectangle();
            switch (RectangleMode)
            {
                case rectagleModes.Corner:
                    Rect = new Rectangle(x, y, w, h);
                    break;
                case rectagleModes.Center:
                    Rect = new Rectangle(x - w / 2, y - h / 2, w, h);
                    break;
                case rectagleModes.Radius:
                    Rect = new Rectangle(x - w, y - h, w * 2, h * 2);
                    break;
                default:
                    break;
            }

            using (Graphics grapics = canvas.CreateGraphics())
            {
                if (Smooth) { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
                else { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }

                brush = new SolidBrush(BrushColor);
                pen = new Pen(PenColor, PenWidht);

                myBuffer.Graphics.FillRectangle(brush, Rect);
                myBuffer.Graphics.DrawRectangle(pen, Rect);
                pen = null;
                brush = null;
            }
            return Rect;
        }

        public static Rectangle rect(Point point1, Point point2)
        {
            point1 = translatePoint(point1);
            point2 = translatePoint(point2);

            Rectangle Rect = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);

            if (point1.X > 0 && point1.Y > 0 && point2.X > 0 && point2.Y > 0)
            {
                if (Smooth) { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
                else { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }

                brush = new SolidBrush(BrushColor);
                pen = new Pen(PenColor, PenWidht);


                myBuffer.Graphics.FillRectangle(brush, Rect);
                myBuffer.Graphics.DrawRectangle(pen, Rect);
                pen = null;
                brush = null;
            }

            return Rect;
        }

        public static void quad(PointF point1, PointF point2, PointF point3, PointF point4)
        {
            point1 = translatePoint(point1);
            point2 = translatePoint(point2);
            point3 = translatePoint(point3);
            point3 = translatePoint(point4);

            if (Smooth) { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
            else { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }

            brush = new SolidBrush(BrushColor);
            pen = new Pen(PenColor, PenWidht);

            PointF[] pointarray =
                {
                        point1,
                        point2,
                        point3,
                        point4
                };

            myBuffer.Graphics.FillPolygon(brush, pointarray);
            myBuffer.Graphics.DrawPolygon(pen, pointarray);
            pen = null;
            brush = null;
        }

        public static void quad(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            x1 += translated.X;
            y1 += translated.Y;
            x2 += translated.X;
            y2 += translated.Y;
            x3 += translated.X;
            y3 += translated.Y;
            x4 += translated.X;
            y4 += translated.Y;
            if (Smooth) { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; }
            else { myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default; }

            brush = new SolidBrush(BrushColor);
            pen = new Pen(PenColor, PenWidht);

            PointF[] pointarray =
                {
                        new PointF((float)x1, (float)y1),
                        new PointF((float)x2, (float)y2),
                        new PointF((float)x3, (float)y3),
                        new PointF((float)x4, (float)y4)
                };


            myBuffer.Graphics.FillPolygon(brush, pointarray);
            myBuffer.Graphics.DrawPolygon(pen, pointarray);
            pen = null;
            brush = null;
        }

        public static void Text(string text, Point point)
        {
            if (size > 0)
            {
                myBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                myBuffer.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                myBuffer.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                Font font = new Font(FontFamily.GenericSansSerif, size, System.Drawing.FontStyle.Regular);
                brush = new SolidBrush(BrushColor);

                //SizeF sizef = myBuffer.Graphics.MeasureString(text, font);
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);

                if (center)
                {
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center; myBuffer.Graphics.DrawString(text, font, brush, point.X, point.Y, format);
                }

                else
                    myBuffer.Graphics.DrawString(text, font, brush, point, format);

            }
        }
        public static float textSize(double fontsize)
        {
            size = (float)fontsize;
            return size;
        }

        public static float rotate(float angle)
        {
            myBuffer.Graphics.RotateTransform(angle);
            return angle;
        }

        public static bool CenterText(bool Center)
        {
            center = Center;
            return center;
        }
    }
}
