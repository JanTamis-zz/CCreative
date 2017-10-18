using System.Windows.Forms.VisualStyles;
using System;
using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;

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

        public virtual void mouseDragged(OpenTK.Input.MouseMoveEventArgs e)
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
        
        static int framecount;
        static double setFramerate = 60;
        static System.Windows.Forms.Timer timer2;
        static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        
        static DateTime TotalTime = new DateTime();
        static double TotalFramerate = 0;
        static Camera2D camera = new Camera2D();

        public static RenderTypes Renderer { get; private set; }
        
        public static Keys? keyCode { get; private set; }
        public static char? key { get; private set; }
        public static bool Shift { get; private set; }
        public static bool Alt { get; private set; }
        public static bool keyRepeat { get; private set; }
        public static bool keypressed { get; private set; }

        public static int mouseX { get; private set; }

        public static int mouseY { get; private set; }

        public static Point mousePos { get; private set; }

        public static int pMouseX { get; private set; }

        public static int pMouseY { get; private set; }

        public static Point pMousePos { get; private set; }

        public static int centerX { get; private set; }

        public static int centerY { get; private set; }

        public static Point centerScreen { get; private set; }

        public static bool mouseDown { get; private set; }

        public static bool mouseUp { get; private set; }

        public static int width { get { return window.Width; } }

        public static int height { get { return window.Height; } }

        public static TimeSpan totalTime { get; private set; }

        public static colorModes RGB { get { return colorModes.RGB; } }

        public static colorModes HSB { get { return colorModes.HSB; } }

        public static RenderTypes GDI { get { return RenderTypes.GDI; } }

        public static RenderTypes P2D { get { return RenderTypes.P2D; } }

        public static RenderTypes P3D { get { return RenderTypes.P3D; } }

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
            //timer2 = new System.Windows.Forms.Timer();
            //timer2.Interval = 1000;
            //timer2.Tick += Time2_Tick;
            //timer2.Enabled = true;
            form1 = form;

            form.Hide();
            mainFunctions = functions;
            mainFunctions.preload();
            mainFunctions.setup();
            
            //timer.Interval = 1000;
            //timer.Tick += Timer_Tick;
            //timer.Enabled = true;
            //timer.Start();
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
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1000;
            timer2.Tick += Time2_Tick;
            timer2.Enabled = true;
            mainFunctions = functions;
            mainFunctions.preload();
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
            mouseX = tempX;
            mouseY = tempY;
            mousePos = tempPos;

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(2, VertexPointerType.Float, Vector2.SizeInBytes, 0);

            if (!cursor)
            {
                window.CursorVisible = false; 
            }

            //window.TargetRenderFrequency = setFramerate;
            TotalFramerate += window.RenderFrequency;

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            mainFunctions.draw();
            
            pMouseX = mouseX;
            pMouseY = mouseY;
            pMousePos = mousePos;

            window.SwapBuffers();
        }

        private static void Window_MouseUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            mouseUp = true;
            mouseDown = false;
        }

        private static void Window_MouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            tempX = e.X;
            tempY = e.Y;
            tempPos = e.Position;

            if (pMouseX != tempX && pMouseY != tempY && mouseDown)
            {
                mainFunctions.mouseDragged(e);
            }
        }

        private static void Window_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == OpenTK.Input.Key.Escape)
            {
                Environment.Exit(0);
            }
            else if (e.Key == OpenTK.Input.Key.F5)
            {
                Application.Restart();
            }
            else if (e.Alt && e.Key == OpenTK.Input.Key.F4)
            {
                Environment.Exit(0);
            }

            Keys key;
            Enum.TryParse(e.Key.ToString(), out key);

            keyCode = key;
            Shift = e.Shift;
            Alt = e.Alt;
            keyRepeat = e.IsRepeat;
            keypressed = true;

            mainFunctions.keyDown();
        }

        private static void Window_MouseWheel(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            mainFunctions.mouseWheel(e);
        }

        private static void Window_MouseDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            mouseUp = false;
            mouseDown = true;
        }

        private static void Time2_Tick(object sender, EventArgs e)
        {
            TotalTime.AddSeconds(1);

            //if (totalTime.Second % 100 == 0)
            //{
            //    GC.WaitForPendingFinalizers();
            //    GC.Collect();
            //}
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

        public static T print<T>(T toPrint)
        {
            Console.Write(toPrint);
            return toPrint;
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

        public static T println<T>(T toPrint)
        {
            Console.WriteLine(toPrint);
            return toPrint;
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

            window.WindowState = WindowState.Maximized;

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
        
        public static void size(int width, int height, bool highQuality)
        {
            if (highQuality)
            {
                window = new GameWindow(width, height, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8), "CCreative");
            }
            else
            {
                window = new GameWindow(width, height, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 0), "CCreative");
            }

            window.Icon = Properties.Resources.icon1;

            window.MouseDown += Window_MouseDown;
            window.MouseWheel += Window_MouseWheel;
            window.KeyDown += Window_KeyDown;
            window.KeyUp += Window_KeyUp;
            window.MouseMove += Window_MouseMove;
            window.MouseUp += Window_MouseUp;
            window.KeyPress += Window_KeyPress;
            window.RenderFrame += Window_RenderFrame;
            window.Closed += Window_Closed;
            window.Resize += Window_Resize;
            window.Load += Window_Load;
            
            window.Run();
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
            size(width, height, true);
        }

        private static void Window_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //zet het gebied waar de tekeningen in getekend kunnen worden
            GL.Viewport(0, 0, window.Width, window.Height);
            //zet de coordiaaten systeem (niet mogelijk in GL 4).
            SetOrthographicProjection();

            img = new FastBitmapLib.FastBitmap(new Bitmap(width, height));

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
            SetOrthographicProjection();
            mainFunctions.resize();

            img.Resize(width, height);
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            framecount = 0;
            noStroke();
            noFill();
            strokeWeight(1);

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
            pixels = new Color[(width * height)];
        }

        static  Matrix4 projectionMatrix;
        static Matrix4 modelViewMatrix;
        //static Vector3 cameraPosition;
        //static Vector3 cameraTarget;
        static Vector3 cameraUp = Vector3.UnitY; // which way is up for the camera

        private static void SetPerspectiveProjection(int width, int height, float FOV)
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (FOV / 180f), width / (float)height, 0.2f, 256.0f);
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