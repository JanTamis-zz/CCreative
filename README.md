# CCreative
A C# library based on Processing and P5.js.

!!!THIS IS A BETA OF THIS LIBRARY!!!

[more information](https://p5js.org/reference/)

How to use 
* step 1: include the files, the CCreative.General is necessary
```cs
using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.Drawing;
using static CCreative.General;
using static CCreative.Data;
using CCreative;
```
* step 2: create a class where the code will be entered, inherit the class with the Function class:
Example:
```cs
public class yourClassName : Function 
{
    
}
```
* Step 3 override the functions you want to use:
example:
```cs
public override void setup()
{
    size(500, 500);
}
```
possible methods to override:

preload();

setup();

draw();

keyPressed();

mouseDragged();

mouseWheel();


* Step 4: add the init method to the constructor of your form:

the method wants to know the form and a new instance of the class we have created.

example:
```cs
public Form1()
{
    InitializeComponent();
    init(this, new yourClassName());
}
```      
example Code:
```cs
using System.Windows.Forms;

using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.Drawing;
using static CCreative.General;
using static CCreative.Data;
using CCreative;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //initalize the library.
            init(this, new yourClassName());
        }
    }

    public class yourClassName : Function
    {
        //ovreride the setup method.
        public override void setup()
        {
            //create a canvas with a width of 500 and a height of 500;
            createCanvas(500, 500);
        }

        //override the draw method.
        public override void draw()
        {
            //set the colormode to RGB;
            colorMode(colorModes.RGB);
						
            //redraw the background with a gray background;
            background(51);
            
            //set the colormode to HSB;
            colorMode(colorModes.HSB);

            //fill the next shapes with a gray fill
            fill(Color.Gray);

            //draw a yellow border around the shape, with a width of 5
            stroke(Color.Yellow);
            strokeWeight(5);

            //draw a ellipse at the mouse position with a diameter of 100px
            ellipse(mouseX, mouseY, 100, 100);
        }
    }
}        
```
© All rights reserved
