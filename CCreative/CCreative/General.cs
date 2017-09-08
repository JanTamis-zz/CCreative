using System;
using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.Drawing;

using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

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

        public virtual void keyReleased(KeyEventArgs e)
        {

        }

        public virtual void mouseDragged(MouseEventArgs e)
        {

        }

        public virtual void mouseWheel(MouseEventArgs e)
        {

        }

        public virtual void mousePressed(MouseEventArgs e)
        {

        }
    }

    public static class General
    {
        static int tempX;
        static int tempY;
        static Point tempPos;

        static int mousex;
        static int mousey;
        static Point mousepos;

        static int pmousex;
        static int pmousey;
        static Point pmousepos;

        static int centerx;
        static int centery;
        static Point centerscreen;

        static int frames = 0;

        static bool mousedown = false;
        static bool mouseup = true;
        static bool fullscreen = false;
        static bool cursor = true;

        static int Width;
        static int Height;

        static int framecount;
        static float Framerate;
        static int setFramerate = 60;
        static System.Windows.Forms.Timer time;
        static System.Windows.Forms.Timer time2;
        static Stopwatch Watch = new Stopwatch();

        static int totalSeconds = 0;
        static Stopwatch watch = new Stopwatch();

        static RenderTypes Renderer;

        public static RenderTypes renderer()
        {
            return Renderer;
        }

        public static int mouseX
        {
            get { return mousex; }
        }

        public static int mouseY
        {
            get { return mousey; }
        }

        public static Point mousePos
        {
            get { return mousepos; }
        }

        public static int pMouseX
        {
            get { return pmousex; }
        }

        public static int pMouseY
        {
            get { return pmousey; }
        }

        public static Point pMousePos
        {
            get { return pmousepos; }
        }

        public static int centerX
        {
            get { return centerx; }
        }

        public static int centerY
        {
            get { return centery; }
        }

        public static Point centerScreen
        {
            get { return centerscreen; }
        }

        public static bool mouseDown
        {
            get { return mousedown; }
        }

        public static bool mouseUp
        {
            get { return mouseup; }
        }

        public static int width
        { 
            get { return Width; }
        }

        public static int height
        {
            get { return Height; }
        }

        public static TimeSpan totalTime
        {
            get { return watch.Elapsed; }
        }

        public static colorModes RGB
        {
            get { return colorModes.RGB; }
        }

        public static colorModes HSB
        {
            get { return colorModes.HSB; }
        }

        public enum RenderTypes
        {
            GDI,
            P2D,
            P3D
        }

        public static RenderTypes GDI
        {
            get { return RenderTypes.GDI; }
        }

        public static RenderTypes P2D
        {
            get { return RenderTypes.P2D; }
        }

        public static RenderTypes P3D
        {
            get { return RenderTypes.P3D; }
        }

        public static Form form;
        static Function mainFunctions;
        private static bool haveLoop = true;
        
        public static void init(Form form, Function functions)
        {
            General.form = form;

            Width = form.Width;
            Height = form.Height;

            CCreative.Drawing.currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(form.CreateGraphics(),
                form.DisplayRectangle);

            seed = random(double.MaxValue);

            time = new System.Windows.Forms.Timer();
            time.Interval = 15;
            time.Tick += Time_Tick;
            time.Enabled = true;

            time2 = new System.Windows.Forms.Timer();
            time2.Interval = 1000;
            time2.Tick += Time2_Tick;
            time2.Enabled = true;

            General.form.Load += Form_Load;
            General.form.MouseMove += Form_MouseMove;
            General.form.Resize += Form_Resize;
            General.form.KeyDown += Form_KeyDown;
            General.form.KeyUp += Form_KeyUp;
            General.form.MouseUp += Form_MouseUp;
            General.form.MouseDown += Form_MouseDown;
            General.form.MouseEnter += Form_MouseEnter;
            General.form.MouseLeave += Form_MouseLeave;
            General.form.MouseWheel += Form_MouseWheel;
            General.form.MouseClick += Form_MouseClick;

            General.form.MaximizeBox = false;
            General.form.FormBorderStyle = FormBorderStyle.FixedSingle;

            General.form.Icon = Properties.Resources.icon1;

            var myObj = functions;
            mainFunctions = myObj as Function;
            
            General.form.Text = functions.GetType().Namespace;
            
            watch.Start();
        }

        private static void Form_MouseClick(object sender, MouseEventArgs e)
        {
            mainFunctions.mousePressed(e);
        }

        private static void UpdateTextPosition()
        {
            Graphics g = General.form.CreateGraphics();
            Double startingPoint = (General.form.Width * 0.97) - (g.MeasureString(General.form.Text.Trim(), General.form.Font).Width * 0.97);
            Double widthOfASpace = g.MeasureString(" ", General.form.Font).Width;
            String tmp = " ";
            Double tmpWidth = 0;

            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmp += " ";
                tmpWidth += widthOfASpace;
            }

            General.form.Text = tmp + General.form.Text.Trim();
        }

        private static void Form_KeyUp(object sender, KeyEventArgs e)
        {
            mainFunctions.keyReleased(e);
        }

        private static void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            mainFunctions.mouseWheel(e);
        }

        private static void Time2_Tick(object sender, EventArgs e)
        {
            Framerate = frames;
            frames = 0;
            totalSeconds++;

            if (totalSeconds % 10 == 0)
            {
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
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

            else if (e.KeyCode == Keys.Pause)
            {
                haveLoop = !haveLoop;

                if (haveLoop)
                {
                    loop();
                }
                else
                {
                    noLoop();
                }
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
            mouseup = false;
            mousedown = true;
        }

        private static void Form_MouseUp(object sender, MouseEventArgs e)
        {
            mouseup = true;
            mousedown = false;
        }

        private static void Form_Resize(object sender, EventArgs e)
        {
            Drawing.currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(form.CreateGraphics(),
                form.DisplayRectangle);
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Width = form.Width;
            Height = form.Height;
        }

        private static void Form_MouseMove(object sender, MouseEventArgs e)
        {
            tempX = e.X;
            tempY = e.Y;
            tempPos = e.Location;

            if (pmousex != tempX && pmousey != tempY)
            {
                mainFunctions.mouseDragged(e);
            }
        }

        private static void Form_Load(object sender, EventArgs e)
        {
            if (smoothing)
            {
                smooth();
            }
            else
            {
                noSmooth();
            }
            mainFunctions.setup();
        }

        private static void Time_Tick(object sender, EventArgs e)
        {
            watch.Restart();
            watch.Start();

            framecount++;
            frames++;

            mousex = tempX;
            mousey = tempY;
            mousepos = tempPos;

            if (smoothing)
            {
                smooth();
            }
            else
            {
                noSmooth();
            }

            mainFunctions.draw();

            Drawing.myBuffer.Render();

            pmousex = mousex;
            pmousey = mousey;
            pmousepos = mousepos;

            if (Watch.ElapsedMilliseconds != 0)
            {
                Framerate = 1000 / Watch.ElapsedMilliseconds; 
            }
            Watch.Stop();
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
        
        public static void noCursor()
        {
            cursor = false;
        }

        public static void clear()
        {
            time.Enabled = false;
        }

        public static int delay(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return milliseconds;
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

        public static T[] printArray<T>(T[] toPrint)
        {
            for (int i = 0; i < toPrint.Length; i++)
            {
                println(String.Format("[{0}] {1}", i, toPrint[i]));
            }
            return toPrint;
        }

        public static bool fullScreen()
        {
            fullscreen = !fullscreen;
            if (fullscreen)
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.Size = Screen.FromControl(form).WorkingArea.Size;
                form.Location = Screen.FromControl(form).WorkingArea.Location;

                centerx = width / 2;
                centery = height / 2;
                centerscreen = new Point(centerx, centerY);
            }
            else
            {
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                centerWindow();
            }
            return fullscreen;
        }

        public static void size(int width, int height, RenderTypes renderer)
        {
            form.ClientSize = new Size(width, height);
            General.Width = form.ClientSize.Width;
            General.Height = form.ClientSize.Height;

            centerx = width / 2;
            centery = height / 2;
            centerscreen = new Point(centerx, centerY);

            centerWindow();
        }

        public static void size(int width, int height)
        {
            size(width, height, GDI);
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
            form.ClientSize = new Size(width + 16, height + 39);
            General.Width = form.ClientSize.Width;
            General.Height = form.ClientSize.Height;

            centerx = width / 2;
            centery = height / 2;
            centerscreen = new Point(centerx, centerY);

            centerWindow();
        }
    }
}