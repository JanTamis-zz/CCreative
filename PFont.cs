using QuickFont;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace CCreative
{
    public class PFont
    {
        public FontFamily family { get; private set; }
        public QFont qfont { get; private set; }

        public PFont(FontFamily font)
        {
            this.family = font;
            qfont = new QFont(new Font(font, 20, FontStyle.Regular));

            qfont.Options.LockToPixel = false;
            qfont.Options.UseDefaultBlendFunction = false;

        }

        public static FontFamily[] list()
        {
            return FontFamily.Families;
        }

        public PointF[] textToPoints(string text, double x, double y, double fontSize, double sampleFactor = 0.25)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddString(text, family, 0, (float)fontSize, new PointF((float)x, (float)y), StringFormat.GenericDefault);
            gp.Flatten(new Matrix(), 1 - (float)sampleFactor);

            return gp.PathPoints;
        }

        public SizeF textBounds(string text)
        {
            return qfont.Measure(text);
        }
    }
}
