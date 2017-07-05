using System;
using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.Drawing;
using System.Windows.Forms;
using System.Drawing;

namespace CCreative
{
    public class Function
    {
        public virtual void preload()
        {

        }
        public virtual void setup()
        {
            
        }

        public virtual void draw()
        {

        }

        public virtual void keyPressed(KeyEventArgs e)
        {

        }

        public virtual void mouseDragged(MouseEventArgs e)
        {

        }
    }

    public static class General
    {
        public static int tempX;
        public static int tempY;
        public static Point tempPos;

        public static int mouseX;
        public static int mouseY;
        public static Point mousePos;

        public static int pMouseX;
        public static int pMouseY;
        public static Point PMousePos;

        private static int frames = 0;

        public static bool mouseDown = false;
        public static bool mouseUp = true;
        private static bool fullscreen = false;
        public static bool cursor = true;

        public static int width;
        public static int height;
        public static Size size;

        public static int framecount;
        public static float Framerate;
        public static int setFramerate = 60;
        private static Timer time;
        private static Timer time2;

        public static Form form;
        private static Function mainFunctions;

        public static void init(Form form, Function functions)
        {
            General.form = form;

            width = form.Width;
            height = form.Height;

            CCreative.Drawing.currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(form.CreateGraphics(),
                form.DisplayRectangle);

            time = new Timer();
            time.Interval = 15;
            time.Tick += Time_Tick;
            time.Enabled = true;

            time2 = new Timer();
            time2.Interval = 1000;
            time2.Tick += Time2_Tick;
            time2.Enabled = true;

            General.form.Load += Form_Load;
            General.form.MouseMove += Form_MouseMove;
            General.form.Resize += Form_Resize;
            General.form.KeyDown += Form_KeyDown;
            General.form.MouseUp += Form_MouseUp;
            General.form.MouseDown += Form_MouseDown;
            General.form.MouseEnter += Form_MouseEnter;
            General.form.MouseLeave += Form_MouseLeave;

            General.form.MaximizeBox = false;
            General.form.FormBorderStyle = FormBorderStyle.FixedSingle;

            var myObj = functions;
            mainFunctions = myObj as Function;
            mainFunctions.preload();
        }

        private static void Time2_Tick(object sender, EventArgs e)
        {
            Framerate = frames;
            frames = 0;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            else if (e.KeyCode == Keys.F5)
            {
                Application.Restart();
            }
            mainFunctions.keyPressed(e);
        }

        private static void Form_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private static void Form_MouseEnter(object sender, EventArgs e)
        {
            if (!General.cursor)
            {
                Cursor.Hide();
            }
        }

        private static void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mouseUp = false;
            mouseDown = true;
        }

        private static void Form_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp = true;
            mouseDown = false;
        }

        private static void Form_Resize(object sender, EventArgs e)
        {
            Drawing.currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(form.CreateGraphics(),
                form.DisplayRectangle);
            GC.WaitForPendingFinalizers();
            GC.Collect();

            width = form.Width;
            height = form.Height;
        }

        private static void Form_MouseMove(object sender, MouseEventArgs e)
        {
            tempX = e.X;
            tempY = e.Y;
            tempPos = e.Location;
            if (pMouseX != mouseX && pMouseY != mouseY)
            {
                mainFunctions.mouseDragged(e);
            }
        }

        private static void Form_Load(object sender, EventArgs e)
        {
            mainFunctions.setup();
        }

        private static void Time_Tick(object sender, EventArgs e)
        {
            framecount++;
            frames++;
            mouseX = tempX;
            mouseY = tempY;
            mousePos = tempPos;
            mainFunctions.draw();

            Drawing.myBuffer.Render();

            pMouseX = mouseX;
            pMouseY = mouseY;
            PMousePos = mousePos;

            BrushColor = Color.Transparent;
            PenColor = Color.Transparent;
            PenWidht = 1;

            if (frameCount % 600 == 0)
            {
                GC.Collect();
            }
        }

        public static int frameCount
        {
            get { return framecount; }
        }

        public static void frameRate(float rate)
        {
            rate = constrain(rate, 1, 60);
            
            time.Interval = (int)(1000 / rate);
            setFramerate = (int)rate;
        }

        public static float frameRate()
        {
            return Framerate;
        }

        public static Color background(Color backcolor)
        {
            Drawing.myBuffer.Graphics.Clear(backcolor);
            return backcolor;
        }

        public static Color background(int value)
        {
            Color clr = Color.Black;
            if (colormode == colorModes.HSB)
            {
                clr = ColorTranslator.FromWin32(ColorHLSToRGB((int)map(value, 0, 360, 0, 240), 120, 240));
            }
            else if (colormode == colorModes.RGB)
            {
                value = (int)constrain(value, 0, 255);
                clr = Color.FromArgb(255, value, value, value);
            }
            myBuffer.Graphics.Clear(clr);
            return clr;
        }

        public static void noCursor()
        {
            cursor = false;
        }

        public static void clear()
        {
            canvas.Invalidate();
            canvas.BackColor = Color.Transparent;
            time.Enabled = false;
        }

        public static void noLoop()
        {
            time.Enabled = false;
        }

        public static void loop()
        {
            time.Enabled = true;
        }

        public static T print<T>(T toPrint)
        {
            Console.Write(toPrint);
            return toPrint;
        }

        public static T println<T>(T toPrint)
        {
            Console.WriteLine(toPrint);
            return toPrint;
        }

        public static bool fullScreen()
        {
            fullscreen = !fullscreen;
            if (fullscreen)
            {
                size = form.Size;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Size = Screen.FromControl(form).WorkingArea.Size;
                form.Location = Screen.FromControl(form).WorkingArea.Location;
            }
            else
            {
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.Size = size;
                centerWindow();
            }
            return fullscreen;
        }

        public static void createCanvas(int width, int height)
        {
            form.Size = new Size(width + 16, height + 39);
            width = form.Width;
            height = form.Height;
            size = form.Size;
            centerWindow();
        }

        public static Point centerWindow()
        {
            Screen screen = Screen.FromControl(form);
            form.Location = new Point((screen.WorkingArea.Width - form.Width) / 2,
                                      (screen.WorkingArea.Height - form.Height) / 2);
            return form.Location;
        }

        public static void resizeCanvas(int width, int height)
        {
            form.Size = new Size(width + 16, height + 39);
            width = form.Width;
            height = form.Width;
            size = form.Size;
            centerWindow();
        }
    }
}