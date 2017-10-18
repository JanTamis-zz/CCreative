using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCreative.FastBitmapLib;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CCreative
{
    public class PImage
    {
        Bitmap fast;
        
        public int width { get { return fast.Width; } }
        public int height { get { return fast.Height; } }
        public bool isLoaded { get; private set; }

        public int? textureID { get; private set; }

        public PImage(string pathName)
        {
            fast = new Bitmap(pathName);
            textureID = ContentPipe.loadTexture(fast);
            isLoaded = true;
        }

        public PImage(Bitmap image)
        {
            fast = image;
            textureID = ContentPipe.loadTexture(fast);
            isLoaded = true;
        }

        public PImage copy()
        {
            return this;
        }
    }
}