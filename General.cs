using System;
using static CCreative.Colors;
using static CCreative.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace CCreative
{
    public class Function
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Called directly before setup(), the preload() function is used to handle asynchronous loading of external files. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public virtual void preload()
        {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The setup() function is called once when the program starts. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public virtual void setup()
        {
            
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Called before the draw function. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public virtual void predraw()
        {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Called directly after setup(), the draw() function continuously executes the lines of code contained inside its block until the program is stopped or noLoop() is called. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public virtual void draw()
        {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The keyPressed() function is called once every time a key is pressed. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public virtual void keyPressed()
        {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The mouseDragged() function is called once every time the mouse moves. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="e">    Mouse move event information. </param>

        public virtual void mouseDragged()
        {

        }

        public virtual void keyReleased()
        {

        }

        public virtual void keyDown()
        {

        }

        public virtual void Closed()
        {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The function mouseWheel() is executed every time a vertical mouse wheel event is detected either triggered by an actual mouse wheel or by a touchpad. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="e">    Mouse wheel event information. </param>

        public virtual void mouseWheel(OpenTK.Input.MouseWheelEventArgs e)
        {

        }

        public virtual void timer()
        {

        }

        public virtual void resize()
        {

        }
    }

    public static class General
    {
        public static GameWindow window;
        static int tempX;
        static int tempY;
        static Point tempPos;
        static int frames = 0;
        static bool fullscreen = false;
        static bool cursor = true;
        static bool loop = true;

        static Form form1;

        public enum filterType
        {
            Gray,
            Invert,
            Blur,
            Threshold,
            Pixelate,
            Sepia,
            Jitter,
            Brightness,
            Opaque,
            Posterize,
            Hue,
            Satuation,
            Red,
            Green,
            Blue
        }

        static int framecount;
        static double setFramerate = double.MaxValue;
        static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        static double TotalFramerate = 0;
        static Camera2D camera = new Camera2D();

        public static RenderTypes Renderer { get; private set; }

        public static Keys? keyCode { get; private set; }
        public static char? key { get; private set; }
        public static bool Shift { get; private set; }
        public static bool Alt { get; private set; }
        public static bool keyRepeat { get { return window.Keyboard.KeyRepeat; } }
        public static bool keypressed { get; private set; }

        public static int mouseX { get { return window.Mouse.X; } }
        public static int mouseY { get { return window.Mouse.Y; } }
        public static Point mousePos { get { return new Point(mouseX, mouseY); } }

        public static int pMouseX { get; private set; }
        public static int pMouseY { get; private set; }
        public static Point pMousePos { get; private set; }

        public static int centerX { get { return width / 2; } }
        public static int centerY { get { return height / 2; } }
        public static Point centerScreen { get { return new Point(centerX, centerY); } }

        public static bool mouseDown { get { return window.Mouse.GetState().IsAnyButtonDown; } }
        public static bool mouseUp { get { return !window.Mouse.GetState().IsAnyButtonDown; } }

        public static int width { get { return window.Width; } }
        public static int height { get { return window.Height; } }

        public static float totalTime { get { return (framecount * averageFramerate()) / (float)(TotalFramerate / framecount) / 60 + 1; } }

        public static colorModes RGB { get { return colorModes.RGB; } }
        public static colorModes HSB { get { return colorModes.HSB; } }

        public static RenderTypes GDI { get { return RenderTypes.GDI; } }
        public static RenderTypes P2D { get { return RenderTypes.P2D; } }
        public static RenderTypes P3D { get { return RenderTypes.P3D; } }

        public static drawModes CENTER { get { return drawModes.Center; } }
        public static drawModes RADIUS { get { return drawModes.Radius; } }
        public static drawModes CORNER { get { return drawModes.Corner; } }

        public static filterType GRAY { get { return filterType.Gray; } }
        public static filterType BLUR { get { return filterType.Blur; } }
        public static filterType BRIGHTNESS { get { return filterType.Brightness; } }
        public static filterType INVERT { get { return filterType.Invert; } }
        public static filterType JITTER { get { return filterType.Jitter; } }
        public static filterType PIXELATE { get { return filterType.Pixelate; } }
        public static filterType SEPIA { get { return filterType.Sepia; } }
        public static filterType THRESHOLD { get { return filterType.Threshold; } }
        public static filterType OPAQUE { get { return filterType.Opaque; } }
        public static filterType POSTERIZE { get { return filterType.Posterize; } }
        public static filterType SATURATION { get { return filterType.Satuation; } }
        public static filterType RED { get { return filterType.Red; } }
        public static filterType GREEN { get { return filterType.Green; } }
        public static filterType BLUE { get { return filterType.Blue; } }
        public static filterType HUE { get { return filterType.Hue; } }

        public static bool focused { get { return window.Focused; } }

        public static int displayWidth { get { return Screen.PrimaryScreen.Bounds.Width; } }
        public static int displayHeight { get { return Screen.PrimaryScreen.Bounds.Height; } }

        private static PerformanceCounter theCPUCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
        public static float cpuUsage { get { return theCPUCounter.NextValue(); } }
        public static float ramUsage { get { return (Process.GetCurrentProcess().PrivateMemorySize64 / 1024f) / 1024f; } }
        
        public enum RenderTypes
        {
            GDI,
            P2D,
            P3D
        }

        static Function mainFunctions;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes the CCreative library. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="form">         The form to use. </param>
        /// <param name="functions">    The functions class that holds the functions. </param>

        public static void init(Form form, Function functions)
        {
            form1 = form;

            form.Hide();
            init(functions);
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            mainFunctions.timer();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the timer interval. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="interval">    The timer interval in milliseconds. </param>
        /// 

        public static void timerInterval(int interval)
        {
            timer.Interval = interval;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the timer interval. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///

        public static int timerInterval()
        {
            return timer.Interval;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes the CCreative library. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="functions">    The functions class that holds the functions. </param>

        public static void init(Function functions)
        {
            mainFunctions = functions;

            thread(mainFunctions.preload);

            mainFunctions.setup();

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
            timer.Start();
        }

        private static void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            framecount++;
            frames++;

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(2, VertexPointerType.Float, Vector2.SizeInBytes, 0);

            if (!cursor)
            {
                window.CursorVisible = false; 
            }
            window.TargetRenderFrequency = setFramerate;
            TotalFramerate += window.RenderFrequency;

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            GL.PushMatrix();

            mainFunctions.draw();

            GL.PopMatrix();
            
            pMouseX = mouseX;
            pMouseY = mouseY;
            pMousePos = mousePos;

            //if (vfr.IsOpen)
            //{
            //    vfr.WriteVideoFrame(getFrame());
            //}
            window.SwapBuffers();
        }

        private static void Window_MouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            tempX = e.X;
            tempY = e.Y;
            tempPos = e.Position;

            if (pMouseX != tempX && pMouseY != tempY && mouseDown)
            {
                mainFunctions.mouseDragged();
            }
        }

        private static void Window_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == OpenTK.Input.Key.Escape)
            {
                window.Close();
            }

            Keys key;
            Enum.TryParse(e.Key.ToString(), out key);

            keyCode = key;
            Shift = e.Shift;
            Alt = e.Alt;
            keypressed = true;

            mainFunctions.keyDown();
        }

        private static void Window_MouseWheel(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            mainFunctions.mouseWheel(e);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The system variable frameCount contains the number of frames that have been displayed since the program started. </summary>
        ///
        /// <value> The frame count. </value>

        public static int frameCount
        {
            get { return framecount; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Specifies the number of frames to be displayed every second. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="rate"> The set Framerate. </param>

        public static void frameRate(double rate)
        {
            setFramerate = rate;
            window.TargetUpdateFrequency = rate;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the current framerate. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The framerate. </returns>

        public static float frameRate()
        {
            return (float)window.RenderFrequency;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Hides the cursor from view. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public static void noCursor()
        {
            window.Cursor = MouseCursor.Empty;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Shows the cursor from view. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>

        public static void Cursor()
        {
            window.Cursor = MouseCursor.Default;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Delays the program to do furder actions during the given time. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="milliseconds"> The milliseconds. </param>
        ///
        /// <returns>   An int. </returns>

        //public static int delay(int milliseconds)
        //{
        //    Thread.Sleep(milliseconds);
        //    return milliseconds;
        //}

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The print() function writes to the console area. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="toPrint">  Value to print. </param>
        ///
        /// <returns>   A T. </returns>

        public static void print<T>(T toPrint)
        {
            Console.Write(toPrint);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The print() function writes to the console area, with a newline. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="toPrint">  Value to print. </param>
        ///
        /// <returns>   A T. </returns>

        public static void println<T>(T toPrint)
        {
            Console.WriteLine(toPrint);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   If argument is given, sets the sketch to fullscreen or not based on the value of the argument. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   true if the window is fullscreen, else the window isn't fullscreen. </returns>

        public static bool fullScreen()
        {
            // fullscreen = !fullscreen;
            // if (fullscreen)
            // {
            //     Size = window.Size;
            //     window.WindowBorder = WindowBorder.Hidden;
            //     window.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //     window.Location = new Point(0, 0);
            // }
            // else
            // {
            //     window.WindowBorder = WindowBorder.Fixed;
            //     window.Size = Size; 
            //}

            window.WindowState = WindowState.Fullscreen;

            //zet het gebied waar de tekeningen in getekend kunnen worden
            GL.Viewport(0, 0, window.Width, window.Height);
            //zet de coordiaaten systeem (niet mogelijk in GL 4).
            SetOrthographicProjection();
            
            return fullscreen;
        }
        
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Defines the dimension of the display window width and height in units of pixels. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="width">    The width of the display window in units of pixels. </param>
        /// <param name="height">   The height of the display window in units of pixels. </param>
        /// <param name="highQuality">   Determents if the drawings are in high quality. </param>
        
        public static void size(int width, int height, int amount)
        {
            window = new GameWindow(width, height, new OpenTK.Graphics.GraphicsMode(32, 24, 0, amount), "CCreative");

            window.Icon = Properties.Resources.icon1;
            
            window.MouseWheel += Window_MouseWheel;
            window.KeyDown += Window_KeyDown;
            window.KeyUp += Window_KeyUp;
            window.MouseMove += Window_MouseMove;
            window.KeyPress += Window_KeyPress;
            window.RenderFrame += Window_RenderFrame;
            window.Closed += Window_Closed;
            window.Resize += Window_Resize;
            window.Load += Window_Load;
            
            window.Run();
            Renderer = P2D;
        }

        private static void Window_KeyPress(object sender, OpenTK.KeyPressEventArgs e)
        {
            key = e.KeyChar;
            mainFunctions.keyPressed();
        }

        private static void Window_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            keypressed = false;
            mainFunctions.keyReleased();
            keyCode = null;
            key = null;
        }

        public static float averageFramerate()
        {
            return (float)TotalFramerate / frameCount;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Defines the dimension of the display window width and height in units of pixels. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="width">    The width of the display window in units of pixels. </param>
        /// <param name="height">   The height of the display window in units of pixels. </param>
         
        public static void size(int width, int height)
        {
            size(width, height, 2);
            Renderer = P2D;
        }

        public static void size(int width, int height, RenderTypes renderer)
        {
            Renderer = renderer;
            size(width, height, 2);
        }

        public static void size(int width, int height, RenderTypes renderer, int amount)
        {
            Renderer = renderer;
            size(width, height, amount);
        }

        private static void Window_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            if (Renderer == RenderTypes.P3D)
            {
                GL.Enable(EnableCap.DepthTest);
            }

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //zet het gebied waar de tekeningen in getekend kunnen worden
            GL.Viewport(0, 0, window.Width, window.Height);
            //zet de coordiaaten systeem (niet mogelijk in GL 4).
            switch (Renderer)
            {
                case RenderTypes.P2D:
                    SetOrthographicProjection();
                    break;
                case RenderTypes.P3D:
                    SetPerspectiveProjection(width, height, 45);
                    break;
            }
            pixels = new Color[width * height];

            Drawing.first = true;

            GL.BindTexture(TextureTarget.Texture2D, textureBuffer);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        private static void Window_Resize(object sender, EventArgs e)
        {
            //zet het gebied waar de tekeningen in getekend kunnen worden
            GL.Viewport(0, 0, window.Width, window.Height);
            //zet de coordiaaten systeem (niet mogelijk in GL 4).
            switch (Renderer)
            {
                case RenderTypes.P2D:
                    SetOrthographicProjection();
                    break;
                case RenderTypes.P3D:
                    SetPerspectiveProjection(width, height, 45);
                    break;
            }
            mainFunctions.resize();

            img = new Bitmap(width, height);
            Drawing.first = true;

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            framecount = 0;
            
            currentStyle = new Style(1, Color.Black, Color.White, Color.White, drawModes.Corner, drawModes.Center, angleModes.Radians);
            colorMode(RGB);

            mainFunctions.Closed();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Resizes the current window by the given dimentions </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="width">    The width of the display window in units of pixels. </param>
        /// <param name="height">   The height of the display window in units of pixels. </param>

        public static void resize(int width, int height)
        {
            window.Size = new Size(width, height);
            GL.Viewport(0, 0, window.Width, window.Height);
        }

        static  Matrix4 projectionMatrix;
        static Matrix4 modelViewMatrix;
        //static Vector3 cameraPosition;
        //static Vector3 cameraTarget;
        static Vector3 cameraUp = Vector3.UnitY; // which way is up for the camera

        private static void SetPerspectiveProjection(int width, int height, float FOV)
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(Math.PI * (FOV / 180f), width / (float)height, 0.2f, 256.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix); // this replaces the old matrix, no need for GL.LoadIdentity()
        }

        private static void SetOrthographicProjection()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity(); // reset matrix
            GL.Viewport(window.ClientRectangle);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Ortho(0, window.Width, window.Height, 0, -1, 1);
        }

        private static void SetLookAtCamera(Vector3 position, Vector3 target, Vector3 up)
        {
            modelViewMatrix = Matrix4.LookAt(position, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);
        }

        public static void rotate(double degrees, double x, double y, double z)
        {
            GL.Rotate(degrees, x, y, z);
        }

        public static void scale(double scalefactor)
        {
            GL.Scale(scalefactor, scalefactor, 0);
        }

        public static void pushMatrix()
        {
            GL.PushMatrix();
        }

        public static void popMatrix()
        {
            GL.PopMatrix();
        }

        public static void resetMatrix()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity(); // reset matrix
            GL.Viewport(window.ClientRectangle);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Ortho(0, window.Width, window.Height, 0, -1, 1);
        }

        public static void Vsync(bool enabled)
        {
            if (enabled)
            {
                window.VSync = VSyncMode.On;
            }
            else
            {
                window.VSync = VSyncMode.Off;
            }
        }

        public static void Title(string title)
        {
            window.Title = title;
        }

        public static void noLoop()
        {
            loop = false;
            window.RenderFrame -= Window_RenderFrame;
        }

        public static void Loop()
        {
            loop = true;
            window.RenderFrame += Window_RenderFrame;
        }

        public static void canResize(bool resizeable)
        {
            if (resizeable)
            {
                window.WindowBorder = WindowBorder.Resizable;
            }
            else
            {
                window.WindowBorder = WindowBorder.Fixed;
            }
        }

        public static void thread(Action method)
        {
            new Thread(new ThreadStart(method)).Start();
        }
        
        public static void saveFrame(string path)
        {
            int w = window.Width;
            int h = window.Height;
            
            using (Bitmap bmp = new Bitmap(w, h))
            {
                BitmapData data = bmp.LockBits(window.ClientRectangle, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.ReadPixels(0, 0, w, h, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);

                bmp.UnlockBits(data);

                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                bmp.Save(path);
            }
            GC.Collect();
        }

        public static Bitmap getFrame()
        {
            int w = window.Width;
            int h = window.Height;

            using (Bitmap bmp = new Bitmap(w, h))
            {
                BitmapData data = bmp.LockBits(window.ClientRectangle, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.ReadPixels(0, 0, w, h, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);

                bmp.UnlockBits(data);

                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                return bmp;
            }
        }

        //public static void startRecording(string filename, int frameRate)
        //{
        //    //if (!vfr.IsOpen)
        //    //{
        //    //    vfr.Open(filename, width, height, frameRate, VideoCodec.Default);
        //    //}
        //}

        //public static void stopRecording()
        //{
        //    //if (vfr.IsOpen)
        //    //{
        //    //    vfr.Close();
        //    //}
        //}
    }

    class Camera2D
    {
        public enum DrawMode
        {
            Normal,
            Wireframe
        }

        public float X { get; set; }
        public float Y { get; set; }

        public float Angle { get; set; }

        private DrawMode _mode;
        public DrawMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                UpdateDrawMode();
            }
        }

        public Camera2D(float x = 0.0f, float y = 0.0f)
        {
            X = x;
            Y = y;
            _mode = DrawMode.Normal;
        }

        private void UpdateDrawMode()
        {
            switch (_mode)
            {
                case DrawMode.Normal:
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    break;
                case DrawMode.Wireframe:
                    GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
                    break;
            }
        }

        public void Update()
        {
            GL.Rotate(Angle, 0, 0, 1);
            GL.Translate(-X, -Y, 0);
        }
    }
}